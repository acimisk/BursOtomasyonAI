using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class FrmBurslar : Form
    {
        public FrmBurslar()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();

        public void Listele()
        {
            // Listeleme yaparken bağlantıyı her seferinde taze alıyoruz
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Burslar", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmBurslar_Load(object sender, EventArgs e)
        {
            Listele();
        }


        // KANKA BU ÇOK ÖNEMLİ: Grid'de bir satıra tıklayınca bilgiler kutulara dolsun
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtBursAd.Text = dr["BursAdı"].ToString();
                txtMiktar.Text = dr["Miktar"].ToString();
                txtKontenjan.Text = dr["Kontenjan"].ToString();
                txtAciklama.Text = dr["Aciklama"].ToString();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var secilenID = gridView1.GetFocusedRowCellValue("ID"); // Sütun adının ID olduğundan emin ol
            if (secilenID == null) return;

            if (XtraMessageBox.Show("Bu burs türünü silmek istiyor musunuz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = bgl.baglanti();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Burslar WHERE ID=@p1", conn);
                    cmd.Parameters.AddWithValue("@p1", secilenID);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    XtraMessageBox.Show("Burs başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                    FormuTemizle();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Silme hatası: " + ex.Message);
                }
            }
        }

        void FormuTemizle()
        {
            txtBursAd.Text = "";
            txtMiktar.Text = "";
            txtKontenjan.Text = "";
            txtAciklama.Text = "";
        }

        private void btnBursTanimla_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBursAd.Text) || string.IsNullOrEmpty(txtMiktar.Text))
            {
                XtraMessageBox.Show("Lütfen burs adını ve miktarını giriniz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection conn = bgl.baglanti();
                // SQL Sütun isimlerinin [BursAdı] falan doğru olduğundan emin ol kanka
                string sorgu = "INSERT INTO Burslar (BursAdı, Miktar, Kontenjan, Aciklama) VALUES (@p1, @p2, @p3, @p4)";

                using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                {
                    cmd.Parameters.AddWithValue("@p1", txtBursAd.Text);
                    cmd.Parameters.AddWithValue("@p2", decimal.Parse(txtMiktar.Text.Replace(".", ",")));
                    cmd.Parameters.AddWithValue("@p3", int.Parse(txtKontenjan.Text));
                    cmd.Parameters.AddWithValue("@p4", txtAciklama.Text ?? "");
                    cmd.ExecuteNonQuery();
                }
                conn.Close();

                XtraMessageBox.Show("Burs başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                Listele();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Kaydetme hatası: " + ex.Message);
            }
        }
    }
}