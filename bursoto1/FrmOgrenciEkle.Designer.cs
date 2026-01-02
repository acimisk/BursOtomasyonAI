namespace bursoto1
{
    partial class FrmOgrenciEkle
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.btnIptal = new DevExpress.XtraEditors.SimpleButton();
            this.btnKaydet = new DevExpress.XtraEditors.SimpleButton();
            this.txtHaneGeliri = new System.Windows.Forms.TextBox();
            this.txtOgrSoyad = new System.Windows.Forms.TextBox();
            this.txtOgrAd = new System.Windows.Forms.TextBox();
            this.txtSinif = new System.Windows.Forms.ComboBox();
            this.txtOgrKardesSayisi = new System.Windows.Forms.ComboBox();
            this.txtBolum = new System.Windows.Forms.ComboBox();
            this.txtAgno = new DevExpress.XtraEditors.TextEdit();
            this.labelAgno = new DevExpress.XtraEditors.LabelControl();
            this.txtTelNo = new DevExpress.XtraEditors.TextEdit();
            this.labelNumara = new DevExpress.XtraEditors.LabelControl();
            this.btnResimSec = new DevExpress.XtraEditors.SimpleButton();
            this.pictureResim = new DevExpress.XtraEditors.PictureEdit();
            this.labelBolum = new DevExpress.XtraEditors.LabelControl();
            this.labelSinif = new DevExpress.XtraEditors.LabelControl();
            this.labelGelir = new DevExpress.XtraEditors.LabelControl();
            this.labelKardes = new DevExpress.XtraEditors.LabelControl();
            this.labelSoyad = new DevExpress.XtraEditors.LabelControl();
            this.labelAd = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureResim.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.btnIptal);
            this.groupControl1.Controls.Add(this.btnKaydet);
            this.groupControl1.Controls.Add(this.txtHaneGeliri);
            this.groupControl1.Controls.Add(this.txtOgrSoyad);
            this.groupControl1.Controls.Add(this.txtOgrAd);
            this.groupControl1.Controls.Add(this.txtSinif);
            this.groupControl1.Controls.Add(this.txtOgrKardesSayisi);
            this.groupControl1.Controls.Add(this.txtBolum);
            this.groupControl1.Controls.Add(this.txtAgno);
            this.groupControl1.Controls.Add(this.labelAgno);
            this.groupControl1.Controls.Add(this.txtTelNo);
            this.groupControl1.Controls.Add(this.labelNumara);
            this.groupControl1.Controls.Add(this.btnResimSec);
            this.groupControl1.Controls.Add(this.pictureResim);
            this.groupControl1.Controls.Add(this.labelBolum);
            this.groupControl1.Controls.Add(this.labelSinif);
            this.groupControl1.Controls.Add(this.labelGelir);
            this.groupControl1.Controls.Add(this.labelKardes);
            this.groupControl1.Controls.Add(this.labelSoyad);
            this.groupControl1.Controls.Add(this.labelAd);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(700, 550);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Yeni Öğrenci Ekle";
            // 
            // btnIptal
            // 
            this.btnIptal.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnIptal.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnIptal.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Appearance.Options.UseBackColor = true;
            this.btnIptal.Appearance.Options.UseFont = true;
            this.btnIptal.Appearance.Options.UseForeColor = true;
            this.btnIptal.Location = new System.Drawing.Point(370, 320);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(158, 40);
            this.btnIptal.TabIndex = 45;
            this.btnIptal.Text = "❌ İptal";
            this.btnIptal.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnKaydet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Appearance.Options.UseBackColor = true;
            this.btnKaydet.Appearance.Options.UseFont = true;
            this.btnKaydet.Appearance.Options.UseForeColor = true;
            this.btnKaydet.Location = new System.Drawing.Point(207, 320);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(158, 40);
            this.btnKaydet.TabIndex = 44;
            this.btnKaydet.Text = "✅ Kaydet";
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // txtHaneGeliri
            // 
            this.txtHaneGeliri.Location = new System.Drawing.Point(207, 136);
            this.txtHaneGeliri.Name = "txtHaneGeliri";
            this.txtHaneGeliri.Size = new System.Drawing.Size(162, 23);
            this.txtHaneGeliri.TabIndex = 43;
            // 
            // txtOgrSoyad
            // 
            this.txtOgrSoyad.Location = new System.Drawing.Point(207, 76);
            this.txtOgrSoyad.Name = "txtOgrSoyad";
            this.txtOgrSoyad.Size = new System.Drawing.Size(162, 23);
            this.txtOgrSoyad.TabIndex = 42;
            // 
            // txtOgrAd
            // 
            this.txtOgrAd.Location = new System.Drawing.Point(207, 47);
            this.txtOgrAd.Name = "txtOgrAd";
            this.txtOgrAd.Size = new System.Drawing.Size(162, 23);
            this.txtOgrAd.TabIndex = 41;
            // 
            // txtSinif
            // 
            this.txtSinif.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtSinif.FormattingEnabled = true;
            this.txtSinif.Items.AddRange(new object[] {
            "Hazırlık",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.txtSinif.Location = new System.Drawing.Point(207, 166);
            this.txtSinif.Name = "txtSinif";
            this.txtSinif.Size = new System.Drawing.Size(162, 24);
            this.txtSinif.TabIndex = 40;
            // 
            // txtOgrKardesSayisi
            // 
            this.txtOgrKardesSayisi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtOgrKardesSayisi.FormattingEnabled = true;
            this.txtOgrKardesSayisi.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4+"});
            this.txtOgrKardesSayisi.Location = new System.Drawing.Point(207, 105);
            this.txtOgrKardesSayisi.Name = "txtOgrKardesSayisi";
            this.txtOgrKardesSayisi.Size = new System.Drawing.Size(162, 24);
            this.txtOgrKardesSayisi.TabIndex = 39;
            // 
            // txtBolum
            // 
            this.txtBolum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBolum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtBolum.FormattingEnabled = true;
            this.txtBolum.Location = new System.Drawing.Point(207, 199);
            this.txtBolum.Name = "txtBolum";
            this.txtBolum.Size = new System.Drawing.Size(162, 24);
            this.txtBolum.TabIndex = 38;
            // 
            // txtAgno
            // 
            this.txtAgno.Location = new System.Drawing.Point(207, 274);
            this.txtAgno.Name = "txtAgno";
            this.txtAgno.Size = new System.Drawing.Size(161, 22);
            this.txtAgno.TabIndex = 36;
            // 
            // labelAgno
            // 
            this.labelAgno.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelAgno.Appearance.Options.UseFont = true;
            this.labelAgno.Location = new System.Drawing.Point(132, 269);
            this.labelAgno.Name = "labelAgno";
            this.labelAgno.Size = new System.Drawing.Size(73, 31);
            this.labelAgno.TabIndex = 35;
            this.labelAgno.Text = "AGNO:";
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(207, 237);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Properties.NullText = "0(5xx) xxx xx xx";
            this.txtTelNo.Size = new System.Drawing.Size(161, 22);
            this.txtTelNo.TabIndex = 34;
            // 
            // labelNumara
            // 
            this.labelNumara.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelNumara.Appearance.Options.UseFont = true;
            this.labelNumara.Location = new System.Drawing.Point(87, 232);
            this.labelNumara.Name = "labelNumara";
            this.labelNumara.Size = new System.Drawing.Size(125, 31);
            this.labelNumara.TabIndex = 33;
            this.labelNumara.Text = "Telefon No:";
            // 
            // btnResimSec
            // 
            this.btnResimSec.Location = new System.Drawing.Point(440, 270);
            this.btnResimSec.Name = "btnResimSec";
            this.btnResimSec.Size = new System.Drawing.Size(82, 29);
            this.btnResimSec.TabIndex = 32;
            this.btnResimSec.Text = "Resim Seç";
            this.btnResimSec.Click += new System.EventHandler(this.btnResimSec_Click);
            // 
            // pictureResim
            // 
            this.pictureResim.Location = new System.Drawing.Point(405, 51);
            this.pictureResim.Name = "pictureResim";
            this.pictureResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureResim.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureResim.Size = new System.Drawing.Size(153, 208);
            this.pictureResim.TabIndex = 31;
            // 
            // labelBolum
            // 
            this.labelBolum.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelBolum.Appearance.Options.UseFont = true;
            this.labelBolum.Location = new System.Drawing.Point(129, 195);
            this.labelBolum.Name = "labelBolum";
            this.labelBolum.Size = new System.Drawing.Size(77, 31);
            this.labelBolum.TabIndex = 29;
            this.labelBolum.Text = "Bölüm:";
            // 
            // labelSinif
            // 
            this.labelSinif.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelSinif.Appearance.Options.UseFont = true;
            this.labelSinif.Location = new System.Drawing.Point(147, 162);
            this.labelSinif.Name = "labelSinif";
            this.labelSinif.Size = new System.Drawing.Size(56, 31);
            this.labelSinif.TabIndex = 28;
            this.labelSinif.Text = "Sınıf:";
            // 
            // labelGelir
            // 
            this.labelGelir.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelGelir.Appearance.Options.UseFont = true;
            this.labelGelir.Location = new System.Drawing.Point(10, 132);
            this.labelGelir.Name = "labelGelir";
            this.labelGelir.Size = new System.Drawing.Size(213, 31);
            this.labelGelir.TabIndex = 27;
            this.labelGelir.Text = "Toplam Hane Geliri:";
            // 
            // labelKardes
            // 
            this.labelKardes.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelKardes.Appearance.Options.UseFont = true;
            this.labelKardes.Location = new System.Drawing.Point(69, 102);
            this.labelKardes.Name = "labelKardes";
            this.labelKardes.Size = new System.Drawing.Size(145, 31);
            this.labelKardes.TabIndex = 26;
            this.labelKardes.Text = "Kardeş Sayısı:";
            // 
            // labelSoyad
            // 
            this.labelSoyad.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelSoyad.Appearance.Options.UseFont = true;
            this.labelSoyad.Location = new System.Drawing.Point(134, 74);
            this.labelSoyad.Name = "labelSoyad";
            this.labelSoyad.Size = new System.Drawing.Size(71, 31);
            this.labelSoyad.TabIndex = 25;
            this.labelSoyad.Text = "Soyad:";
            // 
            // labelAd
            // 
            this.labelAd.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold);
            this.labelAd.Appearance.Options.UseFont = true;
            this.labelAd.Location = new System.Drawing.Point(164, 47);
            this.labelAd.Name = "labelAd";
            this.labelAd.Size = new System.Drawing.Size(36, 31);
            this.labelAd.TabIndex = 24;
            this.labelAd.Text = "Ad:";
            // 
            // FrmOgrenciEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 550);
            this.Controls.Add(this.groupControl1);
            this.Name = "FrmOgrenciEkle";
            this.Text = "Yeni Öğrenci Ekle";
            this.Load += new System.EventHandler(this.FrmOgrenciEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureResim.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.ComboBox txtSinif;
        private System.Windows.Forms.ComboBox txtOgrKardesSayisi;
        private System.Windows.Forms.ComboBox txtBolum;
        private DevExpress.XtraEditors.TextEdit txtAgno;
        private DevExpress.XtraEditors.LabelControl labelAgno;
        private DevExpress.XtraEditors.TextEdit txtTelNo;
        private DevExpress.XtraEditors.LabelControl labelNumara;
        private DevExpress.XtraEditors.SimpleButton btnResimSec;
        private DevExpress.XtraEditors.PictureEdit pictureResim;
        private DevExpress.XtraEditors.LabelControl labelBolum;
        private DevExpress.XtraEditors.LabelControl labelSinif;
        private DevExpress.XtraEditors.LabelControl labelGelir;
        private DevExpress.XtraEditors.LabelControl labelKardes;
        private DevExpress.XtraEditors.LabelControl labelSoyad;
        private DevExpress.XtraEditors.LabelControl labelAd;
        private System.Windows.Forms.TextBox txtOgrAd;
        private System.Windows.Forms.TextBox txtOgrSoyad;
        private System.Windows.Forms.TextBox txtHaneGeliri;
        private DevExpress.XtraEditors.SimpleButton btnKaydet;
        private DevExpress.XtraEditors.SimpleButton btnIptal;
    }
}

