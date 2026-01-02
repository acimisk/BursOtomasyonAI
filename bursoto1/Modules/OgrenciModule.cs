using bursoto1.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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

        public void Listele()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    string sorgu = @"
                        SELECT 
                            ID, 
                            AD, 
                            SOYAD, 
                            BÖLÜMÜ, 
                            SINIF, 
                            AGNO, 
                            [TOPLAM HANE GELİRİ] AS [Hane Geliri], 
                            [KARDEŞ SAYISI] AS [Kardeş], 
                            ISNULL(AISkor, 0) AS [AI Puanı]
                        FROM Ogrenciler 
                        ORDER BY AD, SOYAD";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }

                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;

                gridView1.BestFitColumns();

                // 🔥 EN KRİTİK SATIR
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

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (MessageHelper.ShowConfirm(
                "Seçili öğrenciyi silmek istediğinize emin misiniz?",
                "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd =
                            new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
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

        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            FrmOgrenciEkle frm = new FrmOgrenciEkle();
            frm.ShowDialog();
            Listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null) return;

            OgrenciProfili frm = new OgrenciProfili
            {
                secilenOgrenciID = Convert.ToInt32(dr["ID"]),
                ad = dr["AD"].ToString(),
                soyad = dr["SOYAD"].ToString()
            };

            frm.Show();
        }
    }
}
