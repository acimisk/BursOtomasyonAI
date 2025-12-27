using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class Anasayfa : Form
    {
        // Kanka sınıfı çağırdık
        SqlBaglanti bgl = new SqlBaglanti();

        public Anasayfa()
        {
            InitializeComponent();
        }

        private void Anasayfa_Load(object sender, EventArgs e)
        {
            DashboardVerileriniGetir();
            BolumGrafiginiCiz();
        }

        void DashboardVerileriniGetir()
        {
            try
            {
                // DÜZELTME: bgl.baglanti() metodunu çağırıyoruz
                SqlConnection aktifBaglanti = bgl.baglanti();

                // 1. Tile: Toplam Öğrenci
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", aktifBaglanti);
                string ogrSayisi = cmd1.ExecuteScalar().ToString();

                tileItemOgrenci.Text = "Toplam Öğrenci";
                tileItemOgrenci.Elements[0].Text = ogrSayisi;
                tileItemOgrenci.AppearanceItem.Normal.BackColor = Color.FromArgb(41, 128, 185);

                // 2. Tile: Toplam Burs Yükü
                SqlCommand cmd2 = new SqlCommand("SELECT SUM([TOPLAM HANE GELİRİ]) FROM Ogrenciler", aktifBaglanti);
                object sonucBurs = cmd2.ExecuteScalar();
                string bursMiktari = (sonucBurs != DBNull.Value) ? string.Format("{0:C0}", sonucBurs) : "0 ₺";

                tileItemBurs.Text = "Toplam Burs Hacmi";
                tileItemBurs.Elements[0].Text = bursMiktari;
                tileItemBurs.AppearanceItem.Normal.BackColor = Color.FromArgb(39, 174, 96);

                // 3. Tile: En Başarılı Öğrenci
                SqlCommand cmd3 = new SqlCommand("SELECT TOP 1 AD + ' ' + SOYAD FROM Ogrenciler ORDER BY AGNO DESC", aktifBaglanti);
                object kralOgrenci = cmd3.ExecuteScalar();

                tileItemBasari.Text = "Okul Birincisi";
                tileItemBasari.Elements[0].Text = (kralOgrenci != null) ? kralOgrenci.ToString() : "-";
                tileItemBasari.AppearanceItem.Normal.BackColor = Color.FromArgb(142, 68, 173);

                // En yüksek AGNO'yu çekelim
                SqlCommand cmdAgno = new SqlCommand("SELECT MAX(AGNO) FROM Ogrenciler", bgl.baglanti());
                object maxAgnoObj = cmdAgno.ExecuteScalar();

                if (maxAgnoObj != DBNull.Value && maxAgnoObj != null)
                {
                    decimal maxAgno = Convert.ToDecimal(maxAgnoObj);

                    // Eğer ortalama 3.80 üzerindeyse kutu altın rengi (Gold) olsun
                    if (maxAgno >= 3.80m)
                    {
                        tileItemBasari.AppearanceItem.Normal.BackColor = Color.Gold;
                        tileItemBasari.AppearanceItem.Normal.ForeColor = Color.Black; // Yazı siyah olsun ki okunsun
                        tileItemBasari.Elements[0].Text = "★ " + tileItemBasari.Elements[0].Text + " (Üstün Başarı)";
                    }
                }


                // İşlem bitince kapatıyoruz
                aktifBaglanti.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Dashboard yüklenirken hata kanka: " + ex.Message);
            }
        }

        void BolumGrafiginiCiz()
        {
            try
            {
                SqlConnection aktifBaglanti = bgl.baglanti();

                SqlDataAdapter da = new SqlDataAdapter("SELECT BÖLÜMÜ, COUNT(*) as Sayi FROM Ogrenciler WHERE BÖLÜMÜ IS NOT NULL GROUP BY BÖLÜMÜ", aktifBaglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count == 0) return;

                chartControl1.Series.Clear();
                Series seri = new Series("Öğrenci Dağılımı", ViewType.Pie);

                foreach (DataRow dr in dt.Rows)
                {
                    string bolum = dr["BÖLÜMÜ"].ToString();
                    if (string.IsNullOrEmpty(bolum)) bolum = "Belirtilmemiş";
                    double adet = Convert.ToDouble(dr["Sayi"]);
                    seri.Points.Add(new SeriesPoint(bolum, adet));
                }

                chartControl1.Series.Add(seri);
                seri.Label.TextPattern = "{A}: {V}";
                seri.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                aktifBaglanti.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Grafik Çizim Hatası kanka: " + ex.Message);
            }
        }
    }
}