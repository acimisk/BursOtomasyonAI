using bursoto1.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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

            if (MessageHelper.ShowConfirm(
                $"{seciliSatirlar.Length} adet kaydı silmek istediğinize emin misiniz?",
                "Silme Onayı"))
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
                                // FK constraint hatası için önce ilişkili tablolardan sil
                                // Önce BursGiderleri'nden sil
                                try
                                {
                                    SqlCommand cmdGider = new SqlCommand("DELETE FROM BursGiderleri WHERE OgrenciID=@p1", conn);
                                    cmdGider.Parameters.AddWithValue("@p1", id);
                                    cmdGider.ExecuteNonQuery();
                                }
                                catch { }

                                // Sonra OgrenciBurslari'ndan sil
                                try
                                {
                                    SqlCommand cmdBurs = new SqlCommand("DELETE FROM OgrenciBurslari WHERE OgrenciID=@p1", conn);
                                    cmdBurs.Parameters.AddWithValue("@p1", id);
                                    cmdBurs.ExecuteNonQuery();
                                }
                                catch { }

                                // Son olarak öğrenciyi sil
                                SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                                cmd.Parameters.AddWithValue("@p1", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                    MessageHelper.ShowSuccess("Seçilen kayıtlar başarıyla silindi.", "Silme Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmOgrenciEkle frm = new FrmOgrenciEkle();
            frm.ShowDialog();
            Listele();
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

            // Öğrencinin motivasyon yazısını çek
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

            string ogrenciVerisi = $"Ad Soyad: {ad} {soyad}\n" +
                                   $"Bölüm: {bolum}, Sınıf: {sinif}\n" +
                                   $"AGNO: {agno}\n" +
                                   $"Hane Geliri: {gelir} TL\n" +
                                   $"Kardeş Sayısı: {kardes}\n" +
                                   $"Motivasyon: {(string.IsNullOrWhiteSpace(motivasyon) ? "Açıklama yazılmamış" : motivasyon)}";

            if (lblAIsonuc != null)
            {
                lblAIsonuc.Text = "AI analiz yapılıyor, lütfen bekleyiniz...";
                lblAIsonuc.ForeColor = Color.FromArgb(52, 152, 219);
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

                if (lblAIsonuc != null)
                {
                    lblAIsonuc.Text = $"{ad} {soyad}\n\n{aiSkor}/100 - {uygunluk}\n\n{aiNotu}";
                    lblAIsonuc.ForeColor = renk;
                }

                Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
                DataChangedNotifier.NotifyOgrenciChanged();
            }
            catch (Exception ex)
            {
                if (lblAIsonuc != null)
                {
                    lblAIsonuc.Text = "AI analizi başarısız: " + ex.Message;
                    lblAIsonuc.ForeColor = Color.FromArgb(231, 76, 60);
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

        // Burs Kabul
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

            if (MessageHelper.ShowConfirm($"{adSoyad} öğrencisinin burs başvurusunu KABUL etmek istediğinize emin misiniz?", "Burs Kabul Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE OgrenciBurslari SET Durum = 1, BaslangicTarihi = @tarih WHERE OgrenciID = @id", conn);
                        cmd.Parameters.AddWithValue("@id", ogrenciID);
                        cmd.Parameters.AddWithValue("@tarih", DateTime.Now);
                        int affected = cmd.ExecuteNonQuery();

                        if (affected == 0)
                        {
                            SqlCommand cmdInsert = new SqlCommand("INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) VALUES (@id, 1, @tarih, 1)", conn);
                            cmdInsert.Parameters.AddWithValue("@id", ogrenciID);
                            cmdInsert.Parameters.AddWithValue("@tarih", DateTime.Now);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }

                    MessageHelper.ShowSuccess($"{adSoyad} öğrencisinin burs başvurusu KABUL EDİLDİ.", "İşlem Başarılı");
                    DataChangedNotifier.NotifyOgrenciChanged();
                    DataChangedNotifier.NotifyBursChanged();
                    Listele(cmbFiltre?.SelectedIndex >= 0 ? cmbFiltre.Properties.Items[cmbFiltre.SelectedIndex].ToString() : "Tüm Öğrenciler");
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Kabul Hatası");
                }
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
    }
}
