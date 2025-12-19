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
                    baglanti.Open();

                    string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                                   "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

                    SqlCommand komut = new SqlCommand(sorgu, baglanti);

                    // 2. HATALARIN ÇÖZÜMÜ: Başına "fr1." ekliyoruz çünkü bu kutular fr1'in içinde!
                    komut.Parameters.AddWithValue("@p1", fr1.txtOgrAd.Text);
                    komut.Parameters.AddWithValue("@p2", fr1.txtOgrSoyad.Text);
                    komut.Parameters.AddWithValue("@p3", int.Parse(fr1.txtOgrKardesSayisi.Text));
                    komut.Parameters.AddWithValue("@p4", decimal.Parse(fr1.txtHaneGeliri.Text));
                    komut.Parameters.AddWithValue("@p5", fr1.txtBolum.Text);
                    komut.Parameters.AddWithValue("@p6", fr1.txtSinif.Text);
                    komut.Parameters.AddWithValue("@p7", decimal.Parse(fr1.txtAgno.Text));
                    komut.Parameters.AddWithValue("@p8", fr1.dosyaYolu); // FrmOgrenciler'deki değişken
                    komut.Parameters.AddWithValue("@p9", fr1.txtTelNo.Text);

                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    XtraMessageBox.Show("Öğrenci başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 3. Grid'i anında güncellemek için (FrmOgrenciler'deki Listele public olmalı)
                    fr1.Listele();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Hata oluştu kanka: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (baglanti.State == ConnectionState.Open) baglanti.Close();
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
                        if (conn.State == ConnectionState.Open) conn.Open();
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
    }
}