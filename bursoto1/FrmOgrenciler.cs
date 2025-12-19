using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
            sqlDataSource1.FillAsync();
        }

        public SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");

        // 1. ÇÖZÜM: Başına PUBLIC ekledik, artık Menu.cs bu metodu görebilir.
        public void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Ogrenciler", conn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        // 2. ÇÖZÜM: Değişkeni buraya PUBLIC olarak ekledik.
        public string dosyaYolu = "";

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            Listele();
        }
        public Image Base64ToImage(string base64String)
        {
            // Başındaki "data:image/png;base64," kısmını temizlememiz lazım
            string temizBase64 = base64String.Split(',')[1];
            byte[] imageBytes = Convert.FromBase64String(temizBase64);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                return Image.FromStream(ms, true);
            }
        }
        public async Task TopluAnalizYap()
        {
            GeminiAI ai = new GeminiAI();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ID, AD, SOYAD, AGNO, [TOPLAM HANE GELİRİ], [KARDEŞ SAYISI] FROM Ogrenciler WHERE AISkor IS NULL", conn);
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("Analiz edilecek öğrenci bulunamadı.");
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    string veri = $"AD: {dr["AD"]}, AGNO: {dr["AGNO"]}, GELIR: {dr["TOPLAM HANE GELİRİ"]}, KARDES: {dr["KARDEŞ SAYISI"]}";
                    string sonuc = await ai.BursAnaliziYap(veri);

                    // Gelen sonucu temizle (yıldızları, boşlukları vs.)
                    sonuc = sonuc.Replace("*", "").Trim();

                    // Regex'i daha esnek yapıyoruz: "SKOR" kelimesinden sonra gelen ilk sayıyı al
                    var match = Regex.Match(sonuc, @"SKOR[:\s-]*(\d+)", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        int puan = int.Parse(match.Groups[1].Value);
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        SqlCommand cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor=@p1 WHERE ID=@p2", conn);
                        cmd.Parameters.AddWithValue("@p1", puan);
                        cmd.Parameters.AddWithValue("@p2", dr["ID"]);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        // BURASI ÇOK KRİTİK: Eğer puanı bulamazsa Gemini ne demiş görelim
                        XtraMessageBox.Show($"Öğrenci: {dr["AD"]} için puan bulunamadı!\nGemini'nin cevabı: {sonuc}");
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Hata (Öğrenci {dr["AD"]}): " + ex.Message);
                }

                // Dakikada 15 istek sınırı için 4.5 saniye bekleme
                await Task.Delay(4500);
            }

            XtraMessageBox.Show("İşlem bitti, liste yenileniyor.");
            Listele();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            // Resim seçme mantığını buraya ekledik ki dosyaYolu dolsun
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları |*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureResim.Image = Image.FromFile(ofd.FileName);
                dosyaYolu = ofd.FileName; // Değişkeni güncelliyoruz
            }
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                OgrenciProfili frm = new OgrenciProfili();
                frm.MdiParent = this.MdiParent;

                // --- İŞTE EKSİK OLAN KRİTİK SATIR BURASI ---
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);
                // -------------------------------------------

                frm.ad = dr["AD"].ToString();
                frm.soyad = dr["SOYAD"].ToString();
                frm.haneGeliri = dr["TOPLAM HANE GELİRİ"].ToString();
                frm.fotoYolu = dr["FOTO"].ToString();
                frm.telNo = dr["TELEFON"].ToString();
                frm.bolum = dr["BÖLÜMÜ"].ToString();
                frm.sinif = dr["SINIF"].ToString();
                frm.kardesSayisi = dr["KARDEŞ SAYISI"].ToString();
                frm.agno = dr["AGNO"].ToString();

                frm.Show();
            }
        }

        // Grid'e çift tıklayınca da profil açılsın (opsiyonel)
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnGoster_Click(sender, e);
        }

        private void btnResimSec_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları |*.jpg;*.png;*.jpeg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 1. Ekranda göstermek için
                pictureResim.Image = Image.FromFile(ofd.FileName);

                // 2. DOSYA YOLUNU DEĞİL, RESMİN KENDİSİNİ BASE64 YAPIYORUZ
                byte[] imageArray = File.ReadAllBytes(ofd.FileName);
                string base64Image = Convert.ToBase64String(imageArray);

                // Web formatıyla aynı yapıyoruz:
                dosyaYolu = "data:image/png;base64," + base64Image;
                // Artık dosyaYolu değişkeninde 'C:\...' değil, resmin yazı hali var!
            }
        }
    }
}