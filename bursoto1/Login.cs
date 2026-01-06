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
    public partial class Login : XtraForm
    {
        // Kanka bağlantı sınıfımızı çağırdık
        public SqlBaglanti bgl = new SqlBaglanti();

        public Login()
        {
            InitializeComponent();
        }

        private void ApplyModernStyling()
        {
            // Form arka plan rengi - Ultra koyu antrasit
            this.BackColor = Color.FromArgb(28, 28, 30);
            
            // Form köşelerini yuvarlat (Region) - Load event'inde çağrılacak
            RoundFormCorners();
            
            // PanelControl'e gölge efekti ekle
            if (panelLogin != null)
            {
                panelLogin.Appearance.BackColor = Color.FromArgb(40, 40, 45);
                panelLogin.Appearance.BackColor2 = Color.FromArgb(35, 35, 40);
                panelLogin.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
                panelLogin.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            }
        }

        private void RoundFormCorners()
        {
            try
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = 15;
                int width = this.Width;
                int height = this.Height;
                
                path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
                path.AddArc(width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
                path.AddArc(width - radius * 2, height - radius * 2, radius * 2, radius * 2, 0, 90);
                path.AddArc(0, height - radius * 2, radius * 2, radius * 2, 90, 90);
                path.CloseAllFigures();
                this.Region = new Region(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Köşe yuvarlatma hatası: {ex.Message}");
            }
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
            // Modern stil uygula (form boyutları hazır olduktan sonra)
            ApplyModernStyling();
            
            // Form yüklendiğinde kullanıcı adı alanına odaklan
            txtKullaniciAdi.Focus();
            
            // SVG ikonları yükle
            LoadSvgIcons();
        }

        private void LoadSvgIcons()
        {
            try
            {
                // DevExpress 25.1 için SVG yükleme
                // SVG'ler resources dosyasına eklenebilir veya FromFile ile yüklenebilir
                // Şimdilik SVG ikonları opsiyonel - görsel olarak ikonlar olmadan da çalışır
                // İsterseniz SVG dosyalarını resources'a ekleyip şu şekilde yükleyebilirsiniz:
                // svgPerson.SvgImage = DevExpress.Utils.Svg.SvgImage.FromResources("bursoto1.Resources.person.svg", typeof(Login).Assembly);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"SVG yükleme hatası: {ex.Message}");
            }
        }

        // Designer'da bağlı event handler'lar (boş bırakıldı)
        private void txtKullaniciAdi_EditValueChanged(object sender, EventArgs e) { }
        private void txtSifre_EditValueChanged(object sender, EventArgs e) { }
        private void svgPerson_Click(object sender, EventArgs e) { }
        private void svgKey_Click(object sender, EventArgs e) { }
    }
}