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
                string veri = $"AD: {txtOgrAd.Text}, AGNO: {txtAgno.Text}, GELIR: {txtHaneGeliri.Text}, KARDES: {txtOgrKardesSayisi.Text}";

                rtbAnalizSonuc.EditValue = "Gemini analiz ediyor...";
                string sonuc = await ai.BursAnaliziYap(veri);
                rtbAnalizSonuc.EditValue = sonuc;

                var match = Regex.Match(sonuc, @"SKOR:\s*(\d+)");
                if (match.Success)
                {
                    int puan = int.Parse(match.Groups[1].Value);
                    txtAISkor.Text = puan.ToString();

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
                txtOgrAd.Text = ad;
                txtOgrSoyad.Text = soyad;
                txtHaneGeliri.Text = haneGeliri;
                txtAgno.Text = agno;
                txtBolum.Text = bolum;
                txtOgrKardesSayisi.Text = kardesSayisi;
                txtSinif.Text = sinif;
                txtTelNo.Text = telNo;
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
                            ad = reader["AD"]?.ToString();
                            soyad = reader["SOYAD"]?.ToString();
                            haneGeliri = reader["TOPLAM HANE GELİRİ"]?.ToString();
                            fotoYolu = reader["FOTO"]?.ToString();
                            telNo = reader["TELEFON"]?.ToString();
                            bolum = reader["BÖLÜMÜ"]?.ToString();
                            sinif = reader["SINIF"]?.ToString();
                            kardesSayisi = reader["KARDEŞ SAYISI"]?.ToString();
                            agno = reader["AGNO"]?.ToString();

                            txtOgrAd.Text = ad;
                            txtOgrSoyad.Text = soyad;
                            txtHaneGeliri.Text = haneGeliri;
                            txtAgno.Text = agno;
                            txtBolum.Text = bolum;
                            txtOgrKardesSayisi.Text = kardesSayisi;
                            txtSinif.Text = sinif;
                            txtTelNo.Text = telNo;

                            // Fotoğrafı yükle
                            if (!string.IsNullOrEmpty(fotoYolu))
                            {
                                Image resim = ImageHelper.Base64ToImage(fotoYolu);
                                if (resim != null)
                                    pictureEdit1.EditValue = resim;
                            }

                            // AI Skorunu yükle
                            object aiSkor = reader["AISkor"];
                            if (aiSkor != DBNull.Value && aiSkor != null)
                                txtAISkor.Text = aiSkor.ToString();
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
            if (secilenOgrenciID > 0)
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("SELECT AISkor FROM Ogrenciler WHERE ID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", secilenOgrenciID);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                            txtAISkor.Text = result.ToString();
                    }
                }
                catch { }
            }
        }

        public OgrenciProfili()
        {
            InitializeComponent();
        }

    }
}
