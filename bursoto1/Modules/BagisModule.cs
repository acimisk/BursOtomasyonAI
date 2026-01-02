using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using bursoto1.Helpers;

namespace bursoto1.Modules
{
    public partial class BagisModule : XtraUserControl
    {
        SqlBaglanti bgl = new SqlBaglanti();

        public BagisModule()
        {
            InitializeComponent();
            ConfigGrid();
        }

        void ConfigGrid()
        {
            if (gridView1 != null)
            {
                gridView1.OptionsBehavior.Editable = false;
                gridView1.OptionsView.ShowGroupPanel = false;
                gridView1.OptionsSelection.MultiSelect = false; // Tek seçim
                
                // Row style event'i bağla (renklendirme için)
                gridView1.RowStyle += GridView1_RowStyle;
            }
        }

        private void BagisModule_Load(object sender, EventArgs e)
        {
            Listele();
            SetupContextMenu();
            ApplyDarkGrid(gridView1);
        }

        // Dark Mode Grid Ayarları
        private void ApplyDarkGrid(GridView gv)
        {
            // SATIRLAR
            gv.Appearance.Row.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Row.ForeColor = Color.White;
            gv.Appearance.Row.Options.UseBackColor = true;
            gv.Appearance.Row.Options.UseForeColor = true;

            // BAŞLIK
            gv.Appearance.HeaderPanel.BackColor = Color.FromArgb(45, 45, 48);
            gv.Appearance.HeaderPanel.ForeColor = Color.White;
            gv.Appearance.HeaderPanel.Options.UseBackColor = true;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;

            // SEÇİLİ SATIR
            gv.Appearance.FocusedRow.BackColor = Color.FromArgb(70, 70, 70);
            gv.Appearance.FocusedRow.ForeColor = Color.White;
            gv.Appearance.FocusedRow.Options.UseBackColor = true;
            gv.Appearance.FocusedRow.Options.UseForeColor = true;

            gv.Appearance.SelectedRow.BackColor = Color.FromArgb(60, 60, 60);
            gv.Appearance.SelectedRow.ForeColor = Color.White;
            gv.Appearance.SelectedRow.Options.UseBackColor = true;
            gv.Appearance.SelectedRow.Options.UseForeColor = true;

            // BOŞ ALAN
            gv.Appearance.Empty.BackColor = Color.FromArgb(32, 32, 32);
            gv.Appearance.Empty.Options.UseBackColor = true;

            gv.OptionsView.EnableAppearanceEvenRow = false;
            gv.OptionsView.EnableAppearanceOddRow = false;

            // GRID CONTROL ARKAPLAN
            if (gridControl1 != null)
                gridControl1.BackColor = Color.FromArgb(32, 32, 32);
        }

        void SetupContextMenu()
        {
            // Sağ tık menüsü (ported from master FrmBursVerenler)
            ContextMenuStrip sagTik = new ContextMenuStrip();
            ToolStripMenuItem itemOnayla = new ToolStripMenuItem("✅ Bağışı Onayla");
            ToolStripMenuItem itemReddet = new ToolStripMenuItem("❌ Bağışı Reddet");
            ToolStripMenuItem itemSil = new ToolStripMenuItem("🗑️ Kaydı Sil");

            itemOnayla.Click += ItemOnayla_Click;
            itemReddet.Click += ItemReddet_Click;
            itemSil.Click += ItemSil_Click;

            sagTik.Items.Add(itemOnayla);
            sagTik.Items.Add(itemReddet);
            sagTik.Items.Add(new ToolStripSeparator());
            sagTik.Items.Add(itemSil);
            
            if (gridControl1 != null)
                gridControl1.ContextMenuStrip = sagTik;
        }

