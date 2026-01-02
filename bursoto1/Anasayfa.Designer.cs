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
            DevExpress.XtraEditors.TileItemElement tileItemElement9 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement10 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement11 = new DevExpress.XtraEditors.TileItemElement();
            DevExpress.XtraEditors.TileItemElement tileItemElement12 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItemOgrenci = new DevExpress.XtraEditors.TileItem();
            this.tileItemBurs = new DevExpress.XtraEditors.TileItem();
            this.tileItemBasari = new DevExpress.XtraEditors.TileItem();
            this.tileItemKasa = new DevExpress.XtraEditors.TileItem();
            this.tileItemGelir = new DevExpress.XtraEditors.TileItem();
            this.tileItemGider = new DevExpress.XtraEditors.TileItem();
            this.tileGroup1 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup3 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup4 = new DevExpress.XtraEditors.TileGroup();
            this.tileGroup5 = new DevExpress.XtraEditors.TileGroup();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.ItemContentAnimation = DevExpress.XtraEditors.TileItemContentAnimationType.RandomSegmentedFade;
            this.tileControl1.Location = new System.Drawing.Point(625, -2);
            this.tileControl1.MaxId = 5;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Size = new System.Drawing.Size(602, 569);
            this.tileControl1.TabIndex = 3;
            this.tileControl1.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItemOgrenci);
            this.tileGroup2.Items.Add(this.tileItemBurs);
            this.tileGroup2.Items.Add(this.tileItemBasari);
            this.tileGroup2.Items.Add(this.tileItemKasa);
            this.tileGroup2.Items.Add(this.tileItemGelir);
            this.tileGroup2.Items.Add(this.tileItemGider);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItemOgrenci
            // 
            tileItemElement9.Text = "tileItemogrenci";
            this.tileItemOgrenci.Elements.Add(tileItemElement9);
            this.tileItemOgrenci.Id = 0;
            this.tileItemOgrenci.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemOgrenci.Name = "tileItemOgrenci";
            // 
            // tileItemBurs
            // 
            tileItemElement10.Text = "tileItemBurs";
            this.tileItemBurs.Elements.Add(tileItemElement10);
            this.tileItemBurs.Id = 1;
            this.tileItemBurs.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemBurs.Name = "tileItemBurs";
            this.tileItemBurs.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItemBurs_ItemClick);
            // 
            // tileItemBasari
            // 
            tileItemElement11.Text = "tileItem1";
            this.tileItemBasari.Elements.Add(tileItemElement11);
            this.tileItemBasari.Id = 2;
            this.tileItemBasari.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemBasari.Name = "tileItemBasari";
            // 
            // tileItemKasa
            // 
            tileItemElement12.Text = "tileItemKasa";
            this.tileItemKasa.Elements.Add(tileItemElement12);
            this.tileItemKasa.Id = 3;
            this.tileItemKasa.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemKasa.Name = "tileItemKasa";
            // 
            // tileItemGelir
            // 
            this.tileItemGelir.Id = 4;
            this.tileItemGelir.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemGelir.Name = "tileItemGelir";
            // 
            // tileItemGider
            // 
            this.tileItemGider.Id = 5;
            this.tileItemGider.ItemSize = DevExpress.XtraEditors.TileItemSize.Large;
            this.tileItemGider.Name = "tileItemGider";
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
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 642);
            this.Controls.Add(this.tileControl1);
            this.Name = "Anasayfa";
            this.Text = "Anasayfa";
            this.Load += new System.EventHandler(this.Anasayfa_Load);
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
        private DevExpress.XtraEditors.TileItem tileItemKasa;
        private DevExpress.XtraEditors.TileItem tileItemGelir;
        private DevExpress.XtraEditors.TileItem tileItemGider;
    }
}