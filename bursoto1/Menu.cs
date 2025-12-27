using DevExpress.XtraBars;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
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
    public partial class Menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Menu()
        {
            InitializeComponent();

            // 1. İSTEK: TAM EKRAN YERİNE PENCERELİ VE ORTADA AÇILSIN
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            // 1. Ekranın tam ekran açılmasını KESİNLİKLE engelliyoruz
            this.WindowState = FormWindowState.Normal;

            // 2. Formun Windows başladığında nerede duracağını belirliyoruz (Tam Ortada)
            this.StartPosition = FormStartPosition.CenterScreen;

            // 3. Formun açılış boyutunu elle (Manuel) giriyoruz
            // Buradaki rakamları ekranına göre değiştirebilirsin (Örn: 1200x800)
            this.Size = new Size(1200, 650);

            this.FormBorderStyle = FormBorderStyle.Sizable; // Kenarlardan büyütüp küçültebilirsin
        }
        FrmOgrenciler fr1;
        private void btnOgrenciler_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                fr1 = new FrmOgrenciler();
                fr1.MdiParent=this;
                fr1.Show();
            }
            else
            {
                // Form zaten açıksa arkada kalmış olabilir, öne getirir
                fr1.Activate();
            }
            
        }
        
        SqlConnection baglanti = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");
        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 1. Önce FrmOgrenciler açık mı kontrol etmeliyiz
            if (fr1 != null && !fr1.IsDisposed)
            {
                try
                {
                    if (baglanti.State == ConnectionState.Closed) baglanti.Open();

                    string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                                   "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

                    using (SqlCommand komut = new SqlCommand(sorgu, baglanti))
                    {
                        // Parametreleri eklerken boş kalma ihtimaline karşı kontroller ekliyoruz
                        komut.Parameters.AddWithValue("@p1", fr1.txtOgrAd.Text ?? "");
                        komut.Parameters.AddWithValue("@p2", fr1.txtOgrSoyad.Text ?? "");

                        // Sayısal değerlerde boş kutu hatasını engellemek için 0 atıyoruz
                        int kardes = 0; int.TryParse(fr1.txtOgrKardesSayisi.Text, out kardes);
                        komut.Parameters.AddWithValue("@p3", kardes);

                        decimal haneGeliri = 0; decimal.TryParse(fr1.txtHaneGeliri.Text, out haneGeliri);
                        komut.Parameters.AddWithValue("@p4", haneGeliri);

                        komut.Parameters.AddWithValue("@p5", fr1.txtBolum.Text ?? "");
                        komut.Parameters.AddWithValue("@p6", fr1.txtSinif.Text ?? "");

                        decimal agno = 0; decimal.TryParse(fr1.txtAgno.Text, out agno);
                        komut.Parameters.AddWithValue("@p7", agno);

                        // --- KRİTİK NOKTA ---
                        // Burada sakın File.ReadAllBytes falan kullanma. 
                        // Web'den gelen o uzun string'i direkt gönderiyoruz.
                        komut.Parameters.AddWithValue("@p8", (object)fr1.dosyaYolu ?? DBNull.Value);

                        komut.Parameters.AddWithValue("@p9", fr1.txtTelNo.Text ?? "");

                        komut.ExecuteNonQuery();
                    }

                    baglanti.Close();

                    XtraMessageBox.Show("Öğrenci başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fr1.Listele();
                }
                catch (Exception ex)
                {
                    if (baglanti.State == ConnectionState.Open) baglanti.Close();
                    XtraMessageBox.Show("Hata oluştu kanka: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show("Önce 'Öğrenciler' sayfasını açmalısın kanka!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            // Program açılır açılmaz Anasayfa butonuna basılmış gibi davranır
            // sender ve e parametrelerine null gönderebiliriz, metot içindeki kodlar için sorun olmaz
            btnAnasayfa_ItemClick(null, null);
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult secim = XtraMessageBox.Show("Uygulamadan çıkmak istediğinize emin misiniz?",
                                         "Çıkış Onayı",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (secim == DialogResult.No)
            {
                e.Cancel = true; // Kapatma işlemini iptal et
            }
            else
            {
                Application.ExitThread(); // Uygulamayı tertemiz kapat
            }
        }
        // Eğer ismini 'baglanti' yaptıysan koddaki 'conn' yazılarını 'baglanti' olarak değiştir.
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");
        private void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 1. GridView'dan seçili olan satırın ID'sini alıyoruz
            var secilenID = fr1.gridView1.GetFocusedRowCellValue("ID");

            if (secilenID != null)
            {
                DialogResult onay = MessageBox.Show("Bu öğrenci kaydını silmek istediğinize emin misiniz?", "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (onay == DialogResult.Yes)
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", secilenID);
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        MessageBox.Show("Öğrenci başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fr1.Listele(); // Listeyi yenile ki silinen veri gitsin
                    }
                    catch (Exception ex)
                    {
                        if (conn.State == ConnectionState.Open) conn.Close();
                        MessageBox.Show("Hata oluştu: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz öğrenciyi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        Anasayfa frAna;
        private void btnAnasayfa_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frAna == null || frAna.IsDisposed)
            {
                frAna = new Anasayfa();
                frAna.MdiParent = this;
                frAna.Show();
            }
            else
            {
                frAna.Activate();
            }
        }
        Ara frAra;
        private void btnAra_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frAra == null || frAra.IsDisposed)
            {
                frAra = new Ara();
                frAra.MdiParent = this; // Ana formun içinde sekme olarak açar
                frAra.Show();
            }
            else
            {
                frAra.Activate();
            }
        }

        FrmBurslar frBurs;
        private void btnBursTurleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frBurs == null || frBurs.IsDisposed)
            {
                frBurs = new FrmBurslar();
                frBurs.MdiParent = this;
                frBurs.Show();
            }
            else
            {
                frBurs.Activate();
            }
        }

        private async void btnTopluAnaliz_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 1. Önce Öğrenciler sayfası açık mı kontrol et
            if (fr1 == null || fr1.IsDisposed)
            {
                XtraMessageBox.Show("Toplu analiz yapabilmek için önce 'Öğrenciler' sayfasını açmalısın kanka!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kullanıcıdan son bir onay al (API kotasını harcayabilir sonuçta)
            DialogResult onay = XtraMessageBox.Show("Puanı olmayan tüm öğrenciler analiz edilecek. Devam edilsin mi?", "Toplu Analiz Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                // Butonu geçici olarak kapatalım ki iki kere basılmasın kanka
                btnTopluAnaliz.Enabled = false;

                try
                {
                    // FrmOgrenciler formuna yazdığımız o canavar metodu çağırıyoruz
                    await fr1.TopluAnalizYap();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Hata oluştu: " + ex.Message);
                }
                finally
                {
                    btnTopluAnaliz.Enabled = true;
                }
            }
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }
    }
}