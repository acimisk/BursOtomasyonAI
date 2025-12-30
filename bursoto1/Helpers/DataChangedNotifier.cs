using System;

namespace bursoto1.Helpers
{
    /// <summary>
    /// Veritabanı değişikliklerini tüm formlara bildiren global event sistemi.
    /// Yenile tuşuna basmaya gerek kalmadan otomatik güncelleme sağlar.
    /// </summary>
    public static class DataChangedNotifier
    {
        // Event'ler
        public static event Action OgrenciDegisti;
        public static event Action BursVerenDegisti;
        public static event Action BursDegisti;

        /// <summary>
        /// Öğrenci verisi değiştiğinde çağrılır (ekleme, silme, güncelleme)
        /// </summary>
        public static void NotifyOgrenciChanged()
        {
            OgrenciDegisti?.Invoke();
        }

        /// <summary>
        /// Burs veren verisi değiştiğinde çağrılır (yeni bağış, onay, red)
        /// </summary>
        public static void NotifyBursVerenChanged()
        {
            BursVerenDegisti?.Invoke();
        }

        /// <summary>
        /// Burs verisi değiştiğinde çağrılır (burs ataması, iptal)
        /// </summary>
        public static void NotifyBursChanged()
        {
            BursDegisti?.Invoke();
        }

        /// <summary>
        /// Tüm verilerin değiştiğini bildirir
        /// </summary>
        public static void NotifyAllChanged()
        {
            OgrenciDegisti?.Invoke();
            BursVerenDegisti?.Invoke();
            BursDegisti?.Invoke();
        }
    }
}

