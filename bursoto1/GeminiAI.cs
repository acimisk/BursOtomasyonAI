using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bursoto1
{
    public class GeminiAI
    {
        private const string apiKey = "AIzaSyDd1sR3PpPRYpmmKBtMr6IBKV4Grzk3rZs";
        // Kesin çalışan v1beta URL'si ve model adı
        private const string apiUrl = "https://generativelanguage.googleapis.com/v1/models/gemini-2.5-flash:generateContent?key=" + apiKey;

        public async Task<string> BursAnaliziYap(string ogrenciVerisi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string talimat = "Sen bir burs komisyonu başkanısın. Cevabını asla yıldız (*) kullanmadan düz metin ver. " +
                                     "Format tam olarak şöyle olsun: SKOR: [sayı] ACIKLAMA: [cümle]. Veri: " + ogrenciVerisi;

                    var payload = new
                    {
                        contents = new[] { new { parts = new[] { new { text = talimat } } } }
                    };

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic data = JsonConvert.DeserializeObject(responseString);
                        string rawText = data.candidates[0].content.parts[0].text;
                        return Regex.Replace(rawText, @"[*#_]", "").Trim();
                    }
                    return "AI Hatası: " + responseString;
                }
            }
            catch (Exception ex)
            {
                return "Bağlantı Hatası: " + ex.Message;
            }
        }
    }
}