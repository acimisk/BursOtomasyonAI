namespace bursoto1
{
    partial class Anasayfa
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
            DevExpress.XtraEditors.TileItemElement tileItemElement13 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement14 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement15 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemOgrenci = new DevExpress.XtraEditors.TileItem();
            this.tileItemBurs = new DevExpress.XtraEditors.TileItem();
            this.tileItemBasari = new DevExpress.XtraEditors.TileItem();
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup4 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup5 = new DevExpress.XtraEditors.TileGroup();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.ItemContentAnimation = DevExpress.XtraEditors.TileItemContentAnimationType.RandomSegmentedFade;
            this.tileControl1.Location = new System.Drawing.Point(625, 164);
            this.tileControl1.MaxId = 3;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(812, 269);
            this.tileControl1.TabIndex = 3;
            this.tileControl1.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemOgrenci);
            this.tileGroup2.Items.Add(this.tileItemBurs);
            this.tileGroup2.Items.Add(this.tileItemBasari);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItemOgrenci
            // 
            tileItemElement13.Text = "tileItemogrenci";
            this.tileItemOgrenci.Elements.Add(tileItemElement13);
            this.tileItemOgrenci.Id = 0;
            this.tileItemOgrenci.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemOgrenci.Name = "tileItemOgrenci";
            // 
            // tileItemBurs
            // 
            tileItemElement14.Text = "tileItemBurs";
            this.tileItemBurs.Elements.Add(tileItemElement14);
            this.tileItemBurs.Id = 1;
            this.tileItemBurs.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemBurs.Name = "tileItemBurs";
            this.tileItemBurs.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemBurs_ItemClick);
            // 
            // tileItemBasari
            // 
            tileItemElement15.Text = "tileItem1";
            this.tileItemBasari.Elements.Add(tileItemElement15);
            this.tileItemBasari.Id = 2;
            this.tileItemBasari.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemBasari.Name = "tileItemBasari";
            // 
            // tileGroup1
            // 
            this.tileGroup1.Name = "tileGroup1";
            // 
            // tileGroup3
            // 
            this.tileGroup3.Name = "tileGroup3";
            // 
            // tileGroup4
            // 
            this.tileGroup4.Name = "tileGroup4";
            // 
            // tileGroup5
            // 
            this.tileGroup5.Name = "tileGroup5";
            // 
            // chartControl1
            // 
            this.chartControl1.Location = new System.Drawing.Point(27, 27);
            this.chartControl1.Name = "chartControl1";
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chartControl1.Size = new System.Drawing.Size(592, 507);
            this.chartControl1.TabIndex = 4;
            // 
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 546);
            this.Controls.Add(this.chartControl1);
            this.Controls.Add(this.tileControl1);
            this.Name = "Anasayfa";
            this.Text = "Anasayfa";
            this.Load += new System.EventHandler(this.Anasayfa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItemOgrenci;
        private DevExpress.XtraEditors.TileItem tileItemBurs;
        private DevExpress.XtraEditors.TileGroup tileGroup1;
        private DevExpress.XtraEditors.TileGroup tileGroup3;
        private DevExpress.XtraEditors.TileGroup tileGroup4;
        private DevExpress.XtraEditors.TileGroup tileGroup5;
        private DevExpress.XtraEditors.TileItem tileItemBasari;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}