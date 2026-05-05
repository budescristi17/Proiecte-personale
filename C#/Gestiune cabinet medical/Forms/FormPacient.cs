using CabinetMedical.Models;
using CabinetMedical.Services;

namespace CabinetMedical.Forms;

public class FormPacient : Form
{
    private readonly PacientService pacientService = new();
    private int pacientSelectatId;
    private bool seIncarcaGrid;

    private TextBox txtNume = null!;
    private TextBox txtPrenume = null!;
    private TextBox txtCNP = null!;
    private DateTimePicker dtpDataNasterii = null!;
    private TextBox txtTelefon = null!;
    private TextBox txtAdresa = null!;
    private ComboBox cmbGrupaSanguina = null!;
    private CheckBox chkAsigurat = null!;
    private TextBox txtObservatii = null!;
    private TextBox txtCautarePacient = null!;
    private Button btnAdaugaPacient = null!;
    private Button btnEditeazaPacient = null!;
    private Button btnStergePacient = null!;
    private Button btnResetarePacient = null!;
    private Button btnCautaPacient = null!;
    private DataGridView dgvPacienti = null!;

    public FormPacient()
    {
        InitializeComponent();
        IncarcaPacientiInGrid(pacientService.GetAllPacienti());
    }

    private void InitializeComponent()
    {
        Text = "Gestionare pacienti";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1180, 760);
        MinimumSize = new Size(1180, 760);
        BackColor = Color.FromArgb(246, 250, 252);
        Font = new Font("Segoe UI", 10F);

        Label lblTitlu = new()
        {
            Text = "Gestionare pacienti",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 79, 94),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(20, 15),
            Size = new Size(500, 45)
        };

        GroupBox grpDatePacient = CreeazaGroupBox("Date pacient", 20, 70, 500, 350);
        GroupBox grpOperatiuni = CreeazaGroupBox("Operatiuni", 540, 70, 600, 110);
        GroupBox grpCautare = CreeazaGroupBox("Cautare", 540, 195, 600, 95);
        GroupBox grpLista = CreeazaGroupBox("Lista", 20, 435, 1120, 270);

        txtNume = CreeazaTextBox("txtNume", 165, 32, grpDatePacient);
        txtPrenume = CreeazaTextBox("txtPrenume", 165, 67, grpDatePacient);
        txtCNP = CreeazaTextBox("txtCNP", 165, 102, grpDatePacient);

        dtpDataNasterii = new DateTimePicker
        {
            Name = "dtpDataNasterii",
            Location = new Point(165, 137),
            Size = new Size(300, 27),
            Format = DateTimePickerFormat.Short,
            MaxDate = DateTime.Today,
            Value = new DateTime(2000, 1, 1)
        };

        txtTelefon = CreeazaTextBox("txtTelefon", 165, 172, grpDatePacient);
        txtAdresa = CreeazaTextBox("txtAdresa", 165, 207, grpDatePacient);

        cmbGrupaSanguina = new ComboBox
        {
            Name = "cmbGrupaSanguina",
            Location = new Point(165, 242),
            Size = new Size(300, 27),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        cmbGrupaSanguina.Items.AddRange(new object[] { "A+", "A-", "B+", "B-", "AB+", "AB-", "0+", "0-" });
        cmbGrupaSanguina.SelectedIndex = 0;

        chkAsigurat = new CheckBox
        {
            Name = "chkAsigurat",
            Text = "Pacient asigurat",
            Location = new Point(165, 277),
            Size = new Size(200, 25)
        };

        txtObservatii = new TextBox
        {
            Name = "txtObservatii",
            Location = new Point(165, 307),
            Size = new Size(300, 27)
        };

        AdaugaLabel(grpDatePacient, "Nume:", 20, 35);
        AdaugaLabel(grpDatePacient, "Prenume:", 20, 70);
        AdaugaLabel(grpDatePacient, "CNP:", 20, 105);
        AdaugaLabel(grpDatePacient, "Data nasterii:", 20, 140);
        AdaugaLabel(grpDatePacient, "Telefon:", 20, 175);
        AdaugaLabel(grpDatePacient, "Adresa:", 20, 210);
        AdaugaLabel(grpDatePacient, "Grupa sanguina:", 20, 245);
        AdaugaLabel(grpDatePacient, "Asigurat:", 20, 280);
        AdaugaLabel(grpDatePacient, "Observatii:", 20, 310);
        grpDatePacient.Controls.Add(dtpDataNasterii);
        grpDatePacient.Controls.Add(cmbGrupaSanguina);
        grpDatePacient.Controls.Add(chkAsigurat);
        grpDatePacient.Controls.Add(txtObservatii);

        btnAdaugaPacient = CreeazaButon("btnAdaugaPacient", "Adauga pacient", 20, 35, Color.FromArgb(46, 139, 87), grpOperatiuni);
        btnEditeazaPacient = CreeazaButon("btnEditeazaPacient", "Editeaza pacient", 165, 35, Color.FromArgb(0, 123, 167), grpOperatiuni);
        btnStergePacient = CreeazaButon("btnStergePacient", "Sterge pacient", 315, 35, Color.FromArgb(190, 69, 69), grpOperatiuni);
        btnResetarePacient = CreeazaButon("btnResetarePacient", "Resetare", 460, 35, Color.FromArgb(110, 110, 110), grpOperatiuni);

        txtCautarePacient = new TextBox
        {
            Name = "txtCautarePacient",
            Location = new Point(20, 38),
            Size = new Size(430, 27)
        };

        btnCautaPacient = CreeazaButon("btnCautaPacient", "Cauta", 465, 35, Color.FromArgb(218, 132, 36), grpCautare);
        grpCautare.Controls.Add(txtCautarePacient);

        dgvPacienti = new DataGridView
        {
            Name = "dgvPacienti",
            Location = new Point(15, 30),
            Size = new Size(1090, 220),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };

        StilizeazaGrid(dgvPacienti);
        grpLista.Controls.Add(dgvPacienti);

        btnAdaugaPacient.Click += btnAdaugaPacient_Click;
        btnEditeazaPacient.Click += btnEditeazaPacient_Click;
        btnStergePacient.Click += btnStergePacient_Click;
        btnResetarePacient.Click += btnResetarePacient_Click;
        btnCautaPacient.Click += btnCautaPacient_Click;
        dgvPacienti.SelectionChanged += dgvPacienti_SelectionChanged;

        Controls.Add(lblTitlu);
        Controls.Add(grpDatePacient);
        Controls.Add(grpOperatiuni);
        Controls.Add(grpCautare);
        Controls.Add(grpLista);
    }

    private GroupBox CreeazaGroupBox(string text, int x, int y, int width, int height)
    {
        return new GroupBox
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(width, height),
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            ForeColor = Color.FromArgb(48, 70, 82)
        };
    }

    private TextBox CreeazaTextBox(string name, int x, int y, Control parent)
    {
        TextBox textBox = new()
        {
            Name = name,
            Location = new Point(x, y),
            Size = new Size(300, 27)
        };

        parent.Controls.Add(textBox);
        return textBox;
    }

    private void AdaugaLabel(Control parent, string text, int x, int y)
    {
        parent.Controls.Add(new Label
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(140, 23),
            Font = new Font("Segoe UI", 10F)
        });
    }

    private Button CreeazaButon(string name, string text, int x, int y, Color culoare, Control parent)
    {
        Button button = new()
        {
            Name = name,
            Text = text,
            Location = new Point(x, y),
            Size = new Size(125, 38),
            BackColor = culoare,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };

        parent.Controls.Add(button);
        return button;
    }

    private void StilizeazaGrid(DataGridView grid)
    {
        grid.EnableHeadersVisualStyles = false;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 79, 94);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        grid.RowHeadersVisible = false;
        grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(235, 244, 247);
    }

    private Pacient CitestePacientDinForm()
    {
        return new Pacient
        {
            Id = pacientSelectatId,
            Nume = txtNume.Text.Trim(),
            Prenume = txtPrenume.Text.Trim(),
            CNP = txtCNP.Text.Trim(),
            DataNasterii = dtpDataNasterii.Value.Date,
            Telefon = txtTelefon.Text.Trim(),
            Adresa = txtAdresa.Text.Trim(),
            GrupaSanguina = cmbGrupaSanguina.Text,
            Asigurat = chkAsigurat.Checked,
            Observatii = txtObservatii.Text.Trim()
        };
    }

    private void IncarcaPacientiInGrid(IEnumerable<Pacient> pacienti)
    {
        seIncarcaGrid = true;

        dgvPacienti.DataSource = null;
        dgvPacienti.DataSource = pacienti.Select(p => new
        {
            p.Id,
            p.Nume,
            p.Prenume,
            p.CNP,
            DataNasterii = p.DataNasterii.ToString("dd.MM.yyyy"),
            Varsta = p.CalculVarsta(),
            p.Telefon,
            p.Adresa,
            GrupaSanguina = p.GrupaSanguina,
            Asigurat = p.Asigurat ? "Da" : "Nu",
            p.Observatii
        }).ToList();

        dgvPacienti.ClearSelection();
        seIncarcaGrid = false;
    }

    private void ReseteazaCampuri()
    {
        pacientSelectatId = 0;
        txtNume.Clear();
        txtPrenume.Clear();
        txtCNP.Clear();
        dtpDataNasterii.Value = new DateTime(2000, 1, 1);
        txtTelefon.Clear();
        txtAdresa.Clear();
        cmbGrupaSanguina.SelectedIndex = 0;
        chkAsigurat.Checked = false;
        txtObservatii.Clear();
        dgvPacienti.ClearSelection();
    }

    private void btnAdaugaPacient_Click(object? sender, EventArgs e)
    {
        Pacient pacient = CitestePacientDinForm();
        string mesaj = pacientService.AdaugaPacient(pacient);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare pacient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Pacientul a fost adaugat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaPacientiInGrid(pacientService.GetAllPacienti());
        ReseteazaCampuri();
    }

    private void btnEditeazaPacient_Click(object? sender, EventArgs e)
    {
        if (pacientSelectatId == 0)
        {
            MessageBox.Show("Selecteaza un pacient din tabel pentru editare.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Pacient pacient = CitestePacientDinForm();
        string mesaj = pacientService.EditeazaPacient(pacient);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare pacient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Pacientul a fost editat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaPacientiInGrid(pacientService.GetAllPacienti());
        ReseteazaCampuri();
    }

    private void btnStergePacient_Click(object? sender, EventArgs e)
    {
        if (pacientSelectatId == 0)
        {
            MessageBox.Show("Selecteaza un pacient din tabel pentru stergere.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult rezultat = MessageBox.Show("Sigur vrei sa stergi pacientul selectat?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (rezultat != DialogResult.Yes)
        {
            return;
        }

        string mesaj = pacientService.StergePacient(pacientSelectatId);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Stergere pacient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Pacientul a fost sters cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaPacientiInGrid(pacientService.GetAllPacienti());
        ReseteazaCampuri();
    }

    private void btnResetarePacient_Click(object? sender, EventArgs e)
    {
        ReseteazaCampuri();
        txtCautarePacient.Clear();
        IncarcaPacientiInGrid(pacientService.GetAllPacienti());
    }

    private void btnCautaPacient_Click(object? sender, EventArgs e)
    {
        string text = txtCautarePacient.Text.Trim();
        List<Pacient> rezultat = string.IsNullOrWhiteSpace(text)
            ? pacientService.GetAllPacienti()
            : pacientService.CautaPacientDupaNumeSauCNP(text);

        IncarcaPacientiInGrid(rezultat);
    }

    private void dgvPacienti_SelectionChanged(object? sender, EventArgs e)
    {
        if (seIncarcaGrid || dgvPacienti.CurrentRow == null)
        {
            return;
        }

        object? idCell = dgvPacienti.CurrentRow.Cells["Id"].Value;

        if (idCell == null || !int.TryParse(idCell.ToString(), out int id))
        {
            return;
        }

        Pacient? pacient = pacientService.GetAllPacienti().FirstOrDefault(p => p.Id == id);

        if (pacient == null)
        {
            return;
        }

        pacientSelectatId = pacient.Id;
        txtNume.Text = pacient.Nume;
        txtPrenume.Text = pacient.Prenume;
        txtCNP.Text = pacient.CNP;
        dtpDataNasterii.Value = pacient.DataNasterii;
        txtTelefon.Text = pacient.Telefon;
        txtAdresa.Text = pacient.Adresa;
        cmbGrupaSanguina.Text = pacient.GrupaSanguina;
        chkAsigurat.Checked = pacient.Asigurat;
        txtObservatii.Text = pacient.Observatii;
    }
}
