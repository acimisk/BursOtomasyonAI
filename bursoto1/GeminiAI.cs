using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace bursoto1
{
    public class GeminiAI
    {
        // Groq API - Ücretsiz ve hızlı LLM servisi
        private string apiKey = ConfigurationManager.AppSettings["GroqApiKey"];
        private const string apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        /// <summary>
        /// Öğrenci verilerini analiz eder ve burs uygunluk puanı verir.
        /// Groq API kullanarak tüm faktörleri değerlendirir.
        /// </summary>
        public async Task<string> BursAnaliziYap(string ogrenciVerisi)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                    string sistemMesaji = @"Sen tecrübeli bir burs komisyonu başkanısın. Öğrencinin verdiği cevapları derinlemesine ve ADİL bir şekilde analiz et.

ÖNEMLİ ANALİZ KURALLARI:
1. AIPotansiyelYuzde Yüksekse: Bu bir artı puandır AMA dikkatli ol! Düşük AGNO (örn: < 2.50) olan birinin yüksek yüzde alması 'baz etkisi'dir - düşük başlangıçtan yüksek artış kolay görünür. Buna kanma, gerçek potansiyeli değerlendir.

2. Asıl Başarı: Zor bölümlerde (Mühendislik, Tıp) olup da hedef notu 3.50 ve üzeri olan öğrencileri 'Üstün Başarılı' olarak nitelendir. Bu öğrenciler zorlu bir alanda yüksek performans gösteriyor demektir.

3. Yazılı Cevaplar: Eğer cevaplar 'boş' veya 'hakaret/saçmalık' değilse, cevapların kalitesine %20, akademik potansiyel ve finansal ihtiyaca %80 ağırlık ver. Yazılı cevaplar önemli ama asıl kriter akademik başarı ve finansal ihtiyaçtır.

4. İhtiyaç Analizi: Kişi başı hane geliri (Gelir / Kardeş Sayısı) düşük olan öğrencilere öncelik ver. Örneğin: 10.000 TL gelir, 3 kardeş = kişi başı ~2.500 TL. Bu öğrenciler gerçekten ihtiyaç sahibidir.

PUANLAMA:
- Akademik (40p): AGNO 3.5+=40, 3.0-3.49=30, 2.5-2.99=20, 2.0-2.49=10, <2.0=0
- Ekonomik (40p): KişiBaşıGelir=HaneGeliri/(Kardeş+2). <3000TL=40, 3-5k=30, 5-8k=20, 8-12k=10, >12k=0
- Motivasyon (20p): Samimi/detaylı=20, Orta=10, Boş=0

ÖĞRENCİNİN CEVAPLARINI TEK TEK ANALİZ ET:
- [İHTİYAÇ] sorusuna ne demiş? Gerçekten ihtiyacı var mı yoksa abartıyor mu?
- [HEDEFLER] sorusuna ne demiş? Gerçekçi mi, hayalperest mi?
- [KULLANIM] sorusuna ne demiş? Parayı doğru kullanacak mı?
- [FARK] sorusuna ne demiş? Kendini öne çıkaran bir özelliği var mı?
- [ML.NET TAHMİNİ] varsa: Bu tahmin akademik potansiyeli gösterir. Yüksek tahmin + zor bölüm = güçlü aday.

CEVAP FORMATI:
SKOR: [toplam puan]/100

ANALİZ:
- İhtiyaç cevabı: [Bu cevap hakkında 1 cümle yorum - samimi mi, abartılı mı, inandırıcı mı]
- Hedefler cevabı: [Bu cevap hakkında 1 cümle yorum - gerçekçi mi, azimli mi]
- Kullanım cevabı: [Bu cevap hakkında 1 cümle yorum - planlı mı, bilinçli mi]
- Fark cevabı: [Bu cevap hakkında 1 cümle yorum - öne çıkan özellik var mı]
- ML.NET Tahmini: [Eğer varsa, bu tahminin ne anlama geldiğini 1 cümle ile açıkla]

KİŞİLİK: [2-3 cümle - nasıl biri bu öğrenci? Karakteri, potansiyeli, güvenilirliği]

KARAR: [UYGUN/ŞARTLI/UYGUN DEĞİL] - [Neden bu kararı verdin, 1-2 cümle]

NOT: Cevaplar boşsa 'Cevap verilmemiş' yaz. Yıldız/hashtag kullanma. ADİL ve OBJEKTİF ol.";

                    var payload = new
                    {
                        model = "llama-3.3-70b-versatile",
                        messages = new[]
                        {
                            new { role = "system", content = sistemMesaji },
                            new { role = "user", content = "Bu öğrenciyi değerlendir:\n\n" + ogrenciVerisi }
                        },
                        temperature = 0.3,
                        max_tokens = 500
                    };

                    string jsonPayload = JsonConvert.SerializeObject(payload);
                    var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic data = JsonConvert.DeserializeObject(responseString);
                        string rawText = data.choices[0].message.content;
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