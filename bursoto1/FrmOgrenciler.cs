using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms; // Standart ComboBox için

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
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Ogrenciler", bgl.baglanti());
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Listeleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                SqlConnection conn = bgl.baglanti();
                // DISTINCT: Aynı bölümler tekrar etmesin
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT BolumAdi FROM Bolumler ORDER BY BolumAdi ASC", conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    // Büyük harf standardı
                    txtBolum.Items.Add(dr[0].ToString().ToUpper());
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bölümler yüklenirken hata oluştu: " + ex.Message);
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
                    XtraMessageBox.Show("Lütfen listeden geçerli bir bölüm seçiniz.", "Geçersiz Bölüm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                XtraMessageBox.Show("Lütfen listeden geçerli bir bölüm seçiniz.", "Hatalı Bölüm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSinif.Text)) { UyariVer("Sınıf"); return; }
            if (string.IsNullOrWhiteSpace(txtTelNo.Text)) { UyariVer("Telefon"); return; }

            if (string.IsNullOrWhiteSpace(txtHaneGeliri.Text) || !decimal.TryParse(txtHaneGeliri.Text, out decimal gelir))
            {
                XtraMessageBox.Show("Lütfen 'Hane Geliri' alanına geçerli bir sayı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // AGNO Dönüşümü (Nokta -> Virgül)
            string agnoRaw = txtAgno.Text.Replace(".", ",");
            if (string.IsNullOrWhiteSpace(agnoRaw) || !decimal.TryParse(agnoRaw, out decimal agno))
            {
                XtraMessageBox.Show("Lütfen 'AGNO' alanına geçerli bir not giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (agno < 0 || agno > 4)
            {
                XtraMessageBox.Show("AGNO 0 ile 4 arasında olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Kardeş sayısı string olarak gidiyor ("4+" destekler)
                string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

                SqlCommand cmd = new SqlCommand(sorgu, bgl.baglanti());

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

                XtraMessageBox.Show("Öğrenci sisteme başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
                Temizle();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Kayıt işlemi sırasında hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UyariVer(string alan)
        {
            XtraMessageBox.Show($"Lütfen '{alan}' alanını doldurunuz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // 4. SİL
        public void btnSil_Click(object sender, EventArgs e)
        {
            int[] seciliSatirlar = gridView1.GetSelectedRows();

            if (seciliSatirlar.Length == 0)
            {
                XtraMessageBox.Show("Lütfen silinecek kayıtları seçiniz.", "Seçim Yapılmadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (XtraMessageBox.Show($"{seciliSatirlar.Length} adet kaydı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = bgl.baglanti();
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
                    conn.Close();

                    XtraMessageBox.Show("Seçilen kayıtlar başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Listele();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Silme hatası: " + ex.Message);
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
                    byte[] imageArray = File.ReadAllBytes(ofd.FileName);
                    dosyaYolu = "data:image/png;base64," + Convert.ToBase64String(imageArray);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Resim yüklenirken hata: " + ex.Message);
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