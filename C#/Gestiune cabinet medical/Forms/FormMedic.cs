using CabinetMedical.Models;
using CabinetMedical.Services;

namespace CabinetMedical.Forms;

public class FormMedic : Form
{
    private readonly MedicService medicService = new();
    private int medicSelectatId;
    private bool seIncarcaGrid;

    private TextBox txtNume = null!;
    private TextBox txtPrenume = null!;
    private TextBox txtSpecializare = null!;
    private TextBox txtTelefon = null!;
    private TextBox txtEmail = null!;
    private NumericUpDown numAniExperienta = null!;
    private TextBox txtProgramLucru = null!;
    private TextBox txtCautareMedic = null!;
    private Button btnAdaugaMedic = null!;
    private Button btnEditeazaMedic = null!;
    private Button btnStergeMedic = null!;
    private Button btnResetareMedic = null!;
    private Button btnCautaMedic = null!;
    private DataGridView dgvMedici = null!;

    public FormMedic()
    {
        InitializeComponent();
        IncarcaMediciInGrid(medicService.GetAllMedici());
    }

    private void InitializeComponent()
    {
        Text = "Gestionare medici";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1120, 720);
        MinimumSize = new Size(1120, 720);
        BackColor = Color.FromArgb(246, 250, 252);
        Font = new Font("Segoe UI", 10F);

        Label lblTitlu = new()
        {
            Text = "Gestionare medici",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 79, 94),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(20, 15),
            Size = new Size(500, 45)
        };

        GroupBox grpDateMedic = CreeazaGroupBox("Date medic", 20, 70, 460, 300);
        GroupBox grpOperatiuni = CreeazaGroupBox("Operatiuni", 500, 70, 580, 110);
        GroupBox grpCautare = CreeazaGroupBox("Cautare", 500, 195, 580, 95);
        GroupBox grpLista = CreeazaGroupBox("Lista", 20, 385, 1060, 280);

        txtNume = CreeazaTextBox("txtNume", 150, 32, grpDateMedic);
        txtPrenume = CreeazaTextBox("txtPrenume", 150, 67, grpDateMedic);
        txtSpecializare = CreeazaTextBox("txtSpecializare", 150, 102, grpDateMedic);
        txtTelefon = CreeazaTextBox("txtTelefon", 150, 137, grpDateMedic);
        txtEmail = CreeazaTextBox("txtEmail", 150, 172, grpDateMedic);

        numAniExperienta = new NumericUpDown
        {
            Name = "numAniExperienta",
            Location = new Point(150, 207),
            Size = new Size(280, 27),
            Minimum = 0,
            Maximum = 60
        };

        txtProgramLucru = CreeazaTextBox("txtProgramLucru", 150, 242, grpDateMedic);

        AdaugaLabel(grpDateMedic, "Nume:", 20, 35);
        AdaugaLabel(grpDateMedic, "Prenume:", 20, 70);
        AdaugaLabel(grpDateMedic, "Specializare:", 20, 105);
        AdaugaLabel(grpDateMedic, "Telefon:", 20, 140);
        AdaugaLabel(grpDateMedic, "Email:", 20, 175);
        AdaugaLabel(grpDateMedic, "Ani experienta:", 20, 210);
        AdaugaLabel(grpDateMedic, "Program lucru:", 20, 245);
        grpDateMedic.Controls.Add(numAniExperienta);

        btnAdaugaMedic = CreeazaButon("btnAdaugaMedic", "Adauga medic", 20, 35, Color.FromArgb(46, 139, 87), grpOperatiuni);
        btnEditeazaMedic = CreeazaButon("btnEditeazaMedic", "Editeaza medic", 160, 35, Color.FromArgb(0, 123, 167), grpOperatiuni);
        btnStergeMedic = CreeazaButon("btnStergeMedic", "Sterge medic", 305, 35, Color.FromArgb(190, 69, 69), grpOperatiuni);
        btnResetareMedic = CreeazaButon("btnResetareMedic", "Resetare", 445, 35, Color.FromArgb(110, 110, 110), grpOperatiuni);

        txtCautareMedic = new TextBox
        {
            Name = "txtCautareMedic",
            Location = new Point(20, 38),
            Size = new Size(410, 27)
        };

        btnCautaMedic = CreeazaButon("btnCautaMedic", "Cauta", 445, 35, Color.FromArgb(218, 132, 36), grpCautare);
        grpCautare.Controls.Add(txtCautareMedic);

        dgvMedici = new DataGridView
        {
            Name = "dgvMedici",
            Location = new Point(15, 30),
            Size = new Size(1030, 230),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };

        StilizeazaGrid(dgvMedici);
        grpLista.Controls.Add(dgvMedici);

        btnAdaugaMedic.Click += btnAdaugaMedic_Click;
        btnEditeazaMedic.Click += btnEditeazaMedic_Click;
        btnStergeMedic.Click += btnStergeMedic_Click;
        btnResetareMedic.Click += btnResetareMedic_Click;
        btnCautaMedic.Click += btnCautaMedic_Click;
        dgvMedici.SelectionChanged += dgvMedici_SelectionChanged;

        Controls.Add(lblTitlu);
        Controls.Add(grpDateMedic);
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
            Size = new Size(280, 27)
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
            Size = new Size(120, 23),
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
            Size = new Size(120, 38),
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

    private Medic CitesteMedicDinForm()
    {
        return new Medic
        {
            Id = medicSelectatId,
            Nume = txtNume.Text.Trim(),
            Prenume = txtPrenume.Text.Trim(),
            Specializare = txtSpecializare.Text.Trim(),
            Telefon = txtTelefon.Text.Trim(),
            Email = txtEmail.Text.Trim(),
            AniExperienta = (int)numAniExperienta.Value,
            ProgramLucru = txtProgramLucru.Text.Trim()
        };
    }

    private void IncarcaMediciInGrid(IEnumerable<Medic> medici)
    {
        seIncarcaGrid = true;

        dgvMedici.DataSource = null;
        dgvMedici.DataSource = medici.Select(m => new
        {
            m.Id,
            m.Nume,
            m.Prenume,
            m.Specializare,
            m.Telefon,
            m.Email,
            AniExperienta = m.AniExperienta,
            ProgramLucru = m.ProgramLucru
        }).ToList();

        dgvMedici.ClearSelection();
        seIncarcaGrid = false;
    }

    private void ReseteazaCampuri()
    {
        medicSelectatId = 0;
        txtNume.Clear();
        txtPrenume.Clear();
        txtSpecializare.Clear();
        txtTelefon.Clear();
        txtEmail.Clear();
        numAniExperienta.Value = 0;
        txtProgramLucru.Clear();
        dgvMedici.ClearSelection();
    }

    private void btnAdaugaMedic_Click(object? sender, EventArgs e)
    {
        Medic medic = CitesteMedicDinForm();
        string mesaj = medicService.AdaugaMedic(medic);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare medic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Medicul a fost adaugat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaMediciInGrid(medicService.GetAllMedici());
        ReseteazaCampuri();
    }

    private void btnEditeazaMedic_Click(object? sender, EventArgs e)
    {
        if (medicSelectatId == 0)
        {
            MessageBox.Show("Selecteaza un medic din tabel pentru editare.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Medic medic = CitesteMedicDinForm();
        string mesaj = medicService.EditeazaMedic(medic);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Validare medic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Medicul a fost editat cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaMediciInGrid(medicService.GetAllMedici());
        ReseteazaCampuri();
    }

    private void btnStergeMedic_Click(object? sender, EventArgs e)
    {
        if (medicSelectatId == 0)
        {
            MessageBox.Show("Selecteaza un medic din tabel pentru stergere.", "Atentie", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        DialogResult rezultat = MessageBox.Show("Sigur vrei sa stergi medicul selectat?", "Confirmare stergere", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        if (rezultat != DialogResult.Yes)
        {
            return;
        }

        string mesaj = medicService.StergeMedic(medicSelectatId);

        if (!string.IsNullOrWhiteSpace(mesaj))
        {
            MessageBox.Show(mesaj, "Stergere medic", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        MessageBox.Show("Medicul a fost sters cu succes.", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        IncarcaMediciInGrid(medicService.GetAllMedici());
        ReseteazaCampuri();
    }

    private void btnResetareMedic_Click(object? sender, EventArgs e)
    {
        ReseteazaCampuri();
        txtCautareMedic.Clear();
        IncarcaMediciInGrid(medicService.GetAllMedici());
    }

    private void btnCautaMedic_Click(object? sender, EventArgs e)
    {
        string text = txtCautareMedic.Text.Trim();
        List<Medic> rezultat = string.IsNullOrWhiteSpace(text)
            ? medicService.GetAllMedici()
            : medicService.CautaMedicDupaNume(text);

        IncarcaMediciInGrid(rezultat);
    }

    private void dgvMedici_SelectionChanged(object? sender, EventArgs e)
    {
        if (seIncarcaGrid || dgvMedici.CurrentRow == null)
        {
            return;
        }

        object? idCell = dgvMedici.CurrentRow.Cells["Id"].Value;

        if (idCell == null || !int.TryParse(idCell.ToString(), out int id))
        {
            return;
        }

        Medic? medic = medicService.GetAllMedici().FirstOrDefault(m => m.Id == id);

        if (medic == null)
        {
            return;
        }

        medicSelectatId = medic.Id;
        txtNume.Text = medic.Nume;
        txtPrenume.Text = medic.Prenume;
        txtSpecializare.Text = medic.Specializare;
        txtTelefon.Text = medic.Telefon;
        txtEmail.Text = medic.Email;
        numAniExperienta.Value = medic.AniExperienta;
        txtProgramLucru.Text = medic.ProgramLucru;
    }
}
