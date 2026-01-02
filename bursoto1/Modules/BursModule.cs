using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1.Modules
{
    public partial class BursModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        private int? _editingBursID = null; // Edit mode tracking

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
        }

        void WireEvents()
        {
            // Wire button click (ported from master FrmBurslar)
            if (btnBursTanimla != null)
                btnBursTanimla.Click += btnBursTanimla_Click;

            // Wire grid focus row changed for editing (ported from master)
            if (gridView1 != null)
                gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
        }

        private void BursModule_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Eski FrmBurslar'daki sorgun
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Burslar", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }
                
                // ID veya BursID kolonunu gizle
                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;
                if (gridView1.Columns["BursID"] != null)
                    gridView1.Columns["BursID"].Visible = false;
                    
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
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
            // Önce BursID dene, yoksa ID dene
            var id = gridView1.GetFocusedRowCellValue("BursID");
            if (id == null)
                id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null)
            {
                MessageHelper.ShowWarning("Lütfen silinecek bir burs seçiniz.", "Seçim Yapılmadı");
                return;
            }

            if (MessageHelper.ShowConfirm("Seçili bursu silmek istiyor musunuz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        // Önce BursID ile dene, sonra ID ile dene
                        SqlCommand cmd = new SqlCommand("DELETE FROM Burslar WHERE BursID=@p1 OR ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    FormuTemizle();
                    _editingBursID = null;
                    Listele();
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

                    // Set edit mode - BursID veya ID kolonunu dene
                    if (dr.Table.Columns.Contains("BursID"))
                        _editingBursID = Convert.ToInt32(dr["BursID"]);
                    else if (dr.Table.Columns.Contains("ID"))
                        _editingBursID = Convert.ToInt32(dr["ID"]);
                    
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
            if (string.IsNullOrEmpty(txtBursAd.Text) || string.IsNullOrEmpty(txtMiktar.Text))
            {
                MessageHelper.ShowWarning("Lütfen burs adını ve miktarını giriniz!", "Eksik Bilgi");
                return;
            }

            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    if (_editingBursID.HasValue)
                    {
                        // UPDATE mode (edit existing)
                        // BursID veya ID kolonunu dene (veritabanı yapısına göre)
                        string updateQuery = "UPDATE Burslar SET BursAdı=@p1, Miktar=@p2, Kontenjan=@p3, Aciklama=@p4 WHERE BursID=@p5 OR ID=@p5";
                        using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtBursAd.Text);
                            cmd.Parameters.AddWithValue("@p2", Convert.ToDecimal(txtMiktar.EditValue ?? 0));
                            cmd.Parameters.AddWithValue("@p3", Convert.ToInt32(txtKontenjan.EditValue ?? 0));
                            cmd.Parameters.AddWithValue("@p4", txtAciklama.Text ?? "");
                            cmd.Parameters.AddWithValue("@p5", _editingBursID.Value);
                            cmd.ExecuteNonQuery();
                        }
                        MessageHelper.ShowSuccess("Burs başarıyla güncellendi.", "Bilgi");
                    }
                    else
                    {
                        // INSERT mode (add new) - ported from master
                        string sorgu = "INSERT INTO Burslar (BursAdı, Miktar, Kontenjan, Aciklama) VALUES (@p1, @p2, @p3, @p4)";
                        using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                        {
                            cmd.Parameters.AddWithValue("@p1", txtBursAd.Text);
                            cmd.Parameters.AddWithValue("@p2", Convert.ToDecimal(txtMiktar.EditValue));
                            cmd.Parameters.AddWithValue("@p3", Convert.ToInt32(txtKontenjan.EditValue));
                            cmd.Parameters.AddWithValue("@p4", txtAciklama.Text ?? "");
                            cmd.ExecuteNonQuery();
                        }
                        MessageHelper.ShowSuccess("Burs başarıyla kaydedildi.", "Bilgi");
                    }
                }

                FormuTemizle();
                _editingBursID = null;
                btnBursTanimla.Text = "Burs Tanımla";
                Listele();
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