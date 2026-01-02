using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
                gridView1.OptionsSelection.MultiSelect = false; // Tek seçim
                
                // Row style event'i bağla (renklendirme için)
                gridView1.RowStyle += GridView1_RowStyle;
            }
        }

        private void BagisModule_Load(object sender, EventArgs e)
        {
            Listele();
            SetupContextMenu();
        }

        void SetupContextMenu()
        {
            // Sağ tık menüsü (ported from master FrmBursVerenler)
            ContextMenuStrip sagTik = new ContextMenuStrip();
            ToolStripMenuItem itemOnayla = new ToolStripMenuItem("✅ Bağışı Onayla");
            ToolStripMenuItem itemReddet = new ToolStripMenuItem("❌ Bağışı Reddet");
            ToolStripMenuItem itemSil = new ToolStripMenuItem("🗑️ Kaydı Sil");

            itemOnayla.Click += ItemOnayla_Click;
            itemReddet.Click += ItemReddet_Click;
            itemSil.Click += ItemSil_Click;

            sagTik.Items.Add(itemOnayla);
            sagTik.Items.Add(itemReddet);
            sagTik.Items.Add(new ToolStripSeparator());
            sagTik.Items.Add(itemSil);
            
            if (gridControl1 != null)
                gridControl1.ContextMenuStrip = sagTik;
        }

        // Renklendirme (ported from master)
        private void GridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string durum = gridView1.GetRowCellDisplayText(e.RowHandle, "Durum");
                if (durum == "Onaylandı")
                {
                    e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.BackColor2 = Color.White;
                }
                else if (durum == "Beklemede")
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        public void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BursVerenler", conn);
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }
                
                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;
                    
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        // --- BUTONLAR ---
        public void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Yeni bağışçı ekleme ekranı
            // TODO: FrmBagisEkle formu oluşturulduğunda buraya bağlanacak
            MessageHelper.ShowInfo("Yeni bağışçı ekleme özelliği yakında eklenecek.", "Bilgi");
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
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex) { MessageHelper.ShowException(ex, "Silinemedi"); }
            }
        }

        // --- SAĞ TIK MENÜ İŞLEMLERİ (ported from master) ---
        private void ItemOnayla_Click(object sender, EventArgs e)
        {
            BagisiOnayla();
        }

        private void ItemReddet_Click(object sender, EventArgs e)
        {
            BagisiReddet();
        }

        private void ItemSil_Click(object sender, EventArgs e)
        {
            btnSil_ItemClick(sender, null);
        }

        private void BagisiOnayla()
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen onaylamak istediğiniz bağışı seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string adSoyad = dr["AdSoyad"]?.ToString() ?? "Bilinmeyen";
            decimal miktar = Convert.ToDecimal(dr["BagisMiktari"] ?? 0);
            string id = dr["ID"].ToString();

            if (MessageHelper.ShowConfirm(
                $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışını onaylıyor musunuz?\n\n" +
                "Onaylandıktan sonra bağışçı aktif bağışçılar listesine taşınacak.",
                "Bağışı Onayla"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE BursVerenler SET Durum='Onaylandı' WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess(
                        $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışı onaylandı.\n" +
                        "Bağışçı aktif bağışçılar listesine taşındı.",
                        "Onay Başarılı");
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Onay Hatası");
                }
            }
        }

        private void BagisiReddet()
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen reddetmek istediğiniz bağışı seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string adSoyad = dr["AdSoyad"]?.ToString() ?? "Bilinmeyen";
            decimal miktar = Convert.ToDecimal(dr["BagisMiktari"] ?? 0);
            string id = dr["ID"].ToString();

            if (MessageHelper.ShowConfirm(
                $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışını reddetmek istediğinize emin misiniz?\n\n" +
                "Reddedilen bağış kaydı silinecektir.",
                "Bağışı Reddet"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess(
                        $"{adSoyad} kişisinin bağışı reddedildi ve kayıt silindi.",
                        "Reddetme Başarılı");
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Reddetme Hatası");
                }
            }
        }
    }
}