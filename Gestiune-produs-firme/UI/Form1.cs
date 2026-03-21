using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

namespace Gestiune_produs_firme
{
    public partial class Form1 : Form
    {
        private Container container = new Container();
        private Produs produsSelectat = null;
        private readonly string numeFisier = "produse.json";
        private DatabaseHelper db = new DatabaseHelper();
        private ProdusService produsService = new ProdusService();


        private ToolStripTextBox txtCautare;
        private ToolStripComboBox cmbSortare;
        private ToolStripLabel lblNrProduse;
        private ToolStripLabel lblValoareStoc;

        private GroupBox gbOperatii;
        private GroupBox gbDetalii;
        private GroupBox gbVizualizare;

        private Label lblCodDetaliu;
        private Label lblDenumireDetaliu;
        private Label lblPretDetaliu;
        private Label lblCategorieDetaliu;
        private Label lblStocDetaliu;
        private Label lblLegendaRosu;
        private Label lblLegendaAlbastru;
        private Panel pnlRosu;
        private Panel pnlAlbastru;



        public Form1()
        {
            InitializeComponent();

            StilizareInterfata();
            InitializareLayoutCuGroupBoxuri();
            AranjeazaButonVinde();

            InitializareListaView();
            InitializareToolStrip();


            listViewProduse.DoubleClick += listViewProduse_DoubleClick;
            button3.Click += button3_Click;
            panel1.Paint += panel1_Paint;

            listViewProduse.HideSelection = false;
            listViewProduse.MultiSelect = false;

            db.InitializeDatabase();

            if (!db.AreProduse())
            {
                db.InsertProdus(new Produs(1, "Laptop", 3500, "Laptopuri", 7));
                db.InsertProdus(new Produs(2, "Mouse", 120, "Periferice", 25));
                db.InsertProdus(new Produs(3, "Monitor", 900, "Monitoare", 4));
            }

            container.Produse = db.GetAllProduse();

            if (container.Produse.Count > 0)
                produsSelectat = container.Produse[0];

            AplicaFiltrareSiSortare();

        }


        private void InitializareListaView()
        {
            listViewProduse.Columns.Clear();
            listViewProduse.Columns.Add("Cod", 60);
            listViewProduse.Columns.Add("Denumire", 120);
            listViewProduse.Columns.Add("Pret", 80);
            listViewProduse.Columns.Add("Categorie", 120);
            listViewProduse.Columns.Add("Stoc", 70);

            listViewProduse.Width = 470;
        }

        private void InitializareToolStrip()
        {
            toolStrip1.Items.Clear();

            ToolStripButton btnSalvare = new ToolStripButton("Salvare");
            ToolStripButton btnIncarcare = new ToolStripButton("Incarcare");
            ToolStripButton btnRaport = new ToolStripButton("Raport");

            btnSalvare.Click += btnSalvare_Click;
            btnIncarcare.Click += btnIncarcare_Click;
            btnRaport.Click += btnRaport_Click;

            ToolStripLabel lblCautare = new ToolStripLabel("Cautare:");
            txtCautare = new ToolStripTextBox();
            txtCautare.AutoSize = false;
            txtCautare.Width = 140;
            txtCautare.TextChanged += txtCautare_TextChanged;

            ToolStripLabel lblSortare = new ToolStripLabel("Sortare:");
            cmbSortare = new ToolStripComboBox();
            cmbSortare.AutoSize = false;
            cmbSortare.Width = 170;
            cmbSortare.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSortare.Items.Add("Nesortat");
            cmbSortare.Items.Add("Pret crescator");
            cmbSortare.Items.Add("Pret descrescator");
            cmbSortare.Items.Add("Denumire A-Z");
            cmbSortare.Items.Add("Denumire Z-A");
            cmbSortare.Items.Add("Stoc crescator");
            cmbSortare.Items.Add("Stoc descrescator");
            cmbSortare.SelectedIndex = 0;
            cmbSortare.SelectedIndexChanged += cmbSortare_SelectedIndexChanged;

            lblNrProduse = new ToolStripLabel("Nr. produse: 0");
            lblValoareStoc = new ToolStripLabel("Valoare stoc: 0");

            toolStrip1.Items.Add(btnSalvare);
            toolStrip1.Items.Add(btnIncarcare);
            toolStrip1.Items.Add(btnRaport);
            toolStrip1.Items.Add(new ToolStripSeparator());

            toolStrip1.Items.Add(lblCautare);
            toolStrip1.Items.Add(txtCautare);
            toolStrip1.Items.Add(new ToolStripSeparator());

            toolStrip1.Items.Add(lblSortare);
            toolStrip1.Items.Add(cmbSortare);
            toolStrip1.Items.Add(new ToolStripSeparator());

            toolStrip1.Items.Add(lblNrProduse);
            toolStrip1.Items.Add(new ToolStripSeparator());
            toolStrip1.Items.Add(lblValoareStoc);
        }

