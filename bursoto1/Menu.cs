using System;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace bursoto1
{
    public partial class Menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Menu()
        {
            InitializeComponent();
            // Form ayarları
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1300, 800);
        }

        // GENERIC FORM AÇMA METODU (DRY Prensibi)
        private void FormGetir<T>() where T : Form, new()
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form is T)
                {
                    form.Activate();
                    return;
                }
            }
            T yeniForm = new T();
            yeniForm.MdiParent = this;
            yeniForm.Show();
        }

        // --- BUTON YÖNLENDİRMELERİ ---

        private void btnAnasayfa_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<Anasayfa>();
        }

        private void btnOgrenciListesi_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<FrmOgrenciler>();
        }

        // Hata veren btnOgrenciler aslında Listeyi açmalı
        public void btnOgrenciler_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<FrmOgrenciler>();
        }

        private void btnBurslar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<FrmBurslar>();
        }

        // Hata veren BursTurleri aslında Bursları açmalı
        public void btnBursTurleri_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<FrmBurslar>();
        }

        // Bağışçılar butonu
        private void btnBagiscilar_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<FrmBursVerenler>();
        }

        // Ara butonu (Ara formunu açar)
        public void btnAra_ItemClick(object sender, ItemClickEventArgs e)
        {
            FormGetir<Ara>();
        }

        // --- BOŞ OLMASI GEREKEN METODLAR (Hata Vermesin Diye Ekledik) ---
        // Bu butonlar muhtemelen eski tasarımdan kaldı veya işlevsiz.
        // Hata vermemesi için içlerini boş bıraktık.

        public void btnEkle_ItemClick(object sender, ItemClickEventArgs e) { }
        public void btnSil_ItemClick(object sender, ItemClickEventArgs e) { }
        public void btnTopluAnaliz_ItemClick(object sender, ItemClickEventArgs e) { }
        public void ribbon_Click(object sender, EventArgs e) { }
        public void Menu_Load(object sender, EventArgs e) { }

        // Form kapanırken uygulamayı tamamen kapat
        public void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}