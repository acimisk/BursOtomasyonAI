using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class Ara : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");
        public Ara()
        {
            InitializeComponent();
        }

        private void btnSorgula_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Temel Sorgu Başlangıcı
                // WHERE 1=1 hilesi, arkasından gelecek tüm "AND" ifadelerini güvenle bağlamamızı sağlar.
                string sorgu = "SELECT * FROM Ogrenciler WHERE 1=1";

                // 2. Dinamik Sorgu Oluşturma
                if (!string.IsNullOrEmpty(txtAraAd.Text))
                    sorgu += " AND AD LIKE @p1";

                if (!string.IsNullOrEmpty(txtAraSoyad.Text))
                    sorgu += " AND SOYAD LIKE @p2";

                if (!string.IsNullOrEmpty(txtAraBolum.Text))
                    sorgu += " AND BÖLÜMÜ LIKE @p3"; // İşte burası tüm bölümlerin çıkmasını engelleyecek

                if (!string.IsNullOrEmpty(txtTelNo.Text))
                    sorgu += " AND TELEFON LIKE @p4";

                if (!string.IsNullOrEmpty(txtSınıf.Text))
                    sorgu += " AND SINIF LIKE @p5";

                // 3. SQL Komutunu Hazırlama
                SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);

                // 4. Parametreleri Tanımlama
                // Not: LIKE kullandığımız için başına ve sonuna % ekliyoruz ki "içinde geçeni" bulsun.
                da.SelectCommand.Parameters.AddWithValue("@p1", "%" + txtAraAd.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@p2", "%" + txtAraSoyad.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@p3", "%" + txtAraBolum.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@p4", "%" + txtTelNo.Text + "%");
                da.SelectCommand.Parameters.AddWithValue("@p5", "%" + txtSınıf.Text + "%");

                // 5. Veriyi Çek ve Grid'e Bas
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridAraSonuc.DataSource = dt;

                // Kaç kayıt bulunduğunu kullanıcıya söyleyelim (Motivasyon olur)
                if (dt.Rows.Count > 0)
                    this.Text = $"Arama Sonucu: {dt.Rows.Count} Öğrenci Bulundu";
                else
                    MessageBox.Show("Aranan kriterlere uygun öğrenci bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgulama hatası: " + ex.Message);
            }

        }

        private void Ara_Load(object sender, EventArgs e)
        {

        }
    }
}
