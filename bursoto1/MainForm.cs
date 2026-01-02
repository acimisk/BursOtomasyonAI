using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using bursoto1.Modules;
using bursoto1.Helpers;
using DevExpress.XtraBars;

namespace bursoto1
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();

            // Modern Skin Ayarı
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("WXI");

            // Navigasyon animasyonunu kapat (Kayma sorununu önler)
            navigationFrame1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.False;

            // Ribbon butonlarını bağla
            WireRibbonButtons();
        }

        void WireRibbonButtons()
        {
            if (btnAnaSayfa != null)
                btnAnaSayfa.ItemClick += (s, e) => ShowModule("Anasayfa");
            
            if (btnOgrenciler != null)
                btnOgrenciler.ItemClick += (s, e) => ShowModule("Ogrenciler");
            
            if (btnBurs != null)
                btnBurs.ItemClick += (s, e) => ShowModule("Burslar");
            
            if (btnBagiscilar != null)
                btnBagiscilar.ItemClick += (s, e) => ShowModule("Bagiscilar");
            
            if (btnAra != null)
                btnAra.ItemClick += (s, e) => ShowAraForm();
            
            if (btnEkle != null)
                btnEkle.ItemClick += (s, e) => HandleEkle();
            
            if (btnSil != null)
                btnSil.ItemClick += (s, e) => HandleSil();
            
            if (btnOdeme != null)
                btnOdeme.ItemClick += (s, e) => HandleOdeme();
        }

        void ShowAraForm()
        {
            // Ara formunu göster
            Ara araForm = new Ara();
            araForm.Show();
        }

        void HandleEkle()
        {
            // Aktif modüle göre ekleme işlemi
            var activeModule = GetActiveModule();
            if (activeModule is Modules.OgrenciModule ogrenciModule)
            {
                ogrenciModule.btnYeni_ItemClick(null, null);
            }
            else if (activeModule is Modules.BursModule bursModule)
            {
                bursModule.btnYeni_ItemClick(null, null);
            }
            else if (activeModule is Modules.BagisModule bagisModule)
            {
                bagisModule.btnEkle_ItemClick(null, null);
            }
            else
            {
                // Bu sayfada ekleme yapılamaz - kullanıcıya seçenek sun
                ShowEkleSecenekleri();
            }
        }

        void ShowEkleSecenekleri()
        {
            // Kullanıcıya ne eklemek istediğini sor
            using (XtraForm frm = new XtraForm())
            {
                frm.Text = "Yeni Kayıt Ekle";
                frm.Size = new System.Drawing.Size(350, 200);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                // Başlık etiketi
                var lblBaslik = new LabelControl()
                {
                    Text = "Bu sayfada doğrudan ekleme yapılamaz.\nNe eklemek istiyorsunuz?",
                    Location = new System.Drawing.Point(20, 20),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new System.Drawing.Size(300, 50)
                };
                lblBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10);

                // Öğrenci Ekle butonu
                var btnOgrenci = new SimpleButton()
                {
                    Text = "👨‍🎓 Yeni Öğrenci Ekle",
                    Location = new System.Drawing.Point(20, 80),
                    Size = new System.Drawing.Size(145, 40)
                };
                btnOgrenci.Appearance.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);

                // Bağışçı Ekle butonu
                var btnBagisci = new SimpleButton()
                {
                    Text = "💰 Yeni Bağışçı Ekle",
                    Location = new System.Drawing.Point(175, 80),
                    Size = new System.Drawing.Size(145, 40)
                };
                btnBagisci.Appearance.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);

                btnOgrenci.Click += (s, ev) =>
                {
                    frm.Close();
                    FrmOgrenciEkle ogrForm = new FrmOgrenciEkle();
                    if (ogrForm.ShowDialog() == DialogResult.OK)
                    {
                        // Öğrenci modülünü yenile
                        if (modules.ContainsKey("Ogrenciler") && modules["Ogrenciler"] is Modules.OgrenciModule om)
                            om.Listele();
                        DataChangedNotifier.NotifyOgrenciChanged();
                    }
                };

                btnBagisci.Click += (s, ev) =>
                {
                    frm.Close();
                    // Bağışçılar modülüne git ve ekle
                    ShowModule("Bagiscilar");
                    if (modules.ContainsKey("Bagiscilar") && modules["Bagiscilar"] is Modules.BagisModule bm)
                        bm.btnEkle_ItemClick(null, null);
                };

                frm.Controls.AddRange(new Control[] { lblBaslik, btnOgrenci, btnBagisci });
                frm.ShowDialog(this);
            }
        }

        void HandleSil()
        {
            // Aktif modüle göre silme işlemi
            var activeModule = GetActiveModule();
            if (activeModule is Modules.OgrenciModule ogrenciModule)
            {
                ogrenciModule.btnSil_ItemClick(null, null);
            }
            else if (activeModule is Modules.BursModule bursModule)
            {
                bursModule.btnSil_ItemClick(null, null);
            }
            else if (activeModule is Modules.BagisModule bagisModule)
            {
                bagisModule.btnSil_ItemClick(null, null);
            }
            else
            {
                // Bu sayfada silme yapılamaz
                MessageHelper.ShowInfo(
                    "Bu sayfada silme işlemi yapılamaz.\n\n" +
                    "Silme yapmak için Öğrenciler, Burslar veya Bağışçılar sayfasına gidin.",
                    "Bilgi");
            }
        }

        XtraUserControl GetActiveModule()
        {
            var selectedPage = navigationFrame1.SelectedPage as NavigationPage;
            if (selectedPage != null && selectedPage.Controls.Count > 0)
            {
                return selectedPage.Controls[0] as XtraUserControl;
            }
            return null;
        }

        // Modülleri hafızada tutmak için sözlük
        Dictionary<string, XtraUserControl> modules = new Dictionary<string, XtraUserControl>();

        private void MainForm_Load(object sender, EventArgs e)
        {
            // İlk açılışta Anasayfa gelsin
            ShowModule("Anasayfa");
        }

        // --- DÜZELTİLMİŞ SHOW MODULE METODU ---
        // --- DÜZELTİLMİŞ SHOW MODULE METODU ---
        void ShowModule(string moduleName)
        {
            // 1. Modül daha önce oluşturulmamışsa oluştur
            if (!modules.ContainsKey(moduleName))
            {
                DevExpress.XtraEditors.XtraUserControl module = null;

                switch (moduleName)
                {
                    case "Anasayfa":
                        module = new Modules.AnasayfaModule();
                        break;
                    case "Ogrenciler":
                        module = new Modules.OgrenciModule();
                        break;
                    case "Burslar":
                        module = new Modules.BursModule();
                        break;
                    case "Bagiscilar":
                        module = new Modules.BagisModule();
                        break;
                    default: return; // Tanımsız bir isim geldiyse çık
                }

                if (module != null)
                {
                    module.Dock = DockStyle.Fill;

                    // --- KRİTİK DÜZELTME BURASI ---
                    // Modülü doğrudan Frame'e değil, bir NavigationPage içine koyuyoruz.
                    NavigationPage page = new NavigationPage();
                    page.Caption = moduleName; // Sayfa başlığı
                    page.Tag = moduleName;     // Sayfayı bulmak için etiket
                    page.Controls.Add(module); // Modülü sayfanın içine göm

                    // Sayfayı frame'e ekle
                    navigationFrame1.Pages.Add(page);

                    // Listemize kaydet
                    modules.Add(moduleName, module);
                }
            }

            // 2. İlgili modülü ekranda göster ve yenile
            if (modules.ContainsKey(moduleName))
            {
                var moduleToFind = modules[moduleName];

                // Modülümüzün olduğu sayfayı buluyoruz
                var page = navigationFrame1.Pages.FindFirst(p => p.Controls.Contains(moduleToFind)) as NavigationPage;

                // Ve o sayfayı seçiyoruz
                if (page != null)
                {
                    navigationFrame1.SelectedPage = page;
                }

                // Modülü yenile (responsive davranış için)
                RefreshModule(moduleToFind);
            }
        }

        // Modül yenileme - her navigasyonda güncel veri göster
        void RefreshModule(XtraUserControl module)
        {
            try
            {
                if (module is AnasayfaModule anasayfa)
                {
                    anasayfa.Refresh(); // AnasayfaModule'de Refresh metodu olmalı
                }
                else if (module is OgrenciModule ogrenci)
                {
                    ogrenci.Listele();
                }
                else if (module is BursModule burs)
                {
                    burs.RefreshAndClear(); // Form temizle + listele
                }
                else if (module is BagisModule bagis)
                {
                    bagis.Listele();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Modül yenileme hatası: {ex.Message}");
            }
        }

        // Ödeme işlemi - Burs alıcılarına ödeme yap
        void HandleOdeme()
        {
            SqlBaglanti bgl = new SqlBaglanti();
            
            using (XtraForm frm = new XtraForm())
            {
                frm.Text = "💰 Burs Ödemesi Yap";
                frm.Size = new System.Drawing.Size(750, 550);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                // Burs seçimi
                var lblBurs = new LabelControl()
                {
                    Text = "Burs Seçin:",
                    Location = new System.Drawing.Point(20, 20),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new System.Drawing.Size(100, 25)
                };
                lblBurs.Appearance.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

                var cmbBurs = new ComboBoxEdit()
                {
                    Location = new System.Drawing.Point(130, 17),
                    Size = new System.Drawing.Size(350, 24)
                };
                cmbBurs.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;

                // Bursları yükle
                DataTable dtBurslar = new DataTable();
                try
                {
                    using (var conn = bgl.baglanti())
                    {
                        // BursGiderleri tablosunu kontrol et ve yoksa oluştur
                        SqlCommand cmdCheck = new SqlCommand(@"IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'BursGiderleri')
                            BEGIN
                                CREATE TABLE BursGiderleri (
                                    ID INT IDENTITY(1,1) PRIMARY KEY,
                                    OgrenciID INT,
                                    BursID INT,
                                    Tutar DECIMAL(18,2),
                                    Tarih DATETIME DEFAULT GETDATE(),
                                    Aciklama NVARCHAR(500)
                                )
                            END", conn);
                        cmdCheck.ExecuteNonQuery();

                        string query = "SELECT * FROM Burslar WHERE Kontenjan > 0";
                        var da = new SqlDataAdapter(query, conn);
                        da.Fill(dtBurslar);
                    }

                    foreach (DataRow row in dtBurslar.Rows)
                    {
                        string bursAdi = row["BursAdı"]?.ToString() ?? "Bilinmeyen";
                        decimal miktar = Convert.ToDecimal(row["Miktar"] ?? 0);
                        int bursID = 0;
                        if (dtBurslar.Columns.Contains("BursID"))
                            bursID = Convert.ToInt32(row["BursID"]);
                        else if (dtBurslar.Columns.Contains("ID"))
                            bursID = Convert.ToInt32(row["ID"]);

                        cmbBurs.Properties.Items.Add(new BursOdemeItem(bursID, bursAdi, miktar));
                    }

                    if (cmbBurs.Properties.Items.Count > 0)
                        cmbBurs.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Burs Listesi Yükleme Hatası");
                }

                // Öğrenci listesi
                var lblInfo = new LabelControl()
                {
                    Text = "Bu bursa hak kazanan öğrenciler:",
                    Location = new System.Drawing.Point(20, 55),
                    AutoSizeMode = LabelAutoSizeMode.None,
                    Size = new System.Drawing.Size(300, 25)
                };
                lblInfo.Appearance.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

                var gridOdeme = new DevExpress.XtraGrid.GridControl()
                {
                    Location = new System.Drawing.Point(20, 85),
                    Size = new System.Drawing.Size(695, 300)
                };
                var gridViewOdeme = new DevExpress.XtraGrid.Views.Grid.GridView(gridOdeme);
                gridOdeme.MainView = gridViewOdeme;
                gridViewOdeme.OptionsSelection.MultiSelect = true;
                gridViewOdeme.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                gridViewOdeme.OptionsView.ShowGroupPanel = false;
                gridViewOdeme.OptionsBehavior.Editable = false;

                // Burs değiştiğinde öğrencileri yükle
                Action loadStudents = () =>
                {
                    if (cmbBurs.SelectedItem is BursOdemeItem selectedBurs)
                    {
                        try
                        {
                            DataTable dt = new DataTable();
                            using (var conn = bgl.baglanti())
                            {
                                string query = @"SELECT o.ID, o.AD, o.SOYAD, o.BÖLÜMÜ, o.TELEFON,
                                    CASE WHEN EXISTS(SELECT 1 FROM BursGiderleri bg WHERE bg.OgrenciID = o.ID 
                                        AND bg.BursID = @bursID
                                        AND MONTH(bg.Tarih) = MONTH(GETDATE()) AND YEAR(bg.Tarih) = YEAR(GETDATE())) 
                                        THEN 'Bu Ay Ödendi' ELSE 'Bekliyor' END as OdemeDurumu
                                    FROM Ogrenciler o
                                    INNER JOIN OgrenciBurslari ob ON o.ID = ob.OgrenciID
                                    WHERE ob.BursID = @bursID AND ob.Durum = 1
                                    ORDER BY o.AD, o.SOYAD";

                                var cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@bursID", selectedBurs.BursID);
                                var da = new SqlDataAdapter(cmd);
                                da.Fill(dt);
                            }
                            gridOdeme.DataSource = dt;

                            // Kolon ayarları
                            if (gridViewOdeme.Columns["ID"] != null)
                                gridViewOdeme.Columns["ID"].Visible = false;
                            if (gridViewOdeme.Columns["AD"] != null) gridViewOdeme.Columns["AD"].Caption = "Ad";
                            if (gridViewOdeme.Columns["SOYAD"] != null) gridViewOdeme.Columns["SOYAD"].Caption = "Soyad";
                            if (gridViewOdeme.Columns["BÖLÜMÜ"] != null) gridViewOdeme.Columns["BÖLÜMÜ"].Caption = "Bölüm";
                            if (gridViewOdeme.Columns["TELEFON"] != null) gridViewOdeme.Columns["TELEFON"].Caption = "Telefon";
                            if (gridViewOdeme.Columns["OdemeDurumu"] != null) gridViewOdeme.Columns["OdemeDurumu"].Caption = "Durum";

                            gridViewOdeme.BestFitColumns();

                            // Ödeme tutarını güncelle
                            lblInfo.Text = $"Bu bursa hak kazanan öğrenciler ({dt.Rows.Count} kişi):";
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Öğrenci listesi hatası: {ex.Message}");
                        }
                    }
                };

                cmbBurs.SelectedIndexChanged += (s, ev) => loadStudents();

                // Ödeme tutarı
                var lblTutar = new LabelControl() { Text = "Ödeme Tutarı (₺):", Location = new System.Drawing.Point(20, 400) };
                var txtTutar = new SpinEdit() { Location = new System.Drawing.Point(150, 397), Size = new System.Drawing.Size(150, 24) };
                txtTutar.Properties.MinValue = 0;
                txtTutar.Properties.MaxValue = 999999;
                
                // Burs değiştiğinde tutarı güncelle
                cmbBurs.SelectedIndexChanged += (s, ev) =>
                {
                    if (cmbBurs.SelectedItem is BursOdemeItem selectedBurs)
                        txtTutar.EditValue = selectedBurs.Miktar;
                };

                // Tümünü Seç butonu
                var btnTumunuSec = new SimpleButton()
                {
                    Text = "☑️ Tümünü Seç",
                    Location = new System.Drawing.Point(500, 17),
                    Size = new System.Drawing.Size(120, 28)
                };
                btnTumunuSec.Click += (s, ev) => gridViewOdeme.SelectAll();

                // Seçimi Kaldır butonu
                var btnSecimKaldir = new SimpleButton()
                {
                    Text = "☐ Seçimi Kaldır",
                    Location = new System.Drawing.Point(500, 50),
                    Size = new System.Drawing.Size(120, 28)
                };
                btnSecimKaldir.Click += (s, ev) => gridViewOdeme.ClearSelection();

                // Butonlar
                var btnOdemeYap = new SimpleButton()
                {
                    Text = "✅ Seçilenlere Ödeme Yap",
                    Location = new System.Drawing.Point(20, 450),
                    Size = new System.Drawing.Size(200, 40)
                };
                btnOdemeYap.Appearance.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
                btnOdemeYap.Appearance.ForeColor = System.Drawing.Color.White;
                btnOdemeYap.Appearance.Options.UseBackColor = true;
                btnOdemeYap.Appearance.Options.UseForeColor = true;

                var btnIptal = new SimpleButton()
                {
                    Text = "❌ Kapat",
                    Location = new System.Drawing.Point(230, 450),
                    Size = new System.Drawing.Size(120, 40)
                };

                btnIptal.Click += (s, ev) => frm.Close();
                btnOdemeYap.Click += (s, ev) =>
                {
                    if (!(cmbBurs.SelectedItem is BursOdemeItem selectedBurs))
                    {
                        MessageHelper.ShowWarning("Lütfen bir burs seçin.", "Burs Seçilmedi");
                        return;
                    }

                    int[] selectedRows = gridViewOdeme.GetSelectedRows();
                    if (selectedRows.Length == 0)
                    {
                        MessageHelper.ShowWarning("Lütfen en az bir öğrenci seçin.", "Seçim Yapılmadı");
                        return;
                    }

                    decimal tutar = Convert.ToDecimal(txtTutar.EditValue);
                    if (tutar <= 0)
                    {
                        MessageHelper.ShowWarning("Ödeme tutarı 0'dan büyük olmalıdır.", "Geçersiz Tutar");
                        return;
                    }

                    if (MessageHelper.ShowConfirm($"{selectedBurs.BursAdi} bursu için {selectedRows.Length} öğrenciye toplam {tutar * selectedRows.Length:C} ödeme yapmak istediğinize emin misiniz?", "Ödeme Onayı"))
                    {
                        int basarili = 0;
                        int hatali = 0;

                        try
                        {
                            using (var conn = bgl.baglanti())
                            {
                                foreach (int rowHandle in selectedRows)
                                {
                                    try
                                    {
                                        var ogrenciID = gridViewOdeme.GetRowCellValue(rowHandle, "ID");
                                        if (ogrenciID != null && ogrenciID != DBNull.Value)
                                        {
                                            SqlCommand cmd = new SqlCommand(@"INSERT INTO BursGiderleri 
                                                (OgrenciID, BursID, Tutar, Tarih, Aciklama) 
                                                VALUES (@p1, @p2, @p3, @p4, @p5)", conn);
                                            cmd.Parameters.AddWithValue("@p1", ogrenciID);
                                            cmd.Parameters.AddWithValue("@p2", selectedBurs.BursID);
                                            cmd.Parameters.AddWithValue("@p3", tutar);
                                            cmd.Parameters.AddWithValue("@p4", DateTime.Now);
                                            cmd.Parameters.AddWithValue("@p5", $"{selectedBurs.BursAdi} - {DateTime.Now:MMMM yyyy}");
                                            cmd.ExecuteNonQuery();
                                            basarili++;
                                        }
                                    }
                                    catch (Exception rowEx)
                                    {
                                        System.Diagnostics.Debug.WriteLine($"Ödeme satır hatası: {rowEx.Message}");
                                        hatali++;
                                    }
                                }
                            }

                            if (basarili > 0 && hatali == 0)
                            {
                                MessageHelper.ShowSuccess($"{basarili} öğrenciye toplam {tutar * basarili:C} ödeme başarıyla yapıldı.", "Ödeme Başarılı");
                                DataChangedNotifier.NotifyBursChanged();
                                loadStudents(); // Listeyi yenile
                            }
                            else if (basarili > 0 && hatali > 0)
                            {
                                MessageHelper.ShowWarning($"{basarili} öğrenciye ödeme yapıldı, {hatali} öğrenci için başarısız.", "Kısmi Başarı");
                                loadStudents();
                            }
                            else
                            {
                                MessageHelper.ShowError("Ödeme yapılamadı.", "Hata");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageHelper.ShowException(ex, "Ödeme Hatası");
                        }
                    }
                };

                frm.Controls.AddRange(new Control[] { lblBurs, cmbBurs, lblInfo, gridOdeme, btnTumunuSec, btnSecimKaldir, lblTutar, txtTutar, btnOdemeYap, btnIptal });
                
                // İlk yükleme
                if (cmbBurs.Properties.Items.Count > 0)
                {
                    cmbBurs.SelectedIndex = 0;
                    if (cmbBurs.SelectedItem is BursOdemeItem firstBurs)
                        txtTutar.EditValue = firstBurs.Miktar;
                    loadStudents();
                }

                frm.ShowDialog(this);
            }
        }

        // Burs ödeme için yardımcı sınıf
        private class BursOdemeItem
        {
            public int BursID { get; set; }
            public string BursAdi { get; set; }
            public decimal Miktar { get; set; }
            public BursOdemeItem(int id, string adi, decimal miktar) { BursID = id; BursAdi = adi; Miktar = miktar; }
            public override string ToString() => $"{BursAdi} ({Miktar:C}/ay)";
        }

    }
}