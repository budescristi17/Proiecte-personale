namespace Model_Test_PAW
{
    partial class FormMDI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.fISIERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fisaNouaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tiparesteFiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fisaNouaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 28);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fISIERToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(816, 28);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // fISIERToolStripMenuItem
            // 
            this.fISIERToolStripMenuItem.Name = "fISIERToolStripMenuItem";
            this.fISIERToolStripMenuItem.Size = new System.Drawing.Size(63, 24);
            this.fISIERToolStripMenuItem.Text = "FISIER";
            // 
            // fisaNouaToolStripMenuItem
            // 
            this.fisaNouaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiparesteFiseToolStripMenuItem});
            this.fisaNouaToolStripMenuItem.Name = "fisaNouaToolStripMenuItem";
            this.fisaNouaToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.fisaNouaToolStripMenuItem.Text = "Raport";
            this.fisaNouaToolStripMenuItem.Click += new System.EventHandler(this.fisaNouaToolStripMenuItem_Click);
            // 
            // tiparesteFiseToolStripMenuItem
            // 
            this.tiparesteFiseToolStripMenuItem.Name = "tiparesteFiseToolStripMenuItem";
            this.tiparesteFiseToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.tiparesteFiseToolStripMenuItem.Text = "Tipareste fise";
            this.tiparesteFiseToolStripMenuItem.Click += new System.EventHandler(this.tiparesteFiseToolStripMenuItem_Click);
            // 
            // FormMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 481);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMDI";
            this.Text = "FormMDI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem fisaNouaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fISIERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tiparesteFiseToolStripMenuItem;
    }
}