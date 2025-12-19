namespace bursoto1
{
    partial class FrmBurslar
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
            this.txtBursAd = new DevExpress.XtraEditors.TextEdit();
            this.txtMiktar = new DevExpress.XtraEditors.TextEdit();
            this.txtKontenjan = new DevExpress.XtraEditors.TextEdit();
            this.txtAciklama = new DevExpress.XtraEditors.TextEdit();
            this.btnBursTanimla = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.txtBursAd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMiktar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKontenjan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBursAd
            // 
            this.txtBursAd.Location = new System.Drawing.Point(111, 78);
            this.txtBursAd.Name = "txtBursAd";
            this.txtBursAd.Size = new System.Drawing.Size(125, 22);
            this.txtBursAd.TabIndex = 0;
            // 
            // txtMiktar
            // 
            this.txtMiktar.Location = new System.Drawing.Point(111, 127);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(125, 22);
            this.txtMiktar.TabIndex = 1;
            // 
            // txtKontenjan
            // 
            this.txtKontenjan.Location = new System.Drawing.Point(111, 170);
            this.txtKontenjan.Name = "txtKontenjan";
            this.txtKontenjan.Size = new System.Drawing.Size(125, 22);
            this.txtKontenjan.TabIndex = 2;
            // 
            // txtAciklama
            // 
            this.txtAciklama.Location = new System.Drawing.Point(111, 216);
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(125, 22);
            this.txtAciklama.TabIndex = 3;
            // 
            // btnBursTanimla
            // 
            this.btnBursTanimla.Location = new System.Drawing.Point(124, 264);
            this.btnBursTanimla.Name = "btnBursTanimla";
            this.btnBursTanimla.Size = new System.Drawing.Size(94, 29);
            this.btnBursTanimla.TabIndex = 4;
            this.btnBursTanimla.Text = "Burs Tanımla";
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(343, 121);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(400, 200);
            this.gridControl1.TabIndex = 5;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // FrmBurslar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.btnBursTanimla);
            this.Controls.Add(this.txtAciklama);
            this.Controls.Add(this.txtKontenjan);
            this.Controls.Add(this.txtMiktar);
            this.Controls.Add(this.txtBursAd);
            this.Name = "FrmBurslar";
            this.Text = "FrmBurslar";
            this.Load += new System.EventHandler(this.FrmBurslar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtBursAd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMiktar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKontenjan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAciklama.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit txtBursAd;
        private DevExpress.XtraEditors.TextEdit txtMiktar;
        private DevExpress.XtraEditors.TextEdit txtKontenjan;
        private DevExpress.XtraEditors.TextEdit txtAciklama;
        private DevExpress.XtraEditors.SimpleButton btnBursTanimla;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}