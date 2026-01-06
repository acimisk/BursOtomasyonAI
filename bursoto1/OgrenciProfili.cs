using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using bursoto1.Helpers; // MessageHelper ve ImageHelper için
using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;

namespace bursoto1
{
    public partial class OgrenciProfili : XtraForm
    {
        public string ad, soyad, maas,fotoYolu,agno,haneGeliri,bolum,kardesSayisi,sinif,telNo;
        public int secilenOgrenciID;
        SqlBaglanti bgl = new SqlBaglanti();
        
        private async void btnAIAnaliz_Click(object sender, EventArgs e)
        {
            try
            {
                // HATALI KISIM BURASIYDI: Yeni form oluşturup grid'e bakıyordun.
                // ÇÖZÜM: Form açılırken gelen 'secilenOgrenciID' değişkenini kullanıyoruz.

                if (secilenOgrenciID <= 0)
                {
                    MessageHelper.ShowError("Öğrenci ID bilgisi alınamadı!", "Veri Hatası");
                    return;
                }

                GeminiAI ai = new GeminiAI();
                string ad = txtOgrAd?.Text ?? string.Empty;
                string agno = txtAgno?.Text ?? string.Empty;
                string gelir = txtHaneGeliri?.Text ?? string.Empty;
                string kardes = txtOgrKardesSayisi?.Text ?? string.Empty;
                string veri = $"AD: {ad}, AGNO: {agno}, GELIR: {gelir}, KARDES: {kardes}";

                // Not: OgrenciProfili formunda rtbAnalizSonuc kontrolü yok, bu yüzden sadece veritabanına kaydediyoruz
                string sonuc = await ai.BursAnaliziYap(veri).ConfigureAwait(false);

                var match = Regex.Match(sonuc ?? string.Empty, @"SKOR:\s*(\d+)");
                if (match.Success)
                {
                    int puan = int.Parse(match.Groups[1].Value);
                    // Not: txtAISkor kontrolü bu formda yok, sadece veritabanına kaydediyoruz

                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor=@p1 WHERE ID=@p2", conn);
                        cmd.Parameters.AddWithValue("@p1", puan);
                        cmd.Parameters.AddWithValue("@p2", secilenOgrenciID);
                        int etkilenenSatir = cmd.ExecuteNonQuery();

                        if (etkilenenSatir > 0)
                        {
                            MessageHelper.ShowSuccess($"AI Skoru ({puan}) başarıyla veritabanına kaydedildi.", "Kayıt Başarılı");
                            DataChangedNotifier.NotifyOgrenciChanged();
                        }
                        else
                        {
                            MessageHelper.ShowWarning("Kayıt güncellenemedi (ID bulunamadı).", "Güncelleme Hatası");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "AI Analiz Hatası");
            }
        }


        private void labelSinif_Click(object sender, EventArgs e)
        {

        }

        private void labelHaneGeliri_Click(object sender, EventArgs e)
        {

        }
        // Base64ToImage metodu ImageHelper'a taşındı (DRY prensibi)
        private void OgrenciProfili_Load(object sender, EventArgs e)
        {
            // Eğer secilenOgrenciID varsa veritabanından çek
            if (secilenOgrenciID > 0)
            {
                LoadOgrenciFromDB();
                LoadBursBilgileri(); // Burs bilgilerini yükle
            }
            else
            {
                // Manuel atanan değerleri kullan
                if (txtOgrAd != null) txtOgrAd.Text = ad ?? string.Empty;
                if (txtOgrSoyad != null) txtOgrSoyad.Text = soyad ?? string.Empty;
                if (txtHaneGeliri != null) txtHaneGeliri.Text = haneGeliri ?? string.Empty;
                if (txtAgno != null) txtAgno.Text = agno ?? string.Empty;
                if (txtBolum != null) txtBolum.Text = bolum ?? string.Empty;
                if (txtOgrKardesSayisi != null) txtOgrKardesSayisi.Text = kardesSayisi ?? string.Empty;
                if (txtSinif != null) txtSinif.Text = sinif ?? string.Empty;
                if (txtTelNo != null) txtTelNo.Text = telNo ?? string.Empty;
            }

            this.Text = (ad ?? "") + " " + (soyad ?? "") + " - Öğrenci Profili";

            if (!string.IsNullOrEmpty(fotoYolu))
            {
                try
                {
                    if (fotoYolu.StartsWith("data:") || fotoYolu.Length > 260) // Base64 tespiti
                    {
                        Image resim = ImageHelper.Base64ToImage(fotoYolu);
                        if (resim != null)
                        {
                            pictureEdit1.EditValue = resim;
                        }
                    }
                    else if (File.Exists(fotoYolu)) // Normal dosya yoluysa
                    {
                        pictureEdit1.Image = Image.FromFile(fotoYolu);
                    }

                    // Resmin kutuya tam sığması için
                    pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Resim yükleme hatası: " + ex.Message);
                }
            }

            // AI Skorunu yükle
            LoadAISkor();
        }

        void LoadBursBilgileri()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Burslar tablosundaki ID kolonunu dinamik tespit et
                    string bursIDKolonu = "BursID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Burslar' 
                            AND COLUMN_NAME IN ('BursID', 'ID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'BursID' THEN 1 
                                WHEN 'ID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                            bursIDKolonu = kolonResult.ToString();
                    }
                    catch { }

                    // Sadece onaylanmış (Durum = 1) bursları göster
                    SqlCommand cmd = new SqlCommand($@"
                        SELECT ob.Durum, ob.BaslangicTarihi, b.BursAdı, b.Miktar 
                        FROM OgrenciBurslari ob 
                        LEFT JOIN Burslar b ON (ob.BursID = b.{bursIDKolonu})
                        WHERE ob.OgrenciID = @id AND ob.Durum = 1", conn);
                    cmd.Parameters.AddWithValue("@id", secilenOgrenciID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Sadece onaylanmış burslar buraya gelir
                            string durumText = "✅ Aktif Burslu";
                            Color durumRenk = Color.FromArgb(39, 174, 96);
                            
                            lblBursDurum.Text = "Burs Durumu: " + durumText;
                            lblBursDurum.ForeColor = durumRenk;

                            string bursAdi = reader["BursAdı"]?.ToString() ?? "Belirtilmemiş";
                            decimal miktar = reader["Miktar"] != DBNull.Value ? Convert.ToDecimal(reader["Miktar"]) : 0;
                            lblBursMiktar.Text = $"Burs: {bursAdi} - {miktar:C}";

                            if (reader["BaslangicTarihi"] != DBNull.Value)
                            {
                                DateTime tarih = Convert.ToDateTime(reader["BaslangicTarihi"]);
                                lblBaslangicTarihi.Text = "Başlangıç: " + tarih.ToString("dd.MM.yyyy");
                            }
                            else
                            {
                                lblBaslangicTarihi.Text = "Başlangıç: Henüz belirlenmedi";
                            }
                        }
                        else
                        {
                            // Beklemede veya burs kaydı yok
                            lblBursDurum.Text = "Burs Durumu: ⏳ Beklemede";
                            lblBursDurum.ForeColor = Color.FromArgb(243, 156, 18);
                            lblBursMiktar.Text = "Burs: Henüz atanmadı";
                            lblBaslangicTarihi.Text = "Başlangıç: -";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Burs Bilgisi Yükleme Hatası");
            }
        }

        void LoadOgrenciFromDB()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT AD, SOYAD, [TOPLAM HANE GELİRİ], FOTO, TELEFON, 
                                                      BÖLÜMÜ, SINIF, [KARDEŞ SAYISI], AGNO, AISkor, AINotu
                                                      FROM Ogrenciler WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", secilenOgrenciID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ad = reader["AD"]?.ToString() ?? string.Empty;
                            soyad = reader["SOYAD"]?.ToString() ?? string.Empty;
                            haneGeliri = reader["TOPLAM HANE GELİRİ"]?.ToString() ?? string.Empty;
                            fotoYolu = reader["FOTO"]?.ToString() ?? string.Empty;
                            telNo = reader["TELEFON"]?.ToString() ?? string.Empty;
                            bolum = reader["BÖLÜMÜ"]?.ToString() ?? string.Empty;
                            sinif = reader["SINIF"]?.ToString() ?? string.Empty;
                            kardesSayisi = reader["KARDEŞ SAYISI"]?.ToString() ?? string.Empty;
                            agno = reader["AGNO"]?.ToString() ?? string.Empty;

                            if (txtOgrAd != null) txtOgrAd.Text = ad;
                            if (txtOgrSoyad != null) txtOgrSoyad.Text = soyad;
                            if (txtHaneGeliri != null) txtHaneGeliri.Text = haneGeliri;
                            if (txtAgno != null) txtAgno.Text = agno;
                            if (txtBolum != null) txtBolum.Text = bolum;
                            if (txtOgrKardesSayisi != null) txtOgrKardesSayisi.Text = kardesSayisi;
                            if (txtSinif != null) txtSinif.Text = sinif;
                            if (txtTelNo != null) txtTelNo.Text = telNo;

                            // Fotoğrafı yükle
                            if (!string.IsNullOrEmpty(fotoYolu))
                            {
                                Image resim = ImageHelper.Base64ToImage(fotoYolu);
                                if (resim != null)
                                    pictureEdit1.EditValue = resim;
                            }

                            // AI Skorunu yükle (Not: txtAISkor kontrolü bu formda yok, sadece veritabanından okunuyor)
                            // AI Skor bilgisi veritabanında saklanıyor, formda gösterilmiyor
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Öğrenci Verisi Yükleme Hatası");
            }
        }

        void LoadAISkor()
        {
            // Not: txtAISkor kontrolü bu formda yok
            // AI Skor bilgisi veritabanında saklanıyor, formda gösterilmiyor
            // Bu metod şimdilik boş bırakıldı, gelecekte bir kontrol eklendiğinde kullanılabilir
        }

        public OgrenciProfili()
        {
            InitializeComponent();
        }

    }
}
