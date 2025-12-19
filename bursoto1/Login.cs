using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace bursoto1
{
    public partial class Login : Form
    {
        public SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");



        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void svgPerson_Click(object sender, EventArgs e)
        {

        }

        private void svgKey_Click(object sender, EventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {

        }

        private void btnGiris_Click(object sender, EventArgs e)
{
    // 1. ÖNLEM: Bağlantı zaten açık mı diye kontrol et, kapalıysa aç.
    if (conn.State == ConnectionState.Closed)
    {
        conn.Open();
    }

    string k_adi = txtKullaniciAdi.Text;
    string sifre = txtSifre.Text;
    bool kayitli_Mi = false;

    SqlCommand cmd = new SqlCommand("Select * from login", conn);
    SqlDataReader dr = cmd.ExecuteReader();

    while (dr.Read()) 
    {
        if (k_adi == dr["K_adi"].ToString() && sifre == dr["Sifre"].ToString())
        {
            kayitli_Mi = true;
            break;
        }
    }
    
    // DataReader ile işimiz bitti, onu da kapatalım (önemli!)
    dr.Close();

            if (kayitli_Mi == true) {
                Menu menu = new Menu();
                MessageBox.Show("Giriş Başarılı");
                menu.Show();
                this.Hide();
            } 
            else
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");

    // 2. ÖNLEM: İşlem bitti, bağlantıyı kapat.
    conn.Close(); 
}

        private void btnCikis_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
