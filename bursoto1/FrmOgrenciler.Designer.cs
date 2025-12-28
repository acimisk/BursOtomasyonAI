using System;

namespace bursoto1
{
    partial class FrmOgrenciler
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGoster = new DevExpress.XtraEditors.SimpleButton();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
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
            this.txtOgrAd = new System.Windows.Forms.TextBox();
            this.txtOgrSoyad = new System.Windows.Forms.TextBox();
            this.txtHaneGeliri = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureResim.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(-1, 1);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(795, 347);
            this.gridControl1.TabIndex = 4;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.Click += new System.EventHandler(this.gridControl1_Click);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.Cyan;
            this.gridView1.Appearance.Row.BorderColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.Row.Options.UseBorderColor = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.DoubleClick += new System.EventHandler(this.gridView1_DoubleClick);
            // 
            // btnGoster
            // 
            this.btnGoster.Location = new System.Drawing.Point(292, 354);
            this.btnGoster.Name = "btnGoster";
            this.btnGoster.Size = new System.Drawing.Size(94, 29);
            this.btnGoster.TabIndex = 6;
            this.btnGoster.Text = "Göster";
            this.btnGoster.Click += new System.EventHandler(this.btnGoster_Click);
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.Name = "sqlDataSource1";
            // 
            // groupControl1
            // 
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
            this.groupControl1.Location = new System.Drawing.Point(800, 1);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(713, 347);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "groupControl1";
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
            this.txtSinif.Location = new System.Drawing.Point(237, 166);
            this.txtSinif.Name = "txtSinif";
            this.txtSinif.Size = new System.Drawing.Size(184, 24);
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
            this.txtOgrKardesSayisi.Location = new System.Drawing.Point(237, 105);
            this.txtOgrKardesSayisi.Name = "txtOgrKardesSayisi";
            this.txtOgrKardesSayisi.Size = new System.Drawing.Size(184, 24);
            this.txtOgrKardesSayisi.TabIndex = 39;
            // 
            // txtBolum
            // 
            this.txtBolum.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBolum.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.txtBolum.FormattingEnabled = true;
            this.txtBolum.Location = new System.Drawing.Point(237, 199);
            this.txtBolum.Name = "txtBolum";
            this.txtBolum.Size = new System.Drawing.Size(184, 24);
            this.txtBolum.TabIndex = 38;
            // 
            // txtAgno
            // 
            this.txtAgno.Location = new System.Drawing.Point(237, 274);
            this.txtAgno.Name = "txtAgno";
            this.txtAgno.Size = new System.Drawing.Size(184, 22);
            this.txtAgno.TabIndex = 36;
            // 
            // labelAgno
            // 
            this.labelAgno.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAgno.Appearance.Options.UseFont = true;
            this.labelAgno.Location = new System.Drawing.Point(151, 269);
            this.labelAgno.Name = "labelAgno";
            this.labelAgno.Size = new System.Drawing.Size(73, 31);
            this.labelAgno.TabIndex = 35;
            this.labelAgno.Text = "AGNO:";
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(237, 237);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Properties.NullText = "0(5xx) xxx xx xx";
            this.txtTelNo.Size = new System.Drawing.Size(184, 22);
            this.txtTelNo.TabIndex = 34;
            // 
            // labelNumara
            // 
            this.labelNumara.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelNumara.Appearance.Options.UseFont = true;
            this.labelNumara.Location = new System.Drawing.Point(99, 232);
            this.labelNumara.Name = "labelNumara";
            this.labelNumara.Size = new System.Drawing.Size(125, 31);
            this.labelNumara.TabIndex = 33;
            this.labelNumara.Text = "Telefon No:";
            // 
            // btnResimSec
            // 
            this.btnResimSec.Location = new System.Drawing.Point(503, 270);
            this.btnResimSec.Name = "btnResimSec";
            this.btnResimSec.Size = new System.Drawing.Size(94, 29);
            this.btnResimSec.TabIndex = 32;
            this.btnResimSec.Text = "Resim Seç";
            this.btnResimSec.Click += new System.EventHandler(this.btnResimSec_Click);
            // 
            // pictureResim
            // 
            this.pictureResim.Location = new System.Drawing.Point(463, 51);
            this.pictureResim.Name = "pictureResim";
            this.pictureResim.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureResim.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureResim.Size = new System.Drawing.Size(175, 208);
            this.pictureResim.TabIndex = 31;
            // 
            // labelBolum
            // 
            this.labelBolum.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelBolum.Appearance.Options.UseFont = true;
            this.labelBolum.Location = new System.Drawing.Point(147, 195);
            this.labelBolum.Name = "labelBolum";
            this.labelBolum.Size = new System.Drawing.Size(77, 31);
            this.labelBolum.TabIndex = 29;
            this.labelBolum.Text = "Bölüm:";
            // 
            // labelSinif
            // 
            this.labelSinif.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSinif.Appearance.Options.UseFont = true;
            this.labelSinif.Location = new System.Drawing.Point(168, 162);
            this.labelSinif.Name = "labelSinif";
            this.labelSinif.Size = new System.Drawing.Size(56, 31);
            this.labelSinif.TabIndex = 28;
            this.labelSinif.Text = "Sınıf:";
            // 
            // labelGelir
            // 
            this.labelGelir.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelGelir.Appearance.Options.UseFont = true;
            this.labelGelir.Location = new System.Drawing.Point(11, 132);
            this.labelGelir.Name = "labelGelir";
            this.labelGelir.Size = new System.Drawing.Size(213, 31);
            this.labelGelir.TabIndex = 27;
            this.labelGelir.Text = "Toplam Hane Geliri:";
            // 
            // labelKardes
            // 
            this.labelKardes.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelKardes.Appearance.Options.UseFont = true;
            this.labelKardes.Location = new System.Drawing.Point(79, 102);
            this.labelKardes.Name = "labelKardes";
            this.labelKardes.Size = new System.Drawing.Size(145, 31);
            this.labelKardes.TabIndex = 26;
            this.labelKardes.Text = "Kardeş Sayısı:";
            // 
            // labelSoyad
            // 
            this.labelSoyad.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSoyad.Appearance.Options.UseFont = true;
            this.labelSoyad.Location = new System.Drawing.Point(153, 74);
            this.labelSoyad.Name = "labelSoyad";
            this.labelSoyad.Size = new System.Drawing.Size(71, 31);
            this.labelSoyad.TabIndex = 25;
            this.labelSoyad.Text = "Soyad:";
            // 
            // labelAd
            // 
            this.labelAd.Appearance.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelAd.Appearance.Options.UseFont = true;
            this.labelAd.Location = new System.Drawing.Point(188, 47);
            this.labelAd.Name = "labelAd";
            this.labelAd.Size = new System.Drawing.Size(36, 31);
            this.labelAd.TabIndex = 24;
            this.labelAd.Text = "Ad:";
            // 
            // txtOgrAd
            // 
            this.txtOgrAd.Location = new System.Drawing.Point(237, 47);
            this.txtOgrAd.Name = "txtOgrAd";
            this.txtOgrAd.Size = new System.Drawing.Size(184, 23);
            this.txtOgrAd.TabIndex = 41;
            // 
            // txtOgrSoyad
            // 
            this.txtOgrSoyad.Location = new System.Drawing.Point(237, 76);
            this.txtOgrSoyad.Name = "txtOgrSoyad";
            this.txtOgrSoyad.Size = new System.Drawing.Size(184, 23);
            this.txtOgrSoyad.TabIndex = 42;
            // 
            // txtHaneGeliri
            // 
            this.txtHaneGeliri.Location = new System.Drawing.Point(237, 136);
            this.txtHaneGeliri.Name = "txtHaneGeliri";
            this.txtHaneGeliri.Size = new System.Drawing.Size(184, 23);
            this.txtHaneGeliri.TabIndex = 43;
            // 
            // FrmOgrenciler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Moccasin;
            this.ClientSize = new System.Drawing.Size(1525, 409);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnGoster);
            this.Controls.Add(this.gridControl1);
            this.Name = "FrmOgrenciler";
            this.Text = "FrmOgrenciler";
            this.Load += new System.EventHandler(this.FrmOgrenciler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAgno.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureResim.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            
        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton btnGoster;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        public DevExpress.XtraEditors.TextEdit txtAgno;
        public DevExpress.XtraEditors.LabelControl labelAgno;
        public DevExpress.XtraEditors.TextEdit txtTelNo;
        public DevExpress.XtraEditors.LabelControl labelNumara;
        public DevExpress.XtraEditors.SimpleButton btnResimSec;
        public DevExpress.XtraEditors.PictureEdit pictureResim;
        public DevExpress.XtraEditors.LabelControl labelBolum;
        public DevExpress.XtraEditors.LabelControl labelSinif;
        public DevExpress.XtraEditors.LabelControl labelGelir;
        public DevExpress.XtraEditors.LabelControl labelKardes;
        public DevExpress.XtraEditors.LabelControl labelSoyad;
        public DevExpress.XtraEditors.LabelControl labelAd;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        public DevExpress.XtraGrid.GridControl gridControl1;
        private System.Windows.Forms.ComboBox txtBolum;
        private System.Windows.Forms.ComboBox txtSinif;
        private System.Windows.Forms.ComboBox txtOgrKardesSayisi;
        private System.Windows.Forms.TextBox txtHaneGeliri;
        private System.Windows.Forms.TextBox txtOgrSoyad;
        private System.Windows.Forms.TextBox txtOgrAd;
    }
}