        // Renklendirme (Dark Mode uyumlu)
        private void GridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                string durum = gridView1.GetRowCellDisplayText(e.RowHandle, "Durum");
                if (durum == "Onaylandı")
                {
                    // Dark mode yeşil
                    e.Appearance.BackColor = Color.FromArgb(30, 80, 50);
                    e.Appearance.ForeColor = Color.FromArgb(150, 255, 150);
                }
                else if (durum == "Beklemede")
                {
                    // Dark mode sarı/turuncu
                    e.Appearance.BackColor = Color.FromArgb(80, 70, 30);
                    e.Appearance.ForeColor = Color.FromArgb(255, 220, 100);
                }
                else
                {
                    // Default dark
                    e.Appearance.BackColor = Color.FromArgb(32, 32, 32);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        public void Listele()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM BursVerenler", conn);
                    da.Fill(dt);
                    
                    // Debug: Kaç kayıt geldi
                    System.Diagnostics.Debug.WriteLine($"BursVerenler tablosundan {dt.Rows.Count} kayıt çekildi.");
                    
                    // Kolon isimlerini logla
                    if (dt.Columns.Count > 0)
                    {
                        string kolonlar = string.Join(", ", dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName));
                        System.Diagnostics.Debug.WriteLine($"BursVerenler kolonları: {kolonlar}");
                    }
                    
                    gridControl1.DataSource = dt;
                }
                
                // ID kolonunu gizle (varsa)
                if (gridView1.Columns["ID"] != null)
                    gridView1.Columns["ID"].Visible = false;
                    
                gridView1.BestFitColumns();
                
