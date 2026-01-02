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

        public BursModule()
        {
            InitializeComponent();
            ConfigGrid();
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
            // Yeni burs tanımlama ekranı
            // TODO: FrmBursEkle formu oluşturulduğunda buraya bağlanacak
            MessageHelper.ShowInfo("Yeni burs ekleme özelliği yakında eklenecek.", "Bilgi");
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (MessageHelper.ShowConfirm("Seçili bursu silmek istiyor musunuz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Burslar WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    Listele();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }
    }
}