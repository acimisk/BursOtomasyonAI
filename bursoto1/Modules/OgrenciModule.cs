using bursoto1.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Globalization;
// BursModel sınıfı bursoto1 namespace'inde tanımlı

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

            // Tahmin Panel dark mode
            if (panelTahmin != null)
            {
                panelTahmin.Appearance.BackColor = Color.FromArgb(40, 40, 43);
                panelTahmin.Appearance.Options.UseBackColor = true;
            }
            if (lblTahminBaslik != null)
            {
                lblTahminBaslik.Appearance.ForeColor = Color.FromArgb(200, 200, 200);
            }
            if (lblTahminSonuc != null)
            {
                lblTahminSonuc.Appearance.ForeColor = Color.FromArgb(200, 200, 200);
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


            // AI Analiz butonları
            if (btnAIAnaliz != null)
                btnAIAnaliz.Click += BtnAIAnaliz_Click;
            if (btnBursKabul != null)
                btnBursKabul.Click += BtnBursKabul_Click;
            if (btnBursReddet != null)
                btnBursReddet.Click += BtnBursReddet_Click;
            if (btnYedek != null)
                btnYedek.Click += BtnYedek_Click;

            // GridView event'leri
            if (gridView1 != null)
            {
                gridView1.CustomDrawCell += GridView1_CustomDrawCell;
                // Satır değiştiğinde AI özetini ve tahmin bilgisini sağ panele yansıt
                gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
            }
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
            // SATIRLAR - Varsayılan renk (RowStyle event handler renklendirmeyi override edecek)
            gv.Appearance.Row.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Row.ForeColor = Color.White;
            gv.Appearance.Row.Options.UseBackColor = false; // RowStyle event handler'ına izin ver
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

        public void Listele(string filtreTipi = "Tüm Öğrenciler", int? selectedOgrenciID = null)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                    string ogrenciIDKolonu = "ID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('ID', 'OgrenciID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'ID' THEN 1 
                                WHEN 'OgrenciID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                        {
                            string kolonStr = kolonResult.ToString() ?? string.Empty;
                            if (!string.IsNullOrEmpty(kolonStr))
                                ogrenciIDKolonu = kolonStr;
                        }
                    }
                    catch { }

                    // OgrenciBurslari tablosundaki ID kolonunu kontrol et
                    bool ogrenciBurslariIDVar = false;
                    try
                    {
                        SqlCommand cmdCheck = new SqlCommand(@"SELECT COUNT(*) 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'OgrenciBurslari' AND COLUMN_NAME = 'ID'", conn);
                        ogrenciBurslariIDVar = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;
                    }
                    catch { }

                    // Üniversite kolonu adını dinamik tespit et
                    string universiteKolon = null;
                    try
                    {
                        SqlCommand cmdUniKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME LIKE N'%niversite%'", conn);
                        var uniKolonResult = cmdUniKolon.ExecuteScalar();
                        if (uniKolonResult != null && uniKolonResult != DBNull.Value)
                        {
                            string uniKolonStr = uniKolonResult.ToString() ?? string.Empty;
                            if (!string.IsNullOrEmpty(uniKolonStr))
                                universiteKolon = uniKolonStr;
                        }
                    }
                    catch { }

                    // Üniversite kolonu SELECT'e eklenecek (varsa)
                    string universiteSelect = string.IsNullOrWhiteSpace(universiteKolon) 
                        ? string.Empty 
                        : $", o.[{universiteKolon}] AS [Üniversite]";

                    // AIPotansiyelNotu ve AIPotansiyelYuzde kolonları SELECT'e eklenecek
                    string aiPotansiyelSelect = ", ISNULL(o.AIPotansiyelNotu, 0) AS [AIPotansiyelNotu], ISNULL(o.AIPotansiyelYuzde, '') AS [AIPotansiyelYuzde]";

                    string sorgu;
                    
                    switch (filtreTipi)
                    {
                        case "Burs Alanlar":
                            sorgu = $@"SELECT DISTINCT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect}{aiPotansiyelSelect},
                                     ISNULL(ob.Durum, -1) AS [Durum],
                                     CASE 
                                         WHEN ob.Durum = 1 THEN 'Kabul Edildi'
                                         WHEN ob.Durum = 0 THEN 'Beklemede'
                                         WHEN ob.Durum = 2 THEN 'Yedek'
                                         ELSE 'Reddedildi'
                                     END AS [Durum Metni]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.[{ogrenciIDKolonu}] = ob.OgrenciID
                                     WHERE ob.Durum = 1
                                     ORDER BY o.[{ogrenciIDKolonu}] ASC";
                            break;
                        case "Beklemedeki Öğrenciler":
                            sorgu = $@"SELECT DISTINCT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect}{aiPotansiyelSelect},
                                     ISNULL(ob.Durum, -1) AS [Durum],
                                     CASE 
                                         WHEN ob.Durum = 1 THEN 'Kabul Edildi'
                                         WHEN ob.Durum = 0 THEN 'Beklemede'
                                         WHEN ob.Durum = 2 THEN 'Yedek'
                                         ELSE 'Reddedildi'
                                     END AS [Durum Metni]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.[{ogrenciIDKolonu}] = ob.OgrenciID
                                     WHERE ob.Durum = 0
                                     ORDER BY o.[{ogrenciIDKolonu}] ASC";
                            break;
                        case "Yedek Listesi":
                            sorgu = $@"SELECT DISTINCT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect}{aiPotansiyelSelect},
                                     ISNULL(ob.Durum, -1) AS [Durum],
                                     CASE 
                                         WHEN ob.Durum = 1 THEN 'Kabul Edildi'
                                         WHEN ob.Durum = 0 THEN 'Beklemede'
                                         WHEN ob.Durum = 2 THEN 'Yedek'
                                         ELSE 'Reddedildi'
                                     END AS [Durum Metni]
                                     FROM Ogrenciler o
                                     INNER JOIN OgrenciBurslari ob ON o.[{ogrenciIDKolonu}] = ob.OgrenciID
                                     WHERE ob.Durum = 2
                                     ORDER BY o.[{ogrenciIDKolonu}] ASC";
                            break;
                        case "Reddedilen Öğrenciler":
                            sorgu = $@"SELECT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect}{aiPotansiyelSelect},
                                     -1 AS [Durum],
                                     'Reddedildi' AS [Durum Metni]
                                     FROM Ogrenciler o
                                     WHERE NOT EXISTS (SELECT 1 FROM OgrenciBurslari ob WHERE ob.OgrenciID = o.[{ogrenciIDKolonu}])
                                     ORDER BY o.[{ogrenciIDKolonu}] ASC";
                            break;
                        default: // "Tüm Öğrenciler"
                            string orderByClause = ogrenciBurslariIDVar ? "BaslangicTarihi DESC, ID DESC" : "BaslangicTarihi DESC";
                            sorgu = $@"SELECT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                  o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                  ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect}{aiPotansiyelSelect},
                                  CASE 
                                      WHEN ob.Durum IS NULL THEN -1
                                      WHEN ob.Durum = 1 THEN 1
                                      WHEN ob.Durum = 0 THEN 0
                                      WHEN ob.Durum = 2 THEN 2
                                      ELSE -1
                                  END AS [Durum],
                                  CASE 
                                      WHEN ob.Durum IS NULL THEN 'Reddedildi'
                                      WHEN ob.Durum = 1 THEN 'Kabul Edildi'
                                      WHEN ob.Durum = 0 THEN 'Beklemede'
                                      WHEN ob.Durum = 2 THEN 'Yedek'
                                      ELSE 'Reddedildi'
                                  END AS [Durum Metni]
                                  FROM Ogrenciler o
                                  LEFT JOIN (
                                      SELECT OgrenciID, Durum, 
                                             ROW_NUMBER() OVER (PARTITION BY OgrenciID ORDER BY {orderByClause}) as rn
                                      FROM OgrenciBurslari
                                  ) ob ON o.[{ogrenciIDKolonu}] = ob.OgrenciID AND ob.rn = 1
                                  ORDER BY o.[{ogrenciIDKolonu}] ASC";
                            break;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }

                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;

                // Durum kolonunu en sağa taşı ve görünür yap
                if (gridView1.Columns["Durum Metni"] != null)
                {
                    gridView1.Columns["Durum Metni"].VisibleIndex = gridView1.Columns.Count - 1;
                    gridView1.Columns["Durum Metni"].Caption = "Durum";
                    gridView1.Columns["Durum Metni"].Width = 120;
                }
                if (gridView1.Columns["Durum"] != null)
                {
                    gridView1.Columns["Durum"].Visible = false; // Sadece renklendirme için kullanılacak
                }

                // AIPotansiyelNotu sütununun formatını 2 haneli sayı olacak şekilde ayarla
                var colAIPot = gridView1.Columns["AIPotansiyelNotu"];
                if (colAIPot != null)
                {
                    colAIPot.DisplayFormat.FormatType = FormatType.Numeric;
                    colAIPot.DisplayFormat.FormatString = "n2"; // 2 ondalık hane
                }

                gridView1.BestFitColumns();
                ApplyDarkGrid(gridView1);
                
                // Satır renklendirmesi için event handler ekle (ApplyDarkGrid'den sonra)
                gridView1.RowStyle -= GridView1_RowStyle;
                gridView1.RowStyle += GridView1_RowStyle;
                
                // NOT: Seçili öğrenciyi geri yükleme işlemi Listele() içinden kaldırıldı
                // Çünkü FocusedRowChanged içinden Listele() çağrılırsa sonsuz döngüye neden olabilir
                // Odak geri yükleme sadece btnTahmin_Click ve BtnAIAnaliz_Click içinde yapılmalı
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        // Seçili öğrenciyi ID'ye göre bul ve tekrar seç
        private void RestoreSelectedOgrenci(int ogrenciID)
        {
            try
            {
                if (gridView1 == null) return;
                
                // LocateByValue ile ID'ye göre satırı bul (field name string olarak)
                int foundHandle = gridView1.LocateByValue("ID", ogrenciID);
                
                if (foundHandle >= 0)
                {
                    // Satırı seçili yap (bu otomatik olarak FocusedRowChanged event'ini tetikler)
                    gridView1.FocusedRowHandle = foundHandle;
                    
                    // Grid'i görünür alana kaydır
                    gridView1.MakeRowVisible(foundHandle);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Seçili öğrenci geri yüklenirken hata: {ex.Message}");
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
                        // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                        string ogrenciIDKolonu = "ID";
                        try
                        {
                            SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                                FROM INFORMATION_SCHEMA.COLUMNS 
                                WHERE TABLE_NAME = 'Ogrenciler' 
                                AND COLUMN_NAME IN ('ID', 'OgrenciID')
                                ORDER BY CASE COLUMN_NAME 
                                    WHEN 'ID' THEN 1 
                                    WHEN 'OgrenciID' THEN 2 
                                    ELSE 3 END", conn);
                            var kolonResult = cmdKolon.ExecuteScalar();
                            if (kolonResult != null && kolonResult != DBNull.Value)
                                ogrenciIDKolonu = kolonResult.ToString();
                        }
                        catch { }

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
                                    SqlCommand cmdOgr = new SqlCommand($"DELETE FROM Ogrenciler WHERE [{ogrenciIDKolonu}]=@p1", conn, transaction);
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
                    // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                    string ogrenciIDKolonu = "ID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('ID', 'OgrenciID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'ID' THEN 1 
                                WHEN 'OgrenciID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                        {
                            string kolonStr = kolonResult.ToString();
                            if (!string.IsNullOrEmpty(kolonStr))
                                ogrenciIDKolonu = kolonStr;
                        }
                    }
                    catch { }

                    SqlCommand cmd = new SqlCommand($@"SELECT AD, SOYAD, [TOPLAM HANE GELİRİ], FOTO, TELEFON, 
                                                      BÖLÜMÜ, SINIF, [KARDEŞ SAYISI], AGNO, AISkor, AINotu
                                                      FROM Ogrenciler WHERE [{ogrenciIDKolonu}] = @id", conn);
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

            // Öğrencinin ek bilgilerini çek - ÖNCE GRID'DEN, SONRA VERİTABANINDAN
            string motivasyon = "";
            string ihtiyac = "";
            string hedefler = "";
            string kullanim = "";
            string fark = "";
            string aiPotansiyelNotu = "";
            string aiPotansiyelYuzde = "";
            
            // NOT: Veritabanında Ihtiyac, Hedefler, BursKullanim, FarkliOzellik kolonları yok
            // Bu yüzden bu veriler AINotu içinde saklanmış olabilir veya hiç kaydedilmemiş olabilir
            
            // 1. ÖNCELİK: Grid'den veri okumaya çalış (ekranda görünen veriler)
            try
            {
                // Grid'deki kolon isimlerini kontrol et (farklı varyasyonlar olabilir)
                var gridColumns = dr.Table.Columns;
                
                // Hedefler için farklı kolon isimlerini dene
                if (gridColumns.Contains("Hedefler"))
                    hedefler = dr["Hedefler"]?.ToString() ?? "";
                else if (gridColumns.Contains("HEDEFLER"))
                    hedefler = dr["HEDEFLER"]?.ToString() ?? "";
                
                // Ihtiyac için
                if (gridColumns.Contains("Ihtiyac"))
                    ihtiyac = dr["Ihtiyac"]?.ToString() ?? "";
                else if (gridColumns.Contains("İhtiyaç"))
                    ihtiyac = dr["İhtiyaç"]?.ToString() ?? "";
                
                // BursKullanim için - tüm varyasyonları kontrol et
                if (gridColumns.Contains("BursKullanim"))
                    kullanim = dr["BursKullanim"]?.ToString() ?? "";
                else if (gridColumns.Contains("Burs Kullanım"))
                    kullanim = dr["Burs Kullanım"]?.ToString() ?? "";
                else if (gridColumns.Contains("Kullanim"))
                    kullanim = dr["Kullanim"]?.ToString() ?? "";
                else if (gridColumns.Contains("Kullanım"))
                    kullanim = dr["Kullanım"]?.ToString() ?? "";
                
                // FarkliOzellik için - tüm varyasyonları kontrol et
                if (gridColumns.Contains("FarkliOzellik"))
                    fark = dr["FarkliOzellik"]?.ToString() ?? "";
                else if (gridColumns.Contains("Farklı Özellik"))
                    fark = dr["Farklı Özellik"]?.ToString() ?? "";
                else if (gridColumns.Contains("Fark"))
                    fark = dr["Fark"]?.ToString() ?? "";
                
                // AIPotansiyelNotu ve AIPotansiyelYuzde grid'den
                if (gridColumns.Contains("AIPotansiyelNotu") && dr["AIPotansiyelNotu"] != DBNull.Value)
                    aiPotansiyelNotu = dr["AIPotansiyelNotu"]?.ToString() ?? "";
                if (gridColumns.Contains("AIPotansiyelYuzde") && dr["AIPotansiyelYuzde"] != DBNull.Value)
                    aiPotansiyelYuzde = dr["AIPotansiyelYuzde"]?.ToString() ?? "";
            }
            catch (Exception exGrid)
            {
                System.Diagnostics.Debug.WriteLine($"[GRID OKUMA] Grid'den veri okunurken hata: {exGrid.Message}");
            }
            
            // 2. FALLBACK: Grid'de bulunamayan verileri veritabanından çek
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Önce kolonları kontrol et - TÜM olası kolon isimlerini kontrol et
                    SqlCommand cmdCheck = new SqlCommand(@"SELECT COLUMN_NAME 
                        FROM INFORMATION_SCHEMA.COLUMNS 
                        WHERE TABLE_NAME = 'Ogrenciler' 
                        AND (COLUMN_NAME IN ('Motivasyon', 'Ihtiyac', 'Hedefler', 'BursKullanim', 'FarkliOzellik', 'AINotu', 'AIPotansiyelNotu', 'AIPotansiyelYuzde')
                             OR COLUMN_NAME LIKE '%Kullanim%'
                             OR COLUMN_NAME LIKE '%Kullanım%'
                             OR COLUMN_NAME LIKE '%Fark%'
                             OR COLUMN_NAME LIKE '%Ozellik%'
                             OR COLUMN_NAME LIKE '%İhtiyaç%'
                             OR COLUMN_NAME LIKE '%Hedef%')", conn);
                    
                    // Eksik kolonları otomatik oluştur
                    try
                    {
                        SqlCommand cmdCreateCols = new SqlCommand(@"
                            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'Ihtiyac')
                                ALTER TABLE Ogrenciler ADD Ihtiyac NVARCHAR(MAX) NULL;
                            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'Hedefler')
                                ALTER TABLE Ogrenciler ADD Hedefler NVARCHAR(MAX) NULL;
                            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'BursKullanim')
                                ALTER TABLE Ogrenciler ADD BursKullanim NVARCHAR(MAX) NULL;
                            IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'FarkliOzellik')
                                ALTER TABLE Ogrenciler ADD FarkliOzellik NVARCHAR(MAX) NULL;", conn);
                        cmdCreateCols.ExecuteNonQuery();
                        System.Diagnostics.Debug.WriteLine("[VERİTABANI] Eksik kolonlar oluşturuldu (varsa)");
                    }
                    catch (Exception exCreate)
                    {
                        System.Diagnostics.Debug.WriteLine($"[VERİTABANI KOLON OLUŞTURMA] Hata: {exCreate.Message}");
                    }
                    var kolonlar = new List<string>();
                    using (var reader = cmdCheck.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader[0] != null && reader[0] != DBNull.Value)
                            {
                                string kolonAdi = reader[0].ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(kolonAdi))
                                {
                                    kolonlar.Add(kolonAdi);
                                }
                            }
                        }
                    }
                    
                    // Debug: Bulunan kolonları yazdır
                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI KOLONLAR] Bulunan kolonlar: {string.Join(", ", kolonlar)}");

                    // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                    string ogrenciIDKolonu = "ID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('ID', 'OgrenciID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'ID' THEN 1 
                                WHEN 'OgrenciID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                        {
                            string kolonStr = kolonResult.ToString();
                            if (!string.IsNullOrEmpty(kolonStr))
                                ogrenciIDKolonu = kolonStr;
                        }
                    }
                    catch { }

                    // Kullanım kolonunu bul (farklı isimlerle olabilir)
                    string kullanimKolonu = null;
                    foreach (string kolon in kolonlar)
                    {
                        if (kolon.Equals("BursKullanim", StringComparison.OrdinalIgnoreCase) ||
                            kolon.Contains("Kullanim") || kolon.Contains("Kullanım"))
                        {
                            kullanimKolonu = kolon;
                            break;
                        }
                    }
                    
                    // Fark kolonunu bul
                    string farkKolonu = null;
                    foreach (string kolon in kolonlar)
                    {
                        if (kolon.Equals("FarkliOzellik", StringComparison.OrdinalIgnoreCase) ||
                            kolon.Contains("Fark") || kolon.Contains("Ozellik"))
                        {
                            farkKolonu = kolon;
                            break;
                        }
                    }
                    
                    // Mevcut kolonları kullanarak sorgu oluştur
                    string selectColumns = $"[{ogrenciIDKolonu}]";
                    if (kolonlar.Contains("Motivasyon")) selectColumns += ", ISNULL(Motivasyon, '') as Motivasyon";
                    if (kolonlar.Contains("Ihtiyac")) selectColumns += ", ISNULL(Ihtiyac, '') as Ihtiyac";
                    if (kolonlar.Contains("Hedefler")) selectColumns += ", ISNULL(Hedefler, '') as Hedefler";
                    if (!string.IsNullOrEmpty(kullanimKolonu)) selectColumns += $", ISNULL([{kullanimKolonu}], '') as Kullanim";
                    if (!string.IsNullOrEmpty(farkKolonu)) selectColumns += $", ISNULL([{farkKolonu}], '') as Fark";
                    if (kolonlar.Contains("AINotu")) selectColumns += ", ISNULL(AINotu, '') as AINotu";
                    if (kolonlar.Contains("AIPotansiyelNotu")) selectColumns += ", ISNULL(AIPotansiyelNotu, 0) as AIPotansiyelNotu";
                    if (kolonlar.Contains("AIPotansiyelYuzde")) selectColumns += ", ISNULL(AIPotansiyelYuzde, '') as AIPotansiyelYuzde";

                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI SORGUSU] Kullanim kolonu: '{kullanimKolonu}', Fark kolonu: '{farkKolonu}'");
                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI SORGUSU] SELECT: {selectColumns}");

                    SqlCommand cmd = new SqlCommand($"SELECT {selectColumns} FROM Ogrenciler WHERE [{ogrenciIDKolonu}] = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // ÖNCE veritabanından oku, SONRA grid'deki verilerle override et (eğer grid'de varsa)
                            if (kolonlar.Contains("Motivasyon"))
                            {
                                string dbMotivasyon = reader["Motivasyon"]?.ToString() ?? "";
                                if (!string.IsNullOrWhiteSpace(dbMotivasyon))
                                    motivasyon = dbMotivasyon;
                            }
                            if (kolonlar.Contains("Ihtiyac"))
                            {
                                string dbIhtiyac = reader["Ihtiyac"]?.ToString() ?? "";
                                if (!string.IsNullOrWhiteSpace(dbIhtiyac))
                                    ihtiyac = dbIhtiyac;
                            }
                            if (kolonlar.Contains("Hedefler"))
                            {
                                string dbHedefler = reader["Hedefler"]?.ToString() ?? "";
                                if (!string.IsNullOrWhiteSpace(dbHedefler))
                                    hedefler = dbHedefler;
                            }
                            if (!string.IsNullOrEmpty(kullanimKolonu))
                            {
                                try
                                {
                                    string dbKullanim = reader["Kullanim"]?.ToString() ?? "";
                                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI OKUMA] Kullanim veritabanından: '{dbKullanim}' (uzunluk: {dbKullanim?.Length ?? 0})");
                                    if (!string.IsNullOrWhiteSpace(dbKullanim))
                                        kullanim = dbKullanim;
                                }
                                catch (Exception exKullanim)
                                {
                                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI OKUMA HATASI] Kullanim okunamadı: {exKullanim.Message}");
                                }
                            }
                            if (!string.IsNullOrEmpty(farkKolonu))
                            {
                                try
                                {
                                    string dbFark = reader["Fark"]?.ToString() ?? "";
                                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI OKUMA] Fark veritabanından: '{dbFark}' (uzunluk: {dbFark?.Length ?? 0})");
                                    if (!string.IsNullOrWhiteSpace(dbFark))
                                        fark = dbFark;
                                }
                                catch (Exception exFark)
                                {
                                    System.Diagnostics.Debug.WriteLine($"[VERİTABANI OKUMA HATASI] Fark okunamadı: {exFark.Message}");
                                }
                            }
                            
                            // AINotu varsa ve içinde başvuru cevapları varsa parse et
                            // NOT: Veritabanında Ihtiyac, Hedefler, BursKullanim, FarkliOzellik kolonları yok
                            // Bu yüzden bu veriler başka bir yerde saklanmış olabilir veya hiç kaydedilmemiş
                            if (kolonlar.Contains("AINotu"))
                            {
                                string aiNotu = reader["AINotu"]?.ToString() ?? "";
                                System.Diagnostics.Debug.WriteLine($"[AINOTU OKUMA] AINotu uzunluk: {aiNotu?.Length ?? 0}, içerik başlangıcı: {(aiNotu?.Length > 100 ? aiNotu.Substring(0, 100) : aiNotu)}");
                                
                                // Eğer AINotu içinde başvuru cevapları varsa (eski format), parse et
                                // Ama sadece gerçek başvuru cevapları varsa, AI analiz sonucu değilse
                                if (!string.IsNullOrWhiteSpace(aiNotu) && !aiNotu.Contains("SKOR:") && !aiNotu.Contains("ANALİZ:"))
                                {
                                    // Eski format: [İHTİYAÇ], [HEDEFLER], [KULLANIM], [FARK] içeren metin
                                    try
                                    {
                                        // [İHTİYAÇ] ve [HEDEFLER] arasındaki metni al
                                        int ihtiyacStart = aiNotu.IndexOf("[İHTİYAÇ]");
                                        int hedeflerStart = aiNotu.IndexOf("[HEDEFLER]");
                                        if (ihtiyacStart >= 0 && hedeflerStart > ihtiyacStart)
                                        {
                                            string ihtiyacMetni = aiNotu.Substring(ihtiyacStart + 10, hedeflerStart - ihtiyacStart - 10).Trim();
                                            if (string.IsNullOrWhiteSpace(ihtiyac) && !string.IsNullOrWhiteSpace(ihtiyacMetni) && !ihtiyacMetni.Contains("SKOR:"))
                                                ihtiyac = ihtiyacMetni;
                                        }
                                        
                                        // [HEDEFLER] ve [KULLANIM] arasındaki metni al
                                        int kullanimStart = aiNotu.IndexOf("[KULLANIM]");
                                        if (hedeflerStart >= 0 && kullanimStart > hedeflerStart)
                                        {
                                            string hedeflerMetni = aiNotu.Substring(hedeflerStart + 11, kullanimStart - hedeflerStart - 11).Trim();
                                            if (string.IsNullOrWhiteSpace(hedefler) && !string.IsNullOrWhiteSpace(hedeflerMetni) && !hedeflerMetni.Contains("SKOR:"))
                                                hedefler = hedeflerMetni;
                                        }
                                        
                                        // [KULLANIM] ve [FARK] arasındaki metni al
                                        int farkStart = aiNotu.IndexOf("[FARK]");
                                        if (kullanimStart >= 0 && farkStart > kullanimStart)
                                        {
                                            string kullanimMetni = aiNotu.Substring(kullanimStart + 11, farkStart - kullanimStart - 11).Trim();
                                            if (string.IsNullOrWhiteSpace(kullanim) && !string.IsNullOrWhiteSpace(kullanimMetni) && !kullanimMetni.Contains("SKOR:"))
                                                kullanim = kullanimMetni;
                                        }
                                        
                                        // [FARK] sonrası metni al
                                        if (farkStart >= 0)
                                        {
                                            string farkMetni = aiNotu.Substring(farkStart + 7).Trim();
                                            // SKOR: veya ANALİZ: gibi kelimelerden önceki kısmı al
                                            int skorIndex = farkMetni.IndexOf("SKOR:");
                                            int analizIndex = farkMetni.IndexOf("ANALİZ:");
                                            int minIndex = Math.Min(skorIndex >= 0 ? skorIndex : int.MaxValue, analizIndex >= 0 ? analizIndex : int.MaxValue);
                                            if (minIndex < int.MaxValue && minIndex > 0)
                                                farkMetni = farkMetni.Substring(0, minIndex).Trim();
                                            
                                            if (string.IsNullOrWhiteSpace(fark) && !string.IsNullOrWhiteSpace(farkMetni) && !farkMetni.Contains("SKOR:"))
                                                fark = farkMetni;
                                        }
                                    }
                                    catch (Exception exParse)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"[AINOTU PARSE] Hata: {exParse.Message}");
                                    }
                                }
                            }
                            
                            // ML.NET tahmin verilerini çek (grid'de yoksa)
                            if (string.IsNullOrWhiteSpace(aiPotansiyelNotu) && kolonlar.Contains("AIPotansiyelNotu"))
                            {
                                object potansiyelNotuObj = reader["AIPotansiyelNotu"];
                                if (potansiyelNotuObj != null && potansiyelNotuObj != DBNull.Value)
                                {
                                    aiPotansiyelNotu = potansiyelNotuObj.ToString();
                                }
                            }
                            if (string.IsNullOrWhiteSpace(aiPotansiyelYuzde) && kolonlar.Contains("AIPotansiyelYuzde"))
                            {
                                aiPotansiyelYuzde = reader["AIPotansiyelYuzde"]?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[VERİTABANI OKUMA] Veritabanından veri okunurken hata: {ex.Message}");
            }
            
            // Debug: Tüm verileri konsola yazdır
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] ========================================");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] OgrenciID: {ogrenciID}, Ad: {ad} {soyad}");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] AGNO: {agno}, Gelir: {gelir} TL, Kardeş: {kardes}");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] Ihtiyac: '{(string.IsNullOrWhiteSpace(ihtiyac) ? "BOŞ" : (ihtiyac.Length > 50 ? ihtiyac.Substring(0, 50) + "..." : ihtiyac))}' (uzunluk: {ihtiyac?.Length ?? 0})");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] Hedefler: '{(string.IsNullOrWhiteSpace(hedefler) ? "BOŞ" : (hedefler.Length > 50 ? hedefler.Substring(0, 50) + "..." : hedefler))}' (uzunluk: {hedefler?.Length ?? 0})");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] Kullanim: '{(string.IsNullOrWhiteSpace(kullanim) ? "BOŞ" : (kullanim.Length > 50 ? kullanim.Substring(0, 50) + "..." : kullanim))}' (uzunluk: {kullanim?.Length ?? 0})");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] Fark: '{(string.IsNullOrWhiteSpace(fark) ? "BOŞ" : (fark.Length > 50 ? fark.Substring(0, 50) + "..." : fark))}' (uzunluk: {fark?.Length ?? 0})");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] Motivasyon: '{(string.IsNullOrWhiteSpace(motivasyon) ? "BOŞ" : (motivasyon.Length > 50 ? motivasyon.Substring(0, 50) + "..." : motivasyon))}' (uzunluk: {motivasyon?.Length ?? 0})");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] AIPotansiyelNotu: '{aiPotansiyelNotu}', AIPotansiyelYuzde: '{aiPotansiyelYuzde}'");
            System.Diagnostics.Debug.WriteLine($"[AI ANALİZ VERİ] ========================================");

            // Kişi başı gelir hesapla (AI için önemli)
            decimal haneGeliriDecimal = 0;
            int kardesSayisiInt = 0;
            decimal.TryParse(gelir, out haneGeliriDecimal);
            int.TryParse(kardes, out kardesSayisiInt);
            decimal kisiBasiGelir = (kardesSayisiInt + 2) > 0 ? haneGeliriDecimal / (kardesSayisiInt + 2) : 0;
            
            // AGNO'yu float'a çevir
            float agnoFloat = 0;
            float.TryParse(agno, out agnoFloat);
            
            // AI için detaylı ve benzersiz veri hazırla
            string ogrenciVerisi = $"═══════════════════════════════════════\n";
            ogrenciVerisi += $"ÖĞRENCİ BİLGİLERİ (ID: {ogrenciID})\n";
            ogrenciVerisi += $"═══════════════════════════════════════\n";
            ogrenciVerisi += $"Ad Soyad: {ad} {soyad}\n";
            ogrenciVerisi += $"Bölüm: {bolum}\n";
            ogrenciVerisi += $"Sınıf: {sinif}\n";
            ogrenciVerisi += $"AGNO: {agnoFloat:F2} / 4.00\n";
            ogrenciVerisi += $"Hane Geliri: {haneGeliriDecimal:N0} TL\n";
            ogrenciVerisi += $"Kardeş Sayısı: {kardesSayisiInt}\n";
            ogrenciVerisi += $"Kişi Başı Gelir: {kisiBasiGelir:N0} TL (Hane Geliri / (Kardeş + 2))\n";
            ogrenciVerisi += $"═══════════════════════════════════════\n\n";
            
            // ML.NET tahmin verilerini ekle (varsa)
            if (!string.IsNullOrWhiteSpace(aiPotansiyelNotu) && float.TryParse(aiPotansiyelNotu, out float potansiyelNotu))
            {
                ogrenciVerisi += $"🤖 ML.NET MEZUNİYET TAHMİNİ:\n";
                ogrenciVerisi += $"   Tahmini Mezuniyet Puanı: {potansiyelNotu:F2} / 4.00\n";
                if (agnoFloat > 0)
                {
                    float artis = potansiyelNotu - agnoFloat;
                    float artisYuzde = (artis / agnoFloat) * 100;
                    ogrenciVerisi += $"   Mevcut AGNO'dan Fark: {artis:+#0.00;-#0.00} ({artisYuzde:+#0.0;-#0.0}%)\n";
                }
                if (!string.IsNullOrWhiteSpace(aiPotansiyelYuzde))
                {
                    ogrenciVerisi += $"   Öngörülen Artış: {aiPotansiyelYuzde}\n";
                }
                ogrenciVerisi += $"\n";
            }
            
            ogrenciVerisi += $"═══════════════════════════════════════\n";
            ogrenciVerisi += $"BAŞVURU CEVAPLARI\n";
            ogrenciVerisi += $"═══════════════════════════════════════\n\n";

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

                // AI Skorunu ve AINotu'yu kaydet
                using (SqlConnection conn = bgl.baglanti())
                {
                    // AISkor ve AINotu kolonlarının varlığını kontrol et
                    bool hasAISkor = false;
                    bool hasAINotu = false;
                    try
                    {
                        SqlCommand checkCmd = new SqlCommand(@"SELECT COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('AISkor', 'AINotu')", conn);
                        using (var reader = checkCmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string kolonAdi = reader[0]?.ToString() ?? "";
                                if (kolonAdi == "AISkor") hasAISkor = true;
                                if (kolonAdi == "AINotu") hasAINotu = true;
                            }
                        }
                    }
                    catch { }
                    
                    // Kolonları otomatik oluştur (yoksa)
                    if (!hasAISkor)
                    {
                        try
                        {
                            SqlCommand alterCmd = new SqlCommand("ALTER TABLE Ogrenciler ADD AISkor INT NULL", conn);
                            alterCmd.ExecuteNonQuery();
                            hasAISkor = true;
                        }
                        catch { }
                    }
                    if (!hasAINotu)
                    {
                        try
                        {
                            SqlCommand alterCmd = new SqlCommand("ALTER TABLE Ogrenciler ADD AINotu NVARCHAR(MAX) NULL", conn);
                            alterCmd.ExecuteNonQuery();
                            hasAINotu = true;
                        }
                        catch { }
                    }
                    
                    // UPDATE sorgusu
                    string updateQuery = "";
                    if (hasAISkor && hasAINotu)
                        updateQuery = "UPDATE Ogrenciler SET AISkor = @skor, AINotu = @notu WHERE ID = @id";
                    else if (hasAISkor)
                        updateQuery = "UPDATE Ogrenciler SET AISkor = @skor WHERE ID = @id";
                    else if (hasAINotu)
                        updateQuery = "UPDATE Ogrenciler SET AINotu = @notu WHERE ID = @id";
                    
                    if (!string.IsNullOrEmpty(updateQuery))
                    {
                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        if (hasAISkor)
                            cmd.Parameters.AddWithValue("@skor", aiSkor);
                        if (hasAINotu)
                            cmd.Parameters.AddWithValue("@notu", aiNotu);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        cmd.ExecuteNonQuery();
                    }
                }

                Color renk = GetAISkorColor(aiSkor);

                try
                {
                    if (memoAIsonuc != null)
                    {
                        // Formatlanmış raporu oluştur
                        string formattedReport = FormatAIReport(aiNotu, aiSkor, ad, soyad);

                        // MemoEdit'e Bas
                        memoAIsonuc.EditValue = formattedReport;
                        memoAIsonuc.Properties.Appearance.ForeColor = renk;
                        memoAIsonuc.Properties.Appearance.Font = new Font("Segoe UI Semibold", 10.5F);
                    }

                    // Filtreleme ve bildirim işlemleri - Seçili öğrenciyi koru
                    string filtreTipi = cmbFiltre?.SelectedIndex >= 0 
                        ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() 
                        : "Tüm Öğrenciler";
                    
                    // Listele() çağrısından sonra seçili öğrenciyi geri yükle
                    Listele(filtreTipi);
                    RestoreSelectedOgrenci(ogrenciID);
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

        // AI Analiz Raporunu Formatla - Ortak metod
        private string FormatAIReport(string rawText, int skor, string ad, string soyad)
        {
            try
            {
                // 1. Ayraç Çizgileri
                string thickLine = new string('━', 45); // Kalın ana ayraç
                string thinLine = new string('─', 45);  // İnce alt ayraç
                string n = Environment.NewLine;         // Yeni satır kısayolu

                // 2. Ham metni temizle
                // SKOR satırını kaldır (zaten başlıkta gösteriyoruz)
                string temizMetin = System.Text.RegularExpressions.Regex.Replace(
                    rawText, 
                    @"SKOR:\s*\d+.*?(\r?\n|$)", 
                    "", 
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);

                // Başlıkları formatla - Metinlerin çizgiye yapışmaması için her başlıktan sonra 2 satır atlıyoruz.
                temizMetin = temizMetin
                    .Replace("ANALİZ:", $"{n}🔍  ANALİZ{n}{thinLine}{n}")
                    .Replace("KİŞİLİK:", $"{n}{n}👤  KİŞİLİK ÖZETİ{n}{thinLine}{n}")
                    .Replace("KARAR:", $"{n}{n}📌  SONUÇ VE KARAR{n}{thinLine}{n}");

                // 3. Uygunluk durumu
                string uygunluk = skor >= 70 ? "UYGUN" : skor >= 40 ? "DEĞERLENDIR" : "UYGUN DEĞİL";

                // 4. Raporu İnşa Et
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"         📋 AI DEĞERLENDİRME RAPORU");
                sb.AppendLine(thickLine);
                sb.AppendLine($"👤 Öğrenci  : {ad.ToUpper()} {soyad.ToUpper()}");
                sb.AppendLine($"🎯 Puanlama : {skor} / 100");
                sb.AppendLine($"📢 Durum    : {(uygunluk.ToUpper().Contains("UYGUN") ? "✅ " : "❌ ")}{uygunluk.ToUpper()}");
                sb.AppendLine(thickLine);
                sb.AppendLine(temizMetin);

                return sb.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FormatAIReport hatası: {ex.Message}");
                return rawText; // Hata durumunda ham metni döndür
            }
        }

        // Skora göre renk döndür
        private Color GetAISkorColor(int skor)
        {
            if (skor >= 70)
                return Color.FromArgb(39, 174, 96); // Yeşil
            else if (skor >= 40)
                return Color.FromArgb(243, 156, 18); // Turuncu
            else
                return Color.FromArgb(231, 76, 60); // Kırmızı
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
                frm.StartPosition = FormStartPosition.CenterScreen;
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
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
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
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
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
                            {
                                string kolonStr = kolonResult.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(kolonStr))
                                    bursIDKolonu = kolonStr;
                            }
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

                        // Durum = 2 (Yedek)
                        // Önce Durum kolonunun tipini kontrol et
                        string durumKolonTipi = "INT";
                        try
                        {
                            SqlCommand cmdTip = new SqlCommand(@"SELECT DATA_TYPE 
                                FROM INFORMATION_SCHEMA.COLUMNS 
                                WHERE TABLE_NAME = 'OgrenciBurslari' AND COLUMN_NAME = 'Durum'", conn);
                            var tipResult = cmdTip.ExecuteScalar();
                            if (tipResult != null && tipResult != DBNull.Value)
                            {
                                string tipStr = tipResult.ToString() ?? string.Empty;
                                if (!string.IsNullOrEmpty(tipStr))
                                    durumKolonTipi = tipStr.ToUpper();
                            }
                        }
                        catch { }
                        
                        System.Diagnostics.Debug.WriteLine($"Durum kolonu tipi: {durumKolonTipi}");
                        
                        // Önce mevcut durumu kontrol et
                        SqlCommand cmdCheckDurum = new SqlCommand("SELECT TOP 1 Durum FROM OgrenciBurslari WHERE OgrenciID = @id ORDER BY BaslangicTarihi DESC", conn);
                        cmdCheckDurum.Parameters.AddWithValue("@id", ogrenciID);
                        object mevcutDurumObj = cmdCheckDurum.ExecuteScalar();
                        int mevcutDurum = -1;
                        if (mevcutDurumObj != null && mevcutDurumObj != DBNull.Value)
                        {
                            if (mevcutDurumObj is bool)
                                mevcutDurum = ((bool)mevcutDurumObj) ? 1 : 0;
                            else
                                mevcutDurum = Convert.ToInt32(mevcutDurumObj);
                        }
                        System.Diagnostics.Debug.WriteLine($"Mevcut durum (OgrenciID: {ogrenciID}): {mevcutDurum} (tip: {mevcutDurumObj?.GetType().Name})");
                        
                        SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM OgrenciBurslari WHERE OgrenciID = @id", conn);
                        cmdCheck.Parameters.AddWithValue("@id", ogrenciID);
                        int exists = Convert.ToInt32(cmdCheck.ExecuteScalar());
                        System.Diagnostics.Debug.WriteLine($"OgrenciBurslari kayıt sayısı (OgrenciID: {ogrenciID}): {exists}");

                        if (exists > 0)
                        {
                            // Eğer Durum kolonu BIT ise, önce INT'e çevirmemiz gerekebilir
                            if (durumKolonTipi == "BIT")
                            {
                                // BIT kolonu sadece 0 ve 1 alabilir, bu yüzden önce kolonu INT'e çevirmemiz gerekir
                                try
                                {
                                    // Önce mevcut verileri yedekle (gerekirse)
                                    // ALTER TABLE komutunu çalıştır
                                    SqlCommand cmdAlter = new SqlCommand("ALTER TABLE OgrenciBurslari ALTER COLUMN Durum INT", conn);
                                    cmdAlter.ExecuteNonQuery();
                                    System.Diagnostics.Debug.WriteLine("Durum kolonu BIT'ten INT'e çevrildi");
                                    
                                    // Tip değişikliğini doğrula
                                    SqlCommand cmdVerify = new SqlCommand(@"SELECT DATA_TYPE 
                                        FROM INFORMATION_SCHEMA.COLUMNS 
                                        WHERE TABLE_NAME = 'OgrenciBurslari' AND COLUMN_NAME = 'Durum'", conn);
                                    var verifyResult = cmdVerify.ExecuteScalar();
                                    if (verifyResult != null && verifyResult != DBNull.Value)
                                    {
                                        string verifyStr = verifyResult.ToString();
                                        if (!string.IsNullOrEmpty(verifyStr) && verifyStr.ToUpper() == "INT")
                                        {
                                            durumKolonTipi = "INT";
                                            System.Diagnostics.Debug.WriteLine("Durum kolonu tipi doğrulandı: INT");
                                        }
                                        else
                                        {
                                            throw new Exception($"Durum kolonu tipi hala {verifyStr ?? "NULL"} olarak görünüyor. INT'e çevrilemedi.");
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("Durum kolonu tipi doğrulanamadı. INT'e çevrilemedi.");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine($"Durum kolonu INT'e çevrilemedi: {ex.Message}");
                                    string hataMesaji = $"Durum kolonu BIT tipinde olduğu için yedek durumu (2) kaydedilemiyor.\n\n" +
                                        $"Lütfen SQL Server Management Studio'da şu komutu çalıştırın:\n\n" +
                                        $"ALTER TABLE OgrenciBurslari ALTER COLUMN Durum INT\n\n" +
                                        $"Bu komut çalıştıktan sonra tekrar deneyin.\n\n" +
                                        $"Hata: {ex.Message}";
                                    throw new Exception(hataMesaji);
                                }
                            }
                            
                            // Durum = 2 (Yedek) olarak güncelle
                            string updateQuery = "UPDATE OgrenciBurslari SET Durum = @durum WHERE OgrenciID = @id";
                            SqlCommand cmd = new SqlCommand(updateQuery, conn);
                            cmd.Parameters.AddWithValue("@id", ogrenciID);
                            cmd.Parameters.AddWithValue("@durum", 2);
                            int affected = cmd.ExecuteNonQuery();
                            
                            // Debug: Kaç kayıt güncellendi
                            System.Diagnostics.Debug.WriteLine($"Yedek listesine alındı: {affected} kayıt güncellendi (OgrenciID: {ogrenciID})");
                            
                            // Güncelleme sonrası durumu kontrol et
                            SqlCommand cmdCheckDurum2 = new SqlCommand("SELECT TOP 1 Durum FROM OgrenciBurslari WHERE OgrenciID = @id ORDER BY BaslangicTarihi DESC", conn);
                            cmdCheckDurum2.Parameters.AddWithValue("@id", ogrenciID);
                            object yeniDurumObj = cmdCheckDurum2.ExecuteScalar();
                            int yeniDurum = -1;
                            if (yeniDurumObj != null && yeniDurumObj != DBNull.Value)
                            {
                                if (yeniDurumObj is bool)
                                    yeniDurum = ((bool)yeniDurumObj) ? 1 : 0;
                                else
                                    yeniDurum = Convert.ToInt32(yeniDurumObj);
                            }
                            System.Diagnostics.Debug.WriteLine($"Güncelleme sonrası durum (OgrenciID: {ogrenciID}): {yeniDurum} (tip: {yeniDurumObj?.GetType().Name})");
                            
                            if (affected == 0)
                            {
                                throw new Exception("Kayıt güncellenemedi. Lütfen tekrar deneyin.");
                            }
                            
                            // Eğer durum hala 2 değilse, hata mesajı göster
                            if (yeniDurum != 2)
                            {
                                string hataMesaji = $"Durum güncellenemedi. Beklenen: 2, Gerçek: {yeniDurum}.\n\n" +
                                    $"Durum kolonu hala BIT tipinde olabilir. Lütfen SQL Server Management Studio'da şu komutu çalıştırın:\n\n" +
                                    $"ALTER TABLE OgrenciBurslari ALTER COLUMN Durum INT\n\n" +
                                    $"Bu komut çalıştıktan sonra tekrar deneyin.";
                                throw new Exception(hataMesaji);
                            }
                        }
                        else
                        {
                            // Kayıt yoksa yeni kayıt ekle
                            if (mevcutBursID > 0)
                            {
                                SqlCommand cmd = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) VALUES (@id, @bursID, @tarih, 2)", conn);
                                cmd.Parameters.AddWithValue("@id", ogrenciID);
                                cmd.Parameters.AddWithValue("@bursID", mevcutBursID);
                                cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                                int inserted = cmd.ExecuteNonQuery();
                                System.Diagnostics.Debug.WriteLine($"Yedek listesine yeni kayıt eklendi: {inserted} kayıt (OgrenciID: {ogrenciID}, BursID: {mevcutBursID})");
                            }
                            else
                            {
                                // Burs yoksa BursID olmadan ekle (eğer tablo izin veriyorsa)
                                try
                                {
                                    SqlCommand cmd = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BaslangicTarihi, Durum) VALUES (@id, @tarih, 2)", conn);
                                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                                    cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                                    int inserted = cmd.ExecuteNonQuery();
                                    System.Diagnostics.Debug.WriteLine($"Yedek listesine yeni kayıt eklendi (BursID olmadan): {inserted} kayıt (OgrenciID: {ogrenciID})");
                                }
                                catch (Exception ex)
                                {
                                    // BursID zorunluysa hata mesajı göster
                                    System.Diagnostics.Debug.WriteLine($"Yedek listesine ekleme hatası: {ex.Message}");
                                    throw new Exception($"Sistemde aktif burs bulunamadı. Lütfen önce bir burs tanımlayın.\n\nHata: {ex.Message}");
                                }
                            }
                        }
                    }

                    MessageHelper.ShowSuccess($"{adSoyad} öğrencisi YEDEK LİSTEYE alındı.", "İşlem Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Yedek Listesi Hatası");
                }
            }
        }

        private void btnAIAnaliz_Click_1(object sender, EventArgs e)
        {
            // Bu metod BtnAIAnaliz_Click tarafından kullanılıyor (async metod)
            // BtnAIAnaliz_Click zaten async olduğu için await kullanmaya gerek yok
            BtnAIAnaliz_Click(sender, e);
        }

        // ML.NET Tahmin - Mezuniyet Puanı Tahmini
        private void btnTahmin_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçili öğrenciyi kontrol et
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr == null)
                {
                    MessageHelper.ShowWarning("Lütfen tahmin yapılacak bir öğrenci seçiniz.", "Seçim Yapılmadı");
                    return;
                }

                // Öğrenci verilerini al
                string agnoStr = dr["AGNO"]?.ToString() ?? "0";
                string gelirStr = dr["Hane Geliri"]?.ToString() ?? "0";
                string kardesStr = dr["Kardeş"]?.ToString() ?? "0";
                string bolum = dr["BÖLÜMÜ"]?.ToString() ?? string.Empty;
                int ogrenciID = Convert.ToInt32(dr["ID"]);

                // String değerleri float'a çevir
                if (!float.TryParse(agnoStr, out float mevcutAgno))
                {
                    MessageHelper.ShowWarning("AGNO değeri geçersiz. Lütfen kontrol edin.", "Veri Hatası");
                    return;
                }

                if (!float.TryParse(gelirStr, out float haneGeliri))
                {
                    MessageHelper.ShowWarning("Hane Geliri değeri geçersiz. Lütfen kontrol edin.", "Veri Hatası");
                    return;
                }

                if (!float.TryParse(kardesStr, out float kardesSayisi))
                {
                    MessageHelper.ShowWarning("Kardeş Sayısı değeri geçersiz. Lütfen kontrol edin.", "Veri Hatası");
                    return;
                }

                // Üniversite bilgisini grid'den veya veritabanından güvenli şekilde al
                string universiteAdi = GetUniversiteAdiSafe(dr, ogrenciID);

                // Şehir maliyeti ve bölüm zorluğunu otomatik eşle
                float sehirMaliyet = MapSehirMaliyet(universiteAdi);
                float bolumZorluk = MapBolumZorluk(bolum);

                // Debug: mapping çıktılarını kontrol et
                Console.WriteLine($"[AI] Universite='{universiteAdi}' Bolum='{bolum}' => SehirMaliyet={sehirMaliyet}, BolumZorluk={bolumZorluk}");

                // ModelInput oluştur
                Bursoto1.BursModel.ModelInput input = new Bursoto1.BursModel.ModelInput
                {
                    MevcutAgno = mevcutAgno,
                    HaneGeliri = haneGeliri,
                    KardesSayisi = kardesSayisi,
                    SehirMaliyet = sehirMaliyet,
                    BolumZorluk = bolumZorluk
                };

                // Tahmin yap
                Bursoto1.BursModel.ModelOutput output = Bursoto1.BursModel.Predict(input);
                float tahminEdilenPuan = output.Score;

                // Sonucu göster (mevcut AGNO ile karşılaştır)
                ShowTahminSonucu(tahminEdilenPuan, mevcutAgno);

                // Tahmini veritabanına kaydet (not ve yüzde ile birlikte)
                UpdateAIPotansiyelNotu(ogrenciID, tahminEdilenPuan, mevcutAgno);
                
                // Grid'i otomatik yenile ve seçili öğrenciyi koru
                string filtreTipi = cmbFiltre?.SelectedIndex >= 0 
                    ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() 
                    : "Tüm Öğrenciler";
                
                // Listele() çağrısından sonra seçili öğrenciyi geri yükle
                Listele(filtreTipi);
                RestoreSelectedOgrenci(ogrenciID);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Tahmin Hatası");
            }
        }

        // Tahmin sonucunu görsel olarak göster
        private void ShowTahminSonucu(float tahminEdilenPuan, float mevcutAgno)
        {
            try
            {
                // ProgressBar'ı güncelle (0-4 arası GPA)
                if (progressBarTahmin != null)
                {
                    // ProgressBar 0-100 arası gösterir, biz 0-4 arası GPA'yi 0-100'e çeviriyoruz
                    int progressValue = (int)((tahminEdilenPuan / 4.0f) * 100);
                    progressBarTahmin.Position = Math.Max(0, Math.Min(100, progressValue));
                    progressBarTahmin.Properties.Maximum = 100;
                    progressBarTahmin.Properties.Minimum = 0;
                }

                // Label'ı güncelle
                if (lblTahminSonuc != null)
                {
                    // Temel metin (3.52 ↑ formatını koru)
                    string text = tahminEdilenPuan.ToString("F2");
                    bool potansiyelArtis = tahminEdilenPuan > mevcutAgno;
                    if (potansiyelArtis)
                    {
                        text += " ↑";
                    }

                    lblTahminSonuc.Text = $"🤖 AI Başarı Projeksiyonu: {text}";

                    // Renklendirme: Artış varsa modern yeşil, düşükse kırmızı
                    if (potansiyelArtis)
                    {
                        // Modern yeşil tonu (güzel ve göze hoş)
                        lblTahminSonuc.Appearance.ForeColor = Color.FromArgb(46, 204, 113); // Modern yeşil
                    }
                    else
                    {
                        // Kırmızı tonu (düşük performans)
                        lblTahminSonuc.Appearance.ForeColor = Color.FromArgb(231, 76, 60); // Kırmızı
                    }
                    lblTahminSonuc.Appearance.Options.UseForeColor = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Tahmin sonucu gösterilirken hata: {ex.Message}");
            }
        }

        // --- Üniversite ve Bölüm için Otomatik Mapping ---

        // Önce grid satırından, yoksa veritabanından üniversite adını al
        private string GetUniversiteAdiSafe(DataRow dr, int ogrenciID)
        {
            try
            {
                // 1) Grid'deki kolonlardan universite bilgisini dene
                if (dr != null && dr.Table != null)
                {
                    // Önce "Üniversite" kolonunu dene (tam eşleşme)
                    if (dr.Table.Columns.Contains("Üniversite"))
                    {
                        var val = dr["Üniversite"];
                        if (val != null && val != DBNull.Value)
                        {
                            string uni = val.ToString();
                            if (!string.IsNullOrWhiteSpace(uni))
                            {
                                Console.WriteLine($"[AI] Grid'den Üniversite kolonu okundu: '{uni}'");
                                return uni;
                            }
                        }
                    }

                    // Sonra "Universite" kolonunu dene (İngilizce karakter)
                    if (dr.Table.Columns.Contains("Universite"))
                    {
                        var val = dr["Universite"];
                        if (val != null && val != DBNull.Value)
                        {
                            string uni = val.ToString();
                            if (!string.IsNullOrWhiteSpace(uni))
                            {
                                Console.WriteLine($"[AI] Grid'den Universite kolonu okundu: '{uni}'");
                                return uni;
                            }
                        }
                    }

                    // Son olarak kolon adı içinde "NIVERSITE" geçen herhangi bir kolonu dene
                    foreach (DataColumn col in dr.Table.Columns)
                    {
                        var colName = col.ColumnName ?? string.Empty;
                        if (colName.ToUpperInvariant().Contains("NIVERSITE"))
                        {
                            var val = dr[col];
                            if (val != null && val != DBNull.Value)
                            {
                                string uni = val.ToString();
                                if (!string.IsNullOrWhiteSpace(uni))
                                {
                                    Console.WriteLine($"[AI] Grid'den '{colName}' kolonu okundu: '{uni}'");
                                    return uni;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Grid'den universite okuma hatası: {ex.Message}");
            }

            // 2) Grid'de yoksa veritabanından çek
            Console.WriteLine($"[AI] Grid'de üniversite bulunamadı, veritabanından çekiliyor (OgrenciID: {ogrenciID})");
            return GetUniversiteAdiFromDb(ogrenciID);
        }

        // Öğrencinin üniversite adını veritabanından al
        private string GetUniversiteAdiFromDb(int ogrenciID)
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                    string ogrenciIDKolonu = "ID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('ID', 'OgrenciID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'ID' THEN 1 
                                WHEN 'OgrenciID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                        {
                            string kolonStr = kolonResult.ToString() ?? string.Empty;
                            if (!string.IsNullOrEmpty(kolonStr))
                                ogrenciIDKolonu = kolonStr;
                        }
                    }
                    catch { }

                    // Üniversite kolonu adını tespit et (Üniversite / Universite / UNIVERSITE ...)
                    string universiteKolon = null;
                    try
                    {
                        SqlCommand cmdUniKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND (COLUMN_NAME LIKE N'%niversite%' OR COLUMN_NAME LIKE N'%NIVERSITE%')", conn);
                        var uniKolonResult = cmdUniKolon.ExecuteScalar();
                        if (uniKolonResult != null && uniKolonResult != DBNull.Value)
                        {
                            string uniKolonStr = uniKolonResult.ToString() ?? string.Empty;
                            if (!string.IsNullOrEmpty(uniKolonStr))
                                universiteKolon = uniKolonStr;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Üniversite kolonu tespit hatası: {ex.Message}");
                    }

                    if (string.IsNullOrWhiteSpace(universiteKolon))
                    {
                        Console.WriteLine("[AI] Veritabanında üniversite kolonu bulunamadı.");
                        return string.Empty;
                    }

                    SqlCommand cmd = new SqlCommand(
                        $"SELECT TOP 1 [{universiteKolon}] FROM Ogrenciler WHERE [{ogrenciIDKolonu}] = @id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    object result = cmd.ExecuteScalar();
                    string uniResult = result?.ToString() ?? string.Empty;
                    Console.WriteLine($"[AI] Veritabanından '{universiteKolon}' kolonu okundu: '{uniResult}'");
                    return uniResult;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Üniversite bilgisi alınırken hata: {ex.Message}");
                return string.Empty;
            }
        }

        // Üniversite adına göre şehir maliyeti mapping
        private float MapSehirMaliyet(string universiteAdi)
        {
            if (string.IsNullOrWhiteSpace(universiteAdi))
                return 0.7f;

            string uni = universiteAdi.ToUpper(new CultureInfo("tr-TR"));

            // İstanbul ve üst seviye özel/vakıf üniversiteleri
            if (uni.Contains("İSTANBUL") || uni.Contains("İTÜ") || uni.Contains("İTU"))
            {
                return 1.0f;
            }

            // Ankara ve benzeri yüksek maliyetli şehir üniversiteleri
            if (uni.Contains("ANKARA") || uni.Contains("ODTÜ"))
            {
                return 0.9f;
            }

            // Düşük maliyet: Yozgat / Bozok
            if (uni.Contains("YOZGAT") || uni.Contains("BOZOK"))
            {
                return 0.5f;
            }

            // Diğer tüm üniversiteler
            return 0.7f;
        }

        // Bölüm adına göre bölüm zorluğu mapping
        private float MapBolumZorluk(string bolumAdi)
        {
            if (string.IsNullOrWhiteSpace(bolumAdi))
                return 2.5f;

            // Türkçe karakter uyumlu normalize et:
            // - 'İ' ve 'ı' karakterlerini 'I' yap
            // - Ardından Turkish culture ile büyük harfe çevir
            string bolum = bolumAdi
                .Replace('İ', 'I')
                .Replace('ı', 'I')
                .ToUpper(new CultureInfo("tr-TR"));

            // En zor bölümler - MÜHENDİS, MUHENDIS, TIP -> 4.8f
            if (bolum.Contains("MÜHENDIS") || bolum.Contains("MUHENDIS") || bolum.Contains("TIP"))
            {
                return 4.8f;
            }

            // HEMŞİRE, HEMSIRE -> 4.5f
            if (bolum.Contains("HEMŞİRE") || bolum.Contains("HEMSIRE"))
            {
                return 4.5f;
            }

            // Zor bölümler
            if (bolum.Contains("HUKUK") || bolum.Contains("MİMAR") ||
                bolum.Contains("FEN") || bolum.Contains("MATEMATİK"))
            {
                return 4.0f;
            }

            // Orta zorluk
            if (bolum.Contains("İŞLETME") || bolum.Contains("İKTİSAT") ||
                bolum.Contains("ÖĞRETMEN"))
            {
                return 3.0f;
            }

            // Daha düşük zorluk - TARİH, TARIH -> 2.0f
            if (bolum.Contains("TARIH") || bolum.Contains("TARİH"))
            {
                return 2.0f;
            }

            // Diğer düşük zorluk bölümler
            if (bolum.Contains("EDEBİYAT") || bolum.Contains("ARKEOLOJİ") || bolum.Contains("SPOR"))
            {
                return 2.0f;
            }

            // Diğerleri için
            return 2.5f;
        }

        // Tahmin edilen puanı Ogrenciler.AIPotansiyelNotu ve AIPotansiyelYuzde kolonlarına kaydet
        private void UpdateAIPotansiyelNotu(int ogrenciID, float tahminEdilenPuan, float mevcutAgno)
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                    string ogrenciIDKolonu = "ID";
                    try
                    {
                        SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' 
                            AND COLUMN_NAME IN ('ID', 'OgrenciID')
                            ORDER BY CASE COLUMN_NAME 
                                WHEN 'ID' THEN 1 
                                WHEN 'OgrenciID' THEN 2 
                                ELSE 3 END", conn);
                        var kolonResult = cmdKolon.ExecuteScalar();
                        if (kolonResult != null && kolonResult != DBNull.Value)
                        {
                            string kolonStr = kolonResult.ToString() ?? string.Empty;
                            if (!string.IsNullOrEmpty(kolonStr))
                                ogrenciIDKolonu = kolonStr;
                        }
                    }
                    catch { }

                    // AIPotansiyelNotu kolonu var mı kontrol et
                    bool hasAIPotansiyelNotu = false;
                    try
                    {
                        SqlCommand cmdCheck = new SqlCommand(@"SELECT COUNT(*) 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'AIPotansiyelNotu'", conn);
                        hasAIPotansiyelNotu = Convert.ToInt32(cmdCheck.ExecuteScalar()) > 0;
                    }
                    catch { }

                    // Kolon yoksa otomatik oluştur
                    if (!hasAIPotansiyelNotu)
                    {
                        try
                        {
                            SqlCommand cmdAlter = new SqlCommand(@"ALTER TABLE Ogrenciler 
                                ADD AIPotansiyelNotu FLOAT NULL", conn);
                            cmdAlter.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine("AIPotansiyelNotu kolonu otomatik olarak oluşturuldu.");
                            hasAIPotansiyelNotu = true;
                        }
                        catch (Exception alterEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"AIPotansiyelNotu kolonu oluşturulamadı: {alterEx.Message}");
                            return;
                        }
                    }

                    // AIPotansiyelYuzde kolonu var mı kontrol et
                    bool hasAIPotansiyelYuzde = false;
                    try
                    {
                        SqlCommand cmdCheckYuzde = new SqlCommand(@"SELECT COUNT(*) 
                            FROM INFORMATION_SCHEMA.COLUMNS 
                            WHERE TABLE_NAME = 'Ogrenciler' AND COLUMN_NAME = 'AIPotansiyelYuzde'", conn);
                        hasAIPotansiyelYuzde = Convert.ToInt32(cmdCheckYuzde.ExecuteScalar()) > 0;
                    }
                    catch { }

                    // Kolon yoksa otomatik oluştur
                    if (!hasAIPotansiyelYuzde)
                    {
                        try
                        {
                            SqlCommand cmdAlterYuzde = new SqlCommand(@"ALTER TABLE Ogrenciler 
                                ADD AIPotansiyelYuzde NVARCHAR(20) NULL", conn);
                            cmdAlterYuzde.ExecuteNonQuery();
                            System.Diagnostics.Debug.WriteLine("AIPotansiyelYuzde kolonu otomatik olarak oluşturuldu.");
                            hasAIPotansiyelYuzde = true;
                        }
                        catch (Exception alterEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"AIPotansiyelYuzde kolonu oluşturulamadı: {alterEx.Message}");
                        }
                    }

                    // Puanı 2 ondalık basamağa yuvarla
                    float yuvarlanmisPuan = (float)Math.Round(tahminEdilenPuan, 2);

                    // Artış yüzdesini hesapla
                    string artisYuzdeStr = string.Empty;
                    if (mevcutAgno > 0)
                    {
                        float artisYuzde = ((tahminEdilenPuan - mevcutAgno) / mevcutAgno) * 100;
                        artisYuzdeStr = $"+%{Math.Round(artisYuzde, 1):F1}";
                    }

                    // UPDATE sorgusu - hem notu hem de yüzdeyi kaydet
                    string updateQuery = $"UPDATE Ogrenciler SET AIPotansiyelNotu = @puan";
                    if (hasAIPotansiyelYuzde)
                    {
                        updateQuery += ", AIPotansiyelYuzde = @yuzde";
                    }
                    updateQuery += $" WHERE [{ogrenciIDKolonu}] = @id";

                    SqlCommand cmd = new SqlCommand(updateQuery, conn);
                    cmd.Parameters.AddWithValue("@puan", yuvarlanmisPuan);
                    if (hasAIPotansiyelYuzde)
                    {
                        cmd.Parameters.AddWithValue("@yuzde", artisYuzdeStr);
                    }
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    cmd.ExecuteNonQuery();

                    // Grid'i yenile
                    if (gridControl1 != null)
                    {
                        gridControl1.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AIPotansiyelNotu güncellenirken hata: {ex.Message}");
            }
        }

        private void memoAIsonuc_EditValueChanged(object sender, EventArgs e)
        {

        }

        // Satır değiştiğinde, daha önce yapılmış AI analizini ve ML tahminini sağ panele yansıt
        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (gridView1 == null)
                    return;

                DataRow dr = gridView1.GetDataRow(e.FocusedRowHandle);
                if (dr == null)
                {
                    if (memoAIsonuc != null)
                    {
                        memoAIsonuc.EditValue = "Bu öğrenci için henüz AI analizi yapılmamıştır.";
                        memoAIsonuc.Properties.Appearance.ForeColor = Color.FromArgb(180, 180, 180);
                    }
                    return;
                }

                // Temel alanlar
                int ogrenciID = Convert.ToInt32(dr["ID"]);
                float mevcutAgno = 0;
                float.TryParse(dr["AGNO"]?.ToString() ?? "0", out mevcutAgno);

                // ML.NET tahmini (AIPotansiyelNotu) ve yüzde
                float tahminEdilenPuan = 0;
                bool hasTahmin = false;
                string potansiyelYuzde = string.Empty;

                if (dr.Table.Columns.Contains("AIPotansiyelNotu") && dr["AIPotansiyelNotu"] != DBNull.Value)
                {
                    hasTahmin = float.TryParse(dr["AIPotansiyelNotu"].ToString(), out tahminEdilenPuan);
                }
                if (dr.Table.Columns.Contains("AIPotansiyelYuzde") && dr["AIPotansiyelYuzde"] != DBNull.Value)
                {
                    potansiyelYuzde = dr["AIPotansiyelYuzde"]?.ToString() ?? string.Empty;
                }

                // AISkor (grid'deki "AI Puanı" kolonu)
                int aiSkor = 0;
                bool hasSkor = false;
                if (dr.Table.Columns.Contains("AI Puanı") && dr["AI Puanı"] != DBNull.Value)
                {
                    hasSkor = int.TryParse(dr["AI Puanı"].ToString(), out aiSkor);
                }

                // AINotu veritabanından oku (detaylı analiz metni)
                string aiNotuDetay = string.Empty;
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // Ogrenciler tablosundaki ID kolonunu dinamik tespit et
                        string ogrenciIDKolonu = "ID";
                        try
                        {
                            SqlCommand cmdKolon = new SqlCommand(@"SELECT TOP 1 COLUMN_NAME 
                                FROM INFORMATION_SCHEMA.COLUMNS 
                                WHERE TABLE_NAME = 'Ogrenciler' 
                                AND COLUMN_NAME IN ('ID', 'OgrenciID')
                                ORDER BY CASE COLUMN_NAME 
                                    WHEN 'ID' THEN 1 
                                    WHEN 'OgrenciID' THEN 2 
                                    ELSE 3 END", conn);
                            var kolonResult = cmdKolon.ExecuteScalar();
                            if (kolonResult != null && kolonResult != DBNull.Value)
                            {
                                string kolonStr = kolonResult.ToString();
                                if (!string.IsNullOrEmpty(kolonStr))
                                    ogrenciIDKolonu = kolonStr;
                            }
                        }
                        catch { }

                        SqlCommand cmd = new SqlCommand($@"SELECT ISNULL(AINotu, '') 
                                                           FROM Ogrenciler 
                                                           WHERE [{ogrenciIDKolonu}] = @id", conn);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        object notResult = cmd.ExecuteScalar();
                        if (notResult != null && notResult != DBNull.Value)
                            aiNotuDetay = notResult.ToString();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"AINotu okunamadı: {ex.Message}");
                }

                bool analizVar = (!string.IsNullOrWhiteSpace(aiNotuDetay)) || hasSkor || hasTahmin;

                // Henüz analiz yoksa bilgilendir
                if (!analizVar)
                {
                    if (memoAIsonuc != null)
                    {
                        memoAIsonuc.EditValue = "Bu öğrenci için henüz AI analizi yapılmamıştır.";
                        memoAIsonuc.Properties.Appearance.ForeColor = Color.FromArgb(180, 180, 180);
                    }

                    // Tahmin label ve progress bar'ı temizle
                    if (lblTahminSonuc != null)
                    {
                        lblTahminSonuc.Text = "🤖 AI Başarı Projeksiyonu: -";
                        lblTahminSonuc.Appearance.ForeColor = Color.White;
                    }
                    if (progressBarTahmin != null)
                    {
                        progressBarTahmin.Position = 0;
                    }
                    return;
                }

                // ML tahmin sonucu varsa, tahmin panelini güncelle
                if (hasTahmin)
                {
                    ShowTahminSonucu(tahminEdilenPuan, mevcutAgno);
                }

                // AI analiz notunu ve skoruna göre rengi göster
                if (memoAIsonuc != null)
                {
                    if (!string.IsNullOrWhiteSpace(aiNotuDetay))
                    {
                        // Formatlanmış raporu göster
                        string ad = dr["AD"]?.ToString() ?? "";
                        string soyad = dr["SOYAD"]?.ToString() ?? "";
                        string formattedReport = FormatAIReport(aiNotuDetay, aiSkor, ad, soyad);
                        memoAIsonuc.EditValue = formattedReport;
                    }
                    else
                    {
                        // Sadece skor varsa basit bilgi göster
                        memoAIsonuc.EditValue = hasSkor
                            ? $"Bu öğrenci için AI skoru: {aiSkor} / 100\n\nDetaylı analiz notu bulunamadı."
                            : "Bu öğrenci için henüz AI analizi yapılmamıştır.";
                    }

                    // Skora göre renk (70+ Yeşil, 40+ Turuncu, altı Kırmızı)
                    Color renk = GetAISkorColor(hasSkor ? aiSkor : 0);
                    memoAIsonuc.Properties.Appearance.ForeColor = renk;
                    memoAIsonuc.Properties.Appearance.Font = new Font("Segoe UI Semibold", 10.5F);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"FocusedRowChanged işlenirken hata: {ex.Message}");
            }
        }

        private void btnBursReddet_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBursKabul_Click_1(object sender, EventArgs e)
        {

        }

        // GridView CustomDrawCell - AIPotansiyelNotu kolonu için özel görselleştirme
        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                GridView view = sender as GridView;
                if (view == null) return;

                // Sadece AIPotansiyelNotu kolonunda işlem yap
                if (e.Column == null || (e.Column.FieldName != "AIPotansiyelNotu" && e.Column.Caption != "AIPotansiyelNotu"))
                    return;

                DataRow row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                // AIPotansiyelNotu ve AGNO değerlerini al
                object aiPotansiyelNotuObj = null;
                if (row.Table.Columns.Contains("AIPotansiyelNotu"))
                    aiPotansiyelNotuObj = row["AIPotansiyelNotu"];

                object agnoObj = row["AGNO"];

                if (aiPotansiyelNotuObj == null || aiPotansiyelNotuObj == DBNull.Value)
                    return;

                if (!float.TryParse(aiPotansiyelNotuObj.ToString(), out float tahminEdilenPuan))
                    return;

                float mevcutAgno = 0;
                if (agnoObj != null && agnoObj != DBNull.Value)
                {
                    float.TryParse(agnoObj.ToString(), out mevcutAgno);
                }

                // Hücre metnini oluştur (örnek: 3.40 ↑ %12)
                string cellText = tahminEdilenPuan.ToString("F2");
                bool hasIncrease = (tahminEdilenPuan > mevcutAgno && mevcutAgno > 0);

                if (hasIncrease)
                {
                    float artisYuzde = ((tahminEdilenPuan - mevcutAgno) / mevcutAgno) * 100f;
                    int artisInt = (int)Math.Round(artisYuzde, 0);
                    cellText = $"{tahminEdilenPuan:F2} ↑ %{artisInt}";
                }

                // Sadece metni güncelle (renklendirme yok, default görünüm)
                e.DisplayText = cellText;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"CustomDrawCell hatası: {ex.Message}");
            }
        }

        // Satır renklendirmesi - Durum'a göre
        private void GridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;

            DataRow row = view.GetDataRow(e.RowHandle);
            if (row == null) return;

            // Durum kolonunu kontrol et
            if (row.Table.Columns.Contains("Durum"))
            {
                object durumObj = row["Durum"];
                int durum = -1;
                
                if (durumObj != null && durumObj != DBNull.Value)
                {
                    try
                    {
                        durum = Convert.ToInt32(durumObj);
                    }
                    catch
                    {
                        durum = -1;
                    }
                }
                
                // Durum'a göre renklendir
                switch (durum)
                {
                    case 1: // Bursu Kabul Edildi - Yeşil
                        e.Appearance.BackColor = Color.FromArgb(39, 174, 96);
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.Options.UseBackColor = true;
                        e.Appearance.Options.UseForeColor = true;
                        break;
                    case 2: // Yedek - Mavi
                        e.Appearance.BackColor = Color.FromArgb(52, 152, 219);
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.Options.UseBackColor = true;
                        e.Appearance.Options.UseForeColor = true;
                        break;
                    case 0: // Beklemede - Sarı
                        e.Appearance.BackColor = Color.FromArgb(243, 156, 18);
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.Options.UseBackColor = true;
                        e.Appearance.Options.UseForeColor = true;
                        break;
                    case -1: // Reddedildi (OgrenciBurslari'nda kayıt yok) - Kırmızı
                        e.Appearance.BackColor = Color.FromArgb(231, 76, 60);
                        e.Appearance.ForeColor = Color.White;
                        e.Appearance.Options.UseBackColor = true;
                        e.Appearance.Options.UseForeColor = true;
                        break;
                    default: // Diğer - Varsayılan renk
                        break;
                }
            }
        }
    }
}
