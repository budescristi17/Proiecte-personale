namespace Gestiune_produs_firme
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listViewProduse = new ListView();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            panel1 = new Panel();
            toolStrip1 = new ToolStrip();
            btnVinde = new Button();
            SuspendLayout();
            // 
            // listViewProduse
            // 
            listViewProduse.Columns.AddRange(new ColumnHeader[] { columnHeader2, columnHeader3, columnHeader4 });
            listViewProduse.Cursor = Cursors.Hand;
            listViewProduse.FullRowSelect = true;
            listViewProduse.GridLines = true;
            listViewProduse.Location = new Point(-4, 28);
            listViewProduse.Name = "listViewProduse";
            listViewProduse.Size = new Size(328, 372);
            listViewProduse.TabIndex = 0;
            listViewProduse.UseCompatibleStateImageBehavior = false;
            listViewProduse.View = View.Details;
            listViewProduse.SelectedIndexChanged += listView1_SelectedIndexChanged;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Cod";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Denumire";
            columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Pret";
            columnHeader4.Width = 100;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Desktop;
            button1.Location = new Point(600, 28);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 1;
            button1.Text = "Adaugare";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.Highlight;
            button2.Location = new Point(600, 187);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 2;
            button2.Text = "Stergere";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.BackColor = Color.IndianRed;
            button3.Location = new Point(600, 331);
            button3.Name = "button3";
            button3.Size = new Size(94, 29);
            button3.TabIndex = 3;
            button3.Text = "Minim";
            button3.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(-4, 378);
            panel1.Name = "panel1";
            panel1.Size = new Size(865, 147);
            panel1.TabIndex = 4;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(861, 25);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnVinde
            // 
            btnVinde.Location = new Point(600, 259);
            btnVinde.Name = "btnVinde";
            btnVinde.Size = new Size(94, 29);
            btnVinde.TabIndex = 6;
            btnVinde.Text = "Vinde produs";
            btnVinde.UseVisualStyleBackColor = true;
            btnVinde.Click += this.btnVinde_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(861, 522);
            Controls.Add(btnVinde);
            Controls.Add(toolStrip1);
            Controls.Add(panel1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listViewProduse);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView listViewProduse;
        private Button button1;
        private Button button2;
        private Button button3;
        private Panel panel1;
        private ToolStrip toolStrip1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private Button btnVinde;
    }
}
