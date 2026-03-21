using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestiune_produs_firme
{
    public class FormVanzare : Form
    {
        private Label lblInfo;
        private Label lblCantitate;
        private NumericUpDown numCantitate;
        private Button btnOk;
        private Button btnCancel;

        public int CantitateSelectata { get; private set; }

        public FormVanzare(Produs produs)
        {
            Text = "Vanzare produs";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(350, 220);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            lblInfo = new Label();
            lblInfo.AutoSize = false;
            lblInfo.Location = new Point(20, 20);
            lblInfo.Size = new Size(290, 50);
            lblInfo.Text = "Produs: " + produs.Denumire +
                           "\nStoc disponibil: " + produs.Stoc;

            lblCantitate = new Label();
            lblCantitate.Text = "Cantitate:";
            lblCantitate.Location = new Point(20, 90);
            lblCantitate.AutoSize = true;

            numCantitate = new NumericUpDown();
            numCantitate.Location = new Point(100, 88);
            numCantitate.Size = new Size(120, 25);
            numCantitate.Minimum = 1;
            numCantitate.Maximum = produs.Stoc > 0 ? produs.Stoc : 1;
            numCantitate.Value = 1;

            btnOk = new Button();
            btnOk.Text = "Confirmare";
            btnOk.Location = new Point(40, 130);
            btnOk.Size = new Size(110, 35);
            btnOk.Click += BtnOk_Click;

            btnCancel = new Button();
            btnCancel.Text = "Renunta";
            btnCancel.Location = new Point(170, 130);
            btnCancel.Size = new Size(110, 35);
            btnCancel.Click += BtnCancel_Click;

            Controls.Add(lblInfo);
            Controls.Add(lblCantitate);
            Controls.Add(numCantitate);
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            CantitateSelectata = (int)numCantitate.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
