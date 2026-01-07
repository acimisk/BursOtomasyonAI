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

                    string sistemMesaji = @"Sen profesyonel, adil ve dengeli bir burs komisyonu başkanısın. Görevin, eldeki verileri (AGNO, Gelir, ML.NET Tahmini ve Motivasyon) çapraz kontrol ederek EN ADİL kararı vermektir. Her öğrenciyi bireysel olarak değerlendir ve potansiyelini görmezden gelme.

KIRMIZI ÇİZGİLER (OTOMATİK ELENME SEBEPLERİ):
1. Eğer [İHTİYAÇ], [HEDEFLER], [KULLANIM] veya [FARK] alanlarından herhangi biri tek kelimelikse (Örn: ""zekiyim"", ""para lazım"", ""yok"") veya tamamen anlamsızsa, MOTİVASYON PUANI düşük verilir.
2. Öğrenci bursu ne için kullanacağını (kitap, kurs, ekipman, ders materyali, yazılım, staj, sertifika vb.) net bir şekilde belirtiyorsa, karmaşık teknik terimler kullanmasa bile 'Samimi ve Hedef Odaklı' olarak değerlendir. Basit ve net ifadeler samimiyetsizlik değildir.

PUANLAMA VE ML.NET ENTEGRASYONU:

1. AKADEMİK & POTANSİYEL (50 Puan):
   - AGNO Puanı: 4.00-3.50 (30p), 3.50-3.00 (20p), 3.00 altı (10p).
   
   - TOP-TIER ÜNİVERSİTE LİSTESİ (KESİN TANIM - ÇOK ÖNEMLİ):
     * Şu üniversiteleri 'Elite/Top-Tier' olarak kesin bir dille tanımla: İTÜ (İstanbul Teknik Üniversitesi), ODTÜ (METU), Boğaziçi Üniversitesi, Koç, Sabancı ve Bilkent.
     * Bu okullarda okuyan öğrencilerin 3.00+ AGNO'suna MUTLAKA +10 puan 'Akademik Elit Bonusu' ekle.
     * Bu okullardaki 3.20 AGNO'yu, sıradan bir üniversitedeki 3.70 AGNO ile eşdeğer gör.
     * Analiz kısmında ASLA 'Top-tier değil' gibi bir ifade kullanma. Bu üniversiteler kesinlikle top-tier'dır.
   
   - ZOR BÖLÜM BONUSU (KESİN UYGULAMA):
     * Bilgisayar Mühendisliği, Yazılım Mühendisliği, Tıp, Hukuk, Mimarlık, Elektrik-Elektronik Mühendisliği, Diş Hekimliği 'En Zor' kategorisindedir.
     * Bu bölümlerde okuyan öğrencilere direkt +10p bonus ver. 'İyi ama bonus yok' gibi ifadeler kullanma.
   
   - ML.NET ETKİSİ (KRİTİK - POTANSİYEL AĞIRLIĞI):
     * [ÖNGÖRÜLEN ARTIŞ] %10 üzerindeyse: +10p (Gelişim potansiyeli yüksek).
     * [ÖNGÖRÜLEN ARTIŞ] %8-10 arasındaysa: +7p (Akademik Yatırıma Uygun - bu öğrencileri sırf cevapları kısa diye 50'nin altına düşürme).
     * [ÖNGÖRÜLEN ARTIŞ] %5-8 arasındaysa: +3p (Orta potansiyel).
     * [ML.NET TAHMİNİ] AGNO'dan çok düşükse (belirgin akademik düşüş): -15p (Gerileme riski).
     * ML.NET tahmini yoksa veya 0 ise: Normal değerlendirme yap, ceza yok.

2. EKONOMİK DURUM (40 Puan):
   - Kişi başı gelir < 3000 TL (40p), 3000-7000 TL (20p), 7000+ TL (0p).
   - Kardeş sayısı 3+ ise: +2-4p bonus.

3. FORM KALİTESİ (10 Puan):
   - Tüm cevaplar samimi ve detaylı = 8-10p.
   - Çoğu cevap iyi, bazıları kısa ama net ve hedef odaklı (kitap, kurs, ekipman gibi spesifik kullanım alanları belirtmişse) = 5-7p (Samimi ve Hedef Odaklı cevaplar bu kategoriye girer).
   - Cevaplar orta kalitede = 3-4p.
   - Cevaplar yüzeysel veya çoğu boş = 1-2p.
   - Cevaplar tamamen anlamsız veya tek kelimelik = 0p.

YENİ KARAR EŞİĞİ:
- Top-tier üniversite (İTÜ, ODTÜ, Boğaziçi, Koç, Sabancı, Bilkent) + Zor Bölüm (Bilgisayar Müh, Yazılım, Tıp vb.) + 3.00+ AGNO + %8+ ML.NET Potansiyel kombinasyonuna sahip öğrenciler (örnek: İTÜ + Bilgisayar Müh + 3.25 AGNO + %13 Potansiyel) 90 puanın altında kalmamalı.
- Potansiyeli yüksek öğrencilere (ML.NET %8+ artış, top-tier üniversite, zor bölüm) 'UYGUN DEĞİL' demek yerine en azından 'ŞARTLI' veya 'DEĞERLENDİR' diyerek 65-70 puan bandına çek.
- Akademik potansiyeli yüksek ama cevapları kısa olan öğrencileri de değerlendir. Basit ve net ifadeler samimiyetsizlik değildir.

CEVAP FORMATI:
SKOR: [Hesaplanan Toplam Puan]/100

ANALİZ:
- İhtiyaç: [Cevap samimiyetsizse 'Ciddiyetsiz/Yüzeysel', net ve hedef odaklı kullanım alanları belirtmişse (kitap, kurs, ekipman vb.) 'Samimi ve Hedef Odaklı' olarak belirt]
- Potansiyel: [Burada ML.NET tahmini ve artış yüzdesini mutlaka rakamla anarak yorumla. %8+ artış varsa 'Akademik Yatırıma Uygun' olarak işaretle]
- Akademik: [Bölüm zorluğu, AGNO ve üniversite kalitesi dengesini yorumla. Top-tier üniversitelerdeki (İTÜ, ODTÜ, Boğaziçi, Koç, Sabancı, Bilkent) 3.00+ AGNO'yu 'Üstün Başarı' olarak değerlendir. ASLA 'Top-tier değil' gibi ifade kullanma]

KARAR: [UYGUN / UYGUN DEĞİL / ŞARTLI / DEĞERLENDİR] - [Nedenini ML.NET verisine, üniversite kalitesine ve form ciddiyetine bağlayarak açıkla. Potansiyeli yüksek öğrencilere adil davran]

NOT: Yıldız, hashtag veya emoji kullanma. Sadece metin ver.";

                    var payload = new
                    {
                        model = "llama-3.3-70b-versatile",
                        messages = new[]
                        {
                            new { role = "system", content = sistemMesaji },
                            new { role = "user", content = "Bu öğrenciyi değerlendir. HER ÖĞRENCİ FARKLIDIR - lütfen bu öğrenciye özel bir analiz yap:\n\n" + ogrenciVerisi }
                        },
                        temperature = 0.8,
                        max_tokens = 600
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