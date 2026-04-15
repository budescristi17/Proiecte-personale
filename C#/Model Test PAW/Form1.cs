using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Model_Test_PAW
{
    public partial class Form1 : Form
    {
        private List<FisaPacient> fisePacienti = new List<FisaPacient>();
        private List<Medicament> medicamenteCurente = new List<Medicament>();
        private List<int> cantitatiCurente = new List<int>();
        public FisaPacient FisaCurenta { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnAdaugaMedicament_Click(object sender, EventArgs e)
        {
            int cod;
            float pret;

            if (!int.TryParse(txtCod.Text, out cod))
            {
                MessageBox.Show("Cod invalid.");
                return;
            }

            if (txtDenumire.Text == "")
            {
                MessageBox.Show("Introdu denumirea medicamentului.");
                return;
            }

            if (!float.TryParse(txtPret.Text, out pret))
            {
                MessageBox.Show("Pret invalid.");
                return;
            }

            int cantitate = (int)nudCantitate.Value;

            Medicament m = new Medicament(cod, txtDenumire.Text, pret);

            medicamenteCurente.Add(m);
            cantitatiCurente.Add(cantitate);

            MessageBox.Show("Medicament adaugat.");

            txtCod.Clear();
            txtDenumire.Clear();
            txtPret.Clear();
            nudCantitate.Value = 1;
        }
        

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void btnSalveazaFisa_Click(object sender, EventArgs e)
        {
            if (txtNume.Text == "")
            {
                MessageBox.Show("Introdu numele pacientului.");
                return;
            }

            if (clbSimptome.CheckedItems.Count == 0)
            {
                MessageBox.Show("Selecteaza cel putin un simptom.");
                return;
            }

            if (medicamenteCurente.Count == 0)
            {
                MessageBox.Show("Adauga cel putin un medicament.");
                return;
            }

            string simptome = "";

            for (int i = 0; i < clbSimptome.CheckedItems.Count; i++)
            {
                simptome += clbSimptome.CheckedItems[i].ToString();

                if (i < clbSimptome.CheckedItems.Count - 1)
                {
                    simptome += ", ";
                }
            }

            FisaPacient fisa = new FisaPacient(
                txtNume.Text,
                simptome,
                (int)nudDurata.Value,
                new List<Medicament>(medicamenteCurente),
                new List<int>(cantitatiCurente)
            );

            FisaCurenta = fisa;
            fisePacienti.Add(fisa);

            MessageBox.Show(
                "Fisa salvata.\n" +
                "Nr. medicamente: " + ((int)fisa).ToString() + "\n" +
                "Cost total: " + ((double)fisa).ToString()
            );

            txtNume.Clear();
            nudDurata.Value = 1;

            for (int i = 0; i < clbSimptome.Items.Count; i++)
            {
                clbSimptome.SetItemChecked(i, false);
            }

            medicamenteCurente.Clear();
            cantitatiCurente.Clear();
        }
    }
}