        private void AplicaFiltrareSiSortare()
        {
            IEnumerable<Produs> rezultat = container.Produse;

            string textCautare = txtCautare.Text.Trim().ToLower();

            if (textCautare != "")
            {
                rezultat = rezultat.Where(p =>
                    p.Denumire.ToLower().Contains(textCautare) ||
                    p.Categorie.ToLower().Contains(textCautare) ||
                    p.Cod.ToString().Contains(textCautare));
            }

            string criteriu = cmbSortare.SelectedItem.ToString();

            switch (criteriu)
            {
                case "Pret crescator":
                    rezultat = rezultat.OrderBy(p => p.Pret);
                    break;

                case "Pret descrescator":
                    rezultat = rezultat.OrderByDescending(p => p.Pret);
                    break;

                case "Denumire A-Z":
                    rezultat = rezultat.OrderBy(p => p.Denumire);
                    break;

                case "Denumire Z-A":
                    rezultat = rezultat.OrderByDescending(p => p.Denumire);
                    break;

                case "Stoc crescator":
                    rezultat = rezultat.OrderBy(p => p.Stoc);
                    break;

                case "Stoc descrescator":
                    rezultat = rezultat.OrderByDescending(p => p.Stoc);
                    break;
            }

            AfisareProduse(rezultat.ToList());
        }

        private void AfisareProduse(List<Produs> lista)
        {
            listViewProduse.Items.Clear();

            foreach (Produs p in lista)
            {
                ListViewItem item = new ListViewItem(p.Cod.ToString());
                item.SubItems.Add(p.Denumire);
                item.SubItems.Add(p.Pret.ToString());
                item.SubItems.Add(p.Categorie);
                item.SubItems.Add(p.Stoc.ToString());
                item.Tag = p;

                if (p.Stoc == 0)
                {
                    item.BackColor = Color.LightCoral;
                }
                else if (p.Stoc < 5)
                {
                    item.BackColor = Color.MistyRose;
                }

                listViewProduse.Items.Add(item);
            }

            SelecteazaProdusInLista();
            ActualizeazaStatisticiVizibile();
            ActualizeazaDetaliiProdus();
            panel1.Invalidate();
            ActualizeazaStareButonVinde();
        }

        private void SelecteazaProdusInLista()
        {
            if (produsSelectat == null)
                return;

            foreach (ListViewItem item in listViewProduse.Items)
            {
                Produs p = (Produs)item.Tag;

                if (p.Cod == produsSelectat.Cod)
                {
                    item.Selected = true;
                    item.Focused = true;
                    item.EnsureVisible();
                    break;
                }
            }
        }

        private Produs GetProdusSelectat()
        {
            if (listViewProduse.SelectedItems.Count == 0)
                return null;

            return (Produs)listViewProduse.SelectedItems[0].Tag;
        }

