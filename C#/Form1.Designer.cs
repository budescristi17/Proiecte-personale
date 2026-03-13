namespace Form_depozit {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tbcm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.um = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbd = new System.Windows.Forms.TextBox();
            this.gv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sb = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbv = new System.Windows.Forms.ToolStripStatusLabel();
            this.tbcant = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbpu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.bModifica = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.sb.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cod mat:";
            // 
            // tbcm
            // 
            this.tbcm.Location = new System.Drawing.Point(80, 6);
            this.tbcm.Name = "tbcm";
            this.tbcm.Size = new System.Drawing.Size(90, 20);
            this.tbcm.TabIndex = 1;
            this.tbcm.Text = "0";
            this.tbcm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Unit Masura:";
            // 
            // um
            // 
            this.um.FormattingEnabled = true;
            this.um.Items.AddRange(new object[] {
            "kg",
            "g",
            "t",
            "buc",
            "m",
            "l"});
            this.um.Location = new System.Drawing.Point(81, 46);
            this.um.Name = "um";
            this.um.Size = new System.Drawing.Size(89, 21);
            this.um.TabIndex = 3;
            this.um.Text = "Selecteaza";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(197, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Denumire:";
            // 
            // tbd
            // 
            this.tbd.Location = new System.Drawing.Point(258, 6);
            this.tbd.Name = "tbd";
            this.tbd.Size = new System.Drawing.Size(221, 20);
            this.tbd.TabIndex = 5;
            // 
            // gv
            // 
            this.gv.AllowUserToAddRows = false;
            this.gv.AllowUserToDeleteRows = false;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.gv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gv.Location = new System.Drawing.Point(0, 80);
            this.gv.MultiSelect = false;
            this.gv.Name = "gv";
            this.gv.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gv.Size = new System.Drawing.Size(793, 274);
            this.gv.TabIndex = 6;
            this.gv.SelectionChanged += new System.EventHandler(this.gv_SelectionChanged);
            // 
            // Column1
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column1.HeaderText = "Cod Mat";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Denumire";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Um";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 50;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Cantitate";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Pret_unit";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // sb
            // 
            this.sb.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.sbv});
            this.sb.Location = new System.Drawing.Point(0, 332);
            this.sb.Name = "sb";
            this.sb.Size = new System.Drawing.Size(793, 22);
            this.sb.TabIndex = 7;
            this.sb.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel1.Text = "Valoare:";
            // 
            // sbv
            // 
            this.sbv.Name = "sbv";
            this.sbv.Size = new System.Drawing.Size(13, 17);
            this.sbv.Text = "0";
            // 
            // tbcant
            // 
            this.tbcant.Location = new System.Drawing.Point(241, 46);
            this.tbcant.Name = "tbcant";
            this.tbcant.Size = new System.Drawing.Size(90, 20);
            this.tbcant.TabIndex = 9;
            this.tbcant.Text = "0";
            this.tbcant.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cantitate:";
            // 
            // tbpu
            // 
            this.tbpu.Location = new System.Drawing.Point(389, 46);
            this.tbpu.Name = "tbpu";
            this.tbpu.Size = new System.Drawing.Size(90, 20);
            this.tbpu.TabIndex = 11;
            this.tbpu.Text = "0";
            this.tbpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(334, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Pret unit:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(485, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Adauga";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // bModifica
            // 
            this.bModifica.Location = new System.Drawing.Point(485, 43);
            this.bModifica.Name = "bModifica";
            this.bModifica.Size = new System.Drawing.Size(75, 23);
            this.bModifica.TabIndex = 13;
            this.bModifica.Text = "Modifica";
            this.bModifica.UseVisualStyleBackColor = true;
            this.bModifica.Click += new System.EventHandler(this.bModifica_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 354);
            this.Controls.Add(this.bModifica);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tbpu);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbcant);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sb);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.tbd);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.um);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbcm);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(0, 80, 0, 0);
            this.Text = "Gestiune Materiale";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.sb.ResumeLayout(false);
            this.sb.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbcm;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox um;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbd;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.StatusStrip sb;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel sbv;
        private System.Windows.Forms.TextBox tbcant;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbpu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bModifica;
    }
}

