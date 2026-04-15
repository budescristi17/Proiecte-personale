namespace Model_Test_PAW
{
    partial class Form1
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
            this.btnAdaugaMedicament = new System.Windows.Forms.Button();
            this.btnSalveazaFisa = new System.Windows.Forms.Button();
            this.lblNume = new System.Windows.Forms.Label();
            this.txtNume = new System.Windows.Forms.TextBox();
            this.lblSimptome = new System.Windows.Forms.Label();
            this.clbSimptome = new System.Windows.Forms.CheckedListBox();
            this.lblDurata = new System.Windows.Forms.Label();
            this.nudDurata = new System.Windows.Forms.NumericUpDown();
            this.lblCod = new System.Windows.Forms.Label();
            this.lblDenumire = new System.Windows.Forms.Label();
            this.lblPret = new System.Windows.Forms.Label();
            this.lblCantitate = new System.Windows.Forms.Label();
            this.txtPret = new System.Windows.Forms.TextBox();
            this.txtDenumire = new System.Windows.Forms.TextBox();
            this.txtCod = new System.Windows.Forms.TextBox();
            this.nudCantitate = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudDurata)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantitate)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdaugaMedicament
            // 
            this.btnAdaugaMedicament.Location = new System.Drawing.Point(0, 0);
            this.btnAdaugaMedicament.Name = "btnAdaugaMedicament";
            this.btnAdaugaMedicament.Size = new System.Drawing.Size(192, 86);
            this.btnAdaugaMedicament.TabIndex = 0;
            this.btnAdaugaMedicament.Text = "Adauga medicament";
            this.btnAdaugaMedicament.UseVisualStyleBackColor = true;
            this.btnAdaugaMedicament.Click += new System.EventHandler(this.btnAdaugaMedicament_Click);
            // 
            // btnSalveazaFisa
            // 
            this.btnSalveazaFisa.Location = new System.Drawing.Point(0, 80);
            this.btnSalveazaFisa.Name = "btnSalveazaFisa";
            this.btnSalveazaFisa.Size = new System.Drawing.Size(192, 76);
            this.btnSalveazaFisa.TabIndex = 1;
            this.btnSalveazaFisa.Text = "Salveaza fisa";
            this.btnSalveazaFisa.UseVisualStyleBackColor = true;
            this.btnSalveazaFisa.Click += new System.EventHandler(this.btnSalveazaFisa_Click);
            // 
            // lblNume
            // 
            this.lblNume.AutoSize = true;
            this.lblNume.Location = new System.Drawing.Point(-3, 177);
            this.lblNume.Name = "lblNume";
            this.lblNume.Size = new System.Drawing.Size(90, 16);
            this.lblNume.TabIndex = 2;
            this.lblNume.Text = "Nume pacient";
            // 
            // txtNume
            // 
            this.txtNume.Location = new System.Drawing.Point(93, 171);
            this.txtNume.Name = "txtNume";
            this.txtNume.Size = new System.Drawing.Size(100, 22);
            this.txtNume.TabIndex = 3;
            // 
            // lblSimptome
            // 
            this.lblSimptome.AutoSize = true;
            this.lblSimptome.Location = new System.Drawing.Point(-3, 203);
            this.lblSimptome.Name = "lblSimptome";
            this.lblSimptome.Size = new System.Drawing.Size(68, 16);
            this.lblSimptome.TabIndex = 4;
            this.lblSimptome.Text = "Simptome";
            this.lblSimptome.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // clbSimptome
            // 
            this.clbSimptome.CheckOnClick = true;
            this.clbSimptome.FormattingEnabled = true;
            this.clbSimptome.Items.AddRange(new object[] {
            "Febra",
            "Tuse",
            "Greata"});
            this.clbSimptome.Location = new System.Drawing.Point(93, 203);
            this.clbSimptome.Name = "clbSimptome";
            this.clbSimptome.Size = new System.Drawing.Size(120, 89);
            this.clbSimptome.TabIndex = 5;
            this.clbSimptome.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // lblDurata
            // 
            this.lblDurata.AutoSize = true;
            this.lblDurata.Location = new System.Drawing.Point(-3, 312);
            this.lblDurata.Name = "lblDurata";
            this.lblDurata.Size = new System.Drawing.Size(111, 16);
            this.lblDurata.TabIndex = 6;
            this.lblDurata.Text = "Durata Tratament";
            // 
            // nudDurata
            // 
            this.nudDurata.Location = new System.Drawing.Point(114, 310);
            this.nudDurata.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudDurata.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDurata.Name = "nudDurata";
            this.nudDurata.Size = new System.Drawing.Size(120, 22);
            this.nudDurata.TabIndex = 7;
            this.nudDurata.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCod
            // 
            this.lblCod.AutoSize = true;
            this.lblCod.Location = new System.Drawing.Point(551, 16);
            this.lblCod.Name = "lblCod";
            this.lblCod.Size = new System.Drawing.Size(32, 16);
            this.lblCod.TabIndex = 8;
            this.lblCod.Text = "Cod";
            this.lblCod.Click += new System.EventHandler(this.label1_Click_2);
            // 
            // lblDenumire
            // 
            this.lblDenumire.AutoSize = true;
            this.lblDenumire.Location = new System.Drawing.Point(540, 70);
            this.lblDenumire.Name = "lblDenumire";
            this.lblDenumire.Size = new System.Drawing.Size(65, 16);
            this.lblDenumire.TabIndex = 9;
            this.lblDenumire.Text = "Denumire";
            // 
            // lblPret
            // 
            this.lblPret.AutoSize = true;
            this.lblPret.Location = new System.Drawing.Point(551, 140);
            this.lblPret.Name = "lblPret";
            this.lblPret.Size = new System.Drawing.Size(31, 16);
            this.lblPret.TabIndex = 10;
            this.lblPret.Text = "Pret";
            // 
            // lblCantitate
            // 
            this.lblCantitate.AutoSize = true;
            this.lblCantitate.Location = new System.Drawing.Point(551, 209);
            this.lblCantitate.Name = "lblCantitate";
            this.lblCantitate.Size = new System.Drawing.Size(59, 16);
            this.lblCantitate.TabIndex = 11;
            this.lblCantitate.Text = "Cantitate";
            // 
            // txtPret
            // 
            this.txtPret.Location = new System.Drawing.Point(630, 134);
            this.txtPret.Name = "txtPret";
            this.txtPret.Size = new System.Drawing.Size(100, 22);
            this.txtPret.TabIndex = 13;
            // 
            // txtDenumire
            // 
            this.txtDenumire.Location = new System.Drawing.Point(630, 64);
            this.txtDenumire.Name = "txtDenumire";
            this.txtDenumire.Size = new System.Drawing.Size(100, 22);
            this.txtDenumire.TabIndex = 14;
            // 
            // txtCod
            // 
            this.txtCod.Location = new System.Drawing.Point(630, 13);
            this.txtCod.Name = "txtCod";
            this.txtCod.Size = new System.Drawing.Size(100, 22);
            this.txtCod.TabIndex = 15;
            // 
            // nudCantitate
            // 
            this.nudCantitate.Location = new System.Drawing.Point(630, 201);
            this.nudCantitate.Name = "nudCantitate";
            this.nudCantitate.Size = new System.Drawing.Size(120, 22);
            this.nudCantitate.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 479);
            this.Controls.Add(this.nudCantitate);
            this.Controls.Add(this.txtCod);
            this.Controls.Add(this.txtDenumire);
            this.Controls.Add(this.txtPret);
            this.Controls.Add(this.lblCantitate);
            this.Controls.Add(this.lblPret);
            this.Controls.Add(this.lblDenumire);
            this.Controls.Add(this.lblCod);
            this.Controls.Add(this.nudDurata);
            this.Controls.Add(this.lblDurata);
            this.Controls.Add(this.clbSimptome);
            this.Controls.Add(this.lblSimptome);
            this.Controls.Add(this.txtNume);
            this.Controls.Add(this.lblNume);
            this.Controls.Add(this.btnSalveazaFisa);
            this.Controls.Add(this.btnAdaugaMedicament);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDurata)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantitate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdaugaMedicament;
        private System.Windows.Forms.Button btnSalveazaFisa;
        private System.Windows.Forms.Label lblNume;
        private System.Windows.Forms.TextBox txtNume;
        private System.Windows.Forms.Label lblSimptome;
        private System.Windows.Forms.CheckedListBox clbSimptome;
        private System.Windows.Forms.Label lblDurata;
        private System.Windows.Forms.NumericUpDown nudDurata;
        private System.Windows.Forms.Label lblCod;
        private System.Windows.Forms.Label lblDenumire;
        private System.Windows.Forms.Label lblPret;
        private System.Windows.Forms.Label lblCantitate;
        private System.Windows.Forms.TextBox txtPret;
        private System.Windows.Forms.TextBox txtDenumire;
        private System.Windows.Forms.TextBox txtCod;
        private System.Windows.Forms.NumericUpDown nudCantitate;
    }
}

