using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms; // Standart ComboBox için
using bursoto1.Helpers; // MessageHelper ve ImageHelper için

namespace bursoto1
{
    public partial class FrmOgrenciler : Form
    {
        SqlBaglanti bgl = new SqlBaglanti();
        public string dosyaYolu = "";

        public FrmOgrenciler()
        {
            InitializeComponent();
        }

        // 1. LİSTELEME
        public void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Ogrenciler", conn);
                    da.Fill(dt);
                }
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Listeleme Hatası");
            }
        }

        // 2. BÖLÜMLERİ GETİR (Akıllı Arama)
        public void BolumleriGetir()
        {
            try
            {
                txtBolum.Items.Clear();

                // Kullanıcı yazarken sistem tamamlasın (SuggestAppend)
                txtBolum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBolum.AutoCompleteSource = AutoCompleteSource.ListItems;

                using (SqlConnection conn = bgl.baglanti())
                {
                    // DISTINCT: Aynı bölümler tekrar etmesin
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT BolumAdi FROM Bolumler ORDER BY BolumAdi ASC", conn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Büyük harf standardı
                            txtBolum.Items.Add(dr[0].ToString().ToUpper());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Bölüm Yükleme Hatası");
            }
        }

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            Listele();
            BolumleriGetir();

            // Sınıf ve Kardeş Sayısı sadece seçilebilir olsun, elle yazılamasın
            txtSinif.DropDownStyle = ComboBoxStyle.DropDownList;
            txtOgrKardesSayisi.DropDownStyle = ComboBoxStyle.DropDownList;

            // Grid Çoklu Seçim
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            // Telefon Placeholder
            txtTelNo.Properties.NullText = "0(5xx) xxx xx xx";

            // Bölüm Kutusundan Çıkış Kontrolü (Hatalı yazımı engeller)
            txtBolum.Leave += TxtBolum_Leave;
        }

        // --- HATALI GİRİŞ ENGELLEYİCİ ---
        private void TxtBolum_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBolum.Text)) return;

            // Tam eşleşme var mı?
            int index = txtBolum.FindStringExact(txtBolum.Text.ToUpper());

            if (index == -1)
            {
                // Kısmi eşleşme var mı? (Örn: "YAZILI" -> "YAZILIM MÜH")
                index = txtBolum.FindString(txtBolum.Text.ToUpper());

                if (index != -1)
                {
                    txtBolum.SelectedIndex = index;
                }
                else
                {
                    MessageHelper.ShowWarning("Lütfen listeden geçerli bir bölüm seçiniz.", "Geçersiz Bölüm");
                    txtBolum.Text = "";
                    txtBolum.Focus();
                }
            }
        }

        // 3. KAYDET
        public void btnKaydet_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (string.IsNullOrWhiteSpace(txtOgrAd.Text)) { UyariVer("Ad"); return; }
            if (string.IsNullOrWhiteSpace(txtOgrSoyad.Text)) { UyariVer("Soyad"); return; }
            if (string.IsNullOrWhiteSpace(txtOgrKardesSayisi.Text)) { UyariVer("Kardeş Sayısı"); return; }

            // Bölüm listede var mı kontrolü
            if (string.IsNullOrWhiteSpace(txtBolum.Text) || !txtBolum.Items.Contains(txtBolum.Text))
            {
                MessageHelper.ShowWarning("Lütfen listeden geçerli bir bölüm seçiniz.", "Hatalı Bölüm");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSinif.Text)) { UyariVer("Sınıf"); return; }
            if (string.IsNullOrWhiteSpace(txtTelNo.Text)) { UyariVer("Telefon"); return; }

            if (string.IsNullOrWhiteSpace(txtHaneGeliri.Text) || !decimal.TryParse(txtHaneGeliri.Text, out decimal gelir))
            {
                MessageHelper.ShowWarning("Lütfen 'Hane Geliri' alanına geçerli bir sayı giriniz.", "Geçersiz Değer");
                return;
            }

            // AGNO Dönüşümü (Nokta -> Virgül)
            string agnoRaw = txtAgno.Text.Replace(".", ",");
            if (string.IsNullOrWhiteSpace(agnoRaw) || !decimal.TryParse(agnoRaw, out decimal agno))
            {
                MessageHelper.ShowWarning("Lütfen 'AGNO' alanına geçerli bir not giriniz.", "Geçersiz Değer");
                return;
            }
            if (agno < 0 || agno > 4)
            {
                MessageHelper.ShowWarning("AGNO 0 ile 4 arasında olmalıdır.", "Geçersiz Aralık");
                return;
            }

            try
            {
                // Kardeş sayısı string olarak gidiyor ("4+" destekler)
                string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand(sorgu, conn);
                    cmd.Parameters.AddWithValue("@p1", txtOgrAd.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@p2", txtOgrSoyad.Text.Trim().ToUpper());
                    cmd.Parameters.AddWithValue("@p3", txtOgrKardesSayisi.Text);
                    cmd.Parameters.AddWithValue("@p4", gelir);
                    cmd.Parameters.AddWithValue("@p5", txtBolum.Text);
                    cmd.Parameters.AddWithValue("@p6", txtSinif.Text);
                    cmd.Parameters.AddWithValue("@p7", agno);
                    cmd.Parameters.AddWithValue("@p8", dosyaYolu ?? "");
                    cmd.Parameters.AddWithValue("@p9", txtTelNo.Text.Trim());
                    cmd.ExecuteNonQuery();
                }

                MessageHelper.ShowSuccess("Öğrenci sisteme başarıyla kaydedildi.", "Kayıt Başarılı");
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Kayıt Hatası");
            }
        }

        private void UyariVer(string alan)
        {
            MessageHelper.ShowMissingField(alan);
        }

        // 4. SİL
        public void btnSil_Click(object sender, EventArgs e)
        {
            int[] seciliSatirlar = gridView1.GetSelectedRows();

            if (seciliSatirlar.Length == 0)
            {
                MessageHelper.ShowWarning("Lütfen silinecek kayıtları seçiniz.", "Seçim Yapılmadı");
                return;
            }

            if (MessageHelper.ShowConfirm($"{seciliSatirlar.Length} adet kaydı silmek istediğinize emin misiniz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        foreach (int rowHandle in seciliSatirlar)
                        {
                            var id = gridView1.GetRowCellValue(rowHandle, "ID");
                            if (id != null)
                            {
                                SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                                cmd.Parameters.AddWithValue("@p1", id);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageHelper.ShowSuccess("Seçilen kayıtlar başarıyla silindi.", "Silme Başarılı");
                    Listele();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        // 5. TEMİZLE
        public void Temizle()
        {
            txtOgrAd.Text = "";
            txtOgrSoyad.Text = "";
            txtOgrKardesSayisi.SelectedIndex = -1;
            txtHaneGeliri.Text = "";
            txtSinif.SelectedIndex = -1;
            txtBolum.Text = "";
            txtAgno.Text = "";
            txtTelNo.Text = "";
            pictureResim.Image = null;
            dosyaYolu = "";
        }

        // Resim Seçme
        private void btnResimSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları |*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureResim.Image = Image.FromFile(ofd.FileName);
                    dosyaYolu = ImageHelper.FileToBase64(ofd.FileName);
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Resim Yükleme Hatası");
                }
            }
        }

        // Profil Göster (Çift Tıklama veya Buton)
        private void btnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                OgrenciProfili frm = new OgrenciProfili();
                frm.MdiParent = this.MdiParent;
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);

                frm.ad = dr["AD"]?.ToString();
                frm.soyad = dr["SOYAD"]?.ToString();
                frm.haneGeliri = dr["TOPLAM HANE GELİRİ"]?.ToString();
                frm.fotoYolu = dr["FOTO"]?.ToString();
                frm.telNo = dr["TELEFON"]?.ToString();
                frm.bolum = dr["BÖLÜMÜ"]?.ToString();
                frm.sinif = dr["SINIF"]?.ToString();
                frm.kardesSayisi = dr["KARDEŞ SAYISI"]?.ToString();
                frm.agno = dr["AGNO"]?.ToString();

                frm.Show();
            }
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnGoster_Click(sender, e);
        }
    }
}