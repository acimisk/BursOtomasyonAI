using DevExpress.XtraGrid;
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

        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");

        public void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Burslar", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void FrmBurslar_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnBursEkle_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Burslar (BursAdı, Miktar, Kontenjan, Aciklama) VALUES (@p1, @p2, @p3, @p4)", conn);
                cmd.Parameters.AddWithValue("@p1", txtBursAd.Text);
                cmd.Parameters.AddWithValue("@p2", decimal.Parse(txtMiktar.Text));
                cmd.Parameters.AddWithValue("@p3", int.Parse(txtKontenjan.Text));
                cmd.Parameters.AddWithValue("@p4", txtAciklama.Text);
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Burs türü başarıyla tanımlandı!");
                Listele();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}