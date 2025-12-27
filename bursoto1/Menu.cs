using DevExpress.XtraBars;
using DevExpress.XtraCharts.Native;
using DevExpress.XtraEditors;
using DevExpress.Skins;
using DevExpress.Utils;
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
        #region Fields and Properties
        private readonly string _connectionString = @"Data Source=(localdb)\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE";
        private FrmOgrenciler fr1;
        private Anasayfa frAna;
        private Ara frAra;
        private FrmBurslar frBurs;
        #endregion

        #region Form Management
        private void btnOgrenciler_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                fr1 = new FrmOgrenciler();
                fr1.MdiParent = this;
                fr1.Show();
            }
            else
            {
                fr1.Activate();
            }
        }

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

        private void btnAra_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (frAra == null || frAra.IsDisposed)
            {
                frAra = new Ara();
                frAra.MdiParent = this;
                frAra.Show();
            }
            else
            {
                frAra.Activate();
            }
        }

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
        #endregion

        #region Database Operations
        private async void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                XtraMessageBox.Show("Önce 'Öğrenciler' sayfasını açmalısınız.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                await AddStudentAsync();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Öğrenci eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task AddStudentAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string sorgu = "INSERT INTO Ogrenciler (AD, SOYAD, [KARDEŞ SAYISI], [TOPLAM HANE GELİRİ], BÖLÜMÜ, SINIF, AGNO, FOTO, TELEFON) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";

                using (var komut = new SqlCommand(sorgu, connection))
                {
                    komut.Parameters.AddWithValue("@p1", fr1.txtOgrAd.Text ?? string.Empty);
                    komut.Parameters.AddWithValue("@p2", fr1.txtOgrSoyad.Text ?? string.Empty);

                    int kardes = 0;
                    int.TryParse(fr1.txtOgrKardesSayisi.Text, out kardes);
                    komut.Parameters.AddWithValue("@p3", kardes);

                    decimal haneGeliri = 0;
                    decimal.TryParse(fr1.txtHaneGeliri.Text, out haneGeliri);
                    komut.Parameters.AddWithValue("@p4", haneGeliri);

                    komut.Parameters.AddWithValue("@p5", fr1.txtBolum.Text ?? string.Empty);
                    komut.Parameters.AddWithValue("@p6", fr1.txtSinif.Text ?? string.Empty);

                    decimal agno = 0;
                    decimal.TryParse(fr1.txtAgno.Text, out agno);
                    komut.Parameters.AddWithValue("@p7", agno);

                    komut.Parameters.AddWithValue("@p8", (object)fr1.dosyaYolu ?? DBNull.Value);
                    komut.Parameters.AddWithValue("@p9", fr1.txtTelNo.Text ?? string.Empty);

                    await komut.ExecuteNonQueryAsync();
                }
            }

            XtraMessageBox.Show("Öğrenci başarıyla kaydedildi.", "Bilgi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            fr1.Listele();
        }

        private async void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                XtraMessageBox.Show("Önce 'Öğrenciler' sayfasını açmalısınız.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var secilenID = fr1.gridView1.GetFocusedRowCellValue("ID");
            if (secilenID == null)
            {
                XtraMessageBox.Show("Lütfen silmek istediğiniz öğrenciyi seçin!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult onay = XtraMessageBox.Show("Bu öğrenci kaydını silmek istediğinize emin misiniz?",
                "Kayıt Silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    await DeleteStudentAsync(Convert.ToInt32(secilenID));
                    XtraMessageBox.Show("Öğrenci başarıyla silindi.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fr1.Listele();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Öğrenci silinirken hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async Task DeleteStudentAsync(int studentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", connection))
                {
                    cmd.Parameters.AddWithValue("@p1", studentId);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion

        #region Constructor and Initialization
        public Menu()
        {
            InitializeComponent();
            InitializeLookAndFeel();
            SetupRibbonCustomization();
        }

        private void InitializeLookAndFeel()
        {
            this.LookAndFeel.SkinName = "The Bezier";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.WindowState = FormWindowState.Maximized;
        }

        private void SetupRibbonCustomization()
        {
            ribbonPage1.Text = "Ana Menü";
            ribbonPageGroup1.Text = "İşlemler";
        }
        #endregion

        #region Form Events
        private void Menu_Load(object sender, EventArgs e)
        {
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
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
            }
        }
        #endregion

        #region AI Analysis Operations
        private async void btnTopluAnaliz_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fr1 == null || fr1.IsDisposed)
            {
                XtraMessageBox.Show("Toplu analiz yapabilmek için önce 'Öğrenciler' sayfasını açmalısınız.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult onay = XtraMessageBox.Show("Puanı olmayan tüm öğrenciler analiz edilecek. Devam edilsin mi?",
                "Toplu Analiz Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                btnTopluAnaliz.Enabled = false;
                try
                {
                    await fr1.TopluAnalizYap();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show($"Toplu analizde hata oluştu: {ex.Message}", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    btnTopluAnaliz.Enabled = true;
                }
            }
        }
        #endregion

        private void ribbon_Click(object sender, EventArgs e) { }
    }
}