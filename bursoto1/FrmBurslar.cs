using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using bursoto1.Helpers; // MessageHelper için

namespace bursoto1
{
    public partial class FrmBurslar : Form
    {
        public FrmBurslar()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();

        public void Listele()
        {
            // Listeleme yaparken bağlantıyı her seferinde taze alıyoruz
            DataTable dt = new DataTable();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Burslar", conn);
                da.Fill(dt);
            }
            gridControl1.DataSource = dt;
        }

        private void FrmBurslar_Load(object sender, EventArgs e)
        {
            Listele();
        }


        // KANKA BU ÇOK ÖNEMLİ: Grid'de bir satıra tıklayınca bilgiler kutulara dolsun
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                txtBursAd.Text = dr["BursAdı"].ToString();
                txtMiktar.Text = dr["Miktar"].ToString();
                txtKontenjan.Text = dr["Kontenjan"].ToString();
                txtAciklama.Text = dr["Aciklama"].ToString();
                
                // Seçili bursu alan öğrencileri göster
                BursAlanOgrencileriGetir(Convert.ToInt32(dr["BursID"]));
            }
        }

        // Seçili bursu alan öğrencileri getir
        private void BursAlanOgrencileriGetir(int bursID)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    // OgrenciBurslari tablosu üzerinden burs alan öğrencileri getir
                    string sorgu = @"SELECT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.SINIF, o.AGNO,
                                    ob.BaslangicTarihi,
                                    CASE WHEN ob.Durum = 1 THEN 'Aktif' 
                                         WHEN ob.Durum = 0 THEN 'Beklemede' 
                                         ELSE 'Bilinmiyor' END AS [Durum]
                                    FROM Ogrenciler o
                                    INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                    WHERE ob.BursID = @BursID
                                    ORDER BY o.AD, o.SOYAD";
                    
                    using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                    {
                        cmd.Parameters.AddWithValue("@BursID", bursID);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
                
                // Öğrenci sayısını bilgi mesajı olarak göster
                if (dt.Rows.Count > 0)
                {
                    MessageHelper.ShowInfo(
                        $"Bu bursu alan toplam {dt.Rows.Count} öğrenci bulunmaktadır.\n\n" +
                        $"Aktif: {dt.Select("[Durum] = 'Aktif'").Length} öğrenci\n" +
                        $"Beklemede: {dt.Select("[Durum] = 'Beklemede'").Length} öğrenci",
                        "Burs Alan Öğrenciler"
                    );
                }
                else
                {
                    MessageHelper.ShowInfo("Bu bursu alan öğrenci bulunmamaktadır.", "Bilgi");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Burs Alan Öğrenciler");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            var secilenID = gridView1.GetFocusedRowCellValue("ID"); // Sütun adının ID olduğundan emin ol
            if (secilenID == null) return;

            if (MessageHelper.ShowConfirm("Bu burs türünü silmek istiyor musunuz?", "Silme Onayı"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Burslar WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", secilenID);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess("Burs başarıyla silindi.", "Silme Başarılı");
                    Listele();
                    FormuTemizle();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Silme Hatası");
                }
            }
        }

        void FormuTemizle()
        {
            txtBursAd.Text = "";
            txtMiktar.Text = "";
            txtKontenjan.Text = "";
            txtAciklama.Text = "";
        }

        private void btnBursTanimla_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBursAd.Text) || string.IsNullOrEmpty(txtMiktar.Text))
            {
                MessageHelper.ShowWarning("Lütfen burs adını ve miktarını giriniz!", "Eksik Bilgi");
                return;
            }

            try
            {
                // SQL Sütun isimlerinin [BursAdı] falan doğru olduğundan emin ol kanka
                string sorgu = "INSERT INTO Burslar (BursAdı, Miktar, Kontenjan, Aciklama) VALUES (@p1, @p2, @p3, @p4)";

                using (SqlConnection conn = bgl.baglanti())
                {
                    using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                    {
                        cmd.Parameters.AddWithValue("@p1", txtBursAd.Text);
                        cmd.Parameters.AddWithValue("@p2", decimal.Parse(txtMiktar.Text.Replace(".", ",")));
                        cmd.Parameters.AddWithValue("@p3", int.Parse(txtKontenjan.Text));
                        cmd.Parameters.AddWithValue("@p4", txtAciklama.Text ?? "");
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageHelper.ShowSuccess("Burs başarıyla kaydedildi.", "Kayıt Başarılı");
                FormuTemizle();
                Listele();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Kaydetme Hatası");
            }
        }
    }
}