        private bool ExistaCod(int cod, Produs produsIgnorat = null)
        {
            foreach (Produs p in container.Produse)
            {
                if (p.Cod == cod && p != produsIgnorat)
                    return true;
            }

            return false;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            produsSelectat = GetProdusSelectat();
            ActualizeazaDetaliiProdus();
            panel1.Invalidate();
            ActualizeazaStareButonVinde();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormProdus frm = new FormProdus();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string eroareValidare = ValidareProdus.Valideaza(frm.ProdusRezultat);
                if (eroareValidare != "")
                {
                    MessageBox.Show(eroareValidare);
                    return;
                }

                if (produsService.ExistaCod(container.Produse, frm.ProdusRezultat.Cod))
                {
                    MessageBox.Show("Exista deja un produs cu acest cod!");
                    return;
                }

                container.Produse.Add(frm.ProdusRezultat);
                db.InsertProdus(frm.ProdusRezultat);

                produsSelectat = frm.ProdusRezultat;
                AplicaFiltrareSiSortare();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Produs p = GetProdusSelectat();

            if (p == null)
            {
                MessageBox.Show("Selecteaza un produs pentru stergere!");
                return;
            }

            DialogResult raspuns = MessageBox.Show(
                "Sigur vrei sa stergi produsul selectat?",
                "Confirmare stergere",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (raspuns == DialogResult.No)
                return;

            db.DeleteProdus(p.Cod);
            container.Produse.Remove(p);

            if (container.Produse.Count > 0)
                produsSelectat = container.Produse[0];
            else
                produsSelectat = null;

            AplicaFiltrareSiSortare();
        }

        private void listViewProduse_DoubleClick(object sender, EventArgs e)
        {
            Produs p = GetProdusSelectat();

            if (p == null)
            {
                MessageBox.Show("Selecteaza un produs!");
                return;
            }

            int codInitial = p.Cod;

            FormProdus frm = new FormProdus(p);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string eroareValidare = ValidareProdus.Valideaza(frm.ProdusRezultat);
                if (eroareValidare != "")
                {
                    MessageBox.Show(eroareValidare);
                    return;
                }

                if (produsService.ExistaCod(container.Produse, frm.ProdusRezultat.Cod, p))
                {
                    MessageBox.Show("Exista deja un alt produs cu acest cod!");
                    return;
                }

                p.Cod = frm.ProdusRezultat.Cod;
                p.Denumire = frm.ProdusRezultat.Denumire;
                p.Pret = frm.ProdusRezultat.Pret;
                p.Categorie = frm.ProdusRezultat.Categorie;
                p.Stoc = frm.ProdusRezultat.Stoc;

                db.UpdateProdus(p, codInitial);

                produsSelectat = p;
                AplicaFiltrareSiSortare();
            }
        }


        private void txtCautare_TextChanged(object sender, EventArgs e)
        {
            AplicaFiltrareSiSortare();
        }

        private void cmbSortare_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicaFiltrareSiSortare();
        }

