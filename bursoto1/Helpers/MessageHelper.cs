using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;

namespace bursoto1.Helpers
{
    /// <summary>
    /// Merkezi hata mesaj yönetimi için yardımcı sınıf (DRY prensibi)
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Başarılı işlem mesajı gösterir
        /// </summary>
        public static void ShowSuccess(string mesaj, string baslik = "Başarılı")
        {
            XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hata mesajı gösterir
        /// </summary>
        public static void ShowError(string mesaj, string baslik = "Hata")
        {
            XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Uyarı mesajı gösterir
        /// </summary>
        public static void ShowWarning(string mesaj, string baslik = "Uyarı")
        {
            XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Bilgi mesajı gösterir
        /// </summary>
        public static void ShowInfo(string mesaj, string baslik = "Bilgi")
        {
            XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Onay mesajı gösterir (Yes/No)
        /// </summary>
        public static bool ShowConfirm(string mesaj, string baslik = "Onay")
        {
            return XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        /// <summary>
        /// Exception'dan hata mesajı gösterir
        /// </summary>
        public static void ShowException(Exception ex, string baslik = "Hata")
        {
            string mesaj = $"Bir hata oluştu:\n\n{ex.Message}";
            if (ex.InnerException != null)
            {
                mesaj += $"\n\nDetay: {ex.InnerException.Message}";
            }
            XtraMessageBox.Show(mesaj, baslik, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Eksik alan uyarısı gösterir
        /// </summary>
        public static void ShowMissingField(string alanAdi)
        {
            ShowWarning($"Lütfen '{alanAdi}' alanını doldurunuz.", "Eksik Bilgi");
        }
    }
}

