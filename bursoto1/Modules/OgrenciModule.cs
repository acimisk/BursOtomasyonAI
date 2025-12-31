using DevExpress.XtraEditors;
using DevExpress.XtraBars; // Ribbon işlemleri için
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using bursoto1.Helpers; // Helperların olduğu namespace

namespace bursoto1.Modules
{
    public partial class OgrenciModule : DevExpress.XtraEditors.XtraUserControl
    {
        // Eski FrmOgrenciler'deki bağlantı ve değişkenlerin
        SqlBaglanti bgl = new SqlBaglanti();

        public OgrenciModule()
        {
            InitializeComponent();

            // Grid görünüm ayarları
            if (gridView1 != null)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsSelection.MultiSelect = true;
                gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            }
        }

        private void OgrenciModule_Load(object sender, EventArgs e)
        {
            Listele();
        }

        public void Listele()
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Eski FrmOgrenciler'deki sorgunun aynısı
                    string sorgu = @"SELECT ID, AD, SOYAD, BÖLÜMÜ, SINIF, AGNO, 
                                  [TOPLAM HANE GELİRİ] AS [Hane Geliri], [KARDEŞ SAYISI] AS [Kardeş], 
                                  ISNULL(AISkor, 0) AS [AI Puanı]
                                  FROM Ogrenciler ORDER BY AD, SOYAD";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }

                // ID kolonunu gizle
                if (gridView1.Columns["ID"] != null) gridView1.Columns["ID"].Visible = false;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message);
            }
        }

        // --- BUTON İŞLEMLERİ ---
        // Bu metodları Designer'dan Ribbon üzerindeki butonlara (btnYeni, btnSil) çift tıklayarak bağla.

        public void btnYeni_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Yeni ekleme formu (Pop-up olarak açılacak)
            FrmOgrenciEkle frm = new FrmOgrenciEkle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Listele();
            }
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Silme işlemi
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (XtraMessageBox.Show("Silmek istiyor musunuz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    Listele();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Silinemedi: " + ex.Message);
                }
            }
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // FrmOgrenciEkle bir Form'dur, UserControl değildir.
            // Dialog olarak açılır, kullanıcı veriyi girer, 'Kaydet' der ve pencere kapanır.
            FrmOgrenciEkle frm = new FrmOgrenciEkle();

            // Eğer kaydetme başarılı olursa (DialogResult.OK dönerse) listeyi yenile
            // FrmOgrenciEkle içindeki Kaydet butonunun sonuna 'this.DialogResult = DialogResult.OK;' eklemelisin.
            frm.ShowDialog();

            // Pencere kapandıktan sonra listeyi güncelle
            Listele();

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            // Seçili satırı al
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                // Profil formunu oluştur
                OgrenciProfili frm = new OgrenciProfili();

                // Verileri aktar (Senin OgrenciProfili.cs içindeki değişkenlere göre)
                frm.secilenOgrenciID = Convert.ToInt32(dr["ID"]);
                frm.ad = dr["AD"].ToString();
                frm.soyad = dr["SOYAD"].ToString();
                // ... diğer alanlar ...

                // Formu aç
                frm.Show(); // ShowDialog() yaparsan arkadaki ekran kilitlenir, Show() ile serbest kalır.
            }
        }
    }
}