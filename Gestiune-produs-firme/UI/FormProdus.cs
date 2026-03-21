using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestiune_produs_firme
{
    public class FormProdus : Form
    {
        private TextBox txtCod;
        private TextBox txtDenumire;
        private TextBox txtPret;
        private ComboBox cmbCategorie;
        private NumericUpDown nudStoc;
        private Button btnOK;
        private Button btnCancel;

        public Produs ProdusRezultat { get; private set; }

        public FormProdus()
        {
            InitializeazaControale();
        }

        public FormProdus(Produs p) : this()
        {
            txtCod.Text = p.Cod.ToString();
            txtDenumire.Text = p.Denumire;
            txtPret.Text = p.Pret.ToString();
            cmbCategorie.Text = p.Categorie;
            nudStoc.Value = p.Stoc;
        }

        private void InitializeazaControale()
        {
            Text = "Date produs";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(360, 300);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            Label lblCod = new Label();
            lblCod.Text = "Cod:";
            lblCod.Location = new Point(20, 20);
            lblCod.Size = new Size(90, 25);

            txtCod = new TextBox();
            txtCod.Location = new Point(130, 20);
            txtCod.Size = new Size(170, 25);

            Label lblDenumire = new Label();
            lblDenumire.Text = "Denumire:";
            lblDenumire.Location = new Point(20, 60);
            lblDenumire.Size = new Size(90, 25);

            txtDenumire = new TextBox();
            txtDenumire.Location = new Point(130, 60);
            txtDenumire.Size = new Size(170, 25);

            Label lblPret = new Label();
            lblPret.Text = "Pret:";
            lblPret.Location = new Point(20, 100);
            lblPret.Size = new Size(90, 25);

            txtPret = new TextBox();
            txtPret.Location = new Point(130, 100);
            txtPret.Size = new Size(170, 25);

            Label lblCategorie = new Label();
            lblCategorie.Text = "Categorie:";
            lblCategorie.Location = new Point(20, 140);
            lblCategorie.Size = new Size(90, 25);

            cmbCategorie = new ComboBox();
            cmbCategorie.Location = new Point(130, 140);
            cmbCategorie.Size = new Size(170, 25);
            cmbCategorie.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCategorie.Items.Add("Laptopuri");
            cmbCategorie.Items.Add("Periferice");
            cmbCategorie.Items.Add("Monitoare");
            cmbCategorie.Items.Add("Accesorii");
            cmbCategorie.Items.Add("Componente");
            cmbCategorie.Items.Add("Diverse");
            cmbCategorie.Text = "Diverse";

            Label lblStoc = new Label();
            lblStoc.Text = "Stoc:";
            lblStoc.Location = new Point(20, 180);
            lblStoc.Size = new Size(90, 25);

            nudStoc = new NumericUpDown();
            nudStoc.Location = new Point(130, 180);
            nudStoc.Size = new Size(170, 25);
            nudStoc.Minimum = 0;
            nudStoc.Maximum = 100000;

            btnOK = new Button();
            btnOK.Text = "OK";
            btnOK.Location = new Point(70, 220);
            btnOK.Size = new Size(80, 30);
            btnOK.Click += btnOK_Click;

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(190, 220);
            btnCancel.Size = new Size(80, 30);
            btnCancel.Click += btnCancel_Click;

            Controls.Add(lblCod);
            Controls.Add(txtCod);
            Controls.Add(lblDenumire);
            Controls.Add(txtDenumire);
            Controls.Add(lblPret);
            Controls.Add(txtPret);
            Controls.Add(lblCategorie);
            Controls.Add(cmbCategorie);
            Controls.Add(lblStoc);
            Controls.Add(nudStoc);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);

            AcceptButton = btnOK;
            CancelButton = btnCancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int cod;
            double pret;

            if (!int.TryParse(txtCod.Text, out cod))
            {
                MessageBox.Show("Cod invalid!");
                return;
            }

            if (txtDenumire.Text.Trim() == "")
            {
                MessageBox.Show("Denumire invalida!");
                return;
            }

            if (!double.TryParse(txtPret.Text, out pret) || pret <= 0)
            {
                MessageBox.Show("Pret invalid!");
                return;
            }

            string categorie = cmbCategorie.Text.Trim();
            if (categorie == "")
                categorie = "Diverse";

            ProdusRezultat = new Produs(
                cod,
                txtDenumire.Text.Trim(),
                pret,
                categorie,
                (int)nudStoc.Value);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}