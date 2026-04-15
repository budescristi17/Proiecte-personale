using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.Drawing.Printing;

namespace Model_Test_PAW
{
    public partial class FormMDI : Form
    {
        private string continutRaport = "";
        public FormMDI()
        {
            InitializeComponent();
        }

        private void fisaNouaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.MdiParent = this;
            f.Text = "Fisa pacient " + (this.MdiChildren.Length + 1);
            f.Show();
        }

        private void tiparesteFiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int nr = 1;

            foreach (Form f in this.MdiChildren)
            {
                Form1 formFisa = f as Form1;

                if (formFisa != null && formFisa.FisaCurenta != null)
                {
                    FisaPacient fp = formFisa.FisaCurenta;

                    sb.AppendLine("Fisa " + nr);
                    sb.AppendLine("Nume pacient: " + fp.Nume);
                    sb.AppendLine("Simptome: " + fp.Simptome);
                    sb.AppendLine("Durata tratamentului: " + fp.Durata_tratament);
                    sb.AppendLine("Numarul de medicamente: " + ((int)fp));
                    sb.AppendLine("Costul tratamentului: " + ((double)fp));
                    sb.AppendLine("--------------------------------------------");

                    nr++;
                }
            }

            if (sb.Length == 0)
            {
                MessageBox.Show("Nu exista fise salvate in ferestrele deschise.");
                return;
            }

            continutRaport = sb.ToString();

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += Pd_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = pd;
            preview.Width = 1000;
            preview.Height = 700;
            preview.ShowDialog();
        }

        private void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Arial", 12);

            RectangleF zona = new RectangleF(
                e.MarginBounds.Left,
                e.MarginBounds.Top,
                e.MarginBounds.Width,
                e.MarginBounds.Height
            );

            e.Graphics.DrawString(continutRaport, font, Brushes.Black, zona);
        }
    }
}
