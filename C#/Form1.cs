using Seminar1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Form_depozit {
    public partial class Form1 : Form {


        Depozit obDep;

        public Form1() {

            InitializeComponent();
            obDep = new Depozit("Apaca SRL");
            obDep.evModDep += ObDep_evModDep;
            this.Text += "  " + obDep.nume_dep;
        }

        private void ObDep_evModDep(Depozit arg1, int arg2) {
            gv.Rows.Clear();
            foreach (Material m in arg1.materiale) {
                gv.Rows.Add(m.Cod_Material.ToString(), m.Denumire_Material.ToString(), m.Pret_Unitar.ToString(), m.Cantitate.ToString(), m.Unitate_Masura.ToString());

            }
            sbv.Text = arg1.Valoare.ToString("0.##");
            if ( arg1.materiale.Count>0) bModifica.Show();
            gv.Rows[arg2].Selected = true;
        }

        private void button1_Click(object sender, EventArgs e) {
            if (um.Text=="Selecteaza") MessageBox.Show("Te rog selecteaza o unitate de masura", "Atentionare!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else {
                obDep.adaugaMaterial(new Material() {
                    Cod_Material = int.Parse(tbcm.Text),
                    Denumire_Material = tbd.Text,
                    Unitate_Masura = um.Text,
                    Cantitate = int.Parse(tbcant.Text),
                    Pret_Unitar = double.Parse(tbpu.Text)
                });
                //MessageBox.Show(obDep.Valoare.ToString());
                
            }
        }

        private void Form1_Load(object sender, EventArgs e) {
            bModifica.Hide();
        }

        private void gv_SelectionChanged(object sender, EventArgs e) {
            //DataGridViewRow dgvr = gv.SelectedRows[0];
            //MessageBox.Show(dgvr.Cells[0].Value.ToString());
            //tbcm.Text = dgvr.Cells[0].Value.ToString();
            //tbd.Text = dgvr.Cells[1].Value.ToString();
            //um.Text = dgvr.Cells[2].Value.ToString();
            //tbcant.Text = dgvr.Cells[3].Value.ToString();
            //tbpu.Text = dgvr.Cells[4].Value.ToString();
        }

        private void bModifica_Click(object sender, EventArgs e) {
            if (um.Text == "Selecteaza") MessageBox.Show("Te rog selecteaza o unitate de masura", "Atentionare!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else {
                //obDep.adaugaMaterial(new Material() {
                //    Cod_Material = int.Parse(tbcm.Text),
                //    Denumire_Material = tbd.Text,
                //    Unitate_Masura = um.Text,
                //    Cantitate = int.Parse(tbcant.Text),
                //    Pret_Unitar = double.Parse(tbpu.Text)
                //});
                //MessageBox.Show(obDep.Valoare.ToString());
                int k = gv.SelectedRows[0].Index;
                gv.Rows[k].Cells[0].Value = tbcm.Text;
                gv.Rows[k].Cells[1].Value = tbd.Text;
                gv.Rows[k].Cells[2].Value = um.Text;
                gv.Rows[k].Cells[3].Value = tbcant.Text;
                gv.Rows[k].Cells[4].Value = tbpu.Text;
                sbv.Text = obDep.Valoare.ToString("0.##");
            }
            }
    }
}