        private void btnSalvare_Click(object sender, EventArgs e)
        {
            string json = JsonSerializer.Serialize(container.Produse, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(numeFisier, json);
            MessageBox.Show("Datele au fost salvate.");
        }

        private void btnIncarcare_Click(object sender, EventArgs e)
        {
            if (!File.Exists(numeFisier))
            {
                MessageBox.Show("Fisierul nu exista!");
                return;
            }

            string json = File.ReadAllText(numeFisier);
            List<Produs> lista = JsonSerializer.Deserialize<List<Produs>>(json);

            if (lista != null)
                container.Produse = lista;
            else
                container.Produse = new List<Produs>();

            if (container.Produse.Count > 0)
                produsSelectat = container.Produse[0];
            else
                produsSelectat = null;

            AplicaFiltrareSiSortare();
        }

        private Produs DeterminaProdusMinim()
        {
            if (container.Produse.Count == 0)
                return null;

            Produs minim = container.Produse[0];

            for (int i = 1; i < container.Produse.Count; i++)
            {
                if (container.Produse[i] < minim)
                    minim = container.Produse[i];
            }

            return minim;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Produs minim = produsService.DeterminaProdusMinim(container.Produse);

            if (minim == null)
            {
                MessageBox.Show("Nu exista produse.");
                return;
            }

            produsSelectat = minim;
            SelecteazaProdusInLista();
            ActualizeazaDetaliiProdus();


            MessageBox.Show(
                "Produsul cu pret minim este:\n" +
                "Cod: " + minim.Cod + "\n" +
                "Denumire: " + minim.Denumire + "\n" +
                "Pret: " + minim.Pret + "\n" +
                "Categorie: " + minim.Categorie + "\n" +
                "Stoc: " + minim.Stoc);

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Produs minim = DeterminaProdusMinim();

            if (minim == null)
                return;

            if (produsSelectat == null)
                produsSelectat = minim;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int latime = panel1.Width;
            int inaltime = panel1.Height;

            int centruX = latime / 2;
            int centruY = inaltime / 2;

            double valoareMaxima = Math.Max(minim.Pret, produsSelectat.Pret);

            if (valoareMaxima <= 0)
                return;

            int razaMaximaPanou = Math.Min(latime, inaltime) / 2 - 15;
            double scala = razaMaximaPanou / valoareMaxima;

            int razaMinim = Math.Max(10, (int)(minim.Pret * scala));
            int razaSelectat = Math.Max(10, (int)(produsSelectat.Pret * scala));

            using (Pen penRosu = new Pen(Color.Red, 2))
            using (Pen penAlbastru = new Pen(Color.Blue, 2))
            {
                g.DrawEllipse(penRosu,
                    centruX - razaMinim,
                    centruY - razaMinim,
                    razaMinim * 2,
                    razaMinim * 2);

                g.DrawEllipse(penAlbastru,
                    centruX - razaSelectat,
                    centruY - razaSelectat,
                    razaSelectat * 2,
                    razaSelectat * 2);
            }
        }

        private double CalculeazaValoareTotalaStoc()
        {
            double total = 0;

            foreach (Produs p in container.Produse)
            {
                total += p.Pret * p.Stoc;
            }

            return total;
        }
        private void ActualizeazaStatisticiVizibile()
        {
            lblNrProduse.Text = "Nr. produse: " + container.Produse.Count;
            lblValoareStoc.Text = "Valoare stoc: " + produsService.CalculeazaValoareTotalaStoc(container.Produse).ToString("0.00");
        }


        private string GenereazaRaport()
        {
            if (container.Produse.Count == 0)
                return "Nu exista produse in gestiune.";

            Produs minim = DeterminaProdusMinim();
            Produs maxim = container.Produse[0];

            double sumaPreturi = 0;
            int totalStoc = 0;
            int produseStocMic = 0;
            int produseFaraStoc = 0;

            foreach (Produs p in container.Produse)
            {
                sumaPreturi += p.Pret;
                totalStoc += p.Stoc;

                if (p.Pret > maxim.Pret)
                    maxim = p;

                if (p.Stoc < 5)
                    produseStocMic++;

                if (p.Stoc == 0)
                    produseFaraStoc++;
            }

            double pretMediu = sumaPreturi / container.Produse.Count;
            double valoareTotalaStoc = CalculeazaValoareTotalaStoc();

            string raport = "";
            raport += "================ RAPORT PRODUSE ================\n\n";
            raport += "Numar total produse: " + container.Produse.Count + "\n";
            raport += "Cantitate totala in stoc: " + totalStoc + "\n";
            raport += "Valoare totala stoc: " + valoareTotalaStoc.ToString("0.00") + "\n";
            raport += "Pret mediu produse: " + pretMediu.ToString("0.00") + "\n";
            raport += "Produse cu stoc mic (<5): " + produseStocMic + "\n";
            raport += "Produse fara stoc: " + produseFaraStoc + "\n\n";

            raport += "Produs minim:\n";
            raport += " - " + minim.Denumire + " | Pret: " + minim.Pret + " | Stoc: " + minim.Stoc + "\n\n";

            raport += "Produs maxim:\n";
            raport += " - " + maxim.Denumire + " | Pret: " + maxim.Pret + " | Stoc: " + maxim.Stoc + "\n\n";

            raport += "=============== LISTA PRODUSE ===============\n";
            foreach (Produs p in container.Produse)
            {
                raport += "Cod: " + p.Cod +
                          " | Denumire: " + p.Denumire +
                          " | Pret: " + p.Pret +
                          " | Categorie: " + p.Categorie +
                          " | Stoc: " + p.Stoc +
                          " | Valoare stoc produs: " + (p.Pret * p.Stoc).ToString("0.00") +
                          "\n";
            }

            return raport;
        }
        private void btnRaport_Click(object sender, EventArgs e)
        {
            string raport = produsService.GenereazaRaport(container.Produse);
            FormRaport frm = new FormRaport(raport);
            frm.ShowDialog();
        }

        private void StilizareInterfata()
        {
            Text = "Sistem de gestiune produse";
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.FromArgb(245, 247, 250);
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            MinimumSize = new Size(1100, 700);

            listViewProduse.Font = new Font("Segoe UI", 10F);
            listViewProduse.FullRowSelect = true;
            listViewProduse.GridLines = true;
            listViewProduse.HideSelection = false;

            button1.FlatStyle = FlatStyle.Flat;
            button2.FlatStyle = FlatStyle.Flat;
            button3.FlatStyle = FlatStyle.Flat;

            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatAppearance.BorderSize = 0;

            button1.BackColor = Color.FromArgb(52, 152, 219);
            button2.BackColor = Color.FromArgb(231, 76, 60);
            button3.BackColor = Color.FromArgb(46, 204, 113);

            button1.ForeColor = Color.White;
            button2.ForeColor = Color.White;
            button3.ForeColor = Color.White;

            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            toolStrip1.BackColor = Color.White;
            toolStrip1.RenderMode = ToolStripRenderMode.System;
        }

        private void InitializareLayoutCuGroupBoxuri()
        {
            ClientSize = new Size(1020, 800);

            listViewProduse.Location = new Point(20, 70);
            listViewProduse.Size = new Size(650, 450);
            listViewProduse.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;

            gbOperatii = new GroupBox();
            gbOperatii.Text = "Operatii";
            gbOperatii.Location = new Point(720, 80);
            gbOperatii.Size = new Size(240, 300);
            gbOperatii.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbOperatii.BackColor = Color.White;
            gbOperatii.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            gbDetalii = new GroupBox();
            gbDetalii.Text = "Detalii produs";
            gbDetalii.Location = new Point(720, 395);
            gbDetalii.Size = new Size(240, 180);
            gbDetalii.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbDetalii.BackColor = Color.White;
            gbDetalii.Anchor = AnchorStyles.Top | AnchorStyles.Right;


            Controls.Add(gbOperatii);
            Controls.Add(gbDetalii);
            Controls.Add(gbVizualizare);

            button1.Parent = gbOperatii;
            button2.Parent = gbOperatii;
            button3.Parent = gbOperatii;

            button1.Location = new Point(25, 35);
            button1.Size = new Size(180, 45);

            button2.Location = new Point(25, 95);
            button2.Size = new Size(180, 45);

            button3.Location = new Point(25, 155);
            button3.Size = new Size(180, 45);

            gbVizualizare = new GroupBox();
            gbVizualizare.Text = "Vizualizare preturi";
            gbVizualizare.Location = new Point(20, 590);
            gbVizualizare.Size = new Size(940, 170);
            gbVizualizare.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbVizualizare.BackColor = Color.White;
            gbVizualizare.ForeColor = Color.FromArgb(60, 60, 60);
            gbVizualizare.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;


            Controls.Add(gbVizualizare);

            panel1.Parent = gbVizualizare;
            panel1.Location = new Point(20, 30);
            panel1.Size = new Size(720, 110);
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            pnlRosu = new Panel();
            pnlRosu.Parent = gbVizualizare;
            pnlRosu.BackColor = Color.Red;
            pnlRosu.Location = new Point(780, 45);
            pnlRosu.Size = new Size(20, 20);
            pnlRosu.BorderStyle = BorderStyle.FixedSingle;

            lblLegendaRosu = new Label();
            lblLegendaRosu.Parent = gbVizualizare;
            lblLegendaRosu.Text = "Pret minim";
            lblLegendaRosu.Location = new Point(815, 43);
            lblLegendaRosu.AutoSize = true;
            lblLegendaRosu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblLegendaRosu.ForeColor = Color.Black;

            pnlAlbastru = new Panel();
            pnlAlbastru.Parent = gbVizualizare;
            pnlAlbastru.BackColor = Color.Blue;
            pnlAlbastru.Location = new Point(780, 80);
            pnlAlbastru.Size = new Size(20, 20);
            pnlAlbastru.BorderStyle = BorderStyle.FixedSingle;

            lblLegendaAlbastru = new Label();
            lblLegendaAlbastru.Parent = gbVizualizare;
            lblLegendaAlbastru.Text = "Produs selectat";
            lblLegendaAlbastru.Location = new Point(815, 78);
            lblLegendaAlbastru.AutoSize = true;
            lblLegendaAlbastru.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            lblLegendaAlbastru.ForeColor = Color.Black;


            lblCodDetaliu = new Label();
            lblCodDetaliu.Location = new Point(20, 35);
            lblCodDetaliu.AutoSize = true;
            lblCodDetaliu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lblDenumireDetaliu = new Label();
            lblDenumireDetaliu.Location = new Point(20, 60);
            lblDenumireDetaliu.AutoSize = true;
            lblDenumireDetaliu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lblPretDetaliu = new Label();
            lblPretDetaliu.Location = new Point(20, 85);
            lblPretDetaliu.AutoSize = true;
            lblPretDetaliu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lblCategorieDetaliu = new Label();
            lblCategorieDetaliu.Location = new Point(20, 110);
            lblCategorieDetaliu.AutoSize = true;
            lblCategorieDetaliu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            lblStocDetaliu = new Label();
            lblStocDetaliu.Location = new Point(20, 135);
            lblStocDetaliu.AutoSize = true;
            lblStocDetaliu.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            gbDetalii.Controls.Add(lblCodDetaliu);
            gbDetalii.Controls.Add(lblDenumireDetaliu);
            gbDetalii.Controls.Add(lblPretDetaliu);
            gbDetalii.Controls.Add(lblCategorieDetaliu);
            gbDetalii.Controls.Add(lblStocDetaliu);
           

            ActualizeazaDetaliiProdus();
        }
        private void ActualizeazaDetaliiProdus()
        {
            if (lblCodDetaliu == null)
                return;

            if (produsSelectat == null)
            {
                lblCodDetaliu.Text = "Cod: -";
                lblDenumireDetaliu.Text = "Denumire: -";
                lblPretDetaliu.Text = "Pret: -";
                lblCategorieDetaliu.Text = "Categorie: -";
                lblStocDetaliu.Text = "Stoc: -";
                return;
            }

            lblCodDetaliu.Text = "Cod: " + produsSelectat.Cod;
            lblDenumireDetaliu.Text = "Denumire: " + produsSelectat.Denumire;
            lblPretDetaliu.Text = "Pret: " + produsSelectat.Pret.ToString("0.00");
            lblCategorieDetaliu.Text = "Categorie: " + produsSelectat.Categorie;
            lblStocDetaliu.Text = "Stoc: " + produsSelectat.Stoc;
        }
        private void btnVinde_Click(object sender, EventArgs e)
        {
            Produs p = GetProdusSelectat();

            if (p == null)
            {
                MessageBox.Show("Selecteaza un produs!");
                return;
            }

            FormVanzare frm = new FormVanzare(p);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                string eroare = produsService.VindeProdus(p, frm.CantitateSelectata);

                if (eroare != "")
                {
                    MessageBox.Show(eroare);
                    return;
                }

                db.UpdateProdus(p, p.Cod);

                produsSelectat = p;
                AplicaFiltrareSiSortare();
                ActualizeazaStatisticiVizibile();

                MessageBox.Show("Vanzarea a fost inregistrata cu succes!");
            }
        }
        private void AranjeazaButonVinde()
        {
            btnVinde.Parent = gbOperatii;

            btnVinde.Left = button1.Left;
            btnVinde.Top = button3.Bottom + 15;
            btnVinde.Width = button1.Width;
            btnVinde.Height = button1.Height;

            btnVinde.FlatStyle = FlatStyle.Flat;
            btnVinde.FlatAppearance.BorderSize = 0;
            btnVinde.Font = button1.Font;
            btnVinde.ForeColor = Color.White;
            btnVinde.BackColor = Color.DarkOrange;
            btnVinde.UseVisualStyleBackColor = false;
            btnVinde.Cursor = Cursors.Hand;
            btnVinde.Text = "Vanzare";
            btnVinde.Enabled = false;
        }

        private void ActualizeazaStareButonVinde()
        {
            btnVinde.Enabled = (GetProdusSelectat() != null);
        }



    }
}