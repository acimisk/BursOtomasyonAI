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

        public void Listele(string filtreTipi = "Tüm Öğrenciler")
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

                    string sorgu;
                    
                    switch (filtreTipi)
                    {
                        case "Burs Alanlar":
                            sorgu = $@"SELECT DISTINCT o.[{ogrenciIDKolonu}] AS ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO, 
                                     o.[TOPLAM HANE GELİRİ] AS [Hane Geliri], o.[KARDEŞ SAYISI] AS [Kardeş], 
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect},
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
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect},
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
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect},
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
                                     ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect},
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
                                  ISNULL(o.AISkor, 0) AS [AI Puanı]{universiteSelect},
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

                gridView1.BestFitColumns();
                ApplyDarkGrid(gridView1);
                
                // Satır renklendirmesi için event handler ekle (ApplyDarkGrid'den sonra)
                gridView1.RowStyle -= GridView1_RowStyle;
                gridView1.RowStyle += GridView1_RowStyle;
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

                    // Mevcut kolonları kullanarak sorgu oluştur
                    string selectColumns = $"[{ogrenciIDKolonu}]";
                    if (kolonlar.Contains("Motivasyon")) selectColumns += ", ISNULL(Motivasyon, '') as Motivasyon";
                    if (kolonlar.Contains("Ihtiyac")) selectColumns += ", ISNULL(Ihtiyac, '') as Ihtiyac";
                    if (kolonlar.Contains("Hedefler")) selectColumns += ", ISNULL(Hedefler, '') as Hedefler";
                    if (kolonlar.Contains("BursKullanim")) selectColumns += ", ISNULL(BursKullanim, '') as Kullanim";
                    if (kolonlar.Contains("FarkliOzellik")) selectColumns += ", ISNULL(FarkliOzellik, '') as Fark";
                    if (kolonlar.Contains("AINotu")) selectColumns += ", ISNULL(AINotu, '') as AINotu";

                    SqlCommand cmd = new SqlCommand($"SELECT {selectColumns} FROM Ogrenciler WHERE [{ogrenciIDKolonu}] = @id", conn);
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

                // Tahmini veritabanına kaydet
                UpdateAIPotansiyelNotu(ogrenciID, tahminEdilenPuan);
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
                    // Temel metin
                    string text = $"🤖 AI Başarı Projeksiyonu: {tahminEdilenPuan:F2}";

                    // Eğer tahmini not mevcut AGNO'dan yüksekse, potansiyel artış mesajı ekle
                    bool potansiyelArtis = tahminEdilenPuan > mevcutAgno;
                    if (potansiyelArtis)
                    {
                        text += " ↑ Potansiyel Artış Bekleniyor";
                    }

                    lblTahminSonuc.Text = text;

                    // Renklendirme: Potansiyel artış varsa yeşil, yoksa turuncu
                    if (potansiyelArtis)
                    {
                        lblTahminSonuc.Appearance.ForeColor = Color.FromArgb(39, 174, 96); // Yeşil
                        if (progressBarTahmin != null)
                        {
                            progressBarTahmin.Properties.Appearance.ForeColor = Color.FromArgb(39, 174, 96);
                        }
                    }
                    else
                    {
                        lblTahminSonuc.Appearance.ForeColor = Color.FromArgb(243, 156, 18); // Turuncu
                        if (progressBarTahmin != null)
                        {
                            progressBarTahmin.Properties.Appearance.ForeColor = Color.FromArgb(243, 156, 18);
                        }
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
            if (uni.Contains("İSTANBUL") || uni.Contains("İTÜ") || uni.Contains("İSTANBUL TEKNİK") ||
                uni.Contains("BOĞAZİÇİ") || uni.Contains("KOÇ") ||
                uni.Contains("ITU") || uni.Contains("İTU"))
            {
                return 1.0f;
            }

            // Ankara ve benzeri yüksek maliyetli şehir üniversiteleri
            if (uni.Contains("ANKARA") || uni.Contains("ODTÜ") || uni.Contains("ORTA DOĞU TEKNİK") ||
                uni.Contains("HACETTEPE"))
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

            // En zor bölümler
            // TIP ayrı ele alınır (maksimum zorluk)
            if (bolum.Contains("TIP"))
            {
                return 5.0f;
            }

            // MÜHENDIS (noktasız I) - mühendislik türevleri
            if (bolum.Contains("MÜHENDIS") || bolum.Contains("MUHENDIS"))
            {
                return 4.8f;
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

            // Daha düşük zorluk
            // TARIH ve TARİH kontrolü - Replace("İ", "I") yapıldığı için TARIH kontrolü yeterli
            if (bolum.Contains("EDEBİYAT") || bolum.Contains("TARIH") ||
                bolum.Contains("ARKEOLOJİ") || bolum.Contains("SPOR"))
            {
                return 2.0f;
            }

            // Diğerleri için
            return 2.5f;
        }

        // Tahmin edilen puanı Ogrenciler.AIPotansiyelNotu kolonuna kaydet
        private void UpdateAIPotansiyelNotu(int ogrenciID, float tahminEdilenPuan)
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

                    SqlCommand cmd = new SqlCommand(
                        $"UPDATE Ogrenciler SET AIPotansiyelNotu = @puan WHERE [{ogrenciIDKolonu}] = @id", conn);
                    cmd.Parameters.AddWithValue("@puan", tahminEdilenPuan);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    cmd.ExecuteNonQuery();
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

        private void btnBursReddet_Click_1(object sender, EventArgs e)
        {

        }

        private void btnBursKabul_Click_1(object sender, EventArgs e)
        {

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
