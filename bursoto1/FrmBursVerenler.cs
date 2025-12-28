using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Renkler için gerekli
using System.Windows.Forms;
using DevExpress.XtraEditors; // Modern mesaj kutuları için

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
                // Bağlantıyı güvenli şekilde alıyoruz
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BursVerenler", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;

                // UI İYİLEŞTİRMESİ: Kullanıcının görmesine gerek olmayan ID'yi gizle
                gridView1.Columns["ID"].Visible = false;

                // Grid üzerinde elle değişiklik yapmayı engelle (sadece sağ tık ile onaylasınlar)
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false; // Üstteki gri alanı kaldır
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Liste yüklenirken hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmBursVerenler_Load(object sender, EventArgs e)
        {
            Listele();

            // SAĞ TIK MENÜSÜ (Context Menu)
            ContextMenuStrip sagTik = new ContextMenuStrip();
            ToolStripMenuItem itemOnayla = new ToolStripMenuItem("✅ Bağışı Onayla");
            ToolStripMenuItem itemSil = new ToolStripMenuItem("❌ Kaydı Sil");

            itemOnayla.Click += ItemOnayla_Click;
            itemSil.Click += ItemSil_Click;

            sagTik.Items.Add(itemOnayla);
            sagTik.Items.Add(itemSil);
            gridControl1.ContextMenuStrip = sagTik;
        }

        // --- RENKLENDİRME (UX DOKUNUŞU) ---
        // Onaylananlar Yeşil, Bekleyenler Sarı görünsün
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
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

        private void ItemOnayla_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null) return;

            string id = dr["ID"].ToString();

            if (XtraMessageBox.Show($"{dr["AdSoyad"]} kişisinin bağışını onaylıyor musun?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = bgl.baglanti();
                    SqlCommand cmd = new SqlCommand("UPDATE BursVerenler SET Durum='Onaylandı' WHERE ID=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", id);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    XtraMessageBox.Show("Bağış onaylandı, bütçeye eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                }
                catch (Exception ex) { XtraMessageBox.Show("Hata: " + ex.Message); }
            }
        }

        private void ItemSil_Click(object sender, EventArgs e)
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null) return;

            if (XtraMessageBox.Show("Bu kaydı silmek istediğine emin misin?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = bgl.baglanti();
                    SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", dr["ID"].ToString());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Listele();
                }
                catch (Exception ex) { XtraMessageBox.Show("Hata: " + ex.Message); }
            }
        }
    }
}