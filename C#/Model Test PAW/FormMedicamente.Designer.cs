namespace Model_Test_PAW
{
    partial class FormMedicamente
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
            this.dgvMedicamente = new System.Windows.Forms.DataGridView();
            this.lblCod = new System.Windows.Forms.Label();
            this.lblDenumire = new System.Windows.Forms.Label();
            this.lblPret = new System.Windows.Forms.Label();
            this.txtPret = new System.Windows.Forms.TextBox();
            this.txtCod = new System.Windows.Forms.TextBox();
            this.txtDenumire = new System.Windows.Forms.TextBox();
            this.btnAdauga = new System.Windows.Forms.Button();
            this.btnModifica = new System.Windows.Forms.Button();
            this.btnSterge = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamente)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvMedicamente
            // 
            this.dgvMedicamente.AllowUserToAddRows = false;
            this.dgvMedicamente.AllowUserToDeleteRows = false;
            this.dgvMedicamente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicamente.Location = new System.Drawing.Point(0, 12);
            this.dgvMedicamente.Name = "dgvMedicamente";
            this.dgvMedicamente.ReadOnly = true;
            this.dgvMedicamente.RowHeadersWidth = 51;
            this.dgvMedicamente.RowTemplate.Height = 24;
            this.dgvMedicamente.Size = new System.Drawing.Size(248, 110);
            this.dgvMedicamente.TabIndex = 0;
            // 
            // lblCod
            // 
            this.lblCod.AutoSize = true;
            this.lblCod.Location = new System.Drawing.Point(12, 176);
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new System.Drawing.Size(32, 16);
            this.lblCod.TabIndex = 1;
            this.lblCod.Text = "Cod";
            // 
            // lblDenumire
            // 
            this.lblDenumire.AutoSize = true;
            this.lblDenumire.Location = new System.Drawing.Point(12, 238);
            this.lblDenumire.Name = "lblDenumire";
            this.lblDenumire.Size = new System.Drawing.Size(65, 16);
            this.lblDenumire.TabIndex = 2;
            this.lblDenumire.Text = "Denumire";
            // 
            // lblPret
            // 
            this.lblPret.AutoSize = true;
            this.lblPret.Location = new System.Drawing.Point(12, 290);
            this.lblPret.Name = "lblPret";
            this.lblPret.Size = new System.Drawing.Size(31, 16);
            this.lblPret.TabIndex = 3;
            this.lblPret.Text = "Pret";
            // 
            // txtPret
            // 
            this.txtPret.Location = new System.Drawing.Point(148, 284);
            this.txtPret.Name = "txtPret";
            this.txtPret.Size = new System.Drawing.Size(100, 22);
            this.txtPret.TabIndex = 4;
            // 
            // txtCod
            // 
            this.txtCod.Location = new System.Drawing.Point(148, 170);
            this.txtCod.Name = "txtCod";
            this.txtCod.Size = new System.Drawing.Size(100, 22);
            this.txtCod.TabIndex = 5;
            // 
            // txtDenumire
            // 
            this.txtDenumire.Location = new System.Drawing.Point(148, 232);
            this.txtDenumire.Name = "txtDenumire";
            this.txtDenumire.Size = new System.Drawing.Size(100, 22);
            this.txtDenumire.TabIndex = 6;
            // 
            // btnAdauga
            // 
            this.btnAdauga.Location = new System.Drawing.Point(485, 169);
            this.btnAdauga.Name = "btnAdauga";
            this.btnAdauga.Size = new System.Drawing.Size(75, 23);
            this.btnAdauga.TabIndex = 7;
            this.btnAdauga.Text = "Adauga";
            this.btnAdauga.UseVisualStyleBackColor = true;
            this.btnAdauga.Click += new System.EventHandler(this.btnAdauga_Click_1);
            // 
            // btnModifica
            // 
            this.btnModifica.Location = new System.Drawing.Point(485, 228);
            this.btnModifica.Name = "btnModifica";
            this.btnModifica.Size = new System.Drawing.Size(75, 23);
            this.btnModifica.TabIndex = 8;
            this.btnModifica.Text = "Modifica";
            this.btnModifica.UseVisualStyleBackColor = true;
            // 
            // btnSterge
            // 
            this.btnSterge.Location = new System.Drawing.Point(485, 283);
            this.btnSterge.Name = "btnSterge";
            this.btnSterge.Size = new System.Drawing.Size(75, 23);
            this.btnSterge.TabIndex = 9;
            this.btnSterge.Text = "Sterge";
            this.btnSterge.UseVisualStyleBackColor = true;
            // 
            // FormMedicamente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSterge);
            this.Controls.Add(this.btnModifica);
            this.Controls.Add(this.btnAdauga);
            this.Controls.Add(this.txtDenumire);
            this.Controls.Add(this.txtCod);
            this.Controls.Add(this.txtPret);
            this.Controls.Add(this.lblPret);
            this.Controls.Add(this.lblDenumire);
            this.Controls.Add(this.lblCod);
            this.Controls.Add(this.dgvMedicamente);
            this.Name = "FormMedicamente";
            this.Text = "FormMedicamente";
            this.Load += new System.EventHandler(this.FormMedicamente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicamente)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvMedicamente;
        private System.Windows.Forms.Label lblCod;
        private System.Windows.Forms.Label lblDenumire;
        private System.Windows.Forms.Label lblPret;
        private System.Windows.Forms.TextBox txtPret;
        private System.Windows.Forms.TextBox txtCod;
        private System.Windows.Forms.TextBox txtDenumire;
        private System.Windows.Forms.Button btnAdauga;
        private System.Windows.Forms.Button btnModifica;
        private System.Windows.Forms.Button btnSterge;
    }
}