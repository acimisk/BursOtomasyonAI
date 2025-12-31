using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1.Modules
{
    public partial class AnasayfaModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public AnasayfaModule()
        {
            InitializeComponent();
        }

        private void AnasayfaModule_Load(object sender, EventArgs e)
        {
            VerileriGetir();
            GrafikleriCiz();
        }

        void VerileriGetir()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // 1. TILE 1: Toplam Öğrenci
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", conn);
                    string ogrSayisi = cmd1.ExecuteScalar().ToString();

                    // Designer'daki 'tileItem1' -> Öğrenci Kartı
                    tileItemOgrenci.Elements[0].Text = "Toplam Öğrenci";
                    tileItemOgrenci.Elements[1].Text = ogrSayisi;
                    tileItemOgrenci.AppearanceItem.Normal.BackColor = Color.FromArgb(41, 128, 185); // Mavi

                    // 2. TILE 2: Burs Alanlar
                    SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM OgrenciBurslari WHERE Durum=1", conn);
                    string bursiyerSayisi = cmd2.ExecuteScalar().ToString();

                    // Designer'daki 'tileItem2' -> Bursiyer Kartı
                    tileItemBurs.Elements[0].Text = "Burs Alanlar";
                    tileItemBurs.Elements[1].Text = bursiyerSayisi;
                    tileItemBurs.AppearanceItem.Normal.BackColor = Color.FromArgb(39, 174, 96); // Yeşil

                    // 3. TILE 3: Bağışçılar
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM BursVerenler", conn);
                    string bagisciSayisi = cmd3.ExecuteScalar().ToString();

                    
                }
            }
            catch (Exception ex)
            {
                // Hata olursa boş geçmesin
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void GrafikleriCiz()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT BÖLÜMÜ, COUNT(*) as Sayi FROM Ogrenciler WHERE BÖLÜMÜ IS NOT NULL GROUP BY BÖLÜMÜ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (chartControl1.Series.Count > 0)
                    {
                        chartControl1.Series[0].Points.Clear();

                        // Seri ayarları
                        chartControl1.Series[0].Name = "Bölümler";
                        chartControl1.Series[0].View = new DoughnutSeriesView(); // Modern halka grafik

                        foreach (DataRow dr in dt.Rows)
                        {
                            string bolum = dr["BÖLÜMÜ"].ToString();
                            double sayi = Convert.ToDouble(dr["Sayi"]);
                            chartControl1.Series[0].Points.Add(new SeriesPoint(bolum, sayi));
                        }
                    }
                }
            }
            catch { }
        }
    }
}