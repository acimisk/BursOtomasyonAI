namespace bursoto1.Modules
{
    partial class BagisModule
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelFiltre = new DevExpress.XtraEditors.PanelControl();
            this.lblFiltre = new DevExpress.XtraEditors.LabelControl();
            this.cmbFiltre = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFiltre)).BeginInit();
            this.panelFiltre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFiltre.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelFiltre
            // 
            this.panelFiltre.Controls.Add(this.lblFiltre);
            this.panelFiltre.Controls.Add(this.cmbFiltre);
            this.panelFiltre.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltre.Location = new System.Drawing.Point(0, 0);
            this.panelFiltre.Name = "panelFiltre";
            this.panelFiltre.Size = new System.Drawing.Size(647, 50);
            this.panelFiltre.TabIndex = 1;
            // 
            // lblFiltre
            // 
            this.lblFiltre.Appearance.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.lblFiltre.Appearance.Options.UseFont = true;
            this.lblFiltre.Location = new System.Drawing.Point(20, 15);
            this.lblFiltre.Name = "lblFiltre";
            this.lblFiltre.Size = new System.Drawing.Size(86, 23);
            this.lblFiltre.TabIndex = 0;
            this.lblFiltre.Text = "Filtreleme:";
            // 
            // cmbFiltre
            // 
            this.cmbFiltre.Location = new System.Drawing.Point(115, 12);
            this.cmbFiltre.Name = "cmbFiltre";
            this.cmbFiltre.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFiltre.Properties.Items.AddRange(new object[] {
            "Tümü",
            "Onaylandı",
            "Beklemede"});
            this.cmbFiltre.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbFiltre.Size = new System.Drawing.Size(180, 24);
            this.cmbFiltre.TabIndex = 1;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 50);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(647, 503);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            // 
            // BagisModule
            // 
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelFiltre);
            this.Name = "BagisModule";
            this.Size = new System.Drawing.Size(647, 553);
            this.Load += new System.EventHandler(this.BagisModule_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelFiltre)).EndInit();
            this.panelFiltre.ResumeLayout(false);
            this.panelFiltre.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFiltre.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.PanelControl panelFiltre;
        private DevExpress.XtraEditors.LabelControl lblFiltre;
        private DevExpress.XtraEditors.ComboBoxEdit cmbFiltre;
    }
}
