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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.tabGenel = new DevExpress.XtraTab.XtraTabPage();
            this.tabAkademik = new DevExpress.XtraTab.XtraTabPage();
            this.tabBurs = new DevExpress.XtraTab.XtraTabPage();
            this.tabAI = new DevExpress.XtraTab.XtraTabPage();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.labelOgrAd = new DevExpress.XtraEditors.LabelControl();
            this.labelOgrSoyad = new DevExpress.XtraEditors.LabelControl();
            this.txtHaneGeliri = new DevExpress.XtraEditors.TextEdit();
            this.labelHaneGeliri = new DevExpress.XtraEditors.LabelControl();
            this.txtAgno = new DevExpress.XtraEditors.TextEdit();
            this.labelAgno = new DevExpress.XtraEditors.LabelControl();
            this.txtTelNo = new DevExpress.XtraEditors.TextEdit();
            this.labelNumara = new DevExpress.XtraEditors.LabelControl();
            this.txtBolum = new DevExpress.XtraEditors.TextEdit();
            this.labelBolum = new DevExpress.XtraEditors.LabelControl();
            this.labelSinif = new DevExpress.XtraEditors.LabelControl();
            this.labelKardes = new DevExpress.XtraEditors.LabelControl();
            this.txtOgrSoyad = new DevExpress.XtraEditors.TextEdit();
            this.txtOgrAd = new DevExpress.XtraEditors.TextEdit();
            this.txtOgrKardesSayisi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtSinif = new DevExpress.XtraEditors.ComboBoxEdit();
            this.rtbAnalizSonuc = new DevExpress.XtraEditors.MemoEdit();
            this.btnAIAnaliz = new DevExpress.XtraEditors.SimpleButton();
            this.txtAISkor = new DevExpress.XtraEditors.TextEdit();
            this.lblBursDurum = new DevExpress.XtraEditors.LabelControl();
            this.lblBursMiktar = new DevExpress.XtraEditors.LabelControl();
            this.lblBaslangicTarihi = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.tabGenel.SuspendLayout();
            this.tabAkademik.SuspendLayout();
            this.tabBurs.SuspendLayout();
            this.tabAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHaneGeliri.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBolum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrKardesSayisi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSinif.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbAnalizSonuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAISkor.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.tabGenel;
            this.xtraTabControl1.Size = new System.Drawing.Size(900, 550);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGenel,
            this.tabAkademik,
            this.tabBurs,
            this.tabAI});
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
            this.tabGenel.Size = new System.Drawing.Size(898, 519);
            this.tabGenel.Text = "📋 Genel Bilgiler";
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
            this.tabAkademik.Size = new System.Drawing.Size(898, 519);
            this.tabAkademik.Text = "🎓 Akademik Bilgiler";
            // 
            // tabBurs
            // 
            this.tabBurs.Controls.Add(this.lblBursDurum);
            this.tabBurs.Controls.Add(this.lblBursMiktar);
            this.tabBurs.Controls.Add(this.lblBaslangicTarihi);
            this.tabBurs.Name = "tabBurs";
            this.tabBurs.Size = new System.Drawing.Size(898, 519);
            this.tabBurs.Text = "💰 Burs Bilgileri";
            // 
            // tabAI
            // 
            this.tabAI.Controls.Add(this.btnAIAnaliz);
            this.tabAI.Controls.Add(this.txtAISkor);
            this.tabAI.Controls.Add(this.rtbAnalizSonuc);
            this.tabAI.Name = "tabAI";
            this.tabAI.Size = new System.Drawing.Size(898, 519);
            this.tabAI.Text = "🤖 AI Analiz";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(20, 20);
            this.pictureEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEdit1.Size = new System.Drawing.Size(220, 280);
            this.pictureEdit1.TabIndex = 0;
            // 
            // labelOgrAd
            // 
            this.labelOgrAd.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOgrAd.Appearance.Options.UseFont = true;
            this.labelOgrAd.Location = new System.Drawing.Point(270, 30);
            this.labelOgrAd.Margin = new System.Windows.Forms.Padding(4);
            this.labelOgrAd.Name = "labelOgrAd";
            this.labelOgrAd.Size = new System.Drawing.Size(30, 23);
            this.labelOgrAd.TabIndex = 1;
            this.labelOgrAd.Text = "Ad:";
            // 
            // labelOgrSoyad
            // 
            this.labelOgrSoyad.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOgrSoyad.Appearance.Options.UseFont = true;
            this.labelOgrSoyad.Location = new System.Drawing.Point(270, 70);
            this.labelOgrSoyad.Margin = new System.Windows.Forms.Padding(4);
            this.labelOgrSoyad.Name = "labelOgrSoyad";
            this.labelOgrSoyad.Size = new System.Drawing.Size(55, 23);
            this.labelOgrSoyad.TabIndex = 2;
            this.labelOgrSoyad.Text = "Soyad:";
            // 
            // txtHaneGeliri
            // 
            this.txtHaneGeliri.Location = new System.Drawing.Point(400, 190);
            this.txtHaneGeliri.Margin = new System.Windows.Forms.Padding(4);
            this.txtHaneGeliri.Name = "txtHaneGeliri";
            this.txtHaneGeliri.Properties.ReadOnly = true;
            this.txtHaneGeliri.Size = new System.Drawing.Size(180, 22);
            this.txtHaneGeliri.TabIndex = 5;
            // 
            // labelHaneGeliri
            // 
            this.labelHaneGeliri.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelHaneGeliri.Appearance.Options.UseFont = true;
            this.labelHaneGeliri.Location = new System.Drawing.Point(270, 190);
            this.labelHaneGeliri.Margin = new System.Windows.Forms.Padding(4);
            this.labelHaneGeliri.Name = "labelHaneGeliri";
            this.labelHaneGeliri.Size = new System.Drawing.Size(89, 23);
            this.labelHaneGeliri.TabIndex = 6;
            this.labelHaneGeliri.Text = "Hane Geliri:";
            // 
            // txtAgno
            // 
            this.txtAgno.Location = new System.Drawing.Point(180, 110);
            this.txtAgno.Margin = new System.Windows.Forms.Padding(4);
            this.txtAgno.Name = "txtAgno";
            this.txtAgno.Properties.ReadOnly = true;
            this.txtAgno.Size = new System.Drawing.Size(180, 22);
            this.txtAgno.TabIndex = 52;
            // 
            // labelAgno
            // 
            this.labelAgno.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAgno.Appearance.Options.UseFont = true;
            this.labelAgno.Location = new System.Drawing.Point(30, 110);
            this.labelAgno.Margin = new System.Windows.Forms.Padding(4);
            this.labelAgno.Name = "labelAgno";
            this.labelAgno.Size = new System.Drawing.Size(55, 23);
            this.labelAgno.TabIndex = 51;
            this.labelAgno.Text = "AGNO:";
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(400, 150);
            this.txtTelNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Properties.ReadOnly = true;
            this.txtTelNo.Size = new System.Drawing.Size(180, 22);
            this.txtTelNo.TabIndex = 50;
            // 
            // labelNumara
            // 
            this.labelNumara.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelNumara.Appearance.Options.UseFont = true;
            this.labelNumara.Location = new System.Drawing.Point(270, 150);
            this.labelNumara.Margin = new System.Windows.Forms.Padding(4);
            this.labelNumara.Name = "labelNumara";
            this.labelNumara.Size = new System.Drawing.Size(93, 23);
            this.labelNumara.TabIndex = 49;
            this.labelNumara.Text = "Telefon No:";
            // 
            // txtBolum
            // 
            this.txtBolum.Location = new System.Drawing.Point(180, 30);
            this.txtBolum.Margin = new System.Windows.Forms.Padding(4);
            this.txtBolum.Name = "txtBolum";
            this.txtBolum.Properties.ReadOnly = true;
            this.txtBolum.Size = new System.Drawing.Size(200, 22);
            this.txtBolum.TabIndex = 48;
            // 
            // labelBolum
            // 
            this.labelBolum.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelBolum.Appearance.Options.UseFont = true;
            this.labelBolum.Location = new System.Drawing.Point(30, 30);
            this.labelBolum.Margin = new System.Windows.Forms.Padding(4);
            this.labelBolum.Name = "labelBolum";
            this.labelBolum.Size = new System.Drawing.Size(57, 23);
            this.labelBolum.TabIndex = 47;
            this.labelBolum.Text = "Bölüm:";
            // 
            // labelSinif
            // 
            this.labelSinif.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSinif.Appearance.Options.UseFont = true;
            this.labelSinif.Location = new System.Drawing.Point(30, 70);
            this.labelSinif.Margin = new System.Windows.Forms.Padding(4);
            this.labelSinif.Name = "labelSinif";
            this.labelSinif.Size = new System.Drawing.Size(40, 23);
            this.labelSinif.TabIndex = 46;
            this.labelSinif.Text = "Sınıf:";
            // 
            // labelKardes
            // 
            this.labelKardes.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKardes.Appearance.Options.UseFont = true;
            this.labelKardes.Location = new System.Drawing.Point(270, 110);
            this.labelKardes.Margin = new System.Windows.Forms.Padding(4);
            this.labelKardes.Name = "labelKardes";
            this.labelKardes.Size = new System.Drawing.Size(109, 23);
            this.labelKardes.TabIndex = 44;
            this.labelKardes.Text = "Kardeş Sayısı:";
            // 
            // txtOgrSoyad
            // 
            this.txtOgrSoyad.Location = new System.Drawing.Point(400, 70);
            this.txtOgrSoyad.Margin = new System.Windows.Forms.Padding(4);
            this.txtOgrSoyad.Name = "txtOgrSoyad";
            this.txtOgrSoyad.Properties.ReadOnly = true;
            this.txtOgrSoyad.Size = new System.Drawing.Size(180, 22);
            this.txtOgrSoyad.TabIndex = 38;
            // 
            // txtOgrAd
            // 
            this.txtOgrAd.Location = new System.Drawing.Point(400, 30);
            this.txtOgrAd.Margin = new System.Windows.Forms.Padding(4);
            this.txtOgrAd.Name = "txtOgrAd";
            this.txtOgrAd.Properties.ReadOnly = true;
            this.txtOgrAd.Size = new System.Drawing.Size(180, 22);
            this.txtOgrAd.TabIndex = 37;
            // 
            // txtOgrKardesSayisi
            // 
            this.txtOgrKardesSayisi.Location = new System.Drawing.Point(400, 110);
            this.txtOgrKardesSayisi.Margin = new System.Windows.Forms.Padding(4);
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
            // txtSinif
            // 
            this.txtSinif.Location = new System.Drawing.Point(180, 70);
            this.txtSinif.Margin = new System.Windows.Forms.Padding(4);
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
            // rtbAnalizSonuc
            // 
            this.rtbAnalizSonuc.Location = new System.Drawing.Point(30, 100);
            this.rtbAnalizSonuc.Name = "rtbAnalizSonuc";
            this.rtbAnalizSonuc.Size = new System.Drawing.Size(600, 300);
            this.rtbAnalizSonuc.TabIndex = 53;
            // 
            // btnAIAnaliz
            // 
            this.btnAIAnaliz.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAIAnaliz.Appearance.Options.UseFont = true;
            this.btnAIAnaliz.Location = new System.Drawing.Point(30, 30);
            this.btnAIAnaliz.Margin = new System.Windows.Forms.Padding(4);
            this.btnAIAnaliz.Name = "btnAIAnaliz";
            this.btnAIAnaliz.Size = new System.Drawing.Size(150, 45);
            this.btnAIAnaliz.TabIndex = 54;
            this.btnAIAnaliz.Text = "🤖 AI Analiz Yap";
            this.btnAIAnaliz.Click += new System.EventHandler(this.btnAIAnaliz_Click);
            // 
            // txtAISkor
            // 
            this.txtAISkor.EditValue = "";
            this.txtAISkor.Location = new System.Drawing.Point(200, 37);
            this.txtAISkor.Margin = new System.Windows.Forms.Padding(4);
            this.txtAISkor.Name = "txtAISkor";
            this.txtAISkor.Properties.ReadOnly = true;
            this.txtAISkor.Size = new System.Drawing.Size(100, 22);
            this.txtAISkor.TabIndex = 55;
            // 
            // lblBursDurum
            // 
            this.lblBursDurum.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBursDurum.Appearance.Options.UseFont = true;
            this.lblBursDurum.Location = new System.Drawing.Point(30, 30);
            this.lblBursDurum.Name = "lblBursDurum";
            this.lblBursDurum.Size = new System.Drawing.Size(120, 28);
            this.lblBursDurum.TabIndex = 0;
            this.lblBursDurum.Text = "Burs Durumu:";
            // 
            // lblBursMiktar
            // 
            this.lblBursMiktar.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBursMiktar.Appearance.Options.UseFont = true;
            this.lblBursMiktar.Location = new System.Drawing.Point(30, 80);
            this.lblBursMiktar.Name = "lblBursMiktar";
            this.lblBursMiktar.Size = new System.Drawing.Size(130, 28);
            this.lblBursMiktar.TabIndex = 1;
            this.lblBursMiktar.Text = "Burs Miktarı:";
            // 
            // lblBaslangicTarihi
            // 
            this.lblBaslangicTarihi.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBaslangicTarihi.Appearance.Options.UseFont = true;
            this.lblBaslangicTarihi.Location = new System.Drawing.Point(30, 130);
            this.lblBaslangicTarihi.Name = "lblBaslangicTarihi";
            this.lblBaslangicTarihi.Size = new System.Drawing.Size(140, 28);
            this.lblBaslangicTarihi.TabIndex = 2;
            this.lblBaslangicTarihi.Text = "Başlangıç Tarihi:";
            // 
            // OgrenciProfili
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.xtraTabControl1);
            this.IconOptions.ShowIcon = false;
            this.Name = "OgrenciProfili";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Öğrenci Profili";
            this.Load += new System.EventHandler(this.OgrenciProfili_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.tabGenel.ResumeLayout(false);
            this.tabGenel.PerformLayout();
            this.tabAkademik.ResumeLayout(false);
            this.tabAkademik.PerformLayout();
            this.tabBurs.ResumeLayout(false);
            this.tabBurs.PerformLayout();
            this.tabAI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHaneGeliri.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBolum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOgrKardesSayisi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSinif.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtbAnalizSonuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAISkor.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage tabGenel;
        private DevExpress.XtraTab.XtraTabPage tabAkademik;
        private DevExpress.XtraTab.XtraTabPage tabBurs;
        private DevExpress.XtraTab.XtraTabPage tabAI;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl labelOgrAd;
        private DevExpress.XtraEditors.LabelControl labelOgrSoyad;
        private DevExpress.XtraEditors.TextEdit txtHaneGeliri;
        private DevExpress.XtraEditors.LabelControl labelHaneGeliri;
        public DevExpress.XtraEditors.TextEdit txtAgno;
        public DevExpress.XtraEditors.LabelControl labelAgno;
        public DevExpress.XtraEditors.TextEdit txtTelNo;
        public DevExpress.XtraEditors.LabelControl labelNumara;
        public DevExpress.XtraEditors.TextEdit txtBolum;
        public DevExpress.XtraEditors.LabelControl labelBolum;
        public DevExpress.XtraEditors.LabelControl labelSinif;
        public DevExpress.XtraEditors.LabelControl labelKardes;
        public DevExpress.XtraEditors.TextEdit txtOgrSoyad;
        public DevExpress.XtraEditors.TextEdit txtOgrAd;
        public DevExpress.XtraEditors.ComboBoxEdit txtOgrKardesSayisi;
        public DevExpress.XtraEditors.ComboBoxEdit txtSinif;
        private DevExpress.XtraEditors.MemoEdit rtbAnalizSonuc;
        private DevExpress.XtraEditors.SimpleButton btnAIAnaliz;
        private DevExpress.XtraEditors.TextEdit txtAISkor;
        private DevExpress.XtraEditors.LabelControl lblBursDurum;
        private DevExpress.XtraEditors.LabelControl lblBursMiktar;
        private DevExpress.XtraEditors.LabelControl lblBaslangicTarihi;
    }
}