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
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.Skins;

namespace bursoto1
{
    public partial class OgrenciProfili : XtraForm
    {
        #region Fields and Properties
        public string ad, soyad, maas, fotoYolu, agno, haneGeliri, bolum, kardesSayisi, sinif, telNo;
        public int secilenOgrenciID;
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE";
        private bool _isAnalyzing = false;
        #endregion
        
        #region AI Analysis
        private async void btnAIAnaliz_Click(object sender, EventArgs e)
        {
            if (_isAnalyzing) return;

            try
            {
                if (secilenOgrenciID <= 0)
                {
                    XtraMessageBox.Show("Öğrenci ID bilgisi alınamadı!", "Hata", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _isAnalyzing = true;
                btnAIAnaliz.Enabled = false;
                btnAIAnaliz.Text = "Analiz Ediliyor...";

                var ai = new GeminiAI();
                string veri = $"AD: {txtOgrAd.Text}, AGNO: {txtAgno.Text}, GELIR: {txtHaneGeliri.Text}, KARDES: {txtOgrKardesSayisi.Text}";

                rtbAnalizSonuc.Text = "Gemini analiz ediyor...";
                string sonuc = await ai.BursAnaliziYap(veri);
                rtbAnalizSonuc.Text = sonuc;

                var match = Regex.Match(sonuc, @"SKOR:\s*(\d+)");
                if (match.Success)
                {
                    int puan = int.Parse(match.Groups[1].Value);
                    txtAISkor.Text = puan.ToString();

                    await UpdateStudentScoreAsync(secilenOgrenciID, puan);
                    
                    XtraMessageBox.Show($"AI Skoru ({puan}) başarıyla veritabanına kaydedildi.", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"AI Analiz Hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isAnalyzing = false;
                btnAIAnaliz.Enabled = true;
                btnAIAnaliz.Text = "AI Analiz";
            }
        }

        private async Task UpdateStudentScoreAsync(int studentId, int score)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                using (var cmd = new SqlCommand("UPDATE Ogrenciler SET AISkor=@p1 WHERE ID=@p2", connection))
                {
                    cmd.Parameters.AddWithValue("@p1", score);
                    cmd.Parameters.AddWithValue("@p2", studentId);
                    
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion


        #region Event Handlers
        private void labelSinif_Click(object sender, EventArgs e) { }
        private void labelHaneGeliri_Click(object sender, EventArgs e) { }
        #endregion
        #region Image Processing
        private Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) return null;

            try
            {
                string temizBase64 = base64String.Contains(",") ? base64String.Split(',')[1] : base64String;
                byte[] imageBytes = Convert.FromBase64String(temizBase64);
                
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    return Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Resim dönüştürme hatası: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }
        #endregion
        #region Form Load and Data Binding
        private void OgrenciProfili_Load(object sender, EventArgs e)
        {
            try
            {
                BindStudentData();
                LoadStudentImage();
                this.Text = $"{ad} {soyad} - Profil";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Form yüklenirken hata oluştu: {ex.Message}", "Hata", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindStudentData()
        {
            txtOgrAd.Text = ad ?? string.Empty;
            txtOgrSoyad.Text = soyad ?? string.Empty;
            txtHaneGeliri.Text = haneGeliri ?? string.Empty;
            txtAgno.Text = agno ?? string.Empty;
            txtBolum.Text = bolum ?? string.Empty;
            txtOgrKardesSayisi.Text = kardesSayisi ?? string.Empty;
            txtSinif.Text = sinif ?? string.Empty;
            txtTelNo.Text = telNo ?? string.Empty;
        }

        private void LoadStudentImage()
        {
            if (!string.IsNullOrEmpty(fotoYolu))
            {
                try
                {
                    Image resim = null;
                    
                    if (fotoYolu.StartsWith("data:") || fotoYolu.Length > 260)
                    {
                        resim = Base64ToImage(fotoYolu);
                    }
                    else if (File.Exists(fotoYolu))
                    {
                        resim = Image.FromFile(fotoYolu);
                    }

                    if (resim != null)
                    {
                        pictureEdit1.EditValue = resim;
                        pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Resim yüklenemedi: {ex.Message}", "Uyarı", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion

        #region Constructor and Initialization
        public OgrenciProfili()
        {
            InitializeComponent();
            InitializeLookAndFeel();
            SetupEventHandlers();
        }

        private void InitializeLookAndFeel()
        {
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(800, 600);
        }

        private void SetupEventHandlers()
        {
            this.Load += OgrenciProfili_Load;
            btnAIAnaliz.Click += btnAIAnaliz_Click;
        }
        #endregion

        public void txtOgrAd_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
