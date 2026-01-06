using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.Svg;

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
            this.panelLogin = new DevExpress.XtraEditors.PanelControl();
            this.lblBaslik = new DevExpress.XtraEditors.LabelControl();
            this.lblAciklama = new DevExpress.XtraEditors.LabelControl();
            this.txtKullaniciAdi = new DevExpress.XtraEditors.TextEdit();
            this.txtSifre = new DevExpress.XtraEditors.TextEdit();
            this.btnGiris = new DevExpress.XtraEditors.SimpleButton();
            this.btnCikis = new DevExpress.XtraEditors.SimpleButton();
            this.svgPerson = new DevExpress.XtraEditors.SvgImageBox();
            this.svgKey = new DevExpress.XtraEditors.SvgImageBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelLogin)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgPerson)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgKey)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLogin
            // 
            this.panelLogin.Appearance.BackColor = Color.FromArgb(40, 40, 45);
            this.panelLogin.Appearance.BackColor2 = Color.FromArgb(35, 35, 40);
            this.panelLogin.Appearance.GradientMode = LinearGradientMode.Vertical;
            this.panelLogin.Appearance.Options.UseBackColor = true;
            this.panelLogin.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelLogin.Controls.Add(this.lblBaslik);
            this.panelLogin.Controls.Add(this.lblAciklama);
            this.panelLogin.Controls.Add(this.txtKullaniciAdi);
            this.panelLogin.Controls.Add(this.txtSifre);
            this.panelLogin.Controls.Add(this.btnGiris);
            this.panelLogin.Controls.Add(this.svgPerson);
            this.panelLogin.Controls.Add(this.svgKey);
            this.panelLogin.Location = new Point(50, 50);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new Size(450, 500);
            this.panelLogin.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.Appearance.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
            this.lblBaslik.Appearance.ForeColor = Color.FromArgb(255, 255, 255);
            this.lblBaslik.Appearance.Options.UseFont = true;
            this.lblBaslik.Appearance.Options.UseForeColor = true;
            this.lblBaslik.Location = new Point(125, 60);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new Size(200, 54);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "BURSOTO V1.0";
            // 
            // lblAciklama
            // 
            this.lblAciklama.Appearance.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));
            this.lblAciklama.Appearance.ForeColor = Color.FromArgb(180, 180, 180);
            this.lblAciklama.Appearance.Options.UseFont = true;
            this.lblAciklama.Appearance.Options.UseForeColor = true;
            this.lblAciklama.Location = new Point(90, 120);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new Size(270, 25);
            this.lblAciklama.TabIndex = 1;
            this.lblAciklama.Text = "Yönetim Paneline Hoş Geldiniz";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.EditValue = "";
            this.txtKullaniciAdi.Location = new Point(80, 200);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Properties.Appearance.BackColor = Color.FromArgb(50, 50, 55);
            this.txtKullaniciAdi.Properties.Appearance.BorderColor = Color.FromArgb(80, 80, 85);
            this.txtKullaniciAdi.Properties.Appearance.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));
            this.txtKullaniciAdi.Properties.Appearance.ForeColor = Color.White;
            this.txtKullaniciAdi.Properties.Appearance.Options.UseBackColor = true;
            this.txtKullaniciAdi.Properties.Appearance.Options.UseBorderColor = true;
            this.txtKullaniciAdi.Properties.Appearance.Options.UseFont = true;
            this.txtKullaniciAdi.Properties.Appearance.Options.UseForeColor = true;
            this.txtKullaniciAdi.Properties.AppearanceFocused.BackColor = Color.FromArgb(55, 55, 60);
            this.txtKullaniciAdi.Properties.AppearanceFocused.BorderColor = Color.FromArgb(100, 149, 237);
            this.txtKullaniciAdi.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtKullaniciAdi.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtKullaniciAdi.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtKullaniciAdi.Properties.NullValuePrompt = "Kullanıcı Adı";
            this.txtKullaniciAdi.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtKullaniciAdi.Size = new Size(290, 40);
            this.txtKullaniciAdi.TabIndex = 2;
            this.txtKullaniciAdi.EditValueChanged += new System.EventHandler(this.txtKullaniciAdi_EditValueChanged);
            // 
            // txtSifre
            // 
            this.txtSifre.EditValue = "";
            this.txtSifre.Location = new Point(80, 270);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Properties.Appearance.BackColor = Color.FromArgb(50, 50, 55);
            this.txtSifre.Properties.Appearance.BorderColor = Color.FromArgb(80, 80, 85);
            this.txtSifre.Properties.Appearance.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(162)));
            this.txtSifre.Properties.Appearance.ForeColor = Color.White;
            this.txtSifre.Properties.Appearance.Options.UseBackColor = true;
            this.txtSifre.Properties.Appearance.Options.UseBorderColor = true;
            this.txtSifre.Properties.Appearance.Options.UseFont = true;
            this.txtSifre.Properties.Appearance.Options.UseForeColor = true;
            this.txtSifre.Properties.AppearanceFocused.BackColor = Color.FromArgb(55, 55, 60);
            this.txtSifre.Properties.AppearanceFocused.BorderColor = Color.FromArgb(100, 149, 237);
            this.txtSifre.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSifre.Properties.AppearanceFocused.Options.UseBorderColor = true;
            this.txtSifre.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSifre.Properties.NullValuePrompt = "Şifre";
            this.txtSifre.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtSifre.Properties.UseSystemPasswordChar = true;
            this.txtSifre.Size = new Size(290, 40);
            this.txtSifre.TabIndex = 3;
            this.txtSifre.EditValueChanged += new System.EventHandler(this.txtSifre_EditValueChanged);
            // 
            // btnGiris
            // 
            this.btnGiris.Appearance.BackColor = Color.FromArgb(65, 105, 225);
            this.btnGiris.Appearance.BackColor2 = Color.FromArgb(100, 149, 237);
            this.btnGiris.Appearance.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
            this.btnGiris.Appearance.ForeColor = Color.White;
            this.btnGiris.Appearance.GradientMode = LinearGradientMode.Vertical;
            this.btnGiris.Appearance.Options.UseBackColor = true;
            this.btnGiris.Appearance.Options.UseFont = true;
            this.btnGiris.Appearance.Options.UseForeColor = true;
            this.btnGiris.AppearanceHovered.BackColor = Color.FromArgb(100, 149, 237);
            this.btnGiris.AppearanceHovered.BackColor2 = Color.FromArgb(65, 105, 225);
            this.btnGiris.AppearanceHovered.Options.UseBackColor = true;
            this.btnGiris.AppearancePressed.BackColor = Color.FromArgb(30, 144, 255);
            this.btnGiris.AppearancePressed.Options.UseBackColor = true;
            this.btnGiris.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGiris.Location = new Point(80, 350);
            this.btnGiris.Name = "btnGiris";
            this.btnGiris.Size = new Size(290, 50);
            this.btnGiris.TabIndex = 4;
            this.btnGiris.Text = "Giriş Yap";
            this.btnGiris.Click += new System.EventHandler(this.btnGiris_Click);
            // 
            // btnCikis
            // 
            this.btnCikis.Appearance.BackColor = Color.Transparent;
            this.btnCikis.Appearance.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(162)));
            this.btnCikis.Appearance.ForeColor = Color.FromArgb(180, 180, 180);
            this.btnCikis.Appearance.Options.UseBackColor = true;
            this.btnCikis.Appearance.Options.UseFont = true;
            this.btnCikis.Appearance.Options.UseForeColor = true;
            this.btnCikis.AppearanceHovered.BackColor = Color.FromArgb(231, 76, 60);
            this.btnCikis.AppearanceHovered.ForeColor = Color.White;
            this.btnCikis.AppearanceHovered.Options.UseBackColor = true;
            this.btnCikis.AppearanceHovered.Options.UseForeColor = true;
            this.btnCikis.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCikis.Location = new Point(500, 10);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnCikis.Size = new Size(40, 40);
            this.btnCikis.TabIndex = 5;
            this.btnCikis.Text = "✕";
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // svgPerson
            // 
            this.svgPerson.Location = new Point(40, 200);
            this.svgPerson.Name = "svgPerson";
            this.svgPerson.Size = new Size(30, 40);
            this.svgPerson.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Squeeze;
            this.svgPerson.TabIndex = 6;
            this.svgPerson.TabStop = false;
            this.svgPerson.Click += new System.EventHandler(this.svgPerson_Click);
            // 
            // svgKey
            // 
            this.svgKey.Location = new Point(40, 270);
            this.svgKey.Name = "svgKey";
            this.svgKey.Size = new Size(30, 40);
            this.svgKey.SizeMode = DevExpress.XtraEditors.SvgImageSizeMode.Squeeze;
            this.svgKey.TabIndex = 7;
            this.svgKey.TabStop = false;
            this.svgKey.Click += new System.EventHandler(this.svgKey_Click);
            // 
            // Login
            // 
            this.AcceptButton = this.btnGiris;
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(28, 28, 30);
            this.CancelButton = this.btnCikis;
            this.ClientSize = new Size(550, 600);
            this.ControlBox = false;
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.panelLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BURSOTO - Giriş";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelLogin)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKullaniciAdi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSifre.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgPerson)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.svgKey)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelLogin;
        private DevExpress.XtraEditors.LabelControl lblBaslik;
        private DevExpress.XtraEditors.LabelControl lblAciklama;
        private DevExpress.XtraEditors.TextEdit txtKullaniciAdi;
        private DevExpress.XtraEditors.TextEdit txtSifre;
        private DevExpress.XtraEditors.SimpleButton btnGiris;
        private DevExpress.XtraEditors.SimpleButton btnCikis;
        private DevExpress.XtraEditors.SvgImageBox svgPerson;
        private DevExpress.XtraEditors.SvgImageBox svgKey;
    }
}
