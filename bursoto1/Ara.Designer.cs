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
            this.txtAraAd = new DevExpress.XtraEditors.TextEdit();
            this.txtAraSoyad = new DevExpress.XtraEditors.TextEdit();
            this.txtAraBolum = new DevExpress.XtraEditors.TextEdit();
            this.gridAraSonuc = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnSorgula = new DevExpress.XtraEditors.SimpleButton();
            this.labelOgrAd = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtTelNo = new DevExpress.XtraEditors.TextEdit();
            this.txtSınıf = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraSoyad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraBolum.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAraSonuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSınıf.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtSınıf);
            this.groupControl1.Controls.Add(this.txtTelNo);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.btnSorgula);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelOgrAd);
            this.groupControl1.Controls.Add(this.txtAraBolum);
            this.groupControl1.Controls.Add(this.txtAraSoyad);
            this.groupControl1.Controls.Add(this.txtAraAd);
            this.groupControl1.Location = new System.Drawing.Point(3, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(298, 401);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Öğrenci Arama";
            // 
            // txtAraAd
            // 
            this.txtAraAd.Location = new System.Drawing.Point(111, 123);
            this.txtAraAd.Name = "txtAraAd";
            this.txtAraAd.Size = new System.Drawing.Size(125, 22);
            this.txtAraAd.TabIndex = 0;
            // 
            // txtAraSoyad
            // 
            this.txtAraSoyad.Location = new System.Drawing.Point(111, 151);
            this.txtAraSoyad.Name = "txtAraSoyad";
            this.txtAraSoyad.Size = new System.Drawing.Size(125, 22);
            this.txtAraSoyad.TabIndex = 1;
            // 
            // txtAraBolum
            // 
            this.txtAraBolum.Location = new System.Drawing.Point(111, 179);
            this.txtAraBolum.Name = "txtAraBolum";
            this.txtAraBolum.Size = new System.Drawing.Size(125, 22);
            this.txtAraBolum.TabIndex = 2;
            // 
            // gridAraSonuc
            // 
            this.gridAraSonuc.Location = new System.Drawing.Point(307, 0);
            this.gridAraSonuc.MainView = this.gridView1;
            this.gridAraSonuc.Name = "gridAraSonuc";
            this.gridAraSonuc.Size = new System.Drawing.Size(792, 401);
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
            // btnSorgula
            // 
            this.btnSorgula.Location = new System.Drawing.Point(111, 295);
            this.btnSorgula.Name = "btnSorgula";
            this.btnSorgula.Size = new System.Drawing.Size(94, 34);
            this.btnSorgula.TabIndex = 2;
            this.btnSorgula.Text = "Sorgula";
            this.btnSorgula.Click += new System.EventHandler(this.btnSorgula_Click);
            // 
            // labelOgrAd
            // 
            this.labelOgrAd.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelOgrAd.Appearance.Options.UseFont = true;
            this.labelOgrAd.Location = new System.Drawing.Point(77, 121);
            this.labelOgrAd.Name = "labelOgrAd";
            this.labelOgrAd.Size = new System.Drawing.Size(23, 23);
            this.labelOgrAd.TabIndex = 3;
            this.labelOgrAd.Text = "Ad";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(48, 178);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(52, 23);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Bölüm";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(51, 150);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(49, 23);
            this.labelControl2.TabIndex = 5;
            this.labelControl2.Text = "Soyad";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Location = new System.Drawing.Point(12, 207);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(88, 23);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Telefon No";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Location = new System.Drawing.Point(63, 236);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 23);
            this.labelControl4.TabIndex = 7;
            this.labelControl4.Text = "Sınıf";
            // 
            // txtTelNo
            // 
            this.txtTelNo.Location = new System.Drawing.Point(111, 207);
            this.txtTelNo.Name = "txtTelNo";
            this.txtTelNo.Size = new System.Drawing.Size(125, 22);
            this.txtTelNo.TabIndex = 8;
            // 
            // txtSınıf
            // 
            this.txtSınıf.Location = new System.Drawing.Point(111, 238);
            this.txtSınıf.Name = "txtSınıf";
            this.txtSınıf.Size = new System.Drawing.Size(125, 22);
            this.txtSınıf.TabIndex = 9;
            // 
            // Ara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(1111, 413);
            this.Controls.Add(this.gridAraSonuc);
            this.Controls.Add(this.groupControl1);
            this.Name = "Ara";
            this.Text = "Ara";
            this.Load += new System.EventHandler(this.Ara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraSoyad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAraBolum.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridAraSonuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTelNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSınıf.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtAraBolum;
        private DevExpress.XtraEditors.TextEdit txtAraSoyad;
        private DevExpress.XtraEditors.TextEdit txtAraAd;
        private DevExpress.XtraGrid.GridControl gridAraSonuc;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnSorgula;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelOgrAd;
        private DevExpress.XtraEditors.TextEdit txtSınıf;
        private DevExpress.XtraEditors.TextEdit txtTelNo;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
    }
}