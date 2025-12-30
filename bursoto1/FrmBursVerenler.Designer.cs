namespace bursoto1
{
    partial class FrmBursVerenler
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
            this.gridControlBekleyen = new DevExpress.XtraGrid.GridControl();
            this.gridViewBekleyen = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlAktif = new DevExpress.XtraGrid.GridControl();
            this.gridViewAktif = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.panelActions = new DevExpress.XtraEditors.PanelControl();
            this.btnOnayla = new DevExpress.XtraEditors.SimpleButton();
            this.btnReddet = new DevExpress.XtraEditors.SimpleButton();
            this.btnYenile = new DevExpress.XtraEditors.SimpleButton();
            this.labelControlBekleyen = new DevExpress.XtraEditors.LabelControl();
            this.labelControlAktif = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBekleyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBekleyen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAktif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAktif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelActions)).BeginInit();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridControlBekleyen
            // 
            this.gridControlBekleyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlBekleyen.Location = new System.Drawing.Point(0, 0);
            this.gridControlBekleyen.MainView = this.gridViewBekleyen;
            this.gridControlBekleyen.Name = "gridControlBekleyen";
            this.gridControlBekleyen.Size = new System.Drawing.Size(398, 450);
            this.gridControlBekleyen.TabIndex = 0;
            this.gridControlBekleyen.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBekleyen});
            // 
            // gridViewBekleyen
            // 
            this.gridViewBekleyen.GridControl = this.gridControlBekleyen;
            this.gridViewBekleyen.Name = "gridViewBekleyen";
            this.gridViewBekleyen.OptionsBehavior.Editable = false;
            this.gridViewBekleyen.OptionsView.ShowGroupPanel = false;
            this.gridViewBekleyen.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridViewBekleyen_RowStyle);
            // 
            // gridControlAktif
            // 
            this.gridControlAktif.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlAktif.Location = new System.Drawing.Point(0, 0);
            this.gridControlAktif.MainView = this.gridViewAktif;
            this.gridControlAktif.Name = "gridControlAktif";
            this.gridControlAktif.Size = new System.Drawing.Size(397, 450);
            this.gridControlAktif.TabIndex = 0;
            this.gridControlAktif.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewAktif});
            // 
            // gridViewAktif
            // 
            this.gridViewAktif.GridControl = this.gridControlAktif;
            this.gridViewAktif.Name = "gridViewAktif";
            this.gridViewAktif.OptionsBehavior.Editable = false;
            this.gridViewAktif.OptionsView.ShowGroupPanel = false;
            // 
            // labelControlBekleyen
            // 
            this.labelControlBekleyen.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelControlBekleyen.Appearance.ForeColor = System.Drawing.Color.Orange;
            this.labelControlBekleyen.Appearance.Options.UseFont = true;
            this.labelControlBekleyen.Appearance.Options.UseForeColor = true;
            this.labelControlBekleyen.Location = new System.Drawing.Point(12, 12);
            this.labelControlBekleyen.Name = "labelControlBekleyen";
            this.labelControlBekleyen.Size = new System.Drawing.Size(140, 25);
            this.labelControlBekleyen.TabIndex = 2;
            this.labelControlBekleyen.Text = "⏳ Bekleyen Bağışlar";
            // 
            // labelControlAktif
            // 
            this.labelControlAktif.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.labelControlAktif.Appearance.ForeColor = System.Drawing.Color.Green;
            this.labelControlAktif.Appearance.Options.UseFont = true;
            this.labelControlAktif.Appearance.Options.UseForeColor = true;
            this.labelControlAktif.Location = new System.Drawing.Point(412, 12);
            this.labelControlAktif.Name = "labelControlAktif";
            this.labelControlAktif.Size = new System.Drawing.Size(133, 25);
            this.labelControlAktif.TabIndex = 3;
            this.labelControlAktif.Text = "✅ Aktif Bağışçılar";
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.btnYenile);
            this.panelActions.Controls.Add(this.btnReddet);
            this.panelActions.Controls.Add(this.btnOnayla);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 450);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(800, 60);
            this.panelActions.TabIndex = 4;
            // 
            // btnOnayla
            // 
            this.btnOnayla.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnOnayla.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOnayla.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnOnayla.Appearance.Options.UseBackColor = true;
            this.btnOnayla.Appearance.Options.UseFont = true;
            this.btnOnayla.Appearance.Options.UseForeColor = true;
            this.btnOnayla.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnOnayla.AppearancePressed.Options.UseBackColor = true;
            this.btnOnayla.Location = new System.Drawing.Point(12, 12);
            this.btnOnayla.Name = "btnOnayla";
            this.btnOnayla.Size = new System.Drawing.Size(180, 36);
            this.btnOnayla.TabIndex = 0;
            this.btnOnayla.Text = "✅ Bağışı Onayla";
            this.btnOnayla.Click += new System.EventHandler(this.btnOnayla_Click);
            // 
            // btnReddet
            // 
            this.btnReddet.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnReddet.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReddet.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnReddet.Appearance.Options.UseBackColor = true;
            this.btnReddet.Appearance.Options.UseFont = true;
            this.btnReddet.Appearance.Options.UseForeColor = true;
            this.btnReddet.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnReddet.AppearancePressed.Options.UseBackColor = true;
            this.btnReddet.Location = new System.Drawing.Point(198, 12);
            this.btnReddet.Name = "btnReddet";
            this.btnReddet.Size = new System.Drawing.Size(180, 36);
            this.btnReddet.TabIndex = 1;
            this.btnReddet.Text = "❌ Bağışı Reddet";
            this.btnReddet.Click += new System.EventHandler(this.btnReddet_Click);
            // 
            // btnYenile
            // 
            this.btnYenile.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnYenile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnYenile.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnYenile.Appearance.Options.UseBackColor = true;
            this.btnYenile.Appearance.Options.UseFont = true;
            this.btnYenile.Appearance.Options.UseForeColor = true;
            this.btnYenile.AppearancePressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnYenile.AppearancePressed.Options.UseBackColor = true;
            this.btnYenile.Location = new System.Drawing.Point(608, 12);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(180, 36);
            this.btnYenile.TabIndex = 2;
            this.btnYenile.Text = "🔄 Listeyi Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 50);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Text = "Bekleyen Bağışlar";
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlBekleyen);
            this.splitContainerControl1.Panel2.Text = "Aktif Bağışçılar";
            this.splitContainerControl1.Panel2.Controls.Add(this.gridControlAktif);
            this.splitContainerControl1.Size = new System.Drawing.Size(800, 400);
            this.splitContainerControl1.SplitterPosition = 398;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // FrmBursVerenler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 510);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.labelControlAktif);
            this.Controls.Add(this.labelControlBekleyen);
            this.Controls.Add(this.panelActions);
            this.Name = "FrmBursVerenler";
            this.Text = "Burs Verenler - Bağış Yönetimi";
            this.Load += new System.EventHandler(this.FrmBursVerenler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBekleyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBekleyen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlAktif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewAktif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelActions)).EndInit();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlBekleyen;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBekleyen;
        private DevExpress.XtraGrid.GridControl gridControlAktif;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewAktif;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.PanelControl panelActions;
        private DevExpress.XtraEditors.SimpleButton btnOnayla;
        private DevExpress.XtraEditors.SimpleButton btnReddet;
        private DevExpress.XtraEditors.SimpleButton btnYenile;
        private DevExpress.XtraEditors.LabelControl labelControlBekleyen;
        private DevExpress.XtraEditors.LabelControl labelControlAktif;
    }
}