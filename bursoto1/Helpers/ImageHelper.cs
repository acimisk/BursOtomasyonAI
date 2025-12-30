using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace bursoto1.Helpers
{
    /// <summary>
    /// Resim işleme yardımcı sınıfı (Base64 dönüşümü ve performans optimizasyonu)
    /// </summary>
    public static class ImageHelper
    {
        // Base64 görselleri cache'lemek için (performans için)
        private static readonly Dictionary<string, Image> _imageCache = new Dictionary<string, Image>();
        private const int MAX_CACHE_SIZE = 50; // Maksimum cache boyutu

        /// <summary>
        /// Base64 string'i Image'a dönüştürür (performans optimizasyonlu)
        /// </summary>
        public static Image Base64ToImage(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            try
            {
                // Cache kontrolü
                if (_imageCache.ContainsKey(base64String))
                {
                    return _imageCache[base64String];
                }

                // Base64 string'i temizle
                string temizBase64 = base64String.Contains(",") 
                    ? base64String.Split(',')[1] 
                    : base64String;

                // Base64'ten byte array'e dönüştür
                byte[] imageBytes = Convert.FromBase64String(temizBase64);

                // MemoryStream kullanarak Image oluştur
                Image resim;
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    // MemoryStream'den Image oluştururken kopya oluştur
                    resim = new Bitmap(Image.FromStream(ms));
                }

                // Cache'e ekle (boyut kontrolü ile)
                if (_imageCache.Count >= MAX_CACHE_SIZE)
                {
                    // En eski entry'yi sil (basit FIFO)
                    var firstKey = new List<string>(_imageCache.Keys)[0];
                    _imageCache[firstKey]?.Dispose();
                    _imageCache.Remove(firstKey);
                }

                _imageCache[base64String] = resim;
                return resim;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Resim dönüştürme hatası: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Image'ı Base64 string'e dönüştürür
        /// </summary>
        public static string ImageToBase64(Image image, string mimeType = "image/png")
        {
            if (image == null)
                return string.Empty;

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // PNG formatında kaydet
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();
                    return $"data:{mimeType};base64,{Convert.ToBase64String(imageBytes)}";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Base64 dönüştürme hatası: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Dosya yolundan Base64 string oluşturur
        /// </summary>
        public static string FileToBase64(string dosyaYolu, string mimeType = "image/png")
        {
            if (string.IsNullOrEmpty(dosyaYolu) || !File.Exists(dosyaYolu))
                return string.Empty;

            try
            {
                byte[] imageBytes = File.ReadAllBytes(dosyaYolu);
                return $"data:{mimeType};base64,{Convert.ToBase64String(imageBytes)}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Dosya okuma hatası: {ex.Message}");
                return string.Empty;
            }
        }

        /// <summary>
        /// Cache'i temizler
        /// </summary>
        public static void ClearCache()
        {
            foreach (var image in _imageCache.Values)
            {
                image?.Dispose();
            }
            _imageCache.Clear();
        }
    }
}

