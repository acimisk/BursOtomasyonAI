using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using bursoto1.Modules; // Modüllerin olduğu yer

namespace bursoto1
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();

            // Modern Skin Ayarı
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("WXI");

            // Navigasyon animasyonunu kapat (Kayma sorununu önler)
            navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;

            // Navigasyon olayını bağla
            accordionControl2.ElementClick += AccordionControl2_ElementClick;
        }

        // Modülleri hafızada tutmak için sözlük
        Dictionary<string, XtraUserControl> modules = new Dictionary<string, XtraUserControl>();

        private void MainForm_Load(object sender, EventArgs e)
        {
            // İlk açılışta Anasayfa gelsin
            ShowModule("Anasayfa");
        }

        // --- DÜZELTİLMİŞ SHOW MODULE METODU ---
        // --- DÜZELTİLMİŞ SHOW MODULE METODU ---
        void ShowModule(string moduleName)
        {
            // 1. Modül daha önce oluşturulmamışsa oluştur
            if (!modules.ContainsKey(moduleName))
            {
                DevExpress.XtraEditors.XtraUserControl module = null;

                switch (moduleName)
                {
                    case "Anasayfa":
                        module = new Modules.AnasayfaModule();
                        break;
                    case "Ogrenciler":
                        module = new Modules.OgrenciModule();
                        break;
                    case "Burslar":
                        module = new Modules.BursModule();
                        break;
                    case "Bagiscilar":
                        module = new Modules.BagisModule();
                        break;
                    default: return; // Tanımsız bir isim geldiyse çık
                }

                if (module != null)
                {
                    module.Dock = DockStyle.Fill;

                    // --- KRİTİK DÜZELTME BURASI ---
                    // Modülü doğrudan Frame'e değil, bir NavigationPage içine koyuyoruz.
                    NavigationPage page = new NavigationPage();
                    page.Caption = moduleName; // Sayfa başlığı
                    page.Tag = moduleName;     // Sayfayı bulmak için etiket
                    page.Controls.Add(module); // Modülü sayfanın içine göm

                    // Sayfayı frame'e ekle
                    navigationFrame1.Pages.Add(page);

                    // Listemize kaydet
                    modules.Add(moduleName, module);
                }
            }

            // 2. İlgili modülü ekranda göster
            if (modules.ContainsKey(moduleName))
            {
                var moduleToFind = modules[moduleName];

                // Modülümüzün olduğu sayfayı buluyoruz
                // (Controls.Add yaptığımız için modül sayfanın Controls listesindedir)
                var page = navigationFrame1.Pages.FindFirst(p => p.Controls.Contains(moduleToFind)) as NavigationPage;

                // Ve o sayfayı seçiyoruz
                if (page != null)
                {
                    navigationFrame1.SelectedPage = page;
                }
            }
        }

        // Sol Menüye Tıklayınca
        private void AccordionControl2_ElementClick(object sender, ElementClickEventArgs e)
        {
            string hedefModul = e.Element.Tag?.ToString();

            // --- BU SATIRI EKLE VE DENE ---
            MessageBox.Show($"Tıklanan Element: {e.Element.Text}, Okunan Tag: '{hedefModul}'");
            // -----------------------------

            if (!string.IsNullOrEmpty(hedefModul))
            {
                ShowModule(hedefModul);
            }
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {

        }


    }
}