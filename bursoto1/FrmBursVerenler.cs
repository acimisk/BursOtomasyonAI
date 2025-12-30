using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Renkler için gerekli
using System.Windows.Forms;
using DevExpress.XtraEditors; // Modern mesaj kutuları için
using bursoto1.Helpers;

namespace bursoto1
{
    public partial class FrmBursVerenler : Form
    {
        public FrmBursVerenler()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();

        void Listele()
        {
            try
            {
                DataTable dtBekleyen = new DataTable();
                DataTable dtAktif = new DataTable();

                using (SqlConnection conn = bgl.baglanti())
                {
                    // Bekleyen bağışlar
                    using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BursVerenler WHERE Durum = 'Beklemede'", conn))
                    {
                        da.Fill(dtBekleyen);
                    }

                    // Aktif (Onaylanmış) bağışçılar
                    using (SqlDataAdapter da2 = new SqlDataAdapter("SELECT * FROM BursVerenler WHERE Durum = 'Onaylandı'", conn))
                    {
                        da2.Fill(dtAktif);
                    }
                }

                gridControlBekleyen.DataSource = dtBekleyen;
                gridControlAktif.DataSource = dtAktif;

                if (gridViewBekleyen.Columns["ID"] != null)
                    gridViewBekleyen.Columns["ID"].Visible = false;
                if (gridViewAktif.Columns["ID"] != null)
                    gridViewAktif.Columns["ID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Burs Verenler Listesi");
            }
        }

        private void FrmBursVerenler_Load(object sender, EventArgs e)
        {
            Listele();
            GridAyarlariYap();

            // SAĞ TIK MENÜSÜ (Sadece BEKLEYENLER için - ekstra seçenek)
            ContextMenuStrip sagTikBekleyen = new ContextMenuStrip();
            ToolStripMenuItem itemOnayla = new ToolStripMenuItem("✅ Bağışı Onayla");
            ToolStripMenuItem itemReddet = new ToolStripMenuItem("❌ Bağışı Reddet");
            ToolStripMenuItem itemSil = new ToolStripMenuItem("🗑️ Kaydı Sil");

            itemOnayla.Click += ItemOnayla_Click;
            itemReddet.Click += ItemReddet_Click;
            itemSil.Click += ItemSil_Click;

            sagTikBekleyen.Items.Add(itemOnayla);
            sagTikBekleyen.Items.Add(itemReddet);
            sagTikBekleyen.Items.Add(new ToolStripSeparator());
            sagTikBekleyen.Items.Add(itemSil);
            gridControlBekleyen.ContextMenuStrip = sagTikBekleyen;

            // Grid seçim değiştiğinde buton durumlarını güncelle
            gridViewBekleyen.FocusedRowChanged += GridViewBekleyen_FocusedRowChanged;
        }

        private void GridAyarlariYap()
        {
            // Grid görünüm ayarları
            gridViewBekleyen.OptionsSelection.MultiSelect = false;
            gridViewAktif.OptionsSelection.MultiSelect = false;
            
            // Kolon genişliklerini otomatik ayarla
            gridViewBekleyen.BestFitColumns();
            gridViewAktif.BestFitColumns();
        }

        private void GridViewBekleyen_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // Seçili satır varsa butonları aktif et
            bool satirSecili = e.FocusedRowHandle >= 0;
            btnOnayla.Enabled = satirSecili;
            btnReddet.Enabled = satirSecili;
        }

        // --- RENKLENDİRME (UX DOKUNUŞU) ---
        // Bekleyenler Sarı, Onaylılar Yeşil görünsün
        private void gridViewBekleyen_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string durum = gridViewBekleyen.GetRowCellDisplayText(e.RowHandle, "Durum");
                if (durum == "Beklemede")
                {
                    e.Appearance.BackColor = Color.LightYellow;
                }
            }
        }

        // --- ONAYLAMA İŞLEMİ (DRY: Hem buton hem sağ tık menüsü için) ---
        private void BagisiOnayla()
        {
            var dr = gridViewBekleyen.GetDataRow(gridViewBekleyen.FocusedRowHandle);
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
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Onay Hatası");
                }
            }
        }

        private void ItemOnayla_Click(object sender, EventArgs e)
        {
            BagisiOnayla();
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            BagisiOnayla();
        }

        // --- REDDETME İŞLEMİ (DRY: Hem buton hem sağ tık menüsü için) ---
        private void BagisiReddet()
        {
            var dr = gridViewBekleyen.GetDataRow(gridViewBekleyen.FocusedRowHandle);
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
                        // Reddedilen bağışı sil (veya Durum='Reddedildi' yapılabilir, şu an siliniyor)
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess(
                        $"{adSoyad} kişisinin bağışı reddedildi ve kayıt silindi.",
                        "Reddetme Başarılı");
                    Listele();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Reddetme Hatası");
                }
            }
        }

        private void ItemReddet_Click(object sender, EventArgs e)
        {
            BagisiReddet();
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            BagisiReddet();
        }

        // --- YENİLEME İŞLEMİ ---
        private void btnYenile_Click(object sender, EventArgs e)
        {
            Listele();
            MessageHelper.ShowInfo("Bağışçı listesi yenilendi.", "Bilgi");
        }

        // --- SİLME İŞLEMİ (Sadece sağ tık menüsü için) ---
        private void ItemSil_Click(object sender, EventArgs e)
        {
            var dr = gridViewBekleyen.GetDataRow(gridViewBekleyen.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen silmek istediğiniz bağışı seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string adSoyad = dr["AdSoyad"]?.ToString() ?? "Bilinmeyen";
            string id = dr["ID"].ToString();

            if (MessageHelper.ShowConfirm(
                $"{adSoyad} kişisinin bağış kaydını silmek istediğinize emin misiniz?\n\n" +
                "Bu işlem geri alınamaz!",
                "Bağış Kaydını Sil"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess("Bağış kaydı başarıyla silindi.", "Silme Başarılı");
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