using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;     // Eklendi
using System.Globalization; // Eklendi
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors; // DevExpress mesaj kutusu için

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

            // Başlangıç formu
            Application.Run(new Menu());
        }

        static void GlobalHataYakala(object sender, ThreadExceptionEventArgs e)
        {
            XtraMessageBox.Show("Beklenmedik bir uygulama hatası oluştu:\n" + e.Exception.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        static void GlobalKritikHataYakala(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            XtraMessageBox.Show("Kritik sistem hatası:\n" + ex.Message, "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}