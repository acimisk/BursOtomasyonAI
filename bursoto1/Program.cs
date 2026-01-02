using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.UserSkins;


namespace bursoto1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // 🔴 SKIN REGISTER (ŞART)
            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            CultureInfo culture = new CultureInfo("tr-TR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            if (!SystemInformation.TerminalServerSession)
                WindowsFormsSettings.SetDPIAware();

            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.TrackWindowsAppMode = DefaultBoolean.False;

            // ✅ WXI DARK
            UserLookAndFeel.Default.SetSkinStyle("WXI", "Darkness");


            WindowsFormsSettings.DefaultRibbonStyle = DefaultRibbonControlStyle.Office365;
            AppearanceObject.DefaultFont = new Font("Segoe UI", 9F);

            WindowsFormsSettings.ColumnFilterPopupMode = ColumnFilterPopupMode.Excel;
            WindowsFormsSettings.AllowPixelScrolling = DefaultBoolean.True;
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Touch;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

        }


        static void GlobalHataYakala(object sender, ThreadExceptionEventArgs e)
        {
            XtraMessageBox.Show(
                $"Beklenmedik hata:\n\n{e.Exception.Message}",
                "Uygulama Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        static void GlobalKritikHataYakala(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            XtraMessageBox.Show(
                $"Kritik hata:\n\n{ex?.Message}",
                "Kritik Sistem Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop
            );
        }
    }
}
