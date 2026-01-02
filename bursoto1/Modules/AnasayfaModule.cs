using DevExpress.XtraEditors;
using DevExpress.XtraCharts;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using bursoto1.Helpers;
using DevExpress.XtraCharts.Native;

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
            RefreshDashboard();
        }

        // Navigasyon sırasında çağrılır - dashboard verilerini yenile
        public new void Refresh()
        {
            RefreshDashboard();
        }

        private void RefreshDashboard()
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
                    // --- 1. VERİLERİ SQL'DEN ÇEKME ---

                    // Toplam Öğrenci Sayısı
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler", conn);
                    string toplamOgr = cmd1.ExecuteScalar().ToString();

                    // Toplam Burs Yükü (Hane Geliri Toplamı)
                    SqlCommand cmd2 = new SqlCommand("SELECT SUM([TOPLAM HANE GELİRİ]) FROM Ogrenciler", conn);
                    object sonucBurs = cmd2.ExecuteScalar();
                    string bursMiktari = (sonucBurs != DBNull.Value && sonucBurs != null) ? string.Format("{0:C0}", sonucBurs) : "0 ₺";

                    // TOPLAM GELİR (Onaylanmış bağışlar)
                    SqlCommand cmdGelir = new SqlCommand("SELECT SUM(BagisMiktari) FROM BursVerenler WHERE Durum = 'Onaylandı'", conn);
                    object sonucGelir = cmdGelir.ExecuteScalar();
                    decimal toplamGelir = (sonucGelir != DBNull.Value && sonucGelir != null) ? Convert.ToDecimal(sonucGelir) : 0;

                    // TOPLAM GİDER (Öğrencilere gönderilen burslar)
                    decimal toplamGider = 0;
                    try
                    {
                        SqlCommand cmdGider = new SqlCommand("SELECT SUM(Tutar) FROM BursGiderleri", conn);
                        object sonucGider = cmdGider.ExecuteScalar();
                        toplamGider = (sonucGider != DBNull.Value && sonucGider != null) ? Convert.ToDecimal(sonucGider) : 0;
                    }
                    catch
                    {
                        toplamGider = 0;
                    }

                    // NET KASA (Gelir - Gider)
                    decimal netKasa = toplamGelir - toplamGider;
                    string toplamKasa = string.Format("{0:C0}", netKasa);
                    string gelirStr = string.Format("{0:C0}", toplamGelir);
                    string giderStr = string.Format("{0:C0}", toplamGider);

                    // Burs Verilen Öğrenci Sayısı
                    SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM OgrenciBurslari WHERE Durum=1", conn);
                    string bursluOgr = cmd3.ExecuteScalar().ToString();

                    // En Başarılı Öğrenci ve AGNO
                    SqlCommand cmd4 = new SqlCommand("SELECT TOP 1 AD + ' ' + SOYAD, AGNO FROM Ogrenciler WHERE AGNO IS NOT NULL ORDER BY AGNO DESC", conn);
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

                    // Toplam Öğrenci
                    TileAyarla(tileItemOgrenci, "TOPLAM ÖĞRENCİ", toplamOgr, Color.FromArgb(41, 128, 185));

                    // Burs Alanlar
                    TileAyarla(tileItemBurs, "BURS ALANLAR", bursluOgr, Color.FromArgb(39, 174, 96));
                    
                    // GELİR ve GİDER tile'ları
                    TileAyarla(tileItemGelir, "TOPLAM GELİR", gelirStr, Color.FromArgb(46, 204, 113));
                    TileAyarla(tileItemGider, "TOPLAM GİDER", giderStr, Color.FromArgb(231, 76, 60));
                    
                    // NET KASA (Gelir - Gider) - Renk duruma göre değişir
                    Color kasaRengi = netKasa >= 0 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(231, 76, 60);
                    TileAyarla(tileItemKasa, "NET KASA", toplamKasa, kasaRengi);

                    // Başarı Kutusu Özel Ayarı (Gold Efekti)
                    if (maxAgno >= 3.80m)
                    {
                        TileAyarla(tileItemBasari, "★ OKUL BİRİNCİSİ", kralOgrenci + "\n" + maxAgno.ToString("F2"), Color.Gold);
                        tileItemBasari.AppearanceItem.Normal.ForeColor = Color.Black;
                    }
                    else if (maxAgno > 0)
                    {
                        TileAyarla(tileItemBasari, "LİDER ÖĞRENCİ", kralOgrenci + "\n" + maxAgno.ToString("F2"), Color.FromArgb(142, 68, 173));
                    }
                    else
                    {
                        TileAyarla(tileItemBasari, "LİDER ÖĞRENCİ", "Henüz veri yok", Color.FromArgb(142, 68, 173));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Dashboard Yükleme Hatası");
            }
        }

        void GrafikleriCiz()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Bölüm dağılımı grafiği
                    SqlDataAdapter da = new SqlDataAdapter("SELECT BÖLÜMÜ, COUNT(*) as Sayi FROM Ogrenciler WHERE BÖLÜMÜ IS NOT NULL GROUP BY BÖLÜMÜ", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0 && chartControl1 != null)
                    {
                        chartControl1.Series.Clear();

                        // Seri oluştur
                        Series seri = new Series("Öğrenci Dağılımı", ViewType.Doughnut);
                        seri.Label.TextPattern = "{A}: {V}";
                        seri.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        foreach (DataRow dr in dt.Rows)
                        {
                            string bolum = dr["BÖLÜMÜ"].ToString();
                            if (string.IsNullOrEmpty(bolum)) bolum = "Belirtilmemiş";
                            double sayi = Convert.ToDouble(dr["Sayi"]);
                            seri.Points.Add(new SeriesPoint(bolum, sayi));
                        }

                        chartControl1.Series.Add(seri);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Grafik Çizme Hatası");
            }
        }

        // Tile ayarlama yardımcı metodu (ported from Anasayfa.cs)
        void TileAyarla(DevExpress.XtraEditors.TileItem item, string baslik, string deger, Color renk)
        {
            if (item == null) return;

            item.Elements.Clear();
            item.AppearanceItem.Normal.BackColor = renk;
            item.AppearanceItem.Normal.BorderColor = renk;

            // Başlık Elementi
            DevExpress.XtraEditors.TileItemElement elBaslik = new DevExpress.XtraEditors.TileItemElement();
            elBaslik.Text = baslik;
            elBaslik.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            elBaslik.Appearance.Normal.FontSizeDelta = -1;
            elBaslik.Appearance.Normal.ForeColor = Color.White;

            // Değer Elementi
            DevExpress.XtraEditors.TileItemElement elDeger = new DevExpress.XtraEditors.TileItemElement();
            elDeger.Text = deger;
            elDeger.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            elDeger.Appearance.Normal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            elDeger.Appearance.Normal.ForeColor = Color.White;

            item.Elements.Add(elBaslik);
            item.Elements.Add(elDeger);
        }

        private void tileControl2_Click(object sender, EventArgs e)
        {

        }

        private void tileItemOgrenci_ItemClick(object sender, TileItemEventArgs e)
        {

        }
    }
}