using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using bursoto1.Modules;
using bursoto1.Helpers;
using DevExpress.XtraBars;

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

            // Ribbon butonlarını bağla
            WireRibbonButtons();
        }

        void WireRibbonButtons()
        {
            if (btnAnaSayfa != null)
                btnAnaSayfa.ItemClick += (s, e) => ShowModule("Anasayfa");
            
            if (btnOgrenciler != null)
                btnOgrenciler.ItemClick += (s, e) => ShowModule("Ogrenciler");
            
            if (btnBurs != null)
                btnBurs.ItemClick += (s, e) => ShowModule("Burslar");
            
            if (btnBagiscilar != null)
                btnBagiscilar.ItemClick += (s, e) => ShowModule("Bagiscilar");
            
            if (btnAra != null)
                btnAra.ItemClick += (s, e) => ShowAraForm();
            
            if (btnEkle != null)
                btnEkle.ItemClick += (s, e) => HandleEkle();
            
            if (btnSil != null)
                btnSil.ItemClick += (s, e) => HandleSil();
        }

        void ShowAraForm()
        {
            // Ara formunu göster
            Ara araForm = new Ara();
            araForm.Show();
        }

        void HandleEkle()
        {
            // Aktif modüle göre ekleme işlemi
            var activeModule = GetActiveModule();
            if (activeModule is Modules.OgrenciModule ogrenciModule)
            {
                ogrenciModule.btnYeni_ItemClick(null, null);
            }
            else if (activeModule is Modules.BursModule bursModule)
            {
                bursModule.btnYeni_ItemClick(null, null);
            }
            else if (activeModule is Modules.BagisModule bagisModule)
            {
                bagisModule.btnEkle_ItemClick(null, null);
            }
            else
            {
                // Bu sayfada ekleme yapılamaz - kullanıcıya seçenek sun
                ShowEkleSecenekleri();
            }
        }

        void ShowEkleSecenekleri()
        {
            // Kullanıcıya ne eklemek istediğini sor
            using (XtraForm frm = new XtraForm())
            {
                frm.Text = "Yeni Kayıt Ekle";
                frm.Size = new System.Drawing.Size(350, 200);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                // Başlık etiketi
                var lblBaslik = new LabelControl()
                {
                    Text = "Bu sayfada doğrudan ekleme yapılamaz.\nNe eklemek istiyorsunuz?",
                    Location = new System.Drawing.Point(20, 20),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new System.Drawing.Size(300, 50)
                };
                lblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10);

                // Öğrenci Ekle butonu
                var btnOgrenci = new SimpleButton()
                {
                    Text = "👨‍🎓 Yeni Öğrenci Ekle",
                    Location = new System.Drawing.Point(20, 80),
                    Size = new System.Drawing.Size(145, 40)
                };
                btnOgrenci.Appearance.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);

                // Bağışçı Ekle butonu
                var btnBagisci = new SimpleButton()
                {
                    Text = "💰 Yeni Bağışçı Ekle",
                    Location = new System.Drawing.Point(175, 80),
                    Size = new System.Drawing.Size(145, 40)
                };
                btnBagisci.Appearance.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);

                btnOgrenci.Click += (s, ev) =>
                {
                    frm.Close();
                    FrmOgrenciEkle ogrForm = new FrmOgrenciEkle();
                    if (ogrForm.ShowDialog() == DialogResult.OK)
                    {
                        // Öğrenci modülünü yenile
                        if (modules.ContainsKey("Ogrenciler") && modules["Ogrenciler"] is Modules.OgrenciModule om)
                            om.Listele();
                        DataChangedNotifier.NotifyOgrenciChanged();
                    }
                };

                btnBagisci.Click += (s, ev) =>
                {
                    frm.Close();
                    // Bağışçılar modülüne git ve ekle
                    ShowModule("Bagiscilar");
                    if (modules.ContainsKey("Bagiscilar") && modules["Bagiscilar"] is Modules.BagisModule bm)
                        bm.btnEkle_ItemClick(null, null);
                };

                frm.Controls.AddRange(new Control[] { lblBaslik, btnOgrenci, btnBagisci });
                frm.ShowDialog(this);
            }
        }

        void HandleSil()
        {
            // Aktif modüle göre silme işlemi
            var activeModule = GetActiveModule();
            if (activeModule is Modules.OgrenciModule ogrenciModule)
            {
                ogrenciModule.btnSil_ItemClick(null, null);
            }
            else if (activeModule is Modules.BursModule bursModule)
            {
                bursModule.btnSil_ItemClick(null, null);
            }
            else if (activeModule is Modules.BagisModule bagisModule)
            {
                bagisModule.btnSil_ItemClick(null, null);
            }
            else
            {
                // Bu sayfada silme yapılamaz
                MessageHelper.ShowInfo(
                    "Bu sayfada silme işlemi yapılamaz.\n\n" +
                    "Silme yapmak için Öğrenciler, Burslar veya Bağışçılar sayfasına gidin.",
                    "Bilgi");
            }
        }

        XtraUserControl GetActiveModule()
        {
            var selectedPage = navigationFrame1.SelectedPage as NavigationPage;
            if (selectedPage != null && selectedPage.Controls.Count > 0)
            {
                return selectedPage.Controls[0] as XtraUserControl;
            }
            return null;
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



    }
}