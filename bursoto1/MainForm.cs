using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
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

            // Navigasyon olayını bağla
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
        }

        // Modülleri hafızada tutmak için (Performans için her seferinde new'lemeyelim)
        Dictionary<string, XtraUserControl> modules = new Dictionary<string, XtraUserControl>();

        private void MainForm_Load(object sender, EventArgs e)
        {
            // İlk açılışta Anasayfa gelsin
            ShowModule("Anasayfa");
        }

        // --- SİHİRLİ METOT: Modül Değiştirici ---
        void ShowModule(string moduleName)
        {
            if (!modules.ContainsKey(moduleName))
            {
                XtraUserControl module = null;

                // İstenen modülü oluştur
                switch (moduleName)
                {
                    case "Anasayfa":
                        // module = new AnasayfaModule(); // Bunu da UserControl yapman lazım
                        break;
                    case "Ogrenciler":
                        module = new OgrenciModule(); // Az önce oluşturduğumuz
                        break;
                    case "Burslar":
                        // module = new BursModule(); 
                        break;
                    default: return;
                }

                if (module != null)
                {
                    module.Dock = DockStyle.Fill;
                    // NavigationFrame'e ekle (navigationFrame1 designer'da eklediğin kontrolün adı)
                    navigationFrame1.Controls.Add(module);
                    modules.Add(moduleName, module);
                }
            }

            // O modülü ekranda göster
            if (modules.ContainsKey(moduleName))
            {
                navigationFrame1.SelectedPage = (NavigationPage)navigationFrame1.Pages.FindFirst(p => p.Controls.Contains(modules[moduleName]));
            }
        }

        // Sol Menüye Tıklayınca
        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            // Designer'da Accordion elementlerinin "Tag" özelliğine "Ogrenciler", "Anasayfa" yazmalısın.
            string hedefModul = e.Element.Tag?.ToString();

            if (!string.IsNullOrEmpty(hedefModul))
            {
                ShowModule(hedefModul);
            }
        }
    }
}