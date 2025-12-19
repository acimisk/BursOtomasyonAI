namespace bursoto1
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnOgrenciler = new DevExpress.XtraBars.BarButtonItem();
            this.btnEkle = new DevExpress.XtraBars.BarButtonItem();
            this.btnSil = new DevExpress.XtraBars.BarButtonItem();
            this.btnAnasayfa = new DevExpress.XtraBars.BarButtonItem();
            this.btnAra = new DevExpress.XtraBars.BarButtonItem();
            this.btnBursTurleri = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 669);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(1201, 30);
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.btnOgrenciler,
            this.btnEkle,
            this.btnSil,
            this.btnAnasayfa,
            this.btnAra,
            this.btnBursTurleri});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 10;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1});
            this.ribbon.Size = new System.Drawing.Size(1201, 193);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // btnOgrenciler
            // 
            this.btnOgrenciler.Caption = "ÖĞRENCİLER";
            this.btnOgrenciler.Id = 3;
            this.btnOgrenciler.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOgrenciler.ImageOptions.Image")));
            this.btnOgrenciler.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOgrenciler.ImageOptions.LargeImage")));
            this.btnOgrenciler.Name = "btnOgrenciler";
            this.btnOgrenciler.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOgrenciler_ItemClick);
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
            this.btnSil.Id = 4;
            this.btnSil.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnSil.ImageOptions.SvgImage")));
            this.btnSil.Name = "btnSil";
            this.btnSil.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // btnAnasayfa
            // 
            this.btnAnasayfa.Caption = "ANA SAYFA";
            this.btnAnasayfa.Id = 5;
            this.btnAnasayfa.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAnasayfa.ImageOptions.Image")));
            this.btnAnasayfa.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAnasayfa.ImageOptions.LargeImage")));
            this.btnAnasayfa.Name = "btnAnasayfa";
            this.btnAnasayfa.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAnasayfa_ItemClick);
            // 
            // btnAra
            // 
            this.btnAra.Caption = "Ara";
            this.btnAra.Id = 8;
            this.btnAra.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAra.ImageOptions.Image")));
            this.btnAra.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnAra.ImageOptions.LargeImage")));
            this.btnAra.Name = "btnAra";
            this.btnAra.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAra_ItemClick);
            // 
            // btnBursTurleri
            // 
            this.btnBursTurleri.Caption = "Burslar";
            this.btnBursTurleri.Id = 9;
            this.btnBursTurleri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBursTurleri.ImageOptions.Image")));
            this.btnBursTurleri.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBursTurleri.ImageOptions.LargeImage")));
            this.btnBursTurleri.Name = "btnBursTurleri";
            this.btnBursTurleri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBursTurleri_ItemClick);
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
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAnasayfa);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnOgrenciler);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnEkle);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSil);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnAra);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBursTurleri);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // Menu
            // 
            this.AllowMdiBar = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1201, 699);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.IsMdiContainer = true;
            this.Name = "Menu";
            this.Ribbon = this.ribbon;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.Load += new System.EventHandler(this.Menu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem btnOgrenciler;
        private DevExpress.XtraBars.BarButtonItem btnEkle;
        private DevExpress.XtraBars.BarButtonItem btnSil;
        private DevExpress.XtraBars.BarButtonItem btnAnasayfa;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem btnAra;
        private DevExpress.XtraBars.BarButtonItem btnBursTurleri;
    }
}