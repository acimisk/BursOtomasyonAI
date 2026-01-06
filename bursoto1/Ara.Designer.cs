namespace bursoto1
{
    partial class Ara
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridAraSonuc = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnOgrGoster = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtSınıf = new DevExpress.XtraEditors.TextEdit();
            this.txtTelNo = new DevExpress.XtraEditors.TextEdit();
            this.txtAraBolum = new DevExpress.XtraEditors.TextEdit();
            this.txtAraAd = new DevExpress.XtraEditors.TextEdit();
            this.txtAraSoyad = new DevExpress.XtraEditors.TextEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridAraSonuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSınıf.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraBolum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridAraSonuc);
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1544, 539);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Öğrenci Arama";
            // 
            // gridAraSonuc
            // 
            this.gridAraSonuc.Location = new System.Drawing.Point(463, 0);
            this.gridAraSonuc.MainView = this.gridView1;
            this.gridAraSonuc.Name = "gridAraSonuc";
            this.gridAraSonuc.Size = new System.Drawing.Size(1069, 370);
            this.gridAraSonuc.TabIndex = 1;
            this.gridAraSonuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridAraSonuc;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // btnOgrGoster
            // 
            this.btnOgrGoster.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOgrGoster.Appearance.Options.UseFont = true;
            this.btnOgrGoster.AppearanceHovered.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOgrGoster.AppearanceHovered.Options.UseFont = true;
            this.btnOgrGoster.Location = new System.Drawing.Point(14, 304);
            this.btnOgrGoster.Name = "btnOgrGoster";
            this.btnOgrGoster.Size = new System.Drawing.Size(429, 33);
            this.btnOgrGoster.StyleController = this.layoutControl1;
            this.btnOgrGoster.TabIndex = 11;
            this.btnOgrGoster.Text = "Öğrenci Profilini Göster";
            this.btnOgrGoster.Click += new System.EventHandler(this.btnOgrGoster_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnOgrGoster);
            this.layoutControl1.Controls.Add(this.txtSınıf);
            this.layoutControl1.Controls.Add(this.txtTelNo);
            this.layoutControl1.Controls.Add(this.txtAraBolum);
            this.layoutControl1.Controls.Add(this.txtAraAd);
            this.layoutControl1.Controls.Add(this.txtAraSoyad);
            this.layoutControl1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControl1.Location = new System.Drawing.Point(0, 30);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(457, 374);
            this.layoutControl1.TabIndex = 10;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtSınıf
            // 
            this.txtSınıf.Location = new System.Drawing.Point(14, 278);
            this.txtSınıf.Name = "txtSınıf";
            this.txtSınıf.Size = new System.Drawing.Size(429, 22);
            this.txtSınıf.StyleController = this.layoutControl1;
            this.txtSınıf.TabIndex = 9;
            this.txtSınıf.EditValueChanged += new System.EventHandler(this.txtSınıf_EditValueChanged);
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(14, 220);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Size = new System.Drawing.Size(429, 22);
            this.txtTelNo.StyleController = this.layoutControl1;
            this.txtTelNo.TabIndex = 8;
            this.txtTelNo.EditValueChanged += new System.EventHandler(this.txtTelNo_EditValueChanged);
            // 
            // txtAraBolum
            // 
            this.txtAraBolum.Location = new System.Drawing.Point(14, 46);
            this.txtAraBolum.Name = "txtAraBolum";
            this.txtAraBolum.Size = new System.Drawing.Size(429, 22);
            this.txtAraBolum.StyleController = this.layoutControl1;
            this.txtAraBolum.TabIndex = 2;
            this.txtAraBolum.EditValueChanged += new System.EventHandler(this.txtAraBolum_EditValueChanged);
            // 
            // txtAraAd
            // 
            this.txtAraAd.Location = new System.Drawing.Point(14, 104);
            this.txtAraAd.Name = "txtAraAd";
            this.txtAraAd.Size = new System.Drawing.Size(429, 22);
            this.txtAraAd.StyleController = this.layoutControl1;
            this.txtAraAd.TabIndex = 0;
            this.txtAraAd.EditValueChanged += new System.EventHandler(this.txtAraAd_EditValueChanged);
            // 
            // txtAraSoyad
            // 
            this.txtAraSoyad.Location = new System.Drawing.Point(14, 162);
            this.txtAraSoyad.Name = "txtAraSoyad";
            this.txtAraSoyad.Size = new System.Drawing.Size(429, 22);
            this.txtAraSoyad.StyleController = this.layoutControl1;
            this.txtAraSoyad.TabIndex = 1;
            this.txtAraSoyad.EditValueChanged += new System.EventHandler(this.txtAraSoyad_EditValueChanged);
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem10,
            this.layoutControlItem11,
            this.layoutControlItem4});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(457, 374);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItem1.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem1.Control = this.txtAraBolum;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(433, 58);
            this.layoutControlItem1.Text = "Bölüm";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(69, 28);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItem2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem2.Control = this.txtAraAd;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 58);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(433, 58);
            this.layoutControlItem2.Text = "Ad";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(69, 28);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem3.Control = this.txtAraSoyad;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 116);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(433, 58);
            this.layoutControlItem3.Text = "Soyad";
            this.layoutControlItem3.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(69, 28);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem10.Control = this.txtTelNo;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 174);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(433, 58);
            this.layoutControlItem10.Text = "Telefon";
            this.layoutControlItem10.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(69, 28);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.AppearanceItemCaption.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.Control = this.txtSınıf;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 232);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(433, 58);
            this.layoutControlItem11.Text = "Sınıf";
            this.layoutControlItem11.TextLocation = DevExpress.Utils.Locations.Top;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(69, 28);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnOgrGoster;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 290);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(433, 60);
            this.layoutControlItem4.TextVisible = false;
            // 
            // Ara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1544, 539);
            this.Controls.Add(this.groupControl1);
            this.Name = "Ara";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ara";
            this.Load += new System.EventHandler(this.Ara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridAraSonuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSınıf.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraBolum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtAraBolum;
        private DevExpress.XtraEditors.TextEdit txtAraSoyad;
        private DevExpress.XtraEditors.TextEdit txtAraAd;
        private DevExpress.XtraGrid.GridControl gridAraSonuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.TextEdit txtSınıf;
        private DevExpress.XtraEditors.TextEdit txtTelNo;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraEditors.SimpleButton btnOgrGoster;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
    }
}