using System;

namespace bursoto1
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this.txtSifre = new DevExpress.XtraEditors.TextEdit();
            this.labelKullaniciAdi = new DevExpress.XtraEditors.LabelControl();
            this.labelSifre = new DevExpress.XtraEditors.LabelControl();
            this.btnGiris = new DevExpress.XtraEditors.SimpleButton();
            this.svgKey = new DevExpress.XtraEditors.SvgImageBox();
            this.svgPerson = new DevExpress.XtraEditors.SvgImageBox();
            this.btnCikis = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgPerson)).BeginInit();
            this.SuspendLayout();
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.EditValue = "";
            this.txtKullaniciAdi.Location = new System.Drawing.Point(209, 72);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(125, 22);
            this.txtKullaniciAdi.TabIndex = 0;
            this.txtKullaniciAdi.EditValueChanged += new System.EventHandler(this.txtKullaniciAdi_EditValueChanged);
            // 
            // txtSifre
            // 
            this.txtSifre.EditValue = "";
            this.txtSifre.Location = new System.Drawing.Point(209, 113);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Properties.UseSystemPasswordChar = true;
            this.txtSifre.Size = new System.Drawing.Size(125, 22);
            this.txtSifre.TabIndex = 1;
            this.txtSifre.EditValueChanged += new System.EventHandler(this.txtSifre_EditValueChanged);
            // 
            // labelKullaniciAdi
            // 
            this.labelKullaniciAdi.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelKullaniciAdi.Appearance.Options.UseFont = true;
            this.labelKullaniciAdi.Location = new System.Drawing.Point(15, 48);
            this.labelKullaniciAdi.Name = "labelKullaniciAdi";
            this.labelKullaniciAdi.Size = new System.Drawing.Size(126, 24);
            this.labelKullaniciAdi.TabIndex = 2;
            this.labelKullaniciAdi.Text = "Kullanıcı Adı";
            this.labelKullaniciAdi.Click += new System.EventHandler(this.labelKullaniciAdi_Click);
            // 
            // labelSifre
            // 
            this.labelSifre.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.labelSifre.Appearance.Options.UseFont = true;
            this.labelSifre.Location = new System.Drawing.Point(92, 89);
            this.labelSifre.Name = "labelSifre";
            this.labelSifre.Size = new System.Drawing.Size(48, 24);
            this.labelSifre.TabIndex = 3;
            this.labelSifre.Text = "Şifre";
            this.labelSifre.Click += new System.EventHandler(this.labelSifre_Click);
            // 
            // btnGiris
            // 
            this.btnGiris.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Appearance.Options.UseFont = true;
            this.btnGiris.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGiris.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnGiris.ImageOptions.Image")));
            this.btnGiris.Location = new System.Drawing.Point(164, 167);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new System.Drawing.Size(108, 46);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "Giriş";
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // svgKey
            // 
            this.svgKey.Location = new System.Drawing.Point(177, 112);
            this.svgKey.Name = "svgKey";
            this.svgKey.Size = new System.Drawing.Size(26, 24);
            this.svgKey.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Squeeze;
            this.svgKey.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgKey.SvgImage")));
            this.svgKey.TabIndex = 5;
            this.svgKey.Text = "svgImageBox1";
            this.svgKey.Click += new System.EventHandler(this.svgKey_Click);
            // 
            // svgPerson
            // 
            this.svgPerson.Location = new System.Drawing.Point(179, 72);
            this.svgPerson.Name = "svgPerson";
            this.svgPerson.Size = new System.Drawing.Size(27, 24);
            this.svgPerson.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Squeeze;
            this.svgPerson.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("svgPerson.SvgImage")));
            this.svgPerson.TabIndex = 6;
            this.svgPerson.Text = "svgImageBox2";
            this.svgPerson.Click += new System.EventHandler(this.svgPerson_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCikis.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnCikis.ImageOptions.SvgImage")));
            this.btnCikis.Location = new System.Drawing.Point(344, 12);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCikis.Size = new System.Drawing.Size(55, 50);
            this.btnCikis.TabIndex = 0;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // Login
            // 
            this.AcceptButton = this.btnGiris;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCikis;
            this.ClientSize = new System.Drawing.Size(406, 262);
            this.ControlBox = false;
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.svgPerson);
            this.Controls.Add(this.svgKey);
            this.Controls.Add(this.btnGiris);
            this.Controls.Add(this.labelSifre);
            this.Controls.Add(this.labelKullaniciAdi);
            this.Controls.Add(this.txtSifre);
            this.Controls.Add(this.txtKullaniciAdi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Page";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgPerson)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.LabelControl labelKullaniciAdi;
        private DevExpress.XtraEditors.LabelControl labelSifre;
        private DevExpress.XtraEditors.SimpleButton btnGiris;
        private DevExpress.XtraEditors.SvgImageBox svgKey;
        private DevExpress.XtraEditors.SvgImageBox svgPerson;
        private DevExpress.XtraEditors.SimpleButton btnCikis;
    }
}