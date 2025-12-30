using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;     // Eklendi
using System.Globalization; // Eklendi
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors; // DevExpress mesaj kutusu için
using DevExpress.LookAndFeel; // DevExpress LookAndFeel için

namespace bursoto1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 1. KÜLTÜR AYARI (i/ı sorununu çözer)
            CultureInfo culture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // 2. GLOBAL HATA YAKALAMA
            Application.ThreadException += new ThreadExceptionEventHandler(GlobalHataYakala);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(GlobalKritikHataYakala);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 3. DEVEXPRESS MODERN UI AYARLARI
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
            UserLookAndFeel.Default.UseWindowsXPTheme = false;
            UserLookAndFeel.Default.Style = LookAndFeelStyle.Skin;

            // Başlangıç formu
            Application.Run(new Menu());
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