using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

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
        }

        void VerileriGetir()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // 1. Tile Verileri
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", conn);
                    string ogrSayisi = cmd.ExecuteScalar().ToString();

                    // Tile'lara basma işlemi (Designer'daki isimlere göre)
                    // tileItemOgrenci.Text = ogrSayisi; 
                    // ... diğer veriler ...

                    // 2. Grafik Verileri
                    SqlDataAdapter da = new SqlDataAdapter("SELECT BÖLÜMÜ, COUNT(*) as Sayi FROM Ogrenciler GROUP BY BÖLÜMÜ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    chartControl1.Series.Clear();
                    Series seri = new Series("Bölümler", ViewType.Doughnut); // Pasta yerine Doughnut daha modern

                    foreach (DataRow dr in dt.Rows)
                    {
                        seri.Points.Add(new SeriesPoint(dr["BÖLÜMÜ"].ToString(), dr["Sayi"]));
                    }
                    chartControl1.Series.Add(seri);
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
            }
        }
    }
}