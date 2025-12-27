using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bursoto1
{
    public partial class Ara : Form
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public Ara()
        {
            InitializeComponent();
        }
        private void Ara_Load(object sender, EventArgs e)
        {

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

                SqlDataAdapter da = new SqlDataAdapter(sorgu, bgl.baglanti());

                // KRİTİK NOKTA: Başındaki % işaretini kaldırdık! 
                // Artık sadece "s" yazınca "S" ile başlayanları getirir.
                if (!string.IsNullOrEmpty(txtAraAd.Text)) da.SelectCommand.Parameters.AddWithValue("@p1", txtAraAd.Text + "%");
                if (!string.IsNullOrEmpty(txtAraSoyad.Text)) da.SelectCommand.Parameters.AddWithValue("@p2", txtAraSoyad.Text + "%");
                if (!string.IsNullOrEmpty(txtAraBolum.Text)) da.SelectCommand.Parameters.AddWithValue("@p3", txtAraBolum.Text + "%");
                if (!string.IsNullOrEmpty(txtTelNo.Text)) da.SelectCommand.Parameters.AddWithValue("@p4", txtTelNo.Text + "%");
                if (!string.IsNullOrEmpty(txtSınıf.Text)) da.SelectCommand.Parameters.AddWithValue("@p5", txtSınıf.Text + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);
                gridAraSonuc.DataSource = dt;

                this.Text = "Arama: " + dt.Rows.Count + " Kayıt";
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
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
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
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