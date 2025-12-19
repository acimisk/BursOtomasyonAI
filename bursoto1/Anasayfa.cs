using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class Anasayfa : Form
    {
        public Anasayfa()
        {
            InitializeComponent();
        }

        // Veritabanı bağlantısı (Senin bağlantı cümlen)
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");

        void IstatistikleriGetir()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                // 1. Toplam Öğrenci Sayısı
                SqlCommand komut1 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", conn);
                lblToplamOgrenci.Text = komut1.ExecuteScalar().ToString() + " Kayıtlı Öğrenci";

                // 2. Toplam Burs Yükü (Hane gelirleri üzerinden bir hesaplama örneği)
                SqlCommand komut2 = new SqlCommand("SELECT SUM([TOPLAM HANE GELİRİ]) FROM Ogrenciler", conn);
                var sonucBurs = komut2.ExecuteScalar();
                lblToplamBurs.Text = (sonucBurs != DBNull.Value) ? string.Format("{0:C2}", sonucBurs) : "0,00 TL";

                // 3. AGNO'su en yüksek olan öğrenciyi getir
                SqlCommand komut3 = new SqlCommand("SELECT TOP 1 (AD + ' ' + SOYAD) FROM Ogrenciler ORDER BY AGNO DESC", conn);
                var sonucBasari = komut3.ExecuteScalar();
                lblEnBasarili.Text = (sonucBasari != null) ? sonucBasari.ToString() : "Veri Yok";

                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn.State == ConnectionState.Open) conn.Close();
                MessageBox.Show("İstatistik hatası kanka: " + ex.Message);
            }
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            // Form açıldığında sayıları hemen güncelle
            IstatistikleriGetir();
        }
    }
}