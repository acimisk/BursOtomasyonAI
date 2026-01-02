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

        public Ara()
        {
            InitializeComponent();
        }

        private void Ara_Load(object sender, EventArgs e)
        {
            // Grid dark mode ayarları
            ApplyDarkGrid();
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
        void CanliArama()
        {
            try
            {
                string sorgu = "SELECT * FROM Ogrenciler WHERE 1=1";

                if (!string.IsNullOrEmpty(txtAraAd.Text)) sorgu += " AND AD LIKE @p1";
                if (!string.IsNullOrEmpty(txtAraSoyad.Text)) sorgu += " AND SOYAD LIKE @p2";
                if (!string.IsNullOrEmpty(txtAraBolum.Text)) sorgu += " AND BÖLÜMÜ LIKE @p3";
                if (!string.IsNullOrEmpty(txtTelNo.Text)) sorgu += " AND TELEFON LIKE @p4";
                if (!string.IsNullOrEmpty(txtSınıf.Text)) sorgu += " AND SINIF LIKE @p5";

                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);

                    // KRİTİK NOKTA: Başındaki % işaretini kaldırdık! 
                    // Artık sadece "s" yazınca "S" ile başlayanları getirir.
                    if (!string.IsNullOrEmpty(txtAraAd.Text)) da.SelectCommand.Parameters.AddWithValue("@p1", txtAraAd.Text + "%");
                    if (!string.IsNullOrEmpty(txtAraSoyad.Text)) da.SelectCommand.Parameters.AddWithValue("@p2", txtAraSoyad.Text + "%");
                    if (!string.IsNullOrEmpty(txtAraBolum.Text)) da.SelectCommand.Parameters.AddWithValue("@p3", txtAraBolum.Text + "%");
                    if (!string.IsNullOrEmpty(txtTelNo.Text)) da.SelectCommand.Parameters.AddWithValue("@p4", txtTelNo.Text + "%");
                    if (!string.IsNullOrEmpty(txtSınıf.Text)) da.SelectCommand.Parameters.AddWithValue("@p5", txtSınıf.Text + "%");

                    da.Fill(dt);
                }
                gridAraSonuc.DataSource = dt;

                this.Text = "Arama: " + dt.Rows.Count + " Kayıt";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Arama Hatası");
            }
        }

        // Kanka butonun içini boşalttım, o da canlı aramayı çağırsın yeter.
        private void btnSorgula_Click(object sender, EventArgs e)
        {
            CanliArama();
        }

        // BU EVENTLERİ DESIGNER'DAN BAĞLAMAYI UNUTMA!
        private void txtAraAd_EditValueChanged(object sender, EventArgs e) { CanliArama(); }
        private void txtAraSoyad_EditValueChanged(object sender, EventArgs e) { CanliArama(); }
        private void txtAraBolum_EditValueChanged(object sender, EventArgs e) { CanliArama(); }
        private void txtTelNo_EditValueChanged(object sender, EventArgs e) { CanliArama(); }
        private void txtSınıf_EditValueChanged(object sender, EventArgs e) { CanliArama(); }

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
                OgrenciProfili frm = new OgrenciProfili();
                frm.MdiParent = this.MdiParent;
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);
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
    }
}