                // Kayıt sayısını göster (debug için)
                if (dt.Rows.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("UYARI: BursVerenler tablosu boş!");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowException(ex, "Bağışçı Listeleme Hatası");
                System.Diagnostics.Debug.WriteLine($"BagisModule Listele hatası: {ex.Message}");
            }
        }

        // --- BUTONLAR ---
        public void btnEkle_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Inline bağışçı ekleme dialogu
            using (XtraForm frm = new XtraForm())
            {
                frm.Text = "Yeni Bağışçı Ekle";
                frm.Size = new System.Drawing.Size(450, 350);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.FormBorderStyle = FormBorderStyle.FixedDialog;
                frm.MaximizeBox = false;
                frm.MinimizeBox = false;

                // Form kontrolleri
                LabelControl lblAd = new LabelControl() { Text = "Ad Soyad:", Location = new System.Drawing.Point(20, 30) };
                TextEdit txtAd = new TextEdit() { Location = new System.Drawing.Point(150, 27), Size = new System.Drawing.Size(250, 22) };

                LabelControl lblTel = new LabelControl() { Text = "Telefon:", Location = new System.Drawing.Point(20, 70) };
                TextEdit txtTel = new TextEdit() { Location = new System.Drawing.Point(150, 67), Size = new System.Drawing.Size(250, 22) };

                LabelControl lblEmail = new LabelControl() { Text = "E-posta:", Location = new System.Drawing.Point(20, 110) };
                TextEdit txtEmail = new TextEdit() { Location = new System.Drawing.Point(150, 107), Size = new System.Drawing.Size(250, 22) };

                LabelControl lblMiktar = new LabelControl() { Text = "Bağış Miktarı (₺):", Location = new System.Drawing.Point(20, 150) };
                SpinEdit txtMiktar = new SpinEdit() { Location = new System.Drawing.Point(150, 147), Size = new System.Drawing.Size(150, 22) };
                txtMiktar.Properties.MinValue = 0;
                txtMiktar.Properties.MaxValue = 10000000;
                txtMiktar.Properties.IsFloatValue = true;

                LabelControl lblAciklama = new LabelControl() { Text = "Açıklama:", Location = new System.Drawing.Point(20, 190) };
                MemoEdit txtAciklama = new MemoEdit() { Location = new System.Drawing.Point(150, 187), Size = new System.Drawing.Size(250, 60) };

                SimpleButton btnKaydet = new SimpleButton() { Text = "Kaydet", Location = new System.Drawing.Point(150, 265), Size = new System.Drawing.Size(100, 35) };
                SimpleButton btnIptal = new SimpleButton() { Text = "İptal", Location = new System.Drawing.Point(260, 265), Size = new System.Drawing.Size(100, 35) };

                btnIptal.Click += (s, ev) => frm.DialogResult = DialogResult.Cancel;
                btnKaydet.Click += (s, ev) =>
                {
                    if (string.IsNullOrWhiteSpace(txtAd.Text))
                    {
                        MessageHelper.ShowWarning("Ad Soyad zorunludur.", "Eksik Bilgi");
                        return;
                    }
                    if (Convert.ToDecimal(txtMiktar.EditValue) <= 0)
                    {
                        MessageHelper.ShowWarning("Bağış miktarı 0'dan büyük olmalıdır.", "Eksik Bilgi");
                        return;
                    }

                    try
                    {
                        using (SqlConnection conn = bgl.baglanti())
                        {
                            SqlCommand cmd = new SqlCommand(@"INSERT INTO BursVerenler 
                                (AdSoyad, Telefon, Eposta, BagisMiktari, Aciklama, Durum, Tarih) 
                                VALUES (@p1, @p2, @p3, @p4, @p5, 'Beklemede', @p6)", conn);
                            cmd.Parameters.AddWithValue("@p1", txtAd.Text.Trim());
                            cmd.Parameters.AddWithValue("@p2", txtTel.Text.Trim());
                            cmd.Parameters.AddWithValue("@p3", txtEmail.Text.Trim());
                            cmd.Parameters.AddWithValue("@p4", Convert.ToDecimal(txtMiktar.EditValue));
                            cmd.Parameters.AddWithValue("@p5", txtAciklama.Text.Trim());
                            cmd.Parameters.AddWithValue("@p6", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        MessageHelper.ShowSuccess("Bağışçı başarıyla eklendi.", "Kayıt Başarılı");
                        frm.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageHelper.ShowException(ex, "Kayıt Hatası");
                    }
                };

                frm.Controls.AddRange(new Control[] { lblAd, txtAd, lblTel, txtTel, lblEmail, txtEmail, lblMiktar, txtMiktar, lblAciklama, txtAciklama, btnKaydet, btnIptal });
                
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
            }
        }

        public void btnSil_ItemClick(object sender, ItemClickEventArgs e)
        {
            var id = gridView1.GetFocusedRowCellValue("ID");
            if (id == null) return;

            if (MessageHelper.ShowConfirm("Bağışçıyı silmek istiyor musunuz?", "Silme"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex) { MessageHelper.ShowException(ex, "Silinemedi"); }
            }
        }

        // --- SAĞ TIK MENÜ İŞLEMLERİ (ported from master) ---
        private void ItemOnayla_Click(object sender, EventArgs e)
        {
            BagisiOnayla();
        }

        private void ItemReddet_Click(object sender, EventArgs e)
        {
            BagisiReddet();
        }

        private void ItemSil_Click(object sender, EventArgs e)
        {
            btnSil_ItemClick(sender, null);
        }

        private void BagisiOnayla()
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen onaylamak istediğiniz bağışı seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string adSoyad = dr["AdSoyad"]?.ToString() ?? "Bilinmeyen";
            decimal miktar = Convert.ToDecimal(dr["BagisMiktari"] ?? 0);
            string id = dr["ID"].ToString();

            if (MessageHelper.ShowConfirm(
                $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışını onaylıyor musunuz?\n\n" +
                "Onaylandıktan sonra bağışçı aktif bağışçılar listesine taşınacak.",
                "Bağışı Onayla"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE BursVerenler SET Durum='Onaylandı' WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess(
                        $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışı onaylandı.\n" +
                        "Bağışçı aktif bağışçılar listesine taşındı.",
                        "Onay Başarılı");
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Onay Hatası");
                }
            }
        }

        private void BagisiReddet()
        {
            var dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr == null)
            {
                MessageHelper.ShowWarning("Lütfen reddetmek istediğiniz bağışı seçiniz.", "Seçim Yapılmadı");
                return;
            }

            string adSoyad = dr["AdSoyad"]?.ToString() ?? "Bilinmeyen";
            decimal miktar = Convert.ToDecimal(dr["BagisMiktari"] ?? 0);
            string id = dr["ID"].ToString();

            if (MessageHelper.ShowConfirm(
                $"{adSoyad} kişisinin {miktar:C} tutarındaki bağışını reddetmek istediğinize emin misiniz?\n\n" +
                "Reddedilen bağış kaydı silinecektir.",
                "Bağışı Reddet"))
            {
                try
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM BursVerenler WHERE ID=@p1", conn);
                        cmd.Parameters.AddWithValue("@p1", id);
                        cmd.ExecuteNonQuery();
                    }

                    MessageHelper.ShowSuccess(
                        $"{adSoyad} kişisinin bağışı reddedildi ve kayıt silindi.",
                        "Reddetme Başarılı");
                    Listele();
                    DataChangedNotifier.NotifyBursVerenChanged();
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowException(ex, "Reddetme Hatası");
                }
            }
        }
    }
}