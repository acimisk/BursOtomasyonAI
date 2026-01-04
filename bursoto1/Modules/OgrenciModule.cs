using bursoto1.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace bursoto1.Modules
{
    public partial class OgrenciModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public OgrenciModule()
        {
            InitializeComponent();

            if (gridView1 != null)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode =
                    GridMultiSelectMode.CheckBoxRowSelect;
            }
        }

        private void OgrenciModule_Load(object sender, EventArgs e)
        {
            Listele();
            WireEvents();
            ApplyDarkModeToUI();
        }

        // Dark Mode UI Ayarları
        private void ApplyDarkModeToUI()
        {
            // AI Panel dark mode
            if (panelAI != null)
            {
                panelAI.Appearance.BackColor = Color.FromArgb(35, 35, 38);
                panelAI.Appearance.Options.UseBackColor = true;
            }

            // AI Label'lar dark mode
            if (lblAIbaslik != null)
            {
                lblAIbaslik.Appearance.ForeColor = Color.FromArgb(200, 200, 200);
            }
            if (memoAIsonuc != null)
            {
                memoAIsonuc.Properties.Appearance.ForeColor = Color.FromArgb(180, 180, 180);
            }

            // Filtre label dark mode
            if (lblFiltre != null)
            {
                lblFiltre.Appearance.ForeColor = Color.FromArgb(200, 200, 200);
            }

            // Filtre varsayılan değer
            if (cmbFiltre != null && cmbFiltre.Properties.Items.Count > 0)
            {
                cmbFiltre.SelectedIndex = 0;
            }
        }

        void WireEvents()
        {
            // Filtreleme event'i
            if (cmbFiltre != null)
                cmbFiltre.SelectedIndexChanged += CmbFiltre_SelectedIndexChanged;

            // Profil göster butonu
            if (btnGoster != null)
                btnGoster.Click += BtnGoster_Click;

            // AI Analiz butonları
            if (btnAIAnaliz != null)
                btnAIAnaliz.Click += BtnAIAnaliz_Click;
            if (btnBursKabul != null)
                btnBursKabul.Click += BtnBursKabul_Click;
            if (btnBursReddet != null)
                btnBursReddet.Click += BtnBursReddet_Click;
            if (btnYedek != null)
                btnYedek.Click += BtnYedek_Click;
        }

        private void CmbFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFiltre.SelectedIndex >= 0)
            {
                string secilenFiltre = cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString();
                Listele(secilenFiltre);
            }
        }

        // 🔥 GRID ZORLA DARK MODE
        private void ApplyDarkGrid(GridView gv)
        {
            // SATIRLAR
            gv.Appearance.Row.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Row.ForeColor = Color.White;
            gv.Appearance.Row.Options.UseBackColor = true;
            gv.Appearance.Row.Options.UseForeColor = true;

            // BAŞLIK
            gv.Appearance.HeaderPanel.BackColor = Color.FromArgb(45, 45, 48);
            gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.HeaderPanel.Options.UseBackColor = true;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;

            // SEÇİLİ SATIR
            gv.Appearance.FocusedRow.BackColor = Color.FromArgb(70, 70, 70);
            gv.Appearance.FocusedRow.ForeColor = Color.White;
            gv.Appearance.FocusedRow.Options.UseBackColor = true;
            gv.Appearance.FocusedRow.Options.UseForeColor = true;

            gv.Appearance.SelectedRow.BackColor = Color.FromArgb(60, 60, 60);
            gv.Appearance.SelectedRow.ForeColor = Color.White;
            gv.Appearance.SelectedRow.Options.UseBackColor = true;
            gv.Appearance.SelectedRow.Options.UseForeColor = true;

            // BOŞ ALAN
            gv.Appearance.Empty.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Empty.Options.UseBackColor = true;

            gv.OptionsView.EnableAppearanceEvenRow = false;
            gv.OptionsView.EnableAppearanceOddRow = false;

            // GRID CONTROL ARKAPLAN (WXI override)
            gridControl1.BackColor = Color.FromArgb(32, 32, 32);
        }

        public void Listele(string filtreTipi = "Tüm Öğrenciler")
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    string sorgu;
                    
                    switch (filtreTipi)
                    {
                        case "Burs Alanlar":
                            sorgu = @"SELECT DISTINCT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 1
                                     ORDER BY o.ID ASC";
                            break;
                        case "Beklemedeki Öğrenciler":
                            sorgu = @"SELECT DISTINCT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                     WHERE ob.Durum = 0
                                     ORDER BY o.ID ASC";
                            break;
                        default: // "Tüm Öğrenciler"
                            sorgu = @"SELECT ID, AD, SOYAD, BÖLÜMÜ, SINIF, AGNO, 
                                  [TOPLAM HANE GELİRİ] AS [Hane Geliri], [KARDEŞ SAYISI] AS [Kardeş], 
                                  ISNULL(AISkor, 0) AS [AI Puanı]
                                     FROM Ogrenciler
                                     ORDER BY ID ASC";
                            break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }

                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;

                gridView1.BestFitColumns();
                ApplyDarkGrid(gridView1);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        // --- BUTONLAR ---

        public void btnYeni_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmOgrenciEkle frm = new FrmOgrenciEkle();
            if (frm.ShowDialog() == DialogResult.OK)
                Listele();
        }

        // --- PORTED FROM MASTER: Edit student (double-click or button) ---
        public void btnDuzenle_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                MessageHelper.ShowWarning("Lütfen düzenlenecek bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(id);
            FrmOgrenciEkle frm = new FrmOgrenciEkle(ogrenciID);
            if (frm.ShowDialog() == DialogResult.OK)
                Listele();
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            int[] seciliSatirlar = gridView1.GetSelectedRows();
            if (seciliSatirlar.Length == 0)
            {
                MessageHelper.ShowWarning("Lütfen silinecek kayıtları seçiniz.", "Seçim Yapılmadı");
                return;
            }

            // Seçilen öğrencilerin isimlerini topla
            string ogrenciListesi = "";
            foreach (int rowHandle in seciliSatirlar)
            {
                string ad = gridView1.GetRowCellValue(rowHandle, "AD")?.ToString() ?? "";
                string soyad = gridView1.GetRowCellValue(rowHandle, "SOYAD")?.ToString() ?? "";
                ogrenciListesi += $"• {ad} {soyad}\n";
            }

            if (MessageHelper.ShowConfirm(
                $"{seciliSatirlar.Length} adet öğrenciyi silmek istediğinize emin misiniz?\n\n{ogrenciListesi}\n⚠️ Bu işlem geri alınamaz!\n\nÖğrenciye ait tüm burs ve gider kayıtları da silinecektir.",
                "Silme Onayı"))
            {
                int silinenSayisi = 0;
                int hataluSayisi = 0;

                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        foreach (int rowHandle in seciliSatirlar)
                        {
                            var id = gridView1.GetRowCellValue(rowHandle, "ID");
                            if (id != null)
                            {
                                // Transaction ile güvenli silme
                                SqlTransaction transaction = conn.BeginTransaction();
                                try
                                {
                                    // 1. BursGiderleri'nden sil (FK constraint)
                                    SqlCommand cmdGider = new SqlCommand("DELETE FROM BursGiderleri WHERE OgrenciID=@p1", conn, transaction);
                                    cmdGider.Parameters.AddWithValue("@p1", id);
                                    cmdGider.ExecuteNonQuery();

                                    // 2. OgrenciBurslari'ndan sil (FK constraint)
                                    SqlCommand cmdBurs = new SqlCommand("DELETE FROM OgrenciBurslari WHERE OgrenciID=@p1", conn, transaction);
                                    cmdBurs.Parameters.AddWithValue("@p1", id);
                                    cmdBurs.ExecuteNonQuery();

                                    // 3. Son olarak öğrenciyi sil
                                    SqlCommand cmdOgr = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn, transaction);
                                    cmdOgr.Parameters.AddWithValue("@p1", id);
                                    cmdOgr.ExecuteNonQuery();

                                    transaction.Commit();
                                    silinenSayisi++;
                                }
                                catch (Exception txEx)
                                {
                                    transaction.Rollback();
                                    hataluSayisi++;
                                    System.Diagnostics.Debug.WriteLine($"Öğrenci silme hatası (ID: {id}): {txEx.Message}");
                                }
                            }
                        }
                    }

                    if (silinenSayisi > 0 && hataluSayisi == 0)
                    {
                        MessageHelper.ShowSuccess($"{silinenSayisi} öğrenci başarıyla silindi.", "Silme Başarılı");
                    }
                    else if (silinenSayisi > 0 && hataluSayisi > 0)
                    {
                        MessageHelper.ShowWarning($"{silinenSayisi} öğrenci silindi, {hataluSayisi} öğrenci silinemedi.", "Kısmi Başarı");
                    }
                    else
                    {
                        MessageHelper.ShowError("Hiçbir öğrenci silinemedi.", "Silme Başarısız");
                    }

                    DataChangedNotifier.NotifyOgrenciChanged();
                    Listele(GetCurrentFilter());
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // btnYeni ile aynı işlevi yapar
            btnYeni_ItemClick(sender, e);
        }
        
        // Mevcut filtre değerini al
        private string GetCurrentFilter()
        {
            return cmbFiltre?.SelectedIndex >= 0 
                ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() 
                : "Tüm Öğrenciler";
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            // Double-click opens profile (tabbed form)
            BtnGoster_Click(sender, e);
        }

        // Profil Göster - Tabbed form
        private void BtnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);

            try
            {
                // Tüm öğrenci bilgilerini veritabanından çek
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand(@"SELECT AD, SOYAD, [TOPLAM HANE GELİRİ], FOTO, TELEFON, 
                                                      BÖLÜMÜ, SINIF, [KARDEŞ SAYISI], AGNO, AISkor, AINotu
                                                      FROM Ogrenciler WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                OgrenciProfili frm = new OgrenciProfili();
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

        // AI Analiz
        private async void BtnAIAnaliz_Click(object sender, EventArgs e)
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

            // Öğrencinin ek bilgilerini çek (motivasyon, ihtiyaç cevapları vb.)
            string motivasyon = "";
            string ihtiyac = "";
            string hedefler = "";
            string kullanim = "";
            string fark = "";
            
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Önce kolonları kontrol et
                    SqlCommand cmdCheck = new SqlCommand(@"SELECT COLUMN_NAME 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_NAME = 'Ogrenciler' 
                        AND COLUMN_NAME IN ('Motivasyon', 'Ihtiyac', 'Hedefler', 'BursKullanim', 'FarkliOzellik', 'AINotu')", conn);
                    var kolonlar = new List<string>();
                    using (var reader = cmdCheck.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            kolonlar.Add(reader[0].ToString());
                        }
                    }

                    // Mevcut kolonları kullanarak sorgu oluştur
                    string selectColumns = "ID";
                    if (kolonlar.Contains("Motivasyon")) selectColumns += ", ISNULL(Motivasyon, '') as Motivasyon";
                    if (kolonlar.Contains("Ihtiyac")) selectColumns += ", ISNULL(Ihtiyac, '') as Ihtiyac";
                    if (kolonlar.Contains("Hedefler")) selectColumns += ", ISNULL(Hedefler, '') as Hedefler";
                    if (kolonlar.Contains("BursKullanim")) selectColumns += ", ISNULL(BursKullanim, '') as Kullanim";
                    if (kolonlar.Contains("FarkliOzellik")) selectColumns += ", ISNULL(FarkliOzellik, '') as Fark";
                    if (kolonlar.Contains("AINotu")) selectColumns += ", ISNULL(AINotu, '') as AINotu";

                    SqlCommand cmd = new SqlCommand($"SELECT {selectColumns} FROM Ogrenciler WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (kolonlar.Contains("Motivasyon"))
                                motivasyon = reader["Motivasyon"]?.ToString() ?? "";
                            if (kolonlar.Contains("Ihtiyac"))
                                ihtiyac = reader["Ihtiyac"]?.ToString() ?? "";
                            if (kolonlar.Contains("Hedefler"))
                                hedefler = reader["Hedefler"]?.ToString() ?? "";
                            if (kolonlar.Contains("BursKullanim"))
                                kullanim = reader["Kullanim"]?.ToString() ?? "";
                            if (kolonlar.Contains("FarkliOzellik"))
                                fark = reader["Fark"]?.ToString() ?? "";
                            
                            // AINotu varsa ve diğerleri boşsa onu kullan
                            if (kolonlar.Contains("AINotu"))
                            {
                                string aiNotu = reader["AINotu"]?.ToString() ?? "";
                                if (string.IsNullOrWhiteSpace(ihtiyac) && string.IsNullOrWhiteSpace(motivasyon) && !string.IsNullOrWhiteSpace(aiNotu))
                                {
                                    motivasyon = aiNotu; // Fallback olarak kullan
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Motivasyon sütunları okunamadı: {ex.Message}");
            }

            // AI için detaylı veri hazırla - boş değilse gönder
            string ogrenciVerisi = $"Ad Soyad: {ad} {soyad}\n" +
                                   $"Bölüm: {bolum}, Sınıf: {sinif}\n" +
                                   $"AGNO: {agno}\n" +
                                   $"Hane Geliri: {gelir} TL\n" +
                                   $"Kardeş Sayısı: {kardes}\n\n";

            // Sadece dolu olan cevapları ekle
            if (!string.IsNullOrWhiteSpace(ihtiyac))
                ogrenciVerisi += $"[İHTİYAÇ] Neden bursa ihtiyacınız var?\n{ihtiyac}\n\n";
            else if (!string.IsNullOrWhiteSpace(motivasyon))
                ogrenciVerisi += $"[İHTİYAÇ/MOTİVASYON] Neden bursa ihtiyacınız var?\n{motivasyon}\n\n";
            else
                ogrenciVerisi += $"[İHTİYAÇ] Neden bursa ihtiyacınız var?\nCevap verilmemiş\n\n";

            if (!string.IsNullOrWhiteSpace(hedefler))
                ogrenciVerisi += $"[HEDEFLER] Kariyer hedefleriniz nelerdir?\n{hedefler}\n\n";
            else
                ogrenciVerisi += $"[HEDEFLER] Kariyer hedefleriniz nelerdir?\nCevap verilmemiş\n\n";

            if (!string.IsNullOrWhiteSpace(kullanim))
                ogrenciVerisi += $"[KULLANIM] Bursu nasıl kullanacaksınız?\n{kullanim}\n\n";
            else
                ogrenciVerisi += $"[KULLANIM] Bursu nasıl kullanacaksınız?\nCevap verilmemiş\n\n";

            if (!string.IsNullOrWhiteSpace(fark))
                ogrenciVerisi += $"[FARK] Sizi diğer adaylardan ayıran nedir?\n{fark}\n\n";
            else
                ogrenciVerisi += $"[FARK] Sizi diğer adaylardan ayıran nedir?\nCevap verilmemiş\n\n";
            
            System.Diagnostics.Debug.WriteLine($"AI Analiz için veri (uzunluk: {ogrenciVerisi.Length}):\n{ogrenciVerisi}");

            if (memoAIsonuc != null)
            {
                memoAIsonuc.EditValue = "AI analiz yapılıyor, lütfen bekleyiniz...";
                memoAIsonuc.Properties.Appearance.ForeColor = Color.FromArgb(52, 152, 219);
            }
            if (btnAIAnaliz != null)
                btnAIAnaliz.Enabled = false;

            try
            {
                GeminiAI geminiAI = new GeminiAI();
                string sonuc = await geminiAI.BursAnaliziYap(ogrenciVerisi);
                
                int aiSkor = ParseAISkor(sonuc);
                string aiNotu = ParseAINotu(sonuc);

                // AI Skorunu kaydet
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor = @skor WHERE ID = @id", conn);
                    cmd.Parameters.AddWithValue("@skor", aiSkor);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    cmd.ExecuteNonQuery();
                }

                string uygunluk = aiSkor >= 70 ? "UYGUN" : aiSkor >= 40 ? "DEĞERLENDIR" : "UYGUN DEĞİL";
                Color renk = aiSkor >= 70 ? Color.FromArgb(39, 174, 96) : aiSkor >= 40 ? Color.FromArgb(243, 156, 18) : Color.FromArgb(231, 76, 60);

                try
                {
                    if (memoAIsonuc != null)
                    {
                        // 1. Ayraç Çizgileri
                        string thickLine = new string('━', 45); // Kalın ana ayraç
                        string thinLine = new string('─', 45);  // İnce alt ayraç
                        string n = Environment.NewLine;         // Yeni satır kısayolu

                        // 2. Ham metni temizle ve başlıkları boşluklu hale getir
                        // Metinlerin çizgiye yapışmaması için her başlıktan sonra 2 satır atlıyoruz.
                        string temizNot = aiNotu
                            .Replace("ANALİZ:", $"{n}🔍  ANALİZ{n}{thinLine}{n}")
                            .Replace("KİŞİLİK:", $"{n}{n}👤  KİŞİLİK ÖZETİ{n}{thinLine}{n}")
                            .Replace("KARAR:", $"{n}{n}📌  SONUÇ VE KARAR{n}{thinLine}{n}");

                        // 3. Raporu İnşa Et
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"         📋 AI DEĞERLENDİRME RAPORU");
                        sb.AppendLine(thickLine);
                        sb.AppendLine($"👤 Öğrenci  : {ad.ToUpper()} {soyad.ToUpper()}");
                        sb.AppendLine($"🎯 Puanlama : {aiSkor} / 100");
                        sb.AppendLine($"📢 Durum    : {(uygunluk.ToUpper().Contains("UYGUN") ? "✅ " : "❌ ")}{uygunluk.ToUpper()}");
                        sb.AppendLine(thickLine);
                        sb.AppendLine(temizNot);

                        // 4. MemoEdit'e Bas
                        memoAIsonuc.EditValue = sb.ToString();
                        memoAIsonuc.Properties.Appearance.ForeColor = renk;

                        // FONT ÇOK ÖNEMLİ: Eğer fontun kötüyse ne yapsan kötü durur. 
                        // "Segoe UI" veya "Consolas" (kod gibi dursun istersen) öneririm.
                        memoAIsonuc.Properties.Appearance.Font = new Font("Segoe UI Semibold", 10.5F);
                    }

                    // Filtreleme ve bildirim işlemleri
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
                    DataChangedNotifier.NotifyOgrenciChanged();
                }
                catch (Exception ex)
                {
                    if (memoAIsonuc != null)
                    {
                        memoAIsonuc.EditValue = "❌ HATA: AI analizi sırasında bir sorun oluştu.\r\n\r\nDetay: " + ex.Message;
                        memoAIsonuc.Properties.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            finally
            {
                if (btnAIAnaliz != null)
                    btnAIAnaliz.Enabled = true;
            }
        }

        private int ParseAISkor(string sonuc)
        {
            try
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(sonuc, @"SKOR:\s*(\d+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    return Math.Min(100, Math.Max(0, int.Parse(match.Groups[1].Value)));
                }
            }
            catch { }
            return 50;
        }

        private string ParseAINotu(string sonuc)
        {
            try
            {
                System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(sonuc, @"ACIKLAMA:\s*(.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Singleline);
                if (match.Success)
                    return match.Groups[1].Value.Trim();
            }
            catch { }
            return sonuc;
        }

        // Burs Kabul - Burs seçimi dialogu ile
        private void BtnBursKabul_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);
            string adSoyad = $"{dr["AD"]} {dr["SOYAD"]}";

            // Mevcut bursları ve kontenjanları getir
            DataTable burslar = GetAvailableBurslar();
            if (burslar == null || burslar.Rows.Count == 0)
            {
                MessageHelper.ShowWarning("Kontenjanı olan aktif burs bulunamadı.", "Burs Yok");
                return;
            }

            // Burs seçim dialogu
            using (XtraForm frm = new XtraForm())
            {
                frm.Text = $"Burs Seç - {adSoyad}";
                frm.Size = new System.Drawing.Size(450, 350);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                var lblInfo = new LabelControl()
                {
                    Text = $"{adSoyad} öğrencisine hangi bursu atamak istiyorsunuz?",
                    Location = new System.Drawing.Point(20, 20),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new System.Drawing.Size(400, 30)
                };

                var listBurs = new DevExpress.XtraEditors.ListBoxControl()
                {
                    Location = new System.Drawing.Point(20, 55),
                    Size = new System.Drawing.Size(400, 200)
                };

                // Burslar tablosundaki ID kolonunu tespit et
                string bursIDKolonu = "BursID";
                if (burslar.Columns.Contains("BursID"))
                    bursIDKolonu = "BursID";
                else if (burslar.Columns.Contains("ID"))
                    bursIDKolonu = "ID";

                foreach (DataRow bursRow in burslar.Rows)
                {
                    string bursAdi = bursRow["BursAdı"]?.ToString() ?? "";
                    int kontenjan = Convert.ToInt32(bursRow["Kontenjan"] ?? 0);
                    int dolu = Convert.ToInt32(bursRow["Dolu"] ?? 0);
                    int bos = kontenjan - dolu;
                    decimal miktar = Convert.ToDecimal(bursRow["Miktar"] ?? 0);
                    int bursID = Convert.ToInt32(bursRow[bursIDKolonu] ?? 0);

                    listBurs.Items.Add(new BursItem(bursID, $"{bursAdi} - {miktar:C0}/ay ({dolu}/{kontenjan} dolu, {bos} boş)"));
                }

                if (listBurs.Items.Count > 0)
                    listBurs.SelectedIndex = 0;

                var btnAtama = new SimpleButton()
                {
                    Text = "✅ Bursu Ata",
                    Location = new System.Drawing.Point(20, 265),
                    Size = new System.Drawing.Size(190, 35)
                };
                btnAtama.Appearance.BackColor = Color.FromArgb(46, 204, 113);
                btnAtama.Appearance.ForeColor = Color.White;
                btnAtama.Appearance.Options.UseBackColor = true;
                btnAtama.Appearance.Options.UseForeColor = true;

                var btnIptal = new SimpleButton()
                {
                    Text = "❌ İptal",
                    Location = new System.Drawing.Point(220, 265),
                    Size = new System.Drawing.Size(190, 35)
                };

                btnIptal.Click += (s, ev) => frm.DialogResult = DialogResult.Cancel;
                btnAtama.Click += (s, ev) =>
                {
                    if (listBurs.SelectedItem is BursItem selectedBurs)
                    {
                        try
                        {
                            using (SqlConnection conn = bgl.baglanti())
                            {
                                // Önce mevcut kaydı güncelle veya yeni ekle
                                SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM OgrenciBurslari WHERE OgrenciID = @id", conn);
                                cmdCheck.Parameters.AddWithValue("@id", ogrenciID);
                                int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                                if (exists > 0)
                                {
                                    SqlCommand cmd = new SqlCommand("UPDATE OgrenciBurslari SET BursID = @bursID, Durum = 1, BaslangicTarihi = @tarih WHERE OgrenciID = @id", conn);
                                    cmd.Parameters.AddWithValue("@bursID", selectedBurs.BursID);
                                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    SqlCommand cmd = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) VALUES (@id, @bursID, @tarih, 1)", conn);
                                    cmd.Parameters.AddWithValue("@bursID", selectedBurs.BursID);
                                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            frm.DialogResult = DialogResult.OK;
                        }
                        catch (Exception ex)
                        {
                            MessageHelper.ShowException(ex, "Burs Atama Hatası");
                        }
                    }
                };

                frm.Controls.AddRange(new Control[] { lblInfo, listBurs, btnAtama, btnIptal });

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MessageHelper.ShowSuccess($"{adSoyad} öğrencisine burs başarıyla atandı.", "İşlem Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                    Listele(GetCurrentFilter());
                }
            }
        }

        // Burs listesi için yardımcı sınıf
        private class BursItem
        {
            public int BursID { get; set; }
            public string Display { get; set; }
            public BursItem(int id, string display) { BursID = id; Display = display; }
            public override string ToString() => Display;
        }

        // Kontenjanı boş olan bursları getir
        private DataTable GetAvailableBurslar()
        {
            try
            {
                DataTable dt = new DataTable();
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

                    // Her burs için doluluk sayısını hesapla
                    string query = $@"SELECT b.*, 
                        ISNULL((SELECT COUNT(*) FROM OgrenciBurslari ob WHERE ob.BursID = b.{bursIDKolonu} AND ob.Durum = 1), 0) as Dolu
                        FROM Burslar b
                        WHERE b.Kontenjan > ISNULL((SELECT COUNT(*) FROM OgrenciBurslari ob WHERE ob.BursID = b.{bursIDKolonu} AND ob.Durum = 1), 0)";
                    
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAvailableBurslar hatası: {ex.Message}");
                return null;
            }
        }

        // Burs Reddet
        private void BtnBursReddet_Click(object sender, EventArgs e)
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
                        SqlCommand cmd = new SqlCommand("DELETE FROM OgrenciBurslari WHERE OgrenciID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowInfo($"{adSoyad} öğrencisinin burs başvurusu REDDEDİLDİ.", "İşlem Tamamlandı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                    Listele(GetCurrentFilter());
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Reddetme Hatası");
                }
            }
        }

        // Yedek Listeye Al - Durum = 2
        private void BtnYedek_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }

            int ogrenciID = Convert.ToInt32(dr["ID"]);
            string adSoyad = $"{dr["AD"]} {dr["SOYAD"]}";

            if (MessageHelper.ShowConfirm($"{adSoyad} öğrencisini YEDEK LİSTEYE almak istediğinize emin misiniz?\n\nKontenjan açıldığında bu öğrenci değerlendirilecektir.", "Yedek Listeye Al"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // Durum = 2 (Yedek)
                        SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM OgrenciBurslari WHERE OgrenciID = @id", conn);
                        cmdCheck.Parameters.AddWithValue("@id", ogrenciID);
                        int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (exists > 0)
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE OgrenciBurslari SET Durum = 2 WHERE OgrenciID = @id", conn);
                            cmd.Parameters.AddWithValue("@id", ogrenciID);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) VALUES (@id, 1, @tarih, 2)", conn);
                            cmd.Parameters.AddWithValue("@id", ogrenciID);
                            cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageHelper.ShowSuccess($"{adSoyad} öğrencisi YEDEK LİSTEYE alındı.", "İşlem Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                    Listele(GetCurrentFilter());
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Yedek Listesi Hatası");
                }
            }
        }

        private void btnAIAnaliz_Click_1(object sender, EventArgs e)
        {

        }

        private void memoAIsonuc_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
