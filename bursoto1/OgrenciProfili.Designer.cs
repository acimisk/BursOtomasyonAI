namespace bursoto1
{
    partial class OgrenciProfili
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabBurs = new DevExpress.XtraTab.XtraTabPage();
            this.lblBaslangicTarihi = new DevExpress.XtraEditors.LabelControl();
            this.lblBursMiktar = new DevExpress.XtraEditors.LabelControl();
            this.lblBursDurum = new DevExpress.XtraEditors.LabelControl();
            this.tabAkademik = new DevExpress.XtraTab.XtraTabPage();
            this.txtAgno = new DevExpress.XtraEditors.TextEdit();
            this.labelAgno = new DevExpress.XtraEditors.LabelControl();
            this.txtSinif = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelSinif = new DevExpress.XtraEditors.LabelControl();
            this.txtBolum = new DevExpress.XtraEditors.TextEdit();
            this.labelBolum = new DevExpress.XtraEditors.LabelControl();
            this.tabGenel = new DevExpress.XtraTab.XtraTabPage();
            this.txtHaneGeliri = new DevExpress.XtraEditors.TextEdit();
            this.labelHaneGeliri = new DevExpress.XtraEditors.LabelControl();
            this.txtOgrKardesSayisi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelKardes = new DevExpress.XtraEditors.LabelControl();
            this.txtTelNo = new DevExpress.XtraEditors.TextEdit();
            this.labelNumara = new DevExpress.XtraEditors.LabelControl();
            this.txtOgrSoyad = new DevExpress.XtraEditors.TextEdit();
            this.labelOgrSoyad = new DevExpress.XtraEditors.LabelControl();
            this.txtOgrAd = new DevExpress.XtraEditors.TextEdit();
            this.labelOgrAd = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabBurs.SuspendLayout();
            this.tabAkademik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSinif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBolum.Properties)).BeginInit();
            this.tabGenel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHaneGeliri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrKardesSayisi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBurs
            // 
            this.tabBurs.Controls.Add(this.lblBursDurum);
            this.tabBurs.Controls.Add(this.lblBursMiktar);
            this.tabBurs.Controls.Add(this.lblBaslangicTarihi);
            this.tabBurs.Name = "tabBurs";
            this.tabBurs.Size = new System.Drawing.Size(898, 599);
            this.tabBurs.Text = "💰 Burs Bilgileri";
            // 
            // lblBaslangicTarihi
            // 
            this.lblBaslangicTarihi.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBaslangicTarihi.Appearance.Options.UseFont = true;
            this.lblBaslangicTarihi.Location = new System.Drawing.Point(30, 149);
            this.lblBaslangicTarihi.Name = "lblBaslangicTarihi";
            this.lblBaslangicTarihi.Size = new System.Drawing.Size(156, 28);
            this.lblBaslangicTarihi.TabIndex = 2;
            this.lblBaslangicTarihi.Text = "Başlangıç Tarihi:";
            // 
            // lblBursMiktar
            // 
            this.lblBursMiktar.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBursMiktar.Appearance.Options.UseFont = true;
            this.lblBursMiktar.Location = new System.Drawing.Point(30, 91);
            this.lblBursMiktar.Name = "lblBursMiktar";
            this.lblBursMiktar.Size = new System.Drawing.Size(122, 28);
            this.lblBursMiktar.TabIndex = 1;
            this.lblBursMiktar.Text = "Burs Miktarı:";
            // 
            // lblBursDurum
            // 
            this.lblBursDurum.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBursDurum.Appearance.Options.UseFont = true;
            this.lblBursDurum.Location = new System.Drawing.Point(30, 34);
            this.lblBursDurum.Name = "lblBursDurum";
            this.lblBursDurum.Size = new System.Drawing.Size(130, 28);
            this.lblBursDurum.TabIndex = 0;
            this.lblBursDurum.Text = "Burs Durumu:";
            // 
            // tabAkademik
            // 
            this.tabAkademik.Controls.Add(this.labelBolum);
            this.tabAkademik.Controls.Add(this.txtBolum);
            this.tabAkademik.Controls.Add(this.labelSinif);
            this.tabAkademik.Controls.Add(this.txtSinif);
            this.tabAkademik.Controls.Add(this.labelAgno);
            this.tabAkademik.Controls.Add(this.txtAgno);
            this.tabAkademik.Name = "tabAkademik";
            this.tabAkademik.Size = new System.Drawing.Size(898, 599);
            this.tabAkademik.Text = "🎓 Akademik Bilgiler";
            // 
            // txtAgno
            // 
            this.txtAgno.Location = new System.Drawing.Point(180, 126);
            this.txtAgno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAgno.Name = "txtAgno";
            this.txtAgno.Properties.ReadOnly = true;
            this.txtAgno.Size = new System.Drawing.Size(180, 22);
            this.txtAgno.TabIndex = 52;
            // 
            // labelAgno
            // 
            this.labelAgno.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAgno.Appearance.Options.UseFont = true;
            this.labelAgno.Location = new System.Drawing.Point(30, 126);
            this.labelAgno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelAgno.Name = "labelAgno";
            this.labelAgno.Size = new System.Drawing.Size(55, 23);
            this.labelAgno.TabIndex = 51;
            this.labelAgno.Text = "AGNO:";
            // 
            // txtSinif
            // 
            this.txtSinif.Location = new System.Drawing.Point(180, 80);
            this.txtSinif.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSinif.Name = "txtSinif";
            this.txtSinif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtSinif.Properties.Items.AddRange(new object[] {
            "Hazırlık",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.txtSinif.Properties.ReadOnly = true;
            this.txtSinif.Size = new System.Drawing.Size(180, 22);
            this.txtSinif.TabIndex = 41;
            // 
            // labelSinif
            // 
            this.labelSinif.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSinif.Appearance.Options.UseFont = true;
            this.labelSinif.Location = new System.Drawing.Point(30, 80);
            this.labelSinif.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelSinif.Name = "labelSinif";
            this.labelSinif.Size = new System.Drawing.Size(42, 23);
            this.labelSinif.TabIndex = 46;
            this.labelSinif.Text = "Sınıf:";
            // 
            // txtBolum
            // 
            this.txtBolum.Location = new System.Drawing.Point(180, 34);
            this.txtBolum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBolum.Name = "txtBolum";
            this.txtBolum.Properties.ReadOnly = true;
            this.txtBolum.Size = new System.Drawing.Size(200, 22);
            this.txtBolum.TabIndex = 48;
            // 
            // labelBolum
            // 
            this.labelBolum.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelBolum.Appearance.Options.UseFont = true;
            this.labelBolum.Location = new System.Drawing.Point(30, 34);
            this.labelBolum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelBolum.Name = "labelBolum";
            this.labelBolum.Size = new System.Drawing.Size(57, 23);
            this.labelBolum.TabIndex = 47;
            this.labelBolum.Text = "Bölüm:";
            // 
            // tabGenel
            // 
            this.tabGenel.Controls.Add(this.pictureEdit1);
            this.tabGenel.Controls.Add(this.labelOgrAd);
            this.tabGenel.Controls.Add(this.txtOgrAd);
            this.tabGenel.Controls.Add(this.labelOgrSoyad);
            this.tabGenel.Controls.Add(this.txtOgrSoyad);
            this.tabGenel.Controls.Add(this.labelNumara);
            this.tabGenel.Controls.Add(this.txtTelNo);
            this.tabGenel.Controls.Add(this.labelKardes);
            this.tabGenel.Controls.Add(this.txtOgrKardesSayisi);
            this.tabGenel.Controls.Add(this.labelHaneGeliri);
            this.tabGenel.Controls.Add(this.txtHaneGeliri);
            this.tabGenel.Name = "tabGenel";
            this.tabGenel.Size = new System.Drawing.Size(898, 599);
            this.tabGenel.Text = "📋 Genel Bilgiler";
            // 
            // txtHaneGeliri
            // 
            this.txtHaneGeliri.Location = new System.Drawing.Point(400, 217);
            this.txtHaneGeliri.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtHaneGeliri.Name = "txtHaneGeliri";
            this.txtHaneGeliri.Properties.ReadOnly = true;
            this.txtHaneGeliri.Size = new System.Drawing.Size(180, 22);
            this.txtHaneGeliri.TabIndex = 5;
            // 
            // labelHaneGeliri
            // 
            this.labelHaneGeliri.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelHaneGeliri.Appearance.Options.UseFont = true;
            this.labelHaneGeliri.Location = new System.Drawing.Point(270, 217);
            this.labelHaneGeliri.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelHaneGeliri.Name = "labelHaneGeliri";
            this.labelHaneGeliri.Size = new System.Drawing.Size(94, 23);
            this.labelHaneGeliri.TabIndex = 6;
            this.labelHaneGeliri.Text = "Hane Geliri:";
            // 
            // txtOgrKardesSayisi
            // 
            this.txtOgrKardesSayisi.Location = new System.Drawing.Point(400, 126);
            this.txtOgrKardesSayisi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOgrKardesSayisi.Name = "txtOgrKardesSayisi";
            this.txtOgrKardesSayisi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtOgrKardesSayisi.Properties.DropDownRows = 5;
            this.txtOgrKardesSayisi.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4+"});
            this.txtOgrKardesSayisi.Properties.ReadOnly = true;
            this.txtOgrKardesSayisi.Size = new System.Drawing.Size(180, 22);
            this.txtOgrKardesSayisi.TabIndex = 39;
            // 
            // labelKardes
            // 
            this.labelKardes.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKardes.Appearance.Options.UseFont = true;
            this.labelKardes.Location = new System.Drawing.Point(270, 126);
            this.labelKardes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelKardes.Name = "labelKardes";
            this.labelKardes.Size = new System.Drawing.Size(109, 23);
            this.labelKardes.TabIndex = 44;
            this.labelKardes.Text = "Kardeş Sayısı:";
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(400, 171);
            this.txtTelNo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Properties.ReadOnly = true;
            this.txtTelNo.Size = new System.Drawing.Size(180, 22);
            this.txtTelNo.TabIndex = 50;
            // 
            // labelNumara
            // 
            this.labelNumara.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelNumara.Appearance.Options.UseFont = true;
            this.labelNumara.Location = new System.Drawing.Point(270, 171);
            this.labelNumara.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelNumara.Name = "labelNumara";
            this.labelNumara.Size = new System.Drawing.Size(93, 23);
            this.labelNumara.TabIndex = 49;
            this.labelNumara.Text = "Telefon No:";
            // 
            // txtOgrSoyad
            // 
            this.txtOgrSoyad.Location = new System.Drawing.Point(400, 80);
            this.txtOgrSoyad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOgrSoyad.Name = "txtOgrSoyad";
            this.txtOgrSoyad.Properties.ReadOnly = true;
            this.txtOgrSoyad.Size = new System.Drawing.Size(180, 22);
            this.txtOgrSoyad.TabIndex = 38;
            // 
            // labelOgrSoyad
            // 
            this.labelOgrSoyad.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOgrSoyad.Appearance.Options.UseFont = true;
            this.labelOgrSoyad.Location = new System.Drawing.Point(270, 80);
            this.labelOgrSoyad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelOgrSoyad.Name = "labelOgrSoyad";
            this.labelOgrSoyad.Size = new System.Drawing.Size(54, 23);
            this.labelOgrSoyad.TabIndex = 2;
            this.labelOgrSoyad.Text = "Soyad:";
            // 
            // txtOgrAd
            // 
            this.txtOgrAd.Location = new System.Drawing.Point(400, 34);
            this.txtOgrAd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOgrAd.Name = "txtOgrAd";
            this.txtOgrAd.Properties.ReadOnly = true;
            this.txtOgrAd.Size = new System.Drawing.Size(180, 22);
            this.txtOgrAd.TabIndex = 37;
            // 
            // labelOgrAd
            // 
            this.labelOgrAd.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOgrAd.Appearance.Options.UseFont = true;
            this.labelOgrAd.Location = new System.Drawing.Point(270, 34);
            this.labelOgrAd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelOgrAd.Name = "labelOgrAd";
            this.labelOgrAd.Size = new System.Drawing.Size(28, 23);
            this.labelOgrAd.TabIndex = 1;
            this.labelOgrAd.Text = "Ad:";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(20, 23);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(220, 320);
            this.pictureEdit1.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabGenel;
            this.xtraTabControl1.Size = new System.Drawing.Size(900, 629);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGenel,
            this.tabAkademik,
            this.tabBurs});
            // 
            // OgrenciProfili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 629);
            this.Controls.Add(this.xtraTabControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "OgrenciProfili";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Öğrenci Profili";
            this.Load += new System.EventHandler(this.OgrenciProfili_Load);
            this.tabBurs.ResumeLayout(false);
            this.tabBurs.PerformLayout();
            this.tabAkademik.ResumeLayout(false);
            this.tabAkademik.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSinif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBolum.Properties)).EndInit();
            this.tabGenel.ResumeLayout(false);
            this.tabGenel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHaneGeliri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrKardesSayisi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTab.XtraTabPage tabBurs;
        private DevExpress.XtraEditors.LabelControl lblBursDurum;
        private DevExpress.XtraEditors.LabelControl lblBursMiktar;
        private DevExpress.XtraEditors.LabelControl lblBaslangicTarihi;
        private DevExpress.XtraTab.XtraTabPage tabAkademik;
        public DevExpress.XtraEditors.LabelControl labelBolum;
        public DevExpress.XtraEditors.TextEdit txtBolum;
        public DevExpress.XtraEditors.LabelControl labelSinif;
        public DevExpress.XtraEditors.ComboBoxEdit txtSinif;
        public DevExpress.XtraEditors.LabelControl labelAgno;
        public DevExpress.XtraEditors.TextEdit txtAgno;
        private DevExpress.XtraTab.XtraTabPage tabGenel;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelOgrAd;
        public DevExpress.XtraEditors.TextEdit txtOgrAd;
        private DevExpress.XtraEditors.LabelControl labelOgrSoyad;
        public DevExpress.XtraEditors.TextEdit txtOgrSoyad;
        public DevExpress.XtraEditors.LabelControl labelNumara;
        public DevExpress.XtraEditors.TextEdit txtTelNo;
        public DevExpress.XtraEditors.LabelControl labelKardes;
        public DevExpress.XtraEditors.ComboBoxEdit txtOgrKardesSayisi;
        private DevExpress.XtraEditors.LabelControl labelHaneGeliri;
        private DevExpress.XtraEditors.TextEdit txtHaneGeliri;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
    }
}