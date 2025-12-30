using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using bursoto1.Helpers; // MessageHelper için

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
                // Bağlantıyı alıyoruz
                using (SqlConnection aktifBaglanti = bgl.baglanti())
                {

                // --- 1. VERİLERİ SQL'DEN ÇEKME ---

                // Toplam Öğrenci Sayısı
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", aktifBaglanti);
                string toplamOgr = cmd1.ExecuteScalar().ToString();

                // Toplam Burs Yükü (TL)
                SqlCommand cmd2 = new SqlCommand("SELECT SUM([TOPLAM HANE GELİRİ]) FROM Ogrenciler", aktifBaglanti);
                object sonucBurs = cmd2.ExecuteScalar();
                string bursMiktari = (sonucBurs != DBNull.Value) ? string.Format("{0:C0}", sonucBurs) : "0 ₺";

                // TOPLAM GELİR (Onaylanmış bağışlar)
                SqlCommand cmdGelir = new SqlCommand("SELECT SUM(BagisMiktari) FROM BursVerenler WHERE Durum = 'Onaylandı'", aktifBaglanti);
                object sonucGelir = cmdGelir.ExecuteScalar();
                decimal toplamGelir = (sonucGelir != DBNull.Value) ? Convert.ToDecimal(sonucGelir) : 0;

                // TOPLAM GİDER (Öğrencilere gönderilen burslar)
                decimal toplamGider = 0;
                try
                {
                    SqlCommand cmdGider = new SqlCommand("SELECT SUM(Tutar) FROM BursGiderleri", aktifBaglanti);
                    object sonucGider = cmdGider.ExecuteScalar();
                    toplamGider = (sonucGider != DBNull.Value) ? Convert.ToDecimal(sonucGider) : 0;
                }
                catch
                {
                    // BursGiderleri tablosu yoksa, gider 0 olarak kalır
                    toplamGider = 0;
                }

                // NET KASA (Gelir - Gider)
                decimal netKasa = toplamGelir - toplamGider;
                string toplamKasa = string.Format("{0:C0}", netKasa);
                string gelirStr = string.Format("{0:C0}", toplamGelir);
                string giderStr = string.Format("{0:C0}", toplamGider);

                // Burs Verilen Öğrenci Sayısı (Puanı olanlar burs almış kabul edilsin)
                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE AISkor IS NOT NULL", aktifBaglanti);
                string bursluOgr = cmd3.ExecuteScalar().ToString();

                    // En Başarılı Öğrenci ve AGNO
                    SqlCommand cmd4 = new SqlCommand("SELECT TOP 1 AD + ' ' + SOYAD, AGNO FROM Ogrenciler ORDER BY AGNO DESC", aktifBaglanti);
                    string kralOgrenci = "-";
                    decimal maxAgno = 0;

                    using (SqlDataReader dr = cmd4.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            kralOgrenci = dr[0].ToString();
                            maxAgno = Convert.ToDecimal(dr[1]);
                        }
                    }

                    // --- 2. TILE ELEMENTLERİNİ GÜZELLEŞTİRME ---

                // Ortak Element Ayarlayıcı Metot (Aşağıda tanımladık)
                TileAyarla(tileItemOgrenci, "TOPLAM ÖĞRENCİ", toplamOgr, Color.FromArgb(41, 128, 185));
                TileAyarla(tileItemBurs, "TOPLAM BURS HACMİ", bursMiktari, Color.FromArgb(39, 174, 96));
                
                // GELİR ve GİDER tile'ları
                TileAyarla(tileItemGelir, "TOPLAM GELİR", gelirStr, Color.FromArgb(46, 204, 113));
                TileAyarla(tileItemGider, "TOPLAM GİDER", giderStr, Color.FromArgb(231, 76, 60));
                
                // NET KASA (Gelir - Gider) - Renk duruma göre değişir
                Color kasaRengi = netKasa >= 0 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(231, 76, 60);
                TileAyarla(tileItemKasa, "NET KASA", toplamKasa, kasaRengi);

                    // Yeni İstediğin: Burslu Sayısı
                    // Eğer tasarımda 4. bir Tile eklediysen adını 'tileItemBursluSayisi' yapabilirsin
                    // Şimdilik bunu mevcut bir Tile'ın altına küçük yazı olarak da ekleyebiliriz.

                    // Başarı Kutusu Özel Ayarı (Gold Efekti)
                    if (maxAgno >= 3.80m)
                    {
                        TileAyarla(tileItemBasari, "★ OKUL BİRİNCİSİ", kralOgrenci + " (" + maxAgno + ")", Color.Gold);
                        tileItemBasari.AppearanceItem.Normal.ForeColor = Color.Black;
                    }
                    else
                    {
                        TileAyarla(tileItemBasari, "LİDER ÖĞRENCİ", kralOgrenci, Color.FromArgb(142, 68, 173));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Dashboard Hatası");
            }
        }

        // Kanka kod tekrarını engellemek için bu yardımcı metodu da sınıfın içine ekle:
        void TileAyarla(DevExpress.XtraEditors.TileItem item, string baslik, string deger, Color renk)
        {
            item.Elements.Clear();
            item.AppearanceItem.Normal.BackColor = renk;
            item.AppearanceItem.Normal.BorderColor = renk;

            // Başlık Elementi
            TileItemElement elBaslik = new TileItemElement();
            elBaslik.Text = baslik;
            elBaslik.TextAlignment = TileItemContentAlignment.TopLeft;
            elBaslik.Appearance.Normal.FontSizeDelta = -1; // Biraz küçük olsun

            // Değer Elementi
            TileItemElement elDeger = new TileItemElement();
            elDeger.Text = deger;
            elDeger.TextAlignment = TileItemContentAlignment.MiddleCenter;
            elDeger.Appearance.Normal.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            item.Elements.Add(elBaslik);
            item.Elements.Add(elDeger);
        }

        void BolumGrafiginiCiz()
        {
            try
            {
                using (SqlConnection aktifBaglanti = bgl.baglanti())
                {

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
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Grafik Çizim Hatası");
            }
        }

        private void tileItemBurs_ItemClick(object sender, TileItemEventArgs e)
        {

        }
    }
}