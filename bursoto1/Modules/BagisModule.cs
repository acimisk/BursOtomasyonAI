using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1.Modules
{
    public partial class BagisModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public BagisModule()
        {
            InitializeComponent();
            ConfigGrid();
        }

        void ConfigGrid()
        {
            if (gridView1 != null)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            }
        }

        private void BagisModule_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BursVerenler", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Hata");
            }
        }

        // --- BUTONLAR ---
        public void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // FrmBagisEkle gibi bir formun varsa:
            // FrmBagisEkle frm = new FrmBagisEkle();
            // if(frm.ShowDialog() == DialogResult.OK) Listele();
            XtraMessageBox.Show("Bağışçı ekleme formu buraya.");
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (MessageHelper.ShowConfirm("Bağışçıyı silmek istiyor musunuz?", "Silme"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    Listele();
                }
                catch (Exception ex) { MessageHelper.ShowException(ex, "Silinemedi"); }
            }
        }
    }
}