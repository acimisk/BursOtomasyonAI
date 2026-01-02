namespace bursoto1.Modules
{
    partial class AnasayfaModule
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
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.tileControl2 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemOgrenci = new DevExpress.XtraEditors.TileItem();
            this.tileItemBasari = new DevExpress.XtraEditors.TileItem();
            this.tileItemBurs = new DevExpress.XtraEditors.TileItem();
            this.tileItemKasa = new DevExpress.XtraEditors.TileItem();
            this.tileItemGelir = new DevExpress.XtraEditors.TileItem();
            this.tileItemGider = new DevExpress.XtraEditors.TileItem();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.tileControl2);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.chartControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 700);
            this.splitContainerControl1.SplitterPosition = 343;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // tileControl2
            // 
            this.tileControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl2.Groups.Add(this.tileGroup2);
            this.tileControl2.ItemContentAnimation = DevExpress.XtraEditors.TileItemContentAnimationType.RandomSegmentedFade;
            this.tileControl2.Location = new System.Drawing.Point(0, 0);
            this.tileControl2.MaxId = 6;
            this.tileControl2.Name = "tileControl2";
            this.tileControl2.Padding = new System.Windows.Forms.Padding(20);
            this.tileControl2.Size = new System.Drawing.Size(1200, 343);
            this.tileControl2.TabIndex = 0;
            this.tileControl2.Text = "tileControl2";
            this.tileControl2.Click += new System.EventHandler(this.tileControl2_Click);
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemOgrenci);
            this.tileGroup2.Items.Add(this.tileItemBasari);
            this.tileGroup2.Items.Add(this.tileItemBurs);
            this.tileGroup2.Items.Add(this.tileItemKasa);
            this.tileGroup2.Items.Add(this.tileItemGelir);
            this.tileGroup2.Items.Add(this.tileItemGider);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItemOgrenci
            // 
            this.tileItemOgrenci.Id = 0;
            this.tileItemOgrenci.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemOgrenci.Name = "tileItemOgrenci";
            this.tileItemOgrenci.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemOgrenci_ItemClick);
            // 
            // tileItemBasari
            // 
            this.tileItemBasari.Id = 1;
            this.tileItemBasari.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemBasari.Name = "tileItemBasari";
            // 
            // tileItemBurs
            // 
            this.tileItemBurs.Id = 2;
            this.tileItemBurs.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemBurs.Name = "tileItemBurs";
            // 
            // tileItemKasa
            // 
            this.tileItemKasa.Id = 3;
            this.tileItemKasa.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemKasa.Name = "tileItemKasa";
            // 
            // tileItemGelir
            // 
            this.tileItemGelir.Id = 4;
            this.tileItemGelir.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemGelir.Name = "tileItemGelir";
            // 
            // tileItemGider
            // 
            this.tileItemGider.Id = 5;
            this.tileItemGider.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItemGider.Name = "tileItemGider";
            // 
            // chartControl1
            // 
            this.chartControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControl1.Legend.AlignmentHorizontal = DevExpress.XtraCharts.LegendAlignmentHorizontal.Center;
            this.chartControl1.Legend.AlignmentVertical = DevExpress.XtraCharts.LegendAlignmentVertical.Bottom;
            this.chartControl1.Legend.Direction = DevExpress.XtraCharts.LegendDirection.LeftToRight;
            this.chartControl1.Location = new System.Drawing.Point(0, 0);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(1200, 345);
            this.chartControl1.TabIndex = 0;
            this.chartControl1.Click += new System.EventHandler(this.chartControl1_Click);
            // 
            // AnasayfaModule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "AnasayfaModule";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Load += new System.EventHandler(this.AnasayfaModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraCharts.ChartControl chartControl1;
        private DevExpress.XtraEditors.TileControl tileControl2;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItemOgrenci;
        private DevExpress.XtraEditors.TileItem tileItemBurs;
        private DevExpress.XtraEditors.TileItem tileItemBasari;
        private DevExpress.XtraEditors.TileItem tileItemKasa;
        private DevExpress.XtraEditors.TileItem tileItemGelir;
        private DevExpress.XtraEditors.TileItem tileItemGider;
    }
}
