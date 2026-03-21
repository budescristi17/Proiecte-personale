using System.Drawing;
using System.Windows.Forms;

namespace Gestiune_produs_firme
{
    public class FormRaport : Form
    {
        private RichTextBox rtbRaport;

        public FormRaport(string continutRaport)
        {
            Text = "Raport produse";
            StartPosition = FormStartPosition.CenterParent;
            Size = new Size(700, 500);

            rtbRaport = new RichTextBox();
            rtbRaport.Dock = DockStyle.Fill;
            rtbRaport.ReadOnly = true;
            rtbRaport.Font = new Font("Consolas", 11);
            rtbRaport.Text = continutRaport;

            Controls.Add(rtbRaport);
        }
    }
}