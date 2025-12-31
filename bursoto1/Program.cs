using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace bursoto1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 1. KÜLTÜR AYARI (Mevcut kodunu koruyoruz - Türkçe karakter sorunu için)
            CultureInfo culture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // 2. MODERN DEVEXPRESS AYARLARI (Demodan Entegre Edildi)

            // DPI Ayarları (Yüksek çözünürlüklü ekranlarda bulanıklığı önler)
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession && Screen.AllScreens.Length > 1)
                WindowsFormsSettings.SetPerMonitorDpiAware();
            else
                WindowsFormsSettings.SetDPIAware();

            // Form Skin (Tema) Ayarları
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.ForceDirectXPaint(); // Çizimleri hızlandırır (GPU kullanır)
            WindowsFormsSettings.TrackWindowsAppMode = DevExpress.Utils.DefaultBoolean.True; // Windows Koyu/Açık moduna uyum sağlar

            // En Yeni WXI (Windows 11) Temasını Aktif Et (Demodaki görünüm)
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle(SkinStyle.WXI);

            // Ribbon Stilini Office 365 Yap
            WindowsFormsSettings.DefaultRibbonStyle = DefaultRibbonControlStyle.Office365;

            // Font Ayarı (Modern ve okunaklı Segoe UI)
            DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Segoe UI", 9.25f);

            // Grid ve Liste Ayarları (Excel tarzı filtreleme vb.)
            WindowsFormsSettings.ColumnFilterPopupMode = ColumnFilterPopupMode.Excel;
            WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;

            // 3. GLOBAL HATA YAKALAMA (Mevcut kodunu koruyoruz)
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalHataYakala);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalKritikHataYakala);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 4. BAŞLANGIÇ FORMU
            // Eski 'Menu' formunu değil, yeni tasarladığımız modern 'MainForm'u açıyoruz.
            Application.Run(new MainForm());
        }

        static void GlobalHataYakala(object sender, ThreadExceptionEventArgs e)
        {
            XtraMessageBox.Show(
                $"Beklenmedik bir uygulama hatası oluştu:\n\n{e.Exception.Message}\n\nDetay: {e.Exception.GetType().Name}",
                "Sistem Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        static void GlobalKritikHataYakala(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            XtraMessageBox.Show(
                $"Kritik sistem hatası oluştu:\n\n{ex.Message}\n\nUygulama kapatılabilir.",
                "Kritik Hata",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
            );
        }
    }
}