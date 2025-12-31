namespace bursoto1.Modules
{
    partial class OgrenciModule
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OgrenciModule));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnGoster = new DevExpress.XtraEditors.SimpleButton();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.panelAI = new DevExpress.XtraEditors.PanelControl();
            this.lblAIsonuc = new DevExpress.XtraEditors.LabelControl();
            this.lblAIbaslik = new DevExpress.XtraEditors.LabelControl();
            this.lblFiltre = new DevExpress.XtraEditors.LabelControl();
            this.cmbFiltre = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnBursReddet = new DevExpress.XtraEditors.SimpleButton();
            this.btnBursKabul = new DevExpress.XtraEditors.SimpleButton();
            this.btnAIAnaliz = new DevExpress.XtraEditors.SimpleButton();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnEkle = new DevExpress.XtraBars.BarButtonItem();
            this.btnSil = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAI)).BeginInit();
            this.panelAI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFiltre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            gridLevelNode1.RelationName = "Level1";
            this.gridControl1.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.gridControl1.Location = new System.Drawing.Point(33, 264);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(824, 410);
            this.gridControl1.TabIndex = 13;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Row.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
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
            this.btnGoster.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnGoster.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGoster.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnGoster.Appearance.Options.UseBackColor = true;
            this.btnGoster.Appearance.Options.UseFont = true;
            this.btnGoster.Appearance.Options.UseForeColor = true;
            this.btnGoster.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(80)))), ((int)(((byte)(87)))));
            this.btnGoster.AppearancePressed.Options.UseBackColor = true;
            this.btnGoster.Location = new System.Drawing.Point(33, 680);
            this.btnGoster.Name = "btnGoster";
            this.btnGoster.Size = new System.Drawing.Size(126, 39);
            this.btnGoster.TabIndex = 14;
            this.btnGoster.Text = "Profil Göster";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.Name = "sqlDataSource1";
            // 
            // panelAI
            // 
            this.panelAI.Appearance.BackColor = System.Drawing.Color.White;
            this.panelAI.Appearance.Options.UseBackColor = true;
            this.panelAI.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.panelAI.Controls.Add(this.lblAIsonuc);
            this.panelAI.Controls.Add(this.lblAIbaslik);
            this.panelAI.Location = new System.Drawing.Point(896, 264);
            this.panelAI.Name = "panelAI";
            this.panelAI.Size = new System.Drawing.Size(439, 290);
            this.panelAI.TabIndex = 17;
            // 
            // lblAIsonuc
            // 
            this.lblAIsonuc.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAIsonuc.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.lblAIsonuc.Appearance.Options.UseFont = true;
            this.lblAIsonuc.Appearance.Options.UseForeColor = true;
            this.lblAIsonuc.Appearance.Options.UseTextOptions = true;
            this.lblAIsonuc.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblAIsonuc.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblAIsonuc.Location = new System.Drawing.Point(15, 55);
            this.lblAIsonuc.Name = "lblAIsonuc";
            this.lblAIsonuc.Size = new System.Drawing.Size(290, 330);
            this.lblAIsonuc.TabIndex = 1;
            this.lblAIsonuc.Text = "Bir öğrenci seçip \"AI Analiz\" butonuna tıklayın.\n\nAI, öğrencinin burs uygunluğunu" +
    " değerlendirecek ve puan verecektir.";
            // 
            // lblAIbaslik
            // 
            this.lblAIbaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAIbaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblAIbaslik.Appearance.Options.UseFont = true;
            this.lblAIbaslik.Appearance.Options.UseForeColor = true;
            this.lblAIbaslik.Location = new System.Drawing.Point(15, 15);
            this.lblAIbaslik.Name = "lblAIbaslik";
            this.lblAIbaslik.Size = new System.Drawing.Size(194, 28);
            this.lblAIbaslik.TabIndex = 0;
            this.lblAIbaslik.Text = "🤖 AI Analiz Sonucu";
            // 
            // lblFiltre
            // 
            this.lblFiltre.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblFiltre.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblFiltre.Appearance.Options.UseFont = true;
            this.lblFiltre.Appearance.Options.UseForeColor = true;
            this.lblFiltre.Location = new System.Drawing.Point(27, 221);
            this.lblFiltre.Name = "lblFiltre";
            this.lblFiltre.Size = new System.Drawing.Size(86, 23);
            this.lblFiltre.TabIndex = 16;
            this.lblFiltre.Text = "Filtreleme:";
            // 
            // cmbFiltre
            // 
            this.cmbFiltre.Location = new System.Drawing.Point(154, 218);
            this.cmbFiltre.Name = "cmbFiltre";
            this.cmbFiltre.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.cmbFiltre.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cmbFiltre.Properties.Appearance.Options.UseBackColor = true;
            this.cmbFiltre.Properties.Appearance.Options.UseFont = true;
            this.cmbFiltre.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFiltre.Properties.Items.AddRange(new object[] {
            "Tüm Öğrenciler",
            "Burs Alanlar",
            "Beklemedeki Öğrenciler"});
            this.cmbFiltre.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFiltre.Size = new System.Drawing.Size(194, 28);
            this.cmbFiltre.TabIndex = 15;
            // 
            // btnBursReddet
            // 
            this.btnBursReddet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnBursReddet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBursReddet.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnBursReddet.Appearance.Options.UseBackColor = true;
            this.btnBursReddet.Appearance.Options.UseFont = true;
            this.btnBursReddet.Appearance.Options.UseForeColor = true;
            this.btnBursReddet.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnBursReddet.AppearancePressed.Options.UseBackColor = true;
            this.btnBursReddet.Location = new System.Drawing.Point(1072, 636);
            this.btnBursReddet.Name = "btnBursReddet";
            this.btnBursReddet.Size = new System.Drawing.Size(160, 28);
            this.btnBursReddet.TabIndex = 20;
            this.btnBursReddet.Text = "✗ Bursu Reddet";
            // 
            // btnBursKabul
            // 
            this.btnBursKabul.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnBursKabul.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBursKabul.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnBursKabul.Appearance.Options.UseBackColor = true;
            this.btnBursKabul.Appearance.Options.UseFont = true;
            this.btnBursKabul.Appearance.Options.UseForeColor = true;
            this.btnBursKabul.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(153)))), ((int)(((byte)(84)))));
            this.btnBursKabul.AppearancePressed.Options.UseBackColor = true;
            this.btnBursKabul.Location = new System.Drawing.Point(907, 636);
            this.btnBursKabul.Name = "btnBursKabul";
            this.btnBursKabul.Size = new System.Drawing.Size(160, 28);
            this.btnBursKabul.TabIndex = 19;
            this.btnBursKabul.Text = "✓ Bursu Kabul Et";
            // 
            // btnAIAnaliz
            // 
            this.btnAIAnaliz.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAIAnaliz.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAIAnaliz.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnAIAnaliz.Appearance.Options.UseBackColor = true;
            this.btnAIAnaliz.Appearance.Options.UseFont = true;
            this.btnAIAnaliz.Appearance.Options.UseForeColor = true;
            this.btnAIAnaliz.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnAIAnaliz.AppearancePressed.Options.UseBackColor = true;
            this.btnAIAnaliz.Location = new System.Drawing.Point(907, 577);
            this.btnAIAnaliz.Name = "btnAIAnaliz";
            this.btnAIAnaliz.Size = new System.Drawing.Size(325, 28);
            this.btnAIAnaliz.TabIndex = 18;
            this.btnAIAnaliz.Text = "🤖 AI Analiz Yap";
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnEkle,
            this.btnSil});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 4;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1434, 183);
            this.ribbonControl1.Click += new System.EventHandler(this.ribbonControl1_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Caption = "Ekle";
            this.btnEkle.Id = 2;
            this.btnEkle.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnEkle.ImageOptions.Image")));
            this.btnEkle.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnEkle.ImageOptions.LargeImage")));
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnEkle_ItemClick);
            // 
            // btnSil
            // 
            this.btnSil.Caption = "Sil";
            this.btnSil.Id = 3;
            this.btnSil.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSil.ImageOptions.Image")));
            this.btnSil.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnSil.ImageOptions.LargeImage")));
            this.btnSil.Name = "btnSil";
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEkle);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSil);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // OgrenciModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ribbonControl1);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnGoster);
            this.Controls.Add(this.panelAI);
            this.Controls.Add(this.lblFiltre);
            this.Controls.Add(this.cmbFiltre);
            this.Controls.Add(this.btnBursReddet);
            this.Controls.Add(this.btnBursKabul);
            this.Controls.Add(this.btnAIAnaliz);
            this.Name = "OgrenciModule";
            this.Size = new System.Drawing.Size(1434, 741);
            this.Load += new System.EventHandler(this.OgrenciModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelAI)).EndInit();
            this.panelAI.ResumeLayout(false);
            this.panelAI.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFiltre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gridControl1;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnGoster;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraEditors.PanelControl panelAI;
        private DevExpress.XtraEditors.LabelControl lblAIsonuc;
        private DevExpress.XtraEditors.LabelControl lblAIbaslik;
        private DevExpress.XtraEditors.LabelControl lblFiltre;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFiltre;
        private DevExpress.XtraEditors.SimpleButton btnBursReddet;
        private DevExpress.XtraEditors.SimpleButton btnBursKabul;
        private DevExpress.XtraEditors.SimpleButton btnAIAnaliz;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnEkle;
        private DevExpress.XtraBars.BarButtonItem btnSil;
    }
}
