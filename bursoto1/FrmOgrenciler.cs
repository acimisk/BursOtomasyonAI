using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1
{
    public partial class FrmOgrenciler : Form
    {
        SqlBaglanti bgl = new SqlBaglanti();
        GeminiAI geminiAI = new GeminiAI();

        public FrmOgrenciler()
        {
            InitializeComponent();
            ModernUIAyarla();
            
            // Otomatik yenileme için event'lere abone ol
            DataChangedNotifier.OgrenciDegisti += OnOgrenciDegisti;
            DataChangedNotifier.BursDegisti += OnOgrenciDegisti;
        }

        private void ModernUIAyarla()
        {
            // Modern form ayarları - Soft neutral background
            this.BackColor = Color.FromArgb(233, 236, 239);
            
            // Grid modern görünüm - Better depth
            gridView1.Appearance.HeaderPanel.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            gridView1.Appearance.HeaderPanel.BackColor = Color.FromArgb(44, 62, 80);
            gridView1.Appearance.HeaderPanel.ForeColor = Color.White;
            gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
            // Grid satır görünümü
            gridView1.Appearance.Row.BackColor = Color.White;
            gridView1.Appearance.Row.Options.UseBackColor = true;
            gridView1.Appearance.EvenRow.BackColor = Color.FromArgb(248, 249, 250);
            gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            
            // Filtre combobox
            cmbFiltre.Properties.Appearance.Font = new Font("Segoe UI", 9.5F);
            cmbFiltre.Properties.Appearance.Options.UseFont = true;
            cmbFiltre.Properties.Appearance.BackColor = Color.White;
            cmbFiltre.Properties.Appearance.Options.UseBackColor = true;
            
            // Label
            lblFiltre.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFiltre.Appearance.ForeColor = Color.FromArgb(44, 62, 80);
            lblFiltre.Appearance.Options.UseFont = true;
            lblFiltre.Appearance.Options.UseForeColor = true;
        }

        private void OnOgrenciDegisti()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(OnOgrenciDegisti));
                return;
            }
            
            // Mevcut filtreyi koru ve listeyi yenile
            string secilenFiltre = cmbFiltre.SelectedIndex >= 0 
                ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() 
                : "Beklemedeki Öğrenciler";
            Listele(secilenFiltre);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            DataChangedNotifier.OgrenciDegisti -= OnOgrenciDegisti;
            DataChangedNotifier.BursDegisti -= OnOgrenciDegisti;
            base.OnFormClosed(e);
        }

        // LİSTELEME (Filtreleme desteği ile) - FOTO hariç sadece önemli kolonlar
        public void Listele(string filtreTipi = "Beklemedeki Öğrenciler")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    string sorgu;
                    
                    // FOTO kolonu hariç - sadece ID, AD, SOYAD, AGNO, AISkor ve diğer önemli alanlar
                    switch (filtreTipi)
                    {
                        case "Burs Alanlar":
                            sorgu = @"SELECT DISTINCT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 1
                                     ORDER BY o.AD, o.SOYAD";
                            break;
                        case "Beklemedeki Öğrenciler":
                            sorgu = @"SELECT DISTINCT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 0
                                     ORDER BY o.AD, o.SOYAD";
                            break;
                        default: // "Tüm Öğrenciler"
                            sorgu = @"SELECT ID, AD, SOYAD, BÖLÜMÜ, SINIF, AGNO, 
                                     [TOPLAM HANE GELİRİ] AS [Hane Geliri], [KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler
                                     ORDER BY AD, SOYAD";
                            break;
                    }
                    
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    da.Fill(dt);
                }
                gridControl1.DataSource = dt;
                
                // ID kolonunu gizle (sadece arkaplanda kullanılacak)
                if (gridView1.Columns["ID"] != null) gridView1.Columns["ID"].Visible = false;
                
                // Kolon genişliklerini otomatik ayarla
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            // Varsayılan filtre: Beklemedeki Öğrenciler (index 2)
            cmbFiltre.SelectedIndex = 2; // "Beklemedeki Öğrenciler"
            
            Listele("Beklemedeki Öğrenciler");

            // Grid Çoklu Seçim
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
        }

        private void cmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltre.SelectedIndex >= 0)
            {
                string secilenFiltre = cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString();
                Listele(secilenFiltre);
            }
        }

        // --- AI ANALİZ ---
        public async void btnAIAnaliz_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen analiz edilecek bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);
            string ad = dr["AD"]?.ToString() ?? "";
            string soyad = dr["SOYAD"]?.ToString() ?? "";
            string bolum = dr["BÖLÜMÜ"]?.ToString() ?? "";
            string sinif = dr["SINIF"]?.ToString() ?? "";
            string agno = dr["AGNO"]?.ToString() ?? "0";
            string gelir = dr["Hane Geliri"]?.ToString() ?? "0";
            string kardes = dr["Kardeş"]?.ToString() ?? "0";

            // Öğrencinin motivasyon yazısını (AINotu) veritabanından çek
            string motivasyon = "";
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT AINotu FROM Ogrenciler WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    object result = cmd.ExecuteScalar();
                    motivasyon = result?.ToString() ?? "";
                }
            }
            catch { }

            // AI analizi için tüm verileri hazırla
            string ogrenciVerisi = $"Ad Soyad: {ad} {soyad}\n" +
                                   $"Bölüm: {bolum}, Sınıf: {sinif}\n" +
                                   $"AGNO (Not Ortalaması): {agno}\n" +
                                   $"Hane Geliri: {gelir} TL\n" +
                                   $"Kardeş Sayısı: {kardes}\n" +
                                   $"Öğrencinin Motivasyon Yazısı: {(string.IsNullOrWhiteSpace(motivasyon) ? "Açıklama yazılmamış" : motivasyon)}";

            // Bekleme mesajı
            lblAIsonuc.Text = "AI analiz yapılıyor, lütfen bekleyiniz...";
            lblAIsonuc.ForeColor = Color.FromArgb(52, 152, 219);
            btnAIAnaliz.Enabled = false;

            try
            {
                // AI'dan analiz al
                string sonuc = await geminiAI.BursAnaliziYap(ogrenciVerisi);
                
                // Sonucu parse et - SKOR ve ACIKLAMA ayır
                int aiSkor = ParseAISkor(sonuc);
                string aiNotu = ParseAINotu(sonuc);

                // Sadece AI Skorunu veritabanına kaydet (AINotu'ya dokunma - öğrencinin cevapları orada)
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor = @skor WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@skor", aiSkor);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    cmd.ExecuteNonQuery();
                }

                // Sonucu göster - öğrencinin cevapları + AI değerlendirmesi
                string uygunluk = aiSkor >= 70 ? "UYGUN" : aiSkor >= 40 ? "DEĞERLENDIR" : "UYGUN DEĞİL";
                Color renk = aiSkor >= 70 ? Color.FromArgb(39, 174, 96) : aiSkor >= 40 ? Color.FromArgb(243, 156, 18) : Color.FromArgb(231, 76, 60);

                // Öğrencinin motivasyon cevaplarını formatla
                string ogrenciCevaplari = "";
                if (!string.IsNullOrWhiteSpace(motivasyon) && motivasyon.Contains("["))
                {
                    ogrenciCevaplari = "\n📝 ÖĞRENCİNİN CEVAPLARI:\n" + 
                                       motivasyon.Replace("[İHTİYAÇ]:", "\n• İhtiyaç:")
                                                 .Replace("[HEDEFLER]:", "\n• Hedefler:")
                                                 .Replace("[KULLANIM]:", "\n• Kullanım:")
                                                 .Replace("[FARK]:", "\n• Fark:") + "\n";
                }

                lblAIsonuc.Text = $"{ad} {soyad}\n\n{aiSkor}/100 - {uygunluk}\n{ogrenciCevaplari}\n{aiNotu}";
                lblAIsonuc.ForeColor = renk;

                // Listeyi yenile
                OnOgrenciDegisti();
                DataChangedNotifier.NotifyOgrenciChanged();
            }
            catch (Exception ex)
            {
                lblAIsonuc.Text = "AI analizi başarısız: " + ex.Message;
                lblAIsonuc.ForeColor = Color.FromArgb(231, 76, 60);
            }
            finally
            {
                btnAIAnaliz.Enabled = true;
            }
        }

        private int ParseAISkor(string sonuc)
        {
            try
            {
                // "SKOR: 75" formatını bul
                Match match = Regex.Match(sonuc, @"SKOR:\s*(\d+)", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return Math.Min(100, Math.Max(0, int.Parse(match.Groups[1].Value)));
                }
            }
            catch { }
            return 50; // Varsayılan skor
        }

        private string ParseAINotu(string sonuc)
        {
            try
            {
                // Yeni format: ANALİZ, KİŞİLİK, KARAR bölümlerini ayıkla
                string analiz = "", kisilik = "", karar = "";

                // ANALİZ bölümünü al
                Match matchAnaliz = Regex.Match(sonuc, @"ANALİZ:\s*([\s\S]*?)(?=KİŞİLİK:|KARAR:|$)", RegexOptions.IgnoreCase);
                if (matchAnaliz.Success) analiz = matchAnaliz.Groups[1].Value.Trim();

                // KİŞİLİK bölümünü al
                Match matchKisilik = Regex.Match(sonuc, @"KİŞİLİK:\s*([^\n]+(?:\n(?!KARAR:)[^\n]+)*)", RegexOptions.IgnoreCase);
                if (matchKisilik.Success) kisilik = matchKisilik.Groups[1].Value.Trim();

                // KARAR bölümünü al
                Match matchKarar = Regex.Match(sonuc, @"KARAR:\s*(.+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                if (matchKarar.Success) karar = matchKarar.Groups[1].Value.Trim();

                // Formatla
                string sonucMetni = "";
                if (!string.IsNullOrEmpty(analiz)) 
                    sonucMetni += "📊 CEVAP ANALİZİ:\n" + analiz + "\n\n";
                if (!string.IsNullOrEmpty(kisilik)) 
                    sonucMetni += "👤 KİŞİLİK:\n" + kisilik + "\n\n";
                if (!string.IsNullOrEmpty(karar)) 
                    sonucMetni += "⚖️ KARAR: " + karar;

                return string.IsNullOrEmpty(sonucMetni) ? sonuc : sonucMetni.Trim();
            }
            catch { }
            return sonuc;
        }

        // --- BURS KABUL ---
        public void btnBursKabul_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);
            string adSoyad = $"{dr["AD"]} {dr["SOYAD"]}";

            if (MessageHelper.ShowConfirm($"{adSoyad} öğrencisinin burs başvurusunu KABUL etmek istediğinize emin misiniz?", "Burs Kabul Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // OgrenciBurslari tablosunda Durum = 1 yap
                        SqlCommand cmd = new SqlCommand("UPDATE OgrenciBurslari SET Durum = 1, BaslangicTarihi = @tarih WHERE OgrenciID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                        int affected = cmd.ExecuteNonQuery();

                        if (affected == 0)
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

                            // Mevcut bir burs ID'si bul (herhangi bir burs)
                            int mevcutBursID = 0;
                            try
                            {
                                SqlCommand cmdBurs = new SqlCommand($"SELECT TOP 1 [{bursIDKolonu}] FROM Burslar", conn);
                                var bursResult = cmdBurs.ExecuteScalar();
                                if (bursResult != null && bursResult != DBNull.Value)
                                    mevcutBursID = Convert.ToInt32(bursResult);
                            }
                            catch { }

                            // Kayıt yoksa ekle
                            if (mevcutBursID > 0)
                            {
                                SqlCommand cmdInsert = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) VALUES (@id, @bursID, @tarih, 1)", conn);
                                cmdInsert.Parameters.AddWithValue("@id", ogrenciID);
                                cmdInsert.Parameters.AddWithValue("@bursID", mevcutBursID);
                                cmdInsert.Parameters.AddWithValue("@tarih", DateTime.Now);
                                cmdInsert.ExecuteNonQuery();
                            }
                            else
                            {
                                // Burs yoksa BursID olmadan ekle (eğer tablo izin veriyorsa)
                                try
                                {
                                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BaslangicTarihi, Durum) VALUES (@id, @tarih, 1)", conn);
                                    cmdInsert.Parameters.AddWithValue("@id", ogrenciID);
                                    cmdInsert.Parameters.AddWithValue("@tarih", DateTime.Now);
                                    cmdInsert.ExecuteNonQuery();
                                }
                                catch
                                {
                                    throw new Exception("Sistemde aktif burs bulunamadı. Lütfen önce bir burs tanımlayın.");
                                }
                            }
                        }
                    }

                    MessageHelper.ShowSuccess($"{adSoyad} öğrencisinin burs başvurusu KABUL EDİLDİ.", "İşlem Başarılı");
                    
                    // Tüm formları bilgilendir
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Kabul Hatası");
                }
            }
        }

        // --- BURS REDDET ---
        public void btnBursReddet_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);
            string adSoyad = $"{dr["AD"]} {dr["SOYAD"]}";

            if (MessageHelper.ShowConfirm($"{adSoyad} öğrencisinin burs başvurusunu REDDETMEk istediğinize emin misiniz?\n\nBu işlem geri alınamaz.", "Burs Reddetme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // OgrenciBurslari tablosundan kaydı sil
                        SqlCommand cmd = new SqlCommand("DELETE FROM OgrenciBurslari WHERE OgrenciID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowInfo($"{adSoyad} öğrencisinin burs başvurusu REDDEDİLDİ.", "İşlem Tamamlandı");
                    
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Reddetme Hatası");
                }
            }
        }

        // SİL
        public void btnSil_Click(object sender, EventArgs e)
        {
            int[] seciliSatirlar = gridView1.GetSelectedRows();

            if (seciliSatirlar.Length == 0)
            {
                MessageHelper.ShowWarning("Lütfen silinecek kayıtları seçiniz.", "Seçim Yapılmadı");
                return;
            }

            if (MessageHelper.ShowConfirm($"{seciliSatirlar.Length} adet kaydı silmek istediğinize emin misiniz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        foreach (int rowHandle in seciliSatirlar)
                        {
                            var id = gridView1.GetRowCellValue(rowHandle, "ID");
                            if (id != null)
                            {
                                // Önce OgrenciBurslari'ndan sil
                                SqlCommand cmdBurs = new SqlCommand("DELETE FROM OgrenciBurslari WHERE OgrenciID=@p1", conn);
                                cmdBurs.Parameters.AddWithValue("@p1", id);
                                cmdBurs.ExecuteNonQuery();

                                // Sonra öğrenciyi sil
                                SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                                cmd.Parameters.AddWithValue("@p1", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageHelper.ShowSuccess("Seçilen kayıtlar başarıyla silindi.", "Silme Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        // Profil Göster - Veritabanından tüm bilgileri çek
        private void btnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null) return;

            int ogrenciID = Convert.ToInt32(dr["ID"]);

            try
            {
                // Tüm öğrenci bilgilerini veritabanından çek (FOTO dahil)
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT AD, SOYAD, [TOPLAM HANE GELİRİ], FOTO, TELEFON, 
                                                      BÖLÜMÜ, SINIF, [KARDEŞ SAYISI], AGNO 
                                                      FROM Ogrenciler WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            OgrenciProfili frm = new OgrenciProfili();
                            frm.MdiParent = this.MdiParent;
                            frm.secilenOgrenciID = ogrenciID;

                            frm.ad = reader["AD"]?.ToString();
                            frm.soyad = reader["SOYAD"]?.ToString();
                            frm.haneGeliri = reader["TOPLAM HANE GELİRİ"]?.ToString();
                            frm.fotoYolu = reader["FOTO"]?.ToString();
                            frm.telNo = reader["TELEFON"]?.ToString();
                            frm.bolum = reader["BÖLÜMÜ"]?.ToString();
                            frm.sinif = reader["SINIF"]?.ToString();
                            frm.kardesSayisi = reader["KARDEŞ SAYISI"]?.ToString();
                            frm.agno = reader["AGNO"]?.ToString();

                            frm.Show();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Profil Yükleme Hatası");
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnGoster_Click(sender, e);
        }
    }
}
