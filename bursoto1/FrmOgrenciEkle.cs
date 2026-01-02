using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1
{
    public partial class FrmOgrenciEkle : XtraForm
    {
        SqlBaglanti bgl = new SqlBaglanti();
        public string dosyaYolu = "";
        private int? _editingOgrenciID = null; // Edit mode tracking (ported from master)

        public FrmOgrenciEkle()
        {
            InitializeComponent();
        }

        // Constructor overload for edit mode (ported from master pattern)
        public FrmOgrenciEkle(int ogrenciID) : this()
        {
            _editingOgrenciID = ogrenciID;
            this.Text = "Öğrenci Düzenle";
            LoadOgrenciData(ogrenciID);
        }

        // Load student data for editing (ported from master FrmOgrenciler pattern)
        private void LoadOgrenciData(int ogrenciID)
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Ogrenciler WHERE ID=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ogrenciID);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            txtOgrAd.Text = dr["AD"]?.ToString() ?? "";
                            txtOgrSoyad.Text = dr["SOYAD"]?.ToString() ?? "";
                            txtOgrKardesSayisi.Text = dr["KARDEŞ SAYISI"]?.ToString() ?? "";
                            txtHaneGeliri.Text = dr["TOPLAM HANE GELİRİ"]?.ToString() ?? "";
                            txtBolum.Text = dr["BÖLÜMÜ"]?.ToString() ?? "";
                            txtSinif.Text = dr["SINIF"]?.ToString() ?? "";
                            txtAgno.Text = dr["AGNO"]?.ToString() ?? "";
                            txtTelNo.Text = dr["TELEFON"]?.ToString() ?? "";
                            dosyaYolu = dr["FOTO"]?.ToString() ?? "";

                            // Load image if exists
                            if (!string.IsNullOrEmpty(dosyaYolu))
                            {
                                try
                                {
                                    pictureResim.Image = ImageHelper.Base64ToImage(dosyaYolu);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Öğrenci Verisi Yükleme Hatası");
            }
        }

        private void FrmOgrenciEkle_Load(object sender, EventArgs e)
        {
            BolumleriGetir();

            // Sınıf ve Kardeş Sayısı sadece seçilebilir olsun
            txtSinif.DropDownStyle = ComboBoxStyle.DropDownList;
            txtOgrKardesSayisi.DropDownStyle = ComboBoxStyle.DropDownList;

            // Telefon Placeholder
            txtTelNo.Properties.NullText = "0(5xx) xxx xx xx";

            // Bölüm Kutusundan Çıkış Kontrolü
            txtBolum.Leave += TxtBolum_Leave;
        }

        // BÖLÜMLERİ GETİR
        public void BolumleriGetir()
        {
            try
            {
                txtBolum.Items.Clear();
                txtBolum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtBolum.AutoCompleteSource = AutoCompleteSource.ListItems;

                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT DISTINCT BolumAdi FROM Bolumler ORDER BY BolumAdi ASC", conn);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
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

        private void TxtBolum_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBolum.Text)) return;

            int index = txtBolum.FindStringExact(txtBolum.Text.ToUpper());
            if (index == -1)
            {
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

        // KAYDET
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (string.IsNullOrWhiteSpace(txtOgrAd.Text)) { MessageHelper.ShowMissingField("Ad"); return; }
            if (string.IsNullOrWhiteSpace(txtOgrSoyad.Text)) { MessageHelper.ShowMissingField("Soyad"); return; }
            if (string.IsNullOrWhiteSpace(txtOgrKardesSayisi.Text)) { MessageHelper.ShowMissingField("Kardeş Sayısı"); return; }

            if (string.IsNullOrWhiteSpace(txtBolum.Text) || !txtBolum.Items.Contains(txtBolum.Text))
            {
                MessageHelper.ShowWarning("Lütfen listeden geçerli bir bölüm seçiniz.", "Hatalı Bölüm");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSinif.Text)) { MessageHelper.ShowMissingField("Sınıf"); return; }
            if (string.IsNullOrWhiteSpace(txtTelNo.Text)) { MessageHelper.ShowMissingField("Telefon"); return; }

            if (string.IsNullOrWhiteSpace(txtHaneGeliri.Text) || !decimal.TryParse(txtHaneGeliri.Text, out decimal gelir))
            {
                MessageHelper.ShowWarning("Lütfen 'Hane Geliri' alanına geçerli bir sayı giriniz.", "Geçersiz Değer");
                return;
            }

            // AGNO Dönüşümü
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
                using (SqlConnection conn = bgl.baglanti())
                {
                    if (_editingOgrenciID.HasValue)
                    {
                        // UPDATE mode (edit existing - ported from master pattern)
                        string updateQuery = @"UPDATE Ogrenciler SET 
                            AD=@p1, SOYAD=@p2, [KARDEŞ SAYISI]=@p3, [TOPLAM HANE GELİRİ]=@p4, 
                            BÖLÜMÜ=@p5, SINIF=@p6, AGNO=@p7, FOTO=@p8, TELEFON=@p9 
                            WHERE ID=@p10";

                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@p1", txtOgrAd.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@p2", txtOgrSoyad.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@p3", txtOgrKardesSayisi.Text);
                        cmd.Parameters.AddWithValue("@p4", gelir);
                        cmd.Parameters.AddWithValue("@p5", txtBolum.Text);
                        cmd.Parameters.AddWithValue("@p6", txtSinif.Text);
                        cmd.Parameters.AddWithValue("@p7", agno);
                        cmd.Parameters.AddWithValue("@p8", dosyaYolu ?? "");
                        cmd.Parameters.AddWithValue("@p9", txtTelNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@p10", _editingOgrenciID.Value);
                        cmd.ExecuteNonQuery();

                        MessageHelper.ShowSuccess("Öğrenci bilgileri başarıyla güncellendi.", "Güncelleme Başarılı");
                    }
                    else
                    {
                        // INSERT mode (add new - existing logic)
                        string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                                       "OUTPUT INSERTED.ID " +
                                       "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

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
                        
                        int yeniOgrenciID = Convert.ToInt32(cmd.ExecuteScalar());

                        // Öğrenciyi "Beklemede" durumuna ekle
                        try
                        {
                            string beklemeSorgu = @"INSERT INTO OgrenciBurslari (OgrenciID, BursID, BaslangicTarihi, Durum) 
                                                   VALUES (@OgrenciID, 1, @Tarih, 0)";
                            SqlCommand cmdBekleme = new SqlCommand(beklemeSorgu, conn);
                            cmdBekleme.Parameters.AddWithValue("@OgrenciID", yeniOgrenciID);
                            cmdBekleme.Parameters.AddWithValue("@Tarih", DateTime.Now);
                            cmdBekleme.ExecuteNonQuery();
                        }
                        catch
                        {
                            // OgrenciBurslari tablosu yoksa veya hata olursa devam et
                        }

                        MessageHelper.ShowSuccess("Öğrenci sisteme başarıyla kaydedildi.\nDurum: Beklemede", "Kayıt Başarılı");
                    }
                }

                // Diğer formları bilgilendir
                DataChangedNotifier.NotifyOgrenciChanged();

                if (!_editingOgrenciID.HasValue)
                    Temizle();
                
                // Dialog sonucunu OK olarak işaretle
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Kayıt Hatası");
            }
        }

        // TEMİZLE
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

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

