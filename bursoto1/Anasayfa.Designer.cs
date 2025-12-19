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
            this.lblToplamBurs = new DevExpress.XtraEditors.LabelControl();
            this.lblEnBasarili = new DevExpress.XtraEditors.LabelControl();
            this.lblToplamOgrenci = new DevExpress.XtraEditors.LabelControl();
            this.SuspendLayout();
            // 
            // lblToplamBurs
            // 
            this.lblToplamBurs.Location = new System.Drawing.Point(140, 103);
            this.lblToplamBurs.Name = "lblToplamBurs";
            this.lblToplamBurs.Size = new System.Drawing.Size(100, 16);
            this.lblToplamBurs.TabIndex = 0;
            this.lblToplamBurs.Text = "lblToplamOgrenci";
            // 
            // lblEnBasarili
            // 
            this.lblEnBasarili.Location = new System.Drawing.Point(140, 151);
            this.lblEnBasarili.Name = "lblEnBasarili";
            this.lblEnBasarili.Size = new System.Drawing.Size(100, 16);
            this.lblEnBasarili.TabIndex = 1;
            this.lblEnBasarili.Text = "lblToplamOgrenci";
            // 
            // lblToplamOgrenci
            // 
            this.lblToplamOgrenci.Location = new System.Drawing.Point(140, 203);
            this.lblToplamOgrenci.Name = "lblToplamOgrenci";
            this.lblToplamOgrenci.Size = new System.Drawing.Size(100, 16);
            this.lblToplamOgrenci.TabIndex = 2;
            this.lblToplamOgrenci.Text = "lblToplamOgrenci";
            // 
            // Anasayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblToplamOgrenci);
            this.Controls.Add(this.lblEnBasarili);
            this.Controls.Add(this.lblToplamBurs);
            this.Name = "Anasayfa";
            this.Text = "Anasayfa";
            this.Load += new System.EventHandler(this.Anasayfa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblToplamBurs;
        private DevExpress.XtraEditors.LabelControl lblEnBasarili;
        private DevExpress.XtraEditors.LabelControl lblToplamOgrenci;
    }
}