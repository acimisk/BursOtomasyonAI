using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using bursoto1.Helpers; // MessageHelper için

namespace bursoto1
{
    public partial class Ara : XtraForm
    {
        SqlBaglanti bgl = new SqlBaglanti();
        
        // Debounce için Timer (donmayı önler)
        private System.Windows.Forms.Timer _searchTimer;
        private const int SEARCH_DELAY_MS = 400; // 400ms bekle

        public Ara()
        {
            InitializeComponent();
            
            // Timer'ı başlat
            _searchTimer = new System.Windows.Forms.Timer();
            _searchTimer.Interval = SEARCH_DELAY_MS;
            _searchTimer.Tick += SearchTimer_Tick;
        }

        private void Ara_Load(object sender, EventArgs e)
        {
            // Grid dark mode ayarları
            ApplyDarkGrid();
        }

        // Timer tetiklendiğinde aramayı yap
        private void SearchTimer_Tick(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            CanliAramaGerceklestir();
        }

        // Debounce mekanizması - her tuşa basıldığında timer sıfırlanır
        private void AramayiPlanla()
        {
            _searchTimer.Stop();
            _searchTimer.Start();
        }

        private void ApplyDarkGrid()
        {
            if (gridAraSonuc == null) return;
            
            var gv = gridAraSonuc.MainView as GridView;
            if (gv == null) return;

            // SATIRLAR
            gv.Appearance.Row.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Row.ForeColor = Color.White;
            gv.Appearance.Row.Options.UseBackColor = true;
            gv.Appearance.Row.Options.UseForeColor = true;

            // BAŞLIK
            gv.Appearance.HeaderPanel.BackColor = Color.FromArgb(45, 45, 48);
            gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.HeaderPanel.Options.UseBackColor = true;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;

            // SEÇİLİ SATIR
            gv.Appearance.FocusedRow.BackColor = Color.FromArgb(70, 70, 70);
            gv.Appearance.FocusedRow.ForeColor = Color.White;
            gv.Appearance.FocusedRow.Options.UseBackColor = true;
            gv.Appearance.FocusedRow.Options.UseForeColor = true;

            // BOŞ ALAN
            gv.Appearance.Empty.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Empty.Options.UseBackColor = true;

            // Grid arka planı
            gridAraSonuc.BackColor = Color.FromArgb(32, 32, 32);
        }

        // Kanka asıl motor burası. LIKE parametrelerini düzelttim.
        // Debounce sonrası gerçek arama yapılır
        void CanliAramaGerceklestir()
        {
            // Hiç kriter yoksa arama yapma (gereksiz yükü engelle)
            bool hasAnyFilter = !string.IsNullOrEmpty(txtAraAd.Text) ||
                                !string.IsNullOrEmpty(txtAraSoyad.Text) ||
                                !string.IsNullOrEmpty(txtAraBolum.Text) ||
                                !string.IsNullOrEmpty(txtTelNo.Text) ||
                                !string.IsNullOrEmpty(txtSınıf.Text);

            if (!hasAnyFilter)
            {
                gridAraSonuc.DataSource = null;
                this.Text = "Arama: Kriter giriniz";
                return;
            }

            try
            {
                // UI güncellemesi
                this.Text = "Arama yapılıyor...";
                Application.DoEvents(); // UI'ı güncelle

                string sorgu = "SELECT ID, AD, SOYAD, BÖLÜMÜ, SINIF, TELEFON, AGNO FROM Ogrenciler WHERE 1=1";

                if (!string.IsNullOrEmpty(txtAraAd.Text)) sorgu += " AND AD LIKE @p1";
                if (!string.IsNullOrEmpty(txtAraSoyad.Text)) sorgu += " AND SOYAD LIKE @p2";
                if (!string.IsNullOrEmpty(txtAraBolum.Text)) sorgu += " AND BÖLÜMÜ LIKE @p3";
                if (!string.IsNullOrEmpty(txtTelNo.Text)) sorgu += " AND TELEFON LIKE @p4";
                if (!string.IsNullOrEmpty(txtSınıf.Text)) sorgu += " AND SINIF LIKE @p5";

                DataTable dt = new DataTable();
                
                // Connection string ile direkt bağlantı aç (connection pooling kullan)
                string connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bursOtoDeneme1;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                    {
                        // Parametreleri ekle
                        if (!string.IsNullOrEmpty(txtAraAd.Text))
                            cmd.Parameters.AddWithValue("@p1", txtAraAd.Text + "%");
                        if (!string.IsNullOrEmpty(txtAraSoyad.Text))
                            cmd.Parameters.AddWithValue("@p2", txtAraSoyad.Text + "%");
                        if (!string.IsNullOrEmpty(txtAraBolum.Text))
                            cmd.Parameters.AddWithValue("@p3", txtAraBolum.Text + "%");
                        if (!string.IsNullOrEmpty(txtTelNo.Text))
                            cmd.Parameters.AddWithValue("@p4", txtTelNo.Text + "%");
                        if (!string.IsNullOrEmpty(txtSınıf.Text))
                            cmd.Parameters.AddWithValue("@p5", txtSınıf.Text + "%");

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                gridAraSonuc.DataSource = dt;
                this.Text = "Arama: " + dt.Rows.Count + " Kayıt";
            }
            catch (Exception ex)
            {
                this.Text = "Arama: Hata!";
                MessageHelper.ShowException(ex, "Arama Hatası");
            }
        }

        // Buton tıklandığında anında arama yap (timer'ı beklemeden)
        private void btnSorgula_Click(object sender, EventArgs e)
        {
            _searchTimer.Stop();
            CanliAramaGerceklestir();
        }

        // BU EVENTLERİ DESIGNER'DAN BAĞLAMAYI UNUTMA!
        // Her tuşa basıldığında timer sıfırlanır - donma önlenir
        private void txtAraAd_EditValueChanged(object sender, EventArgs e) { AramayiPlanla(); }
        private void txtAraSoyad_EditValueChanged(object sender, EventArgs e) { AramayiPlanla(); }
        private void txtAraBolum_EditValueChanged(object sender, EventArgs e) { AramayiPlanla(); }
        private void txtTelNo_EditValueChanged(object sender, EventArgs e) { AramayiPlanla(); }
        private void txtSınıf_EditValueChanged(object sender, EventArgs e) { AramayiPlanla(); }

        private void btnOgrGoster_Click(object sender, EventArgs e)
        {
            // gridAraSonuc'un MainView'ını kullan
            var gridView = gridAraSonuc.MainView as GridView;
            if (gridView == null)
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
                return;
            }
            
            DataRow dr = gridView.GetDataRow(gridView.FocusedRowHandle);
            if (dr != null)
            {
                // ID ile profil formunu aç - form kendi verilerini yükleyecek
                int ogrenciID = Convert.ToInt32(dr["ID"]);
                OgrenciProfili frm = new OgrenciProfili();
                frm.secilenOgrenciID = ogrenciID;
                frm.Show();
            }
            else
            {
                MessageHelper.ShowWarning("Lütfen bir öğrenci seçiniz.", "Seçim Yapılmadı");
            }
        }
    }
}