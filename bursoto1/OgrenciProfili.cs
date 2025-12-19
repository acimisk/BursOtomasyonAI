using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class OgrenciProfili : Form
    {
        public string ad, soyad, maas,fotoYolu,agno,haneGeliri,bolum,kardesSayisi,sinif,telNo;
        public int secilenOgrenciID;
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");
        
        private async void btnAIAnaliz_Click(object sender, EventArgs e)
        {
            try
            {
                // HATALI KISIM BURASIYDI: Yeni form oluşturup grid'e bakıyordun.
                // ÇÖZÜM: Form açılırken gelen 'secilenOgrenciID' değişkenini kullanıyoruz.

                if (secilenOgrenciID <= 0)
                {
                    MessageBox.Show("Öğrenci ID bilgisi alınamadı!");
                    return;
                }

                GeminiAI ai = new GeminiAI();
                string veri = $"AD: {txtOgrAd.Text}, AGNO: {txtAgno.Text}, GELIR: {txtHaneGeliri.Text}, KARDES: {txtOgrKardesSayisi.Text}";

                rtbAnalizSonuc.Text = "Gemini analiz ediyor...";
                string sonuc = await ai.BursAnaliziYap(veri);
                rtbAnalizSonuc.Text = sonuc;

                var match = Regex.Match(sonuc, @"SKOR:\s*(\d+)");
                if (match.Success)
                {
                    int puan = int.Parse(match.Groups[1].Value);
                    txtAISkor.Text = puan.ToString();

                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor=@p1 WHERE ID=@p2", baglanti);
                    cmd.Parameters.AddWithValue("@p1", puan);
                    cmd.Parameters.AddWithValue("@p2", secilenOgrenciID); // Dışarıdan gelen ID

                    int etkilenenSatir = cmd.ExecuteNonQuery();
                    baglanti.Close();

                    if (etkilenenSatir > 0)
                    {
                        MessageBox.Show($"AI Skoru ({puan}) başarıyla veritabanına kaydedildi.");
                        // frm.Listele(); -> Bu satır hata verebilir çünkü frm yeni bir form. 
                        // Şimdilik burayı yorum satırı yap veya ana formdaki listeyi yenilemek için delegate kullanmalısın.
                    }
                    else
                    {
                        MessageBox.Show("Kayıt güncellenemedi (ID bulunamadı).");
                    }
                }
            }
            catch (Exception ex)
            {
                if (baglanti.State == ConnectionState.Open) baglanti.Close();
                MessageBox.Show("Hata: " + ex.Message);
            }
        }


        private void labelSinif_Click(object sender, EventArgs e)
        {

        }

        private void labelHaneGeliri_Click(object sender, EventArgs e)
        {

        }

        private void OgrenciProfili_Load(object sender, EventArgs e)
        {
            txtOgrAd.Text = ad;
            txtOgrSoyad.Text = soyad;
            txtHaneGeliri.Text = haneGeliri;
            txtAgno.Text = agno;
            txtBolum.Text=bolum;
            txtOgrKardesSayisi.Text = kardesSayisi;
            txtSinif.Text=sinif;
            txtTelNo.Text=telNo;
            // Sekmenin üzerinde "Ali Yılmaz - Profil" yazar. Çok elit durur.
            this.Text = ad + " " + soyad + " - Profil";
            if (!string.IsNullOrEmpty(fotoYolu))
            {
                pictureEdit1.Image = Image.FromFile(fotoYolu);
            }
        }

        public OgrenciProfili()
        {
            InitializeComponent();
        }

        public void txtOgrAd_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
