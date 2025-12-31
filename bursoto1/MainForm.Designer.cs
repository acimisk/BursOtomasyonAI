namespace bursoto1
{
    partial class MainForm
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
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.navigationFrame1 = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).BeginInit();
            this.navigationFrame1.SuspendLayout();
            this.navigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbon
            // 
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 1;
            this.ribbon.Name = "ribbon";
            this.ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbon.Size = new System.Drawing.Size(770, 193);
            this.ribbon.StatusBar = this.ribbonStatusBar;
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 498);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(770, 30);
            // 
            // navigationFrame1
            // 
            this.navigationFrame1.Controls.Add(this.navigationPage1);
            this.navigationFrame1.Controls.Add(this.navigationPage2);
            this.navigationFrame1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationFrame1.Location = new System.Drawing.Point(0, 193);
            this.navigationFrame1.Name = "navigationFrame1";
            this.navigationFrame1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navigationPage1,
            this.navigationPage2});
            this.navigationFrame1.SelectedPage = this.navigationPage1;
            this.navigationFrame1.Size = new System.Drawing.Size(770, 305);
            this.navigationFrame1.TabIndex = 2;
            this.navigationFrame1.Text = "navigationFrame1";
            // 
            // navigationPage1
            // 
            this.navigationPage1.Controls.Add(this.accordionControl1);
            this.navigationPage1.Name = "navigationPage1";
            this.navigationPage1.Size = new System.Drawing.Size(770, 305);
            // 
            // navigationPage2
            // 
            this.navigationPage2.Name = "navigationPage2";
            this.navigationPage2.Size = new System.Drawing.Size(770, 305);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(0, 0);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.Size = new System.Drawing.Size(260, 305);
            this.accordionControl1.TabIndex = 0;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 528);
            this.Controls.Add(this.navigationFrame1);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Name = "MainForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrame1)).EndInit();
            this.navigationFrame1.ResumeLayout(false);
            this.navigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrame1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
    }
}