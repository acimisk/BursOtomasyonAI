namespace bursoto1
{
    partial class FrmAylikBurs
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
            this.cmbBursTur = new DevExpress.XtraEditors.LookUpEdit();
            this.lblBursTur = new DevExpress.XtraEditors.LabelControl();
            this.gridControlOgrenciler = new DevExpress.XtraGrid.GridControl();
            this.gridViewOgrenciler = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnBursGonder = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBursTur.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOgrenciler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOgrenciler)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbBursTur
            // 
            this.cmbBursTur.Location = new System.Drawing.Point(24, 52);
            this.cmbBursTur.Name = "cmbBursTur";
            this.cmbBursTur.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbBursTur.Properties.NullText = "Burs türü seçiniz...";
            this.cmbBursTur.Size = new System.Drawing.Size(334, 22);
            this.cmbBursTur.TabIndex = 0;
            // 
            // lblBursTur
            // 
            this.lblBursTur.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblBursTur.Appearance.Options.UseFont = true;
            this.lblBursTur.Location = new System.Drawing.Point(24, 26);
            this.lblBursTur.Name = "lblBursTur";
            this.lblBursTur.Size = new System.Drawing.Size(160, 23);
            this.lblBursTur.TabIndex = 1;
            this.lblBursTur.Text = "Aylık Burs Türü Seç:";
            // 
            // gridControlOgrenciler
            // 
            this.gridControlOgrenciler.Location = new System.Drawing.Point(24, 95);
            this.gridControlOgrenciler.MainView = this.gridViewOgrenciler;
            this.gridControlOgrenciler.Name = "gridControlOgrenciler";
            this.gridControlOgrenciler.Size = new System.Drawing.Size(1200, 500);
            this.gridControlOgrenciler.TabIndex = 2;
            this.gridControlOgrenciler.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewOgrenciler});
            // 
            // gridViewOgrenciler
            // 
            this.gridViewOgrenciler.GridControl = this.gridControlOgrenciler;
            this.gridViewOgrenciler.Name = "gridViewOgrenciler";
            this.gridViewOgrenciler.OptionsSelection.MultiSelect = true;
            this.gridViewOgrenciler.OptionsView.ShowGroupPanel = false;
            // 
            // btnBursGonder
            // 
            this.btnBursGonder.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnBursGonder.Appearance.Options.UseFont = true;
            this.btnBursGonder.Location = new System.Drawing.Point(24, 613);
            this.btnBursGonder.Name = "btnBursGonder";
            this.btnBursGonder.Size = new System.Drawing.Size(1200, 45);
            this.btnBursGonder.TabIndex = 3;
            this.btnBursGonder.Text = "Seçilen Öğrencilere Bu Ayki Bursu Gönder";
            this.btnBursGonder.Click += new System.EventHandler(this.btnBursGonder_Click);
            // 
            // FrmAylikBurs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 680);
            this.Controls.Add(this.btnBursGonder);
            this.Controls.Add(this.gridControlOgrenciler);
            this.Controls.Add(this.lblBursTur);
            this.Controls.Add(this.cmbBursTur);
            this.Name = "FrmAylikBurs";
            this.Text = "Aylık Burs Tanımlama";
            this.Load += new System.EventHandler(this.FrmAylikBurs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbBursTur.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlOgrenciler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewOgrenciler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LookUpEdit cmbBursTur;
        private DevExpress.XtraEditors.LabelControl lblBursTur;
        private DevExpress.XtraGrid.GridControl gridControlOgrenciler;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewOgrenciler;
        private DevExpress.XtraEditors.SimpleButton btnBursGonder;
    }
}


