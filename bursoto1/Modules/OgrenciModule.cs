using bursoto1.Helpers; // Senin helper klasörün
using DevExpress.XtraBars; // Ribbon işlemleri için
using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace bursoto1.Modules
{
    // DİKKAT: Artık Form değil, XtraUserControl'den türüyor
    public partial class OgrenciModule : DevExpress.XtraEditors.XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();
        GeminiAI geminiAI = new GeminiAI();

        public OgrenciModule()
        {
            InitializeComponent();

            // Grid ayarlarını UserControl yüklenirken yapalım
            ConfigGrid();
        }

        private void ConfigGrid()
        {
            // Modern Outlook görünümü için Grid ayarları
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
            gridView1.OptionsView.ShowIndicator = false;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
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
                    // Senin sorgun
                    string sorgu = @"SELECT ID, AD, SOYAD, BÖLÜMÜ, SINIF, AGNO, 
                                  [TOPLAM HANE GELİRİ] AS [Hane Geliri], [KARDEŞ SAYISI] AS [Kardeş], 
                                  ISNULL(AISkor, 0) AS [AI Puanı]
                                  FROM Ogrenciler ORDER BY AD, SOYAD";

                    SqlDataAdapter da = new SqlDataAdapter(sorgu, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gridControl1.DataSource = dt;
                }

                if (gridView1.Columns["ID"] != null) gridView1.Columns["ID"].Visible = false;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Hata");
            }
        }

        // --- RIBBON BUTONLARI ---
        // Bu metodları Designer'dan Ribbon üzerindeki butonlara çift tıklayarak bağla.

        public void btnYeni_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Popup olarak ekleme formunu açabilirsin
            FrmOgrenciEkle frm = new FrmOgrenciEkle();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Listele();
            }
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (MessageHelper.ShowConfirm("Seçili öğrenciyi silmek istiyor musunuz?", "Silme"))
            {
                // Silme işlemleri (Senin eski kodun aynısı buraya gelecek)
                // ...
                Listele();
            }
        }

        public void btnAIAnaliz_ItemClick(object sender, ItemClickEventArgs e)
        {
            // AI Analiz kodların buraya...
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}