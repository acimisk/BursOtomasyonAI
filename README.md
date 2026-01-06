# 🎓 Burs Yönetim Sistemi

Modern ve kapsamlı bir burs yönetim sistemi. Öğrenci başvurularını yönetir, AI destekli değerlendirme yapar ve ML.NET ile mezuniyet puanı tahmini sunar.

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknolojiler](#-teknolojiler)
- [Gereksinimler](#-gereksinimler)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [ML Model](#-ml-model)
- [Veritabanı](#-veritabanı)
- [Proje Yapısı](#-proje-yapısı)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)

## ✨ Özellikler

### 🖥️ Desktop Uygulama (Windows Forms)
- **Öğrenci Yönetimi**: Öğrenci ekleme, düzenleme, silme ve listeleme
- **Burs Yönetimi**: Burs tanımlama, kontenjan takibi ve öğrenci-burs eşleştirme
- **Bağışçı Yönetimi**: Bağışçı bilgilerinin yönetimi
- **Ödeme Sistemi**: Aylık burs ödemelerinin takibi ve yönetimi
- **AI Destekli Değerlendirme**: Gemini AI ile öğrenci başvurularının otomatik analizi
- **ML.NET Tahmin**: Mezuniyet puanı tahmini için makine öğrenmesi modeli
- **Filtreleme**: Durum bazlı öğrenci filtreleme (Burs Alanlar, Beklemedeki, Yedek Liste, vb.)
- **Dark Mode UI**: Modern WXI Dark tema desteği

### 🌐 Web Uygulaması (Blazor)
- **Online Başvuru Formu**: Öğrencilerin web üzerinden burs başvurusu yapabilmesi
- **Responsive Tasarım**: Mobil ve masaüstü uyumlu arayüz
- **Form Validasyonu**: Kapsamlı form doğrulama mekanizması

## 🛠️ Teknolojiler

### Desktop Uygulama
- **.NET Framework 4.8**
- **C# Windows Forms**
- **DevExpress WinForms 25.1** (UI bileşenleri)
- **ML.NET 3.0.1** (Makine öğrenmesi)
- **Microsoft SQL Server** (Veritabanı)
- **Gemini AI API** (AI analiz)

### Web Uygulaması
- **ASP.NET Core Blazor**
- **Bootstrap 5**
- **SQL Server**

## 📦 Gereksinimler

### Desktop Uygulama
- Windows 10/11 veya Windows Server 2016+
- .NET Framework 4.8
- SQL Server 2016+ veya SQL Server LocalDB
- Visual Studio 2019+ (geliştirme için)
- DevExpress WinForms lisansı (ticari kullanım için)

### Web Uygulaması
- .NET 6.0+ SDK
- SQL Server 2016+ veya SQL Server LocalDB

## 🚀 Kurulum

### 1. Repository'yi Klonlayın

```bash
git clone https://github.com/kullaniciadi/bursoto1.git
cd bursoto1
```

### 2. Veritabanı Kurulumu

1. SQL Server Management Studio (SSMS) ile bağlanın
2. `bursOtoDeneme1` adında yeni bir veritabanı oluşturun
3. Gerekli tabloları oluşturun (aşağıdaki SQL scriptlerini çalıştırın):

```sql
-- Temel tablolar
CREATE TABLE Ogrenciler (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    AD NVARCHAR(100),
    SOYAD NVARCHAR(100),
    BÖLÜMÜ NVARCHAR(200),
    SINIF NVARCHAR(50),
    AGNO FLOAT,
    [TOPLAM HANE GELİRİ] DECIMAL(18,2),
    [KARDEŞ SAYISI] INT,
    TELEFON NVARCHAR(20),
    Üniversite NVARCHAR(200),
    AISkor INT,
    AIPotansiyelNotu FLOAT
);

CREATE TABLE Burslar (
    BursID INT IDENTITY(1,1) PRIMARY KEY,
    BursAdı NVARCHAR(200),
    Miktar DECIMAL(18,2),
    Kontenjan INT
);

CREATE TABLE OgrenciBurslari (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OgrenciID INT,
    BursID INT,
    Durum INT, -- 0: Beklemede, 1: Kabul, 2: Yedek
    BaslangicTarihi DATETIME
);

CREATE TABLE BursGiderleri (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    OgrenciID INT,
    BursID INT,
    Tutar DECIMAL(18,2),
    OdemeTarihi DATETIME DEFAULT GETDATE(),
    Ay INT,
    Yil INT,
    Aciklama NVARCHAR(500)
);
```

### 3. Bağlantı String'ini Ayarlayın

`bursoto1/SqlBaglanti.cs` dosyasındaki connection string'i kendi SQL Server bilgilerinize göre düzenleyin:

```csharp
private readonly string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=bursOtoDeneme1;Integrated Security=True";
```

### 4. NuGet Paketlerini Yükleyin

Visual Studio'da Solution'ı açın ve NuGet paketlerini restore edin:

```bash
# Visual Studio Package Manager Console'da
Update-Package -reinstall
```

### 5. ML Model Dosyasını Kontrol Edin

`bursoto1/BursModel.mlnet` dosyasının proje çıktı dizinine kopyalandığından emin olun (`.csproj` dosyasında `CopyToOutputDirectory` ayarı mevcut).

### 6. Gemini AI API Key (Opsiyonel)

AI analiz özelliğini kullanmak için `bursoto1/GeminiAI.cs` dosyasına API anahtarınızı ekleyin.

## 💻 Kullanım

### Desktop Uygulama

1. Projeyi Visual Studio'da açın
2. `bursoto1.sln` dosyasını çalıştırın
3. Uygulama başladığında:
   - **Anasayfa**: Genel istatistikler ve özet bilgiler
   - **Öğrenciler**: Öğrenci listesi, filtreleme ve AI analiz
   - **Burslar**: Burs tanımları ve kontenjan yönetimi
   - **Bağışçılar**: Bağışçı bilgileri

### ML.NET Tahmin Kullanımı

1. Öğrenciler modülünde bir öğrenci seçin
2. **🔮 Tahmin Et** butonuna tıklayın
3. Sistem otomatik olarak:
   - Üniversite adından şehir maliyetini hesaplar
   - Bölüm adından bölüm zorluğunu hesaplar
   - ML.NET modeli ile mezuniyet puanı tahmini yapar
4. Sonuç `AIPotansiyelNotu` kolonuna kaydedilir

### AI Analiz Kullanımı

1. Öğrenciler modülünde bir öğrenci seçin
2. **🤖 AI Analiz Yap** butonuna tıklayın
3. Gemini AI öğrenci başvurusunu analiz eder ve puan verir

### Web Uygulaması

1. `BursBasvuruWeb` projesini çalıştırın
2. Tarayıcıda `https://localhost:5001` adresine gidin
3. Öğrenciler online başvuru formunu doldurabilir

## 🤖 ML Model

### Model Bilgileri

- **Model Tipi**: Regression (Regresyon)
- **Algoritma**: FastTree veya LightGBM
- **R² Skoru**: ~0.75
- **Input Features**:
  - `MevcutAgno`: Mevcut akademik not ortalaması
  - `HaneGeliri`: Toplam hane geliri
  - `KardesSayisi`: Kardeş sayısı
  - `SehirMaliyet`: Şehir maliyet katsayısı (0.5-1.0)
  - `BolumZorluk`: Bölüm zorluk katsayısı (2.0-5.0)
- **Output**: `MezuniyetPuani` (tahmin edilen mezuniyet puanı)

### Mapping Fonksiyonları

#### Şehir Maliyeti Mapping
```csharp
// İstanbul ve üst seviye üniversiteler
İSTANBUL, İTÜ, BOĞAZİÇİ, KOÇ → 1.0f

// Ankara ve benzeri
ANKARA, ODTÜ, HACETTEPE → 0.9f

// Düşük maliyet
YOZGAT, BOZOK → 0.5f

// Diğerleri
Default → 0.7f
```

#### Bölüm Zorluğu Mapping
```csharp
TIP → 5.0f
MÜHENDİS → 4.8f
HUKUK, MİMAR, FEN, MATEMATİK → 4.0f
İŞLETME, İKTİSAT, ÖĞRETMEN → 3.0f
EDEBİYAT, TARİH, ARKEOLOJİ, SPOR → 2.0f
Default → 2.5f
```

### Model Yeniden Eğitme

Modeli yeniden eğitmek için:

1. Visual Studio'da ML.NET Model Builder extension'ını yükleyin
2. `BursModel.mbconfig` dosyasını açın
3. Yeni veri seti ile modeli yeniden eğitin
4. Eğitilen modeli `BursModel.mlnet` olarak kaydedin

## 🗄️ Veritabanı

### Ana Tablolar

- **Ogrenciler**: Öğrenci bilgileri
- **Burslar**: Burs tanımları
- **OgrenciBurslari**: Öğrenci-burs ilişkileri
- **BursGiderleri**: Aylık ödeme kayıtları

### Dinamik Kolon Desteği

Sistem, veritabanı şemasındaki farklılıkları otomatik tespit eder:
- `ID` veya `OgrenciID` kolonları
- `Üniversite` veya `Universite` kolonları
- `BursID` veya `ID` kolonları

## 📁 Proje Yapısı

```
bursoto1/
├── bursoto1/                    # Desktop uygulama
│   ├── Modules/                 # Modül sınıfları
│   │   ├── OgrenciModule.cs    # Öğrenci yönetimi
│   │   ├── BursModule.cs       # Burs yönetimi
│   │   └── BagisModule.cs      # Bağışçı yönetimi
│   ├── Helpers/                 # Yardımcı sınıflar
│   ├── BursModel.mlnet         # ML.NET model dosyası
│   └── SqlBaglanti.cs          # Veritabanı bağlantısı
├── BursBasvuruWeb/              # Blazor web uygulaması
│   ├── Components/
│   │   └── Pages/
│   │       └── BursBasvuru.razor
│   └── Program.cs
└── README.md
```

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Değişikliklerinizi commit edin (`git commit -m 'Add amazing feature'`)
4. Branch'inizi push edin (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

## 📝 Lisans

Bu proje özel bir projedir. Ticari kullanım için lisans gereklidir.

## 📧 İletişim

Sorularınız için issue açabilir veya doğrudan iletişime geçebilirsiniz.

## 🙏 Teşekkürler

- **DevExpress** - UI bileşenleri için
- **Microsoft ML.NET** - Makine öğrenmesi desteği için
- **Google Gemini AI** - AI analiz özelliği için

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

