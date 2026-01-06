using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using bursoto1.Helpers;

namespace bursoto1
{
    public partial class Login : XtraForm
    {
        public SqlBaglanti bgl = new SqlBaglanti();

        public Login()
        {
            InitializeComponent();
        }

        private void ApplyModernStyling()
        {
            // Form arka plan rengi - Koyu antrasit
            this.BackColor = Color.FromArgb(30, 30, 30);
            
            // Form köşelerini yuvarlat
            RoundFormCorners();
        }

        private void RoundFormCorners()
        {
            try
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = 20;
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
            // Uygulamayı güvenli şekilde kapat
            this.Close();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                // SQL bağlantı ve kontrol mantığı - DEĞİŞTİRİLMEDİ
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM login WHERE K_adi=@p1 AND Sifre=@p2", conn);
                    cmd.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@p2", txtSifre.Text);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MainForm anaMenu = new MainForm();
                            anaMenu.Show();
                            this.Hide();
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
            // Modern stil uygula
            ApplyModernStyling();
            
            // Material Design alt çizgileri ekle
            AddMaterialUnderlines();
            
            // Form kapanırken uygulamayı kapat
            this.FormClosing += Login_FormClosing;
            
            // Form yüklendiğinde kullanıcı adı alanına odaklan
            txtKullaniciAdi.Focus();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Login formu kapatılırsa uygulamayı tamamen kapat
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Application.Exit() tüm formları otomatik kapatır, thread-safe
                Application.Exit();
            }
        }

        private void AddMaterialUnderlines()
        {
            // Input alanlarının altına Material Design çizgileri ekle
            if (txtKullaniciAdi != null)
            {
                txtKullaniciAdi.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(Color.FromArgb(100, 100, 100), 1))
                    {
                        e.Graphics.DrawLine(pen, 0, txtKullaniciAdi.Height - 1, txtKullaniciAdi.Width, txtKullaniciAdi.Height - 1);
                    }
                };
            }

            if (txtSifre != null)
            {
                txtSifre.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(Color.FromArgb(100, 100, 100), 1))
                    {
                        e.Graphics.DrawLine(pen, 0, txtSifre.Height - 1, txtSifre.Width, txtSifre.Height - 1);
                    }
                };
            }
        }

        // Designer'da bağlı event handler'lar
        private void txtKullaniciAdi_EditValueChanged(object sender, EventArgs e) { }
        private void txtSifre_EditValueChanged(object sender, EventArgs e) { }
    }
}
