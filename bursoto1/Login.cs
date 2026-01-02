using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // Sql kütüphanesi yeterli
using DevExpress.XtraEditors; // XtraMessageBox kullanabilmek için
using bursoto1.Helpers; // MessageHelper için

namespace bursoto1
{
    public partial class Login : Form
    {
        // Kanka bağlantı sınıfımızı çağırdık
        public SqlBaglanti bgl = new SqlBaglanti();

        public Login()
        {
            InitializeComponent();
            // Modern UI ayarları
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(20);
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. ADIM: Bağlantıyı alıp komutu hazırlıyoruz
                // bgl.baglanti() zaten açık bağlantı döndürüyor
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM login WHERE K_adi=@p1 AND Sifre=@p2", conn);
                    cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@p2", txtSifre.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 2. ADIM: Eğer kullanıcı varsa
                        if (dr.Read())
                        {
                            // Giriş başarılıysa Ana Menü formunu açıyoruz
                            MainForm anaMenu = new MainForm();
                            anaMenu.Show();
                            this.Hide(); // Login formunu gizle
                        }
                        else
                        {
                            MessageHelper.ShowError("Kullanıcı adı veya şifre hatalı. Lütfen bilgilerinizi kontrol ediniz.", "Giriş Hatası");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Bağlantı Hatası");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde kullanıcı adı alanına odaklan
            txtKullaniciAdi.Focus();
        }

        // Designer'da bağlı event handler'lar (boş bırakıldı)
        private void txtKullaniciAdi_EditValueChanged(object sender, EventArgs e) { }
        private void txtSifre_EditValueChanged(object sender, EventArgs e) { }
        private void labelKullaniciAdi_Click(object sender, EventArgs e) { }
        private void labelSifre_Click(object sender, EventArgs e) { }
        private void svgPerson_Click(object sender, EventArgs e) { }
        private void svgKey_Click(object sender, EventArgs e) { }
    }
}