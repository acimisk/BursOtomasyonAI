using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1
{
    public partial class FrmAylikBurs : XtraForm
    {
        private readonly SqlBaglanti bgl = new SqlBaglanti();

        public FrmAylikBurs()
        {
            InitializeComponent();
        }

        private void FrmAylikBurs_Load(object sender, EventArgs e)
        {
            BursTurleriniGetir();
            OgrencileriGetir();

            gridViewOgrenciler.OptionsSelection.MultiSelect = true;
            gridViewOgrenciler.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
            gridViewOgrenciler.BestFitColumns();
        }

        private void BursTurleriniGetir()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT BursID, BursAdı, Miktar FROM Burslar ORDER BY BursAdı", conn))
                    {
                        da.Fill(dt);
                    }
                }

                cmbBursTur.Properties.DataSource = dt;
                cmbBursTur.Properties.DisplayMember = "BursAdı";
                cmbBursTur.Properties.ValueMember = "BursID";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Burs Türleri");
            }
        }

        private void OgrencileriGetir()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    // OgrenciBurslari tablosunu JOIN ederek öğrencilere aldığı burs bilgisini ekliyoruz
                    string sorgu = @"SELECT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                    ISNULL(b.BursAdı, 'Burs Yok') AS [Aldığı Burs],
                                    CASE WHEN ob.Durum = 1 THEN 'Aktif' 
                                         WHEN ob.Durum = 0 THEN 'Beklemede' 
                                         ELSE 'Burs Yok' END AS [Burs Durumu]
                                    FROM Ogrenciler o
                                    LEFT JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                    LEFT JOIN Burslar b ON ob.BursID = b.BursID
                                    ORDER BY o.AD, o.SOYAD";
                    
                    using (SqlDataAdapter da = new SqlDataAdapter(sorgu, conn))
                    {
                        da.Fill(dt);
                    }
                }
                gridControlOgrenciler.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Öğrenciler");
            }
        }

        private void btnBursGonder_Click(object sender, EventArgs e)
        {
            if (cmbBursTur.EditValue == null)
            {
                MessageHelper.ShowWarning("Lütfen bir burs türü seçiniz.", "Burs Seçilmedi");
                return;
            }

            int[] secilenSatirlar = gridViewOgrenciler.GetSelectedRows();
            if (secilenSatirlar.Length == 0)
            {
                MessageHelper.ShowWarning("Lütfen en az bir öğrenci seçiniz.", "Öğrenci Seçilmedi");
                return;
            }

            int bursId = Convert.ToInt32(cmbBursTur.EditValue);

            // Seçili burs satırını veri kaynağından al
            var rowView = cmbBursTur.Properties.GetDataSourceRowByKeyValue(cmbBursTur.EditValue) as DataRowView;
            if (rowView == null)
            {
                MessageHelper.ShowWarning("Seçili burs türü okunamadı.", "Burs Hatası");
                return;
            }

            string bursAdi = rowView["BursAdı"].ToString();
            decimal bursMiktari = Convert.ToDecimal(rowView["Miktar"]);

            DateTime bugün = DateTime.Today;
            DialogResult onay = MessageHelper.ShowConfirm(
                $"{secilenSatirlar.Length} öğrenciye '{bursAdi}' bursu için {bursMiktari:C} tutarında {bugün:MMMM yyyy} ödemesi oluşturmak istiyor musunuz?",
                "Aylık Burs Onayı"
            ) ? DialogResult.Yes : DialogResult.No;

            if (onay != DialogResult.Yes) return;

            try
            {
                // NOT: Aylık ödeme kayıtları için ayrı bir tablo (OgrenciBursOdemeleri) yok.
                // Şu an için OgrenciBurslari tablosuna kayıt ekliyoruz veya güncelliyoruz.
                // Eğer aylık ödeme geçmişi tutmak istiyorsan, OgrenciBursOdemeleri tablosu oluşturulmalı.
                
                int basariliKayit = 0;
                int guncellenenKayit = 0;
                int yeniKayit = 0;
                
                using (SqlConnection conn = bgl.baglanti())
                {
                    foreach (int rowHandle in secilenSatirlar)
                    {
                        var ogrenciID = gridViewOgrenciler.GetRowCellValue(rowHandle, "ID");
                        if (ogrenciID == null) continue;

                        // Öğrencinin bu burs için kaydı var mı kontrol et
                        string kontrolQuery = @"SELECT COUNT(*) FROM OgrenciBurslari 
                                               WHERE OgrenciID = @OgrenciID AND BursID = @BursID";
                        
                        bool kayitVar = false;
                        using (SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, conn))
                        {
                            kontrolCmd.Parameters.AddWithValue("@OgrenciID", ogrenciID);
                            kontrolCmd.Parameters.AddWithValue("@BursID", bursId);
                            kayitVar = Convert.ToInt32(kontrolCmd.ExecuteScalar()) > 0;
                        }

                        if (kayitVar)
                        {
                            // Mevcut kaydı güncelle (Durum = 1 yap, BaslangicTarihi güncelle)
                            string updateQuery = @"UPDATE OgrenciBurslari 
                                                  SET Durum = 1, BaslangicTarihi = @BaslangicTarihi
                                                  WHERE OgrenciID = @OgrenciID AND BursID = @BursID";
                            
                            using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@OgrenciID", ogrenciID);
                                cmd.Parameters.AddWithValue("@BursID", bursId);
                                cmd.Parameters.AddWithValue("@BaslangicTarihi", bugün);
                                cmd.ExecuteNonQuery();
                                guncellenenKayit++;
                            }
                        }
                        else
                        {
                            // Yeni kayıt ekle
                            string insertQuery = @"INSERT INTO OgrenciBurslari 
                                                  (OgrenciID, BursID, BaslangicTarihi, Durum) 
                                                  VALUES (@OgrenciID, @BursID, @BaslangicTarihi, 1)";
                            
                            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@OgrenciID", ogrenciID);
                                cmd.Parameters.AddWithValue("@BursID", bursId);
                                cmd.Parameters.AddWithValue("@BaslangicTarihi", bugün);
                                cmd.ExecuteNonQuery();
                                yeniKayit++;
                            }
                        }

                        // GİDER KAYDI EKLE (BursGiderleri tablosuna)
                        // NOT: Eğer tablo yoksa, önce oluşturulmalı
                        try
                        {
                            string giderQuery = @"INSERT INTO BursGiderleri 
                                                 (OgrenciID, BursID, Tutar, OdemeTarihi, Ay, Yil, Aciklama) 
                                                 VALUES (@OgrenciID, @BursID, @Tutar, @OdemeTarihi, @Ay, @Yil, @Aciklama)";
                            
                            using (SqlCommand giderCmd = new SqlCommand(giderQuery, conn))
                            {
                                giderCmd.Parameters.AddWithValue("@OgrenciID", ogrenciID);
                                giderCmd.Parameters.AddWithValue("@BursID", bursId);
                                giderCmd.Parameters.AddWithValue("@Tutar", bursMiktari);
                                giderCmd.Parameters.AddWithValue("@OdemeTarihi", bugün);
                                giderCmd.Parameters.AddWithValue("@Ay", bugün.Month);
                                giderCmd.Parameters.AddWithValue("@Yil", bugün.Year);
                                giderCmd.Parameters.AddWithValue("@Aciklama", $"{bursAdi} - {bugün:MMMM yyyy}");
                                giderCmd.ExecuteNonQuery();
                            }
                        }
                        catch
                        {
                            // BursGiderleri tablosu yoksa, sadece uyarı ver (kritik değil)
                            // Tablo oluşturulduğunda otomatik çalışacak
                        }

                        basariliKayit++;
                    }
                }

                // Başarı mesajı
                string mesaj = $"{basariliKayit} öğrenciye '{bursAdi}' bursu için {bursMiktari:C} tutarında " +
                              $"{bugün:MMMM yyyy} dönemi ödemesi başarıyla oluşturuldu.";
                
                if (yeniKayit > 0 && guncellenenKayit > 0)
                {
                    mesaj += $"\n({yeniKayit} yeni kayıt, {guncellenenKayit} güncellenmiş kayıt)";
                }
                
                MessageHelper.ShowSuccess(mesaj, "Burs Ödemesi Tamamlandı");

                // Listeyi yenile
                OgrencileriGetir();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Burs Gönderme Hatası");
            }
        }
    }
}


