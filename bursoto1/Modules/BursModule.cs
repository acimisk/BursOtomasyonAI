using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1.Modules
{
    public partial class BursModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        private int? _editingBursID = null; // Edit mode tracking
        private string _idColumnName = "BursID"; // DB'deki gerçek ID kolon adı

        public BursModule()
        {
            InitializeComponent();
            ConfigGrid();
            WireEvents();
        }

        void ConfigGrid()
        {
            // Grid ayarları (Modern Görünüm)
            if (gridView1 != null)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            }

            // SpinEdit ayarları - negatif değer engelleme
            ConfigureSpinEdits();
        }

        void ConfigureSpinEdits()
        {
            // Kontenjan ayarları
            if (txtKontenjan != null)
            {
                txtKontenjan.Properties.MinValue = 0;
                txtKontenjan.Properties.MaxValue = 9999;
                txtKontenjan.Properties.IsFloatValue = false;
                txtKontenjan.Properties.Increment = 1;
                // Mouse wheel ile değişimi engelle
                txtKontenjan.MouseWheel += SpinEdit_MouseWheel;
            }

            // Miktar ayarları
            if (txtMiktar != null)
            {
                txtMiktar.Properties.MinValue = 0;
                txtMiktar.Properties.MaxValue = 999999999;
                txtMiktar.Properties.IsFloatValue = true;
                txtMiktar.Properties.Increment = 100;
                txtMiktar.Properties.DisplayFormat.FormatString = "C0"; // Para birimi formatı
                txtMiktar.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                // Mouse wheel ile değişimi engelle
                txtMiktar.MouseWheel += SpinEdit_MouseWheel;
            }
        }

        // Mouse wheel ile SpinEdit değişimini engelle
        private void SpinEdit_MouseWheel(object sender, MouseEventArgs e)
        {
            // Mouse wheel eventini iptal et (handled olarak işaretle)
            ((System.Windows.Forms.HandledMouseEventArgs)e).Handled = true;
        }

        void WireEvents()
        {
            // Wire button click (ported from master FrmBurslar)
            if (btnBursTanimla != null)
                btnBursTanimla.Click += btnBursTanimla_Click;

            // Wire grid focus row changed for editing (ported from master)
            if (gridView1 != null)
                gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;

            // Sağ tık menüsü ekle
            SetupContextMenu();
        }

        void SetupContextMenu()
        {
            ContextMenuStrip sagTik = new ContextMenuStrip();
            
            ToolStripMenuItem itemAktif = new ToolStripMenuItem("✅ Bursu Aktif Yap");
            ToolStripMenuItem itemPasif = new ToolStripMenuItem("❌ Bursu Pasif Yap");
            ToolStripMenuItem itemSil = new ToolStripMenuItem("🗑️ Bursu Sil");

            itemAktif.Click += (s, e) => BursDurumDegistir(true);
            itemPasif.Click += (s, e) => BursDurumDegistir(false);
            itemSil.Click += (s, e) => btnSil_ItemClick(null, null);

            sagTik.Items.Add(itemAktif);
            sagTik.Items.Add(itemPasif);
            sagTik.Items.Add(new ToolStripSeparator());
            sagTik.Items.Add(itemSil);

            if (gridControl1 != null)
                gridControl1.ContextMenuStrip = sagTik;
        }

        // Burs durumunu Aktif/Pasif yap
        private void BursDurumDegistir(bool aktif)
        {
            var id = gridView1.GetFocusedRowCellValue(_idColumnName);
            if (id == null || id == DBNull.Value)
            {
                MessageHelper.ShowWarning("Lütfen bir burs seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string bursAdi = gridView1.GetFocusedRowCellValue("BursAdı")?.ToString() ?? "Seçili Burs";
            string yeniDurum = aktif ? "Aktif" : "Pasif";
            int yeniKontenjan = aktif ? 50 : 0; // Aktif yapılınca default 50 kontenjan

            if (MessageHelper.ShowConfirm($"'{bursAdi}' bursunu {yeniDurum} yapmak istediğinize emin misiniz?", "Durum Değişikliği"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // Pasif = Kontenjan 0, Aktif = Kontenjan > 0
                        if (!aktif)
                        {
                            // Pasife al - kontenjanı 0 yap
                            string query = $"UPDATE Burslar SET Kontenjan = 0 WHERE {_idColumnName}=@p1";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@p1", id);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // Aktif yap - kontenjanı eski değerine veya 50'ye çıkar
                            int mevcutKontenjan = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Kontenjan") ?? 0);
                            if (mevcutKontenjan == 0) mevcutKontenjan = 50;
                            
                            string query = $"UPDATE Burslar SET Kontenjan = @k WHERE {_idColumnName}=@p1";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@k", mevcutKontenjan);
                            cmd.Parameters.AddWithValue("@p1", id);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageHelper.ShowSuccess($"'{bursAdi}' bursu {yeniDurum} durumuna alındı.", "İşlem Başarılı");
                    Listele();
                    DataChangedNotifier.NotifyBursChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Durum Değişikliği Hatası");
                }
            }
        }

        private void BursModule_Load(object sender, EventArgs e)
        {
            RefreshAndClear();
        }

        // Navigasyon sırasında çağrılır - formu temizle ve listele
        public void RefreshAndClear()
        {
            FormuTemizle();
            _editingBursID = null;
            btnBursTanimla.Text = "Burs Tanımla";
            Listele();
        }

        public void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Doluluk bilgisi ile birlikte çek
                    string query = @"SELECT b.*, 
                        ISNULL((SELECT COUNT(*) FROM OgrenciBurslari ob WHERE ob.BursID = b.BursID AND ob.Durum = 1), 0) as Dolu,
                        CASE WHEN b.Kontenjan > 0 THEN 'Aktif' ELSE 'Pasif' END as Durum
                        FROM Burslar b";
                    
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                    }
                    catch
                    {
                        // BursID yoksa ID ile dene
                        query = @"SELECT b.*, 
                            ISNULL((SELECT COUNT(*) FROM OgrenciBurslari ob WHERE ob.BursID = b.ID AND ob.Durum = 1), 0) as Dolu,
                            CASE WHEN b.Kontenjan > 0 THEN 'Aktif' ELSE 'Pasif' END as Durum
                            FROM Burslar b";
                        SqlDataAdapter da = new SqlDataAdapter(query, conn);
                        da.Fill(dt);
                    }

                    // Kontenjan gösterim kolonu ekle (ör: "5/50")
                    dt.Columns.Add("Doluluk", typeof(string));
                    foreach (DataRow row in dt.Rows)
                    {
                        int dolu = Convert.ToInt32(row["Dolu"] ?? 0);
                        int kontenjan = Convert.ToInt32(row["Kontenjan"] ?? 0);
                        row["Doluluk"] = $"{dolu} / {kontenjan}";
                    }

                    gridControl1.DataSource = dt;
                }

                // Gerçek ID kolon adını tespit et ve kaydet
                if (dt.Columns.Contains("BursID"))
                    _idColumnName = "BursID";
                else if (dt.Columns.Contains("ID"))
                    _idColumnName = "ID";
                else
                    _idColumnName = dt.Columns[0].ColumnName;

                System.Diagnostics.Debug.WriteLine($"Burslar tablosu ID kolonu: {_idColumnName}");
                
                // Gereksiz kolonları gizle
                if (gridView1.Columns[_idColumnName] != null)
                    gridView1.Columns[_idColumnName].Visible = false;
                if (gridView1.Columns["Dolu"] != null)
                    gridView1.Columns["Dolu"].Visible = false;
                    
                // Kolon başlıklarını düzenle
                if (gridView1.Columns["Doluluk"] != null)
                    gridView1.Columns["Doluluk"].Caption = "Doluluk (Dolu/Kontenjan)";
                if (gridView1.Columns["Durum"] != null)
                    gridView1.Columns["Durum"].Caption = "Durum";
                    
                gridView1.BestFitColumns();
                ApplyDarkGrid();
                
                // Satır renklendirme event'i
                gridView1.RowStyle -= GridView1_RowStyle;
                gridView1.RowStyle += GridView1_RowStyle;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        // Satır renklendirme - Aktif/Pasif durum
        private void GridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string durum = gridView1.GetRowCellDisplayText(e.RowHandle, "Durum");
                int dolu = 0;
                int kontenjan = 0;
                
                try
                {
                    dolu = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "Dolu") ?? 0);
                    kontenjan = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "Kontenjan") ?? 0);
                }
                catch { }

                if (durum == "Pasif" || kontenjan == 0)
                {
                    // Pasif - Kırmızı
                    e.Appearance.BackColor = Color.FromArgb(60, 30, 30);
                    e.Appearance.ForeColor = Color.FromArgb(255, 150, 150);
                }
                else if (dolu >= kontenjan)
                {
                    // Dolu - Turuncu
                    e.Appearance.BackColor = Color.FromArgb(60, 50, 30);
                    e.Appearance.ForeColor = Color.FromArgb(255, 200, 100);
                }
                else
                {
                    // Aktif ve boş yer var - Yeşil
                    e.Appearance.BackColor = Color.FromArgb(30, 60, 40);
                    e.Appearance.ForeColor = Color.FromArgb(150, 255, 150);
                }
            }
        }

        // Dark mode grid ayarları
        private void ApplyDarkGrid()
        {
            gridView1.Appearance.Row.BackColor = Color.FromArgb(32, 32, 32);
            gridView1.Appearance.Row.ForeColor = Color.White;
            gridView1.Appearance.Row.Options.UseBackColor = true;
            gridView1.Appearance.Row.Options.UseForeColor = true;
            gridView1.Appearance.HeaderPanel.BackColor = Color.FromArgb(45, 45, 48);
            gridView1.Appearance.HeaderPanel.ForeColor = Color.White;
            gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            gridView1.Appearance.FocusedRow.BackColor = Color.FromArgb(70, 70, 70);
            gridView1.Appearance.FocusedRow.ForeColor = Color.White;
            gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            gridView1.Appearance.Empty.BackColor = Color.FromArgb(32, 32, 32);
            gridView1.Appearance.Empty.Options.UseBackColor = true;
            if (gridControl1 != null)
                gridControl1.BackColor = Color.FromArgb(32, 32, 32);
        }

        // --- BUTONLAR (Ribbon'dan tıklandığında) ---

        public void btnYeni_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Yeni burs moduna geç (formu temizle)
            FormuTemizle();
            _editingBursID = null;
            btnBursTanimla.Text = "Burs Tanımla"; // Reset button text
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Tespit edilen ID kolonunu kullan
            var id = gridView1.GetFocusedRowCellValue(_idColumnName);
            if (id == null || id == DBNull.Value)
            {
                MessageHelper.ShowWarning("Lütfen silinecek bir burs seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string bursAdi = gridView1.GetFocusedRowCellValue("BursAdı")?.ToString() ?? "Seçili Burs";

            if (MessageHelper.ShowConfirm($"'{bursAdi}' bursunu silmek istiyor musunuz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // Tespit edilen kolon adını kullan
                        string deleteQuery = $"DELETE FROM Burslar WHERE {_idColumnName}=@p1";
                        SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    FormuTemizle();
                    _editingBursID = null;
                    Listele();
                    DataChangedNotifier.NotifyBursChanged();
                    MessageHelper.ShowSuccess("Burs başarıyla silindi.", "Silme Başarılı");
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        // --- PORTED FROM MASTER: Grid Focus Row Changed (for editing) ---
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                if (dr != null)
                {
                    // Populate form fields (ported from master FrmBurslar)
                    // Güvenli kolon erişimi
                    if (dr.Table.Columns.Contains("BursAdı"))
                        txtBursAd.Text = dr["BursAdı"]?.ToString() ?? "";
                    
                    if (dr.Table.Columns.Contains("Miktar"))
                        txtMiktar.EditValue = dr["Miktar"];
                    
                    if (dr.Table.Columns.Contains("Kontenjan"))
                        txtKontenjan.EditValue = dr["Kontenjan"];
                    
                    if (dr.Table.Columns.Contains("Aciklama"))
                        txtAciklama.Text = dr["Aciklama"]?.ToString() ?? "";

                    // Tespit edilen ID kolonunu kullan
                    if (dr.Table.Columns.Contains(_idColumnName))
                        _editingBursID = Convert.ToInt32(dr[_idColumnName]);
                    
                    btnBursTanimla.Text = "Bursu Güncelle";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("FocusedRowChanged hatası: " + ex.Message);
            }
        }

        // --- PORTED FROM MASTER: Burs Add/Edit (btnBursTanimla_Click) ---
        private void btnBursTanimla_Click(object sender, EventArgs e)
        {
            // Validation (ported from master)
            if (string.IsNullOrEmpty(txtBursAd.Text))
            {
                MessageHelper.ShowWarning("Lütfen burs adını giriniz!", "Eksik Bilgi");
                txtBursAd.Focus();
                return;
            }

            decimal miktar = Convert.ToDecimal(txtMiktar.EditValue ?? 0);
            int kontenjan = Convert.ToInt32(txtKontenjan.EditValue ?? 0);

            if (miktar < 0)
            {
                MessageHelper.ShowWarning("Burs miktarı negatif olamaz!", "Geçersiz Değer");
                txtMiktar.Focus();
                return;
            }

            if (kontenjan < 0)
            {
                MessageHelper.ShowWarning("Kontenjan negatif olamaz!", "Geçersiz Değer");
                txtKontenjan.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    if (_editingBursID.HasValue)
                    {
                        // UPDATE mode - tespit edilen ID kolonunu kullan
                        string updateQuery = $"UPDATE Burslar SET BursAdı=@p1, Miktar=@p2, Kontenjan=@p3, Aciklama=@p4 WHERE {_idColumnName}=@p5";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtBursAd.Text.Trim());
                            cmd.Parameters.AddWithValue("@p2", miktar);
                            cmd.Parameters.AddWithValue("@p3", kontenjan);
                            cmd.Parameters.AddWithValue("@p4", txtAciklama.Text?.Trim() ?? "");
                            cmd.Parameters.AddWithValue("@p5", _editingBursID.Value);
                            cmd.ExecuteNonQuery();
                        }
                        MessageHelper.ShowSuccess("Burs başarıyla güncellendi.", "Güncelleme Başarılı");
                    }
                    else
                    {
                        // INSERT mode (add new) - ported from master
                        string sorgu = "INSERT INTO Burslar (BursAdı, Miktar, Kontenjan, Aciklama) VALUES (@p1, @p2, @p3, @p4)";
                        using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtBursAd.Text.Trim());
                            cmd.Parameters.AddWithValue("@p2", miktar);
                            cmd.Parameters.AddWithValue("@p3", kontenjan);
                            cmd.Parameters.AddWithValue("@p4", txtAciklama.Text?.Trim() ?? "");
                            cmd.ExecuteNonQuery();
                        }
                        MessageHelper.ShowSuccess("Burs başarıyla kaydedildi.", "Kayıt Başarılı");
                    }
                }

                FormuTemizle();
                _editingBursID = null;
                btnBursTanimla.Text = "Burs Tanımla";
                Listele();
                DataChangedNotifier.NotifyBursChanged();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Kaydetme Hatası");
            }
        }

        // --- PORTED FROM MASTER: Form Clear ---
        void FormuTemizle()
        {
            txtBursAd.Text = "";
            txtMiktar.EditValue = 0;
            txtKontenjan.EditValue = 0;
            txtAciklama.Text = "";
        }
    }
}