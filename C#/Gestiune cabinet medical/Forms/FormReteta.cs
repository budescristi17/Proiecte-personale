using System.Text;
using CabinetMedical.Models;
using CabinetMedical.Services;

namespace CabinetMedical.Forms;

public class FormReteta : Form
{
    private readonly MedicService medicService = new();
    private readonly PacientService pacientService = new();
    private readonly RetetaService retetaService = new();
    private int retetaSelectataId;
    private bool seIncarcaGrid;

    private ComboBox cmbMedic = null!;
    private ComboBox cmbPacient = null!;
    private DateTimePicker dtpDataEmiterii = null!;
    private TextBox txtDiagnostic = null!;
    private TextBox txtMedicamente = null!;
    private TextBox txtDozaj = null!;
    private NumericUpDown numDurataTratament = null!;
    private TextBox txtRecomandari = null!;
    private CheckBox chkCompensata = null!;
    private TextBox txtCautareReteta = null!;
    private Button btnAdaugaReteta = null!;
    private Button btnEditeazaReteta = null!;
    private Button btnStergeReteta = null!;
    private Button btnResetareReteta = null!;
    private Button btnCautaReteta = null!;
    private Button btnPrinteazaReteta = null!;
    private DataGridView dgvRetete = null!;

    public FormReteta()
    {
        InitializeComponent();
        IncarcaComboBoxuri();
        IncarcaReteteInGrid(retetaService.GetAllRetete());
    }

    private void InitializeComponent()
    {
        Text = "Gestionare retete";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1240, 810);
        MinimumSize = new Size(1240, 810);
        BackColor = Color.FromArgb(246, 250, 252);
        Font = new Font("Segoe UI", 10F);

        Label lblTitlu = new()
        {
            Text = "Gestionare retete",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 79, 94),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(20, 15),
            Size = new Size(500, 45)
        };

        GroupBox grpDateReteta = CreeazaGroupBox("Date reteta", 20, 70, 540, 450);
        GroupBox grpOperatiuni = CreeazaGroupBox("Operatiuni", 580, 70, 620, 125);
        GroupBox grpCautare = CreeazaGroupBox("Cautare", 580, 210, 620, 95);
        GroupBox grpLista = CreeazaGroupBox("Lista", 20, 535, 1180, 220);

        cmbMedic = new ComboBox
        {
            Name = "cmbMedic",
            Location = new Point(170, 32),
            Size = new Size(335, 28),
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        cmbPacient = new ComboBox
        {
            Name = "cmbPacient",
            Location = new Point(170, 67),
            Size = new Size(335, 28),
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        dtpDataEmiterii = new DateTimePicker
        {
            Name = "dtpDataEmiterii",
            Location = new Point(170, 102),
            Size = new Size(335, 27),
            Format = DateTimePickerFormat.Short,
            Value = DateTime.Today
        };

        txtDiagnostic = CreeazaTextBox("txtDiagnostic", 170, 137, grpDateReteta);

        txtMedicamente = new TextBox
        {
            Name = "txtMedicamente",
            Location = new Point(170, 172),
            Size = new Size(335, 55),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };

        txtDozaj = CreeazaTextBox("txtDozaj", 170, 240, grpDateReteta);

        numDurataTratament = new NumericUpDown
        {
            Name = "numDurataTratament",
            Location = new Point(170, 275),
            Size = new Size(335, 27),
            Minimum = 1,
            Maximum = 365,
            Value = 1
        };

        txtRecomandari = new TextBox
        {
            Name = "txtRecomandari",
            Location = new Point(170, 310),
            Size = new Size(335, 65),
            Multiline = true,
            ScrollBars = ScrollBars.Vertical
        };

        chkCompensata = new CheckBox
        {
            Name = "chkCompensata",
            Text = "Reteta compensata",
            Location = new Point(170, 390),
            Size = new Size(250, 25)
        };

        AdaugaLabel(grpDateReteta, "Medic:", 20, 35);
        AdaugaLabel(grpDateReteta, "Pacient:", 20, 70);
        AdaugaLabel(grpDateReteta, "Data emiterii:", 20, 105);
        AdaugaLabel(grpDateReteta, "Diagnostic:", 20, 140);
        AdaugaLabel(grpDateReteta, "Medicamente:", 20, 175);
        AdaugaLabel(grpDateReteta, "Dozaj:", 20, 243);
        AdaugaLabel(grpDateReteta, "Durata zile:", 20, 278);
        AdaugaLabel(grpDateReteta, "Recomandari:", 20, 313);
        AdaugaLabel(grpDateReteta, "Compensata:", 20, 393);
        grpDateReteta.Controls.Add(cmbMedic);
        grpDateReteta.Controls.Add(cmbPacient);
        grpDateReteta.Controls.Add(dtpDataEmiterii);
        grpDateReteta.Controls.Add(txtMedicamente);
        grpDateReteta.Controls.Add(numDurataTratament);
        grpDateReteta.Controls.Add(txtRecomandari);
        grpDateReteta.Controls.Add(chkCompensata);

        btnAdaugaReteta = CreeazaButon("btnAdaugaReteta", "Adauga reteta", 20, 35, Color.FromArgb(46, 139, 87), grpOperatiuni);
        btnEditeazaReteta = CreeazaButon("btnEditeazaReteta", "Editeaza reteta", 165, 35, Color.FromArgb(0, 123, 167), grpOperatiuni);
        btnStergeReteta = CreeazaButon("btnStergeReteta", "Sterge reteta", 315, 35, Color.FromArgb(190, 69, 69), grpOperatiuni);
        btnResetareReteta = CreeazaButon("btnResetareReteta", "Resetare", 465, 35, Color.FromArgb(110, 110, 110), grpOperatiuni);
        btnPrinteazaReteta = CreeazaButon("btnPrinteazaReteta", "Printeaza", 20, 78, Color.FromArgb(145, 99, 182), grpOperatiuni);

        txtCautareReteta = new TextBox
        {
            Name = "txtCautareReteta",
            Location = new Point(20, 38),
            Size = new Size(450, 27)
        };

        btnCautaReteta = CreeazaButon("btnCautaReteta", "Cauta", 485, 35, Color.FromArgb(218, 132, 36), grpCautare);
        grpCautare.Controls.Add(txtCautareReteta);

        dgvRetete = new DataGridView
        {
            Name = "dgvRetete",
            Location = new Point(15, 30),
            Size = new Size(1150, 170),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };

        StilizeazaGrid(dgvRetete);
        grpLista.Controls.Add(dgvRetete);

        btnAdaugaReteta.Click += btnAdaugaReteta_Click;
        btnEditeazaReteta.Click += btnEditeazaReteta_Click;
        btnStergeReteta.Click += btnStergeReteta_Click;
        btnResetareReteta.Click += btnResetareReteta_Click;
        btnCautaReteta.Click += btnCautaReteta_Click;
        btnPrinteazaReteta.Click += btnPrinteazaReteta_Click;
        dgvRetete.SelectionChanged += dgvRetete_SelectionChanged;

        Controls.Add(lblTitlu);
        Controls.Add(grpDateReteta);
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
            Size = new Size(335, 27)
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
            Size = new Size(145, 23),
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
            Size = new Size(130, 38),
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

    private void IncarcaComboBoxuri()
    {
        cmbMedic.DisplayMember = "Text";
        cmbMedic.ValueMember = "Id";
        cmbMedic.DataSource = medicService.GetAllMedici()
            .Select(m => new ComboItem { Id = m.Id, Text = $"{m.Nume} {m.Prenume} - {m.Specializare}" })
            .ToList();
        cmbMedic.SelectedIndex = -1;

        cmbPacient.DisplayMember = "Text";
        cmbPacient.ValueMember = "Id";
        cmbPacient.DataSource = pacientService.GetAllPacienti()
            .Select(p => new ComboItem { Id = p.Id, Text = $"{p.Nume} {p.Prenume} - CNP: {p.CNP}" })
            .ToList();
        cmbPacient.SelectedIndex = -1;
    }

    private Reteta CitesteRetetaDinForm()
    {
        return new Reteta
        {
            Id = retetaSelectataId,
            MedicId = cmbMedic.SelectedValue is int medicId ? medicId : 0,
            PacientId = cmbPacient.SelectedValue is int pacientId ? pacientId : 0,
            DataEmiterii = dtpDataEmiterii.Value.Date,
            Diagnostic = txtDiagnostic.Text.Trim(),
            Medicamente = txtMedicamente.Text.Trim(),
            Dozaj = txtDozaj.Text.Trim(),
            DurataTratamentZile = (int)numDurataTratament.Value,
            Recomandari = txtRecomandari.Text.Trim(),
            Compensata = chkCompensata.Checked
        };
    }

    private void IncarcaReteteInGrid(IEnumerable<Reteta> retete)
    {
        seIncarcaGrid = true;

        dgvRetete.DataSource = null;
        dgvRetete.DataSource = retete.Select(r => new
        {
            r.Id,
            Data = r.DataEmiterii.ToString("dd.MM.yyyy"),
            Medic = ObtineNumeMedic(r.MedicId),
            Pacient = ObtineNumePacient(r.PacientId),
            r.Diagnostic,
            r.Medicamente,
            r.Dozaj,
            DurataZile = r.DurataTratamentZile,
            Compensata = r.Compensata ? "Da" : "Nu"
        }).ToList();

        dgvRetete.ClearSelection();
        seIncarcaGrid = false;
    }

    private string ObtineNumeMedic(int medicId)
    {
        Medic? medic = medicService.GetAllMedici().FirstOrDefault(m => m.Id == medicId);
        return medic == null ? "Medic sters" : $"{medic.Nume} {medic.Prenume}";
    }

    private string ObtineNumePacient(int pacientId)
    {
        Pacient? pacient = pacientService.GetAllPacienti().FirstOrDefault(p => p.Id == pacientId);
        return pacient == null ? "Pacient sters" : $"{pacient.Nume} {pacient.Prenume}";
    }

    private void ReseteazaCampuri()
    {
        retetaSelectataId = 0;
        cmbMedic.SelectedIndex = -1;
        cmbPacient.SelectedIndex = -1;
        dtpDataEmiterii.Value = DateTime.Today;
        txtDiagnostic.Clear();
        txtMedicamente.Clear();
        txtDozaj.Clear();
        numDurataTratament.Value = 1;
        txtRecomandari.Clear();
        chkCompensata.Checked = false;
        dgvRetete.ClearSelection();
    }

    private Reteta? ObtineRetetaSelectata()
    {
        if (retetaSelectataId == 0)
        {
            return null;
        }

        return retetaService.GetAllRetete().FirstOrDefault(r => r.Id == retetaSelectataId);
    }

    private void btnAdaugaReteta_Click(object? sender, EventArgs e)
    {
        Reteta reteta = CitesteRetetaDinForm();
        string mesaj = retetaService.AdaugaReteta(reteta);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare reteta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Reteta a fost adaugata cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaReteteInGrid(retetaService.GetAllRetete());
        ReseteazaCampuri();
    }

    private void btnEditeazaReteta_Click(object? sender, EventArgs e)
    {
        if (retetaSelectataId == 0)
        {
            MessageBox.Show("Selecteaza o reteta din tabel pentru editare.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Reteta reteta = CitesteRetetaDinForm();
        string mesaj = retetaService.EditeazaReteta(reteta);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare reteta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Reteta a fost editata cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaReteteInGrid(retetaService.GetAllRetete());
        ReseteazaCampuri();
    }

    private void btnStergeReteta_Click(object? sender, EventArgs e)
    {
        if (retetaSelectataId == 0)
        {
            MessageBox.Show("Selecteaza o reteta din tabel pentru stergere.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult rezultat = MessageBox.Show("Sigur vrei sa stergi reteta selectata?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (rezultat != DialogResult.Yes)
        {
            return;
        }

        string mesaj = retetaService.StergeReteta(retetaSelectataId);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Stergere reteta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Reteta a fost stearsa cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaReteteInGrid(retetaService.GetAllRetete());
        ReseteazaCampuri();
    }

    private void btnResetareReteta_Click(object? sender, EventArgs e)
    {
        ReseteazaCampuri();
        txtCautareReteta.Clear();
        IncarcaComboBoxuri();
        IncarcaReteteInGrid(retetaService.GetAllRetete());
    }

    private void btnCautaReteta_Click(object? sender, EventArgs e)
    {
        string text = txtCautareReteta.Text.Trim();
        List<Reteta> rezultat = string.IsNullOrWhiteSpace(text)
            ? retetaService.GetAllRetete()
            : retetaService.CautaRetete(text);

        IncarcaReteteInGrid(rezultat);
    }

    private void btnPrinteazaReteta_Click(object? sender, EventArgs e)
    {
        Reteta? reteta = ObtineRetetaSelectata();

        if (reteta == null)
        {
            MessageBox.Show("Selecteaza o reteta din tabel pentru previzualizare.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        StringBuilder mesaj = new();
        mesaj.AppendLine("PREVIZUALIZARE RETETA");
        mesaj.AppendLine();
        mesaj.AppendLine($"Pacient: {ObtineNumePacient(reteta.PacientId)}");
        mesaj.AppendLine($"Medic: {ObtineNumeMedic(reteta.MedicId)}");
        mesaj.AppendLine($"Diagnostic: {reteta.Diagnostic}");
        mesaj.AppendLine($"Medicamente: {reteta.Medicamente}");
        mesaj.AppendLine($"Dozaj: {reteta.Dozaj}");
        mesaj.AppendLine($"Durata tratament: {reteta.DurataTratamentZile} zile");
        mesaj.AppendLine($"Recomandari: {reteta.Recomandari}");
        mesaj.AppendLine($"Compensata: {(reteta.Compensata ? "Da" : "Nu")}");

        MessageBox.Show(mesaj.ToString(), "Previzualizare reteta", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void dgvRetete_SelectionChanged(object? sender, EventArgs e)
    {
        if (seIncarcaGrid || dgvRetete.CurrentRow == null)
        {
            return;
        }

        object? idCell = dgvRetete.CurrentRow.Cells["Id"].Value;

        if (idCell == null || !int.TryParse(idCell.ToString(), out int id))
        {
            return;
        }

        Reteta? reteta = retetaService.GetAllRetete().FirstOrDefault(r => r.Id == id);

        if (reteta == null)
        {
            return;
        }

        retetaSelectataId = reteta.Id;
        cmbMedic.SelectedValue = reteta.MedicId;
        cmbPacient.SelectedValue = reteta.PacientId;
        dtpDataEmiterii.Value = reteta.DataEmiterii;
        txtDiagnostic.Text = reteta.Diagnostic;
        txtMedicamente.Text = reteta.Medicamente;
        txtDozaj.Text = reteta.Dozaj;
        numDurataTratament.Value = reteta.DurataTratamentZile;
        txtRecomandari.Text = reteta.Recomandari;
        chkCompensata.Checked = reteta.Compensata;
    }

    private sealed class ComboItem
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
