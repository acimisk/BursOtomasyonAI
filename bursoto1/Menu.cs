using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using bursoto1.Helpers; // MessageHelper için

namespace bursoto1
{
    public partial class Menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        // Form referansları
        FrmOgrenciler fr1;
        FrmBurslar frBurs;
        FrmBursVerenler frBagis;
        FrmAylikBurs frAylikBurs;
        FrmOgrenciEkle frOgrenciEkle;
        Ara frAra;
        Anasayfa frAna;

        public Menu()
        {
            InitializeComponent();

            // Form açılış ayarları (Ortada ve boyutlandırılabilir)
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1300, 750);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            // Modern UI iyileştirmeleri
            this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Show;
            this.ribbon.ShowToolbarCustomizeItem = false;
            this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            
            // Ribbon görünüm ayarları
            this.ribbonPageGroup1.ShowCaptionButton = false;
        }

        // --- SAYFA AÇMA YÖNETİMİ (GENERIC METOT - DRY) ---
        // Bu metot, form açıksa öne getirir, kapalıysa veya yoksa yenisini oluşturur.
        private void FormGetir<T>(ref T formField) where T : Form, new()
        {
            if (formField == null || formField.IsDisposed)
            {
                formField = new T();
                formField.MdiParent = this;
                formField.Show();
            }
            else
            {
                formField.Activate();
            }
        }

        // --- MENÜ BUTONLARI ---

        private void btnAnasayfa_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir(ref frAna);
        }

        private void btnOgrenciler_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir(ref fr1);
        }

        private void btnBurslar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir(ref frBurs);
        }

        private void btnBagiscilar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir(ref frBagis);
        }

        public void btnAra_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir(ref frAra);
        }

        // --- İŞLEM BUTONLARI (EKLE / SİL - AKILLI YÖNETİM) ---

        private void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Yeni Öğrenci Ekleme Formunu Aç (MDI)
            if (frOgrenciEkle == null || frOgrenciEkle.IsDisposed)
            {
                frOgrenciEkle = new FrmOgrenciEkle();
                frOgrenciEkle.MdiParent = this;
                frOgrenciEkle.Show();
            }
            else
            {
                frOgrenciEkle.Activate();
            }
        }

        private void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            Form aktifForm = this.ActiveMdiChild;

            if (aktifForm is FrmOgrenciler ogrenciForm)
            {
                ogrenciForm.btnSil_Click(null, null);
            }
            else if (aktifForm is FrmBursVerenler)
            {
                MessageHelper.ShowInfo("Bağışçılar listesinde satıra sağ tıklayarak silme işlemi yapabilirsiniz.", "Bilgi");
            }
            else
            {
                MessageHelper.ShowWarning("Silme işlemi için geçerli bir liste sayfası (Öğrenciler vb.) açık olmalıdır.", "Uyarı");
            }
        }

        // --- UYGULAMA OLAYLARI ---

        private void Menu_Load(object sender, EventArgs e)
        {
            // Uygulama başladığında Anasayfa otomatik gelsin
            btnAnasayfa_ItemClick(null, null);
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MessageHelper.ShowConfirm("Uygulamadan çıkmak istediğinize emin misiniz?", "Çıkış Onayı"))
            {
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
            }
        }

        // Tasarımcı hatası almamak için boş bırakılan eventler
        public void ribbon_Click(object sender, EventArgs e) { }
        public void btnBursTurleri_ItemClick(object sender, ItemClickEventArgs e) { btnBurslar_ItemClick(sender, e); }
        public void btnTopluAnaliz_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Aylık burs tanımlama ekranını aç
            FormGetir(ref frAylikBurs);
        }

        private void btnOdeme_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            // Aylık burs ödeme ekranını aç (MDI)
            if (frAylikBurs == null || frAylikBurs.IsDisposed)
            {
                frAylikBurs = new FrmAylikBurs();
                frAylikBurs.MdiParent = this;
                frAylikBurs.Show();
            }
            else
            {
                frAylikBurs.Activate();
            }
        }

    }
}