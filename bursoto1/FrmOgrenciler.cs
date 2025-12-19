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
    public partial class FrmOgrenciler : Form
    {
        public FrmOgrenciler()
        {
            InitializeComponent();
            sqlDataSource1.FillAsync();
        }

        public SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; initial catalog=bursOtoDeneme1; Integrated Security=TRUE");

        // 1. ÇÖZÜM: Başına PUBLIC ekledik, artık Menu.cs bu metodu görebilir.
        public void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Ogrenciler", conn);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            gridControl1.DataSource = dataTable;
        }

        // 2. ÇÖZÜM: Değişkeni buraya PUBLIC olarak ekledik.
        public string dosyaYolu = "";

        private void FrmOgrenciler_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            // Resim seçme mantığını buraya ekledik ki dosyaYolu dolsun
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Resim Dosyaları |*.jpg;*.png;*.jpeg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureResim.Image = Image.FromFile(ofd.FileName);
                dosyaYolu = ofd.FileName; // Değişkeni güncelliyoruz
            }
        }

        private void btnGoster_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                OgrenciProfili frm = new OgrenciProfili();
                frm.MdiParent = this.MdiParent;

                // --- İŞTE EKSİK OLAN KRİTİK SATIR BURASI ---
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);
                // -------------------------------------------

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

        // Grid'e çift tıklayınca da profil açılsın (opsiyonel)
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            btnGoster_Click(sender, e);
        }

        private void btnResimSec_Click_1(object sender, EventArgs e)
        {
            // Bilgisayardan dosya seçmek için pencereyi hazırlıyoruz
            OpenFileDialog ofd = new OpenFileDialog();

            // Sadece resim formatlarını görmesini sağlıyoruz (Kullanıcı gidip pdf seçmesin)
            ofd.Filter = "Resim Dosyaları |*.jpg;*.png;*.jpeg";
            ofd.Title = "Öğrenci Fotoğrafı Seç";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 1. Seçilen resmi PictureEdit kontrolüne basıyoruz (Ekranda görünsün)
                pictureResim.Image = Image.FromFile(ofd.FileName);

                // 2. Seçilen dosyanın yolunu daha önce oluşturduğumuz public değişkene atıyoruz
                // Bu çok kritik! Menu.cs buradan alıp SQL'e yazacak.
                dosyaYolu = ofd.FileName;
            }
        }
    }
}