using CabinetMedical.Models;
using CabinetMedical.Services;

namespace CabinetMedical.Forms;

public class FormRaport : Form
{
    private readonly MedicService medicService = new();
    private readonly PacientService pacientService = new();
    private readonly RetetaService retetaService = new();

    private Label lblTotalMedici = null!;
    private Label lblTotalPacienti = null!;
    private Label lblTotalRetete = null!;
    private Label lblPacientiAsigurati = null!;
    private Label lblReteteCompensate = null!;
    private Button btnActualizeazaRaport = null!;
    private DataGridView dgvRaportRetete = null!;
    private ComboBox cmbFiltruRaport = null!;
    private Button btnFiltreazaRaport = null!;

    public FormRaport()
    {
        InitializeComponent();
        ActualizeazaRaport();
    }

    private void InitializeComponent()
    {
        Text = "Rapoarte cabinet medical";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(1120, 690);
        MinimumSize = new Size(1120, 690);
        BackColor = Color.FromArgb(246, 250, 252);
        Font = new Font("Segoe UI", 10F);

        Label lblTitlu = new()
        {
            Text = "Rapoarte si statistici",
            Font = new Font("Segoe UI", 20F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 79, 94),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new Point(20, 15),
            Size = new Size(500, 45)
        };

        GroupBox grpStatistici = CreeazaGroupBox("Statistici", 20, 70, 360, 250);
        GroupBox grpFiltrare = CreeazaGroupBox("Filtrare", 400, 70, 680, 120);
        GroupBox grpLista = CreeazaGroupBox("Lista", 20, 335, 1060, 285);

        lblTotalMedici = CreeazaLabelStatistica("lblTotalMedici", "Total medici: 0", 25, 35, grpStatistici);
        lblTotalPacienti = CreeazaLabelStatistica("lblTotalPacienti", "Total pacienti: 0", 25, 75, grpStatistici);
        lblTotalRetete = CreeazaLabelStatistica("lblTotalRetete", "Total retete: 0", 25, 115, grpStatistici);
        lblPacientiAsigurati = CreeazaLabelStatistica("lblPacientiAsigurati", "Pacienti asigurati: 0", 25, 155, grpStatistici);
        lblReteteCompensate = CreeazaLabelStatistica("lblReteteCompensate", "Retete compensate: 0", 25, 195, grpStatistici);

        btnActualizeazaRaport = new Button
        {
            Name = "btnActualizeazaRaport",
            Text = "Actualizeaza raport",
            Location = new Point(400, 215),
            Size = new Size(230, 45),
            BackColor = Color.FromArgb(0, 123, 167),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold)
        };

        cmbFiltruRaport = new ComboBox
        {
            Name = "cmbFiltruRaport",
            Location = new Point(25, 45),
            Size = new Size(430, 28),
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        cmbFiltruRaport.Items.AddRange(new object[]
        {
            "Toate retetele",
            "Retete compensate",
            "Retete necompensate",
            "Pacienti asigurati",
            "Pacienti neasigurati"
        });
        cmbFiltruRaport.SelectedIndex = 0;

        btnFiltreazaRaport = new Button
        {
            Name = "btnFiltreazaRaport",
            Text = "Filtreaza raport",
            Location = new Point(475, 40),
            Size = new Size(170, 38),
            BackColor = Color.FromArgb(218, 132, 36),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 9F, FontStyle.Bold)
        };

        grpFiltrare.Controls.Add(cmbFiltruRaport);
        grpFiltrare.Controls.Add(btnFiltreazaRaport);

        dgvRaportRetete = new DataGridView
        {
            Name = "dgvRaportRetete",
            Location = new Point(15, 30),
            Size = new Size(1030, 235),
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            BackgroundColor = Color.White,
            BorderStyle = BorderStyle.None
        };

        StilizeazaGrid(dgvRaportRetete);
        grpLista.Controls.Add(dgvRaportRetete);

        btnActualizeazaRaport.Click += btnActualizeazaRaport_Click;
        btnFiltreazaRaport.Click += btnFiltreazaRaport_Click;

        Controls.Add(lblTitlu);
        Controls.Add(grpStatistici);
        Controls.Add(grpFiltrare);
        Controls.Add(btnActualizeazaRaport);
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

    private Label CreeazaLabelStatistica(string name, string text, int x, int y, Control parent)
    {
        Label label = new()
        {
            Name = name,
            Text = text,
            Location = new Point(x, y),
            Size = new Size(300, 28),
            Font = new Font("Segoe UI", 11F, FontStyle.Bold),
            ForeColor = Color.FromArgb(48, 70, 82)
        };

        parent.Controls.Add(label);
        return label;
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

    private void ActualizeazaRaport()
    {
        List<Medic> medici = medicService.GetAllMedici();
        List<Pacient> pacienti = pacientService.GetAllPacienti();
        List<Reteta> retete = retetaService.GetAllRetete();

        lblTotalMedici.Text = $"Total medici: {medici.Count}";
        lblTotalPacienti.Text = $"Total pacienti: {pacienti.Count}";
        lblTotalRetete.Text = $"Total retete: {retete.Count}";
        lblPacientiAsigurati.Text = $"Pacienti asigurati: {pacienti.Count(p => p.Asigurat)}";
        lblReteteCompensate.Text = $"Retete compensate: {retete.Count(r => r.Compensata)}";

        IncarcaReteteInGrid(retete);
    }

    private void IncarcaReteteInGrid(IEnumerable<Reteta> retete)
    {
        dgvRaportRetete.DataSource = null;
        dgvRaportRetete.DataSource = retete.Select(r =>
        {
            Medic? medic = medicService.GetAllMedici().FirstOrDefault(m => m.Id == r.MedicId);
            Pacient? pacient = pacientService.GetAllPacienti().FirstOrDefault(p => p.Id == r.PacientId);

            return new
            {
                r.Id,
                Data = r.DataEmiterii.ToString("dd.MM.yyyy"),
                Medic = medic == null ? "Medic sters" : $"{medic.Nume} {medic.Prenume}",
                Pacient = pacient == null ? "Pacient sters" : $"{pacient.Nume} {pacient.Prenume}",
                Asigurat = pacient != null && pacient.Asigurat ? "Da" : "Nu",
                r.Diagnostic,
                r.Medicamente,
                DurataZile = r.DurataTratamentZile,
                Compensata = r.Compensata ? "Da" : "Nu"
            };
        }).ToList();

        dgvRaportRetete.ClearSelection();
    }

    private List<Reteta> AplicaFiltru()
    {
        string filtru = cmbFiltruRaport.Text;
        List<Reteta> retete = retetaService.GetAllRetete();

        if (filtru == "Retete compensate")
        {
            return retete.Where(r => r.Compensata).ToList();
        }

        if (filtru == "Retete necompensate")
        {
            return retete.Where(r => !r.Compensata).ToList();
        }

        if (filtru == "Pacienti asigurati")
        {
            return retete.Where(r =>
            {
                Pacient? pacient = pacientService.GetAllPacienti().FirstOrDefault(p => p.Id == r.PacientId);
                return pacient != null && pacient.Asigurat;
            }).ToList();
        }

        if (filtru == "Pacienti neasigurati")
        {
            return retete.Where(r =>
            {
                Pacient? pacient = pacientService.GetAllPacienti().FirstOrDefault(p => p.Id == r.PacientId);
                return pacient != null && !pacient.Asigurat;
            }).ToList();
        }

        return retete;
    }

    private void btnActualizeazaRaport_Click(object? sender, EventArgs e)
    {
        ActualizeazaRaport();
    }

    private void btnFiltreazaRaport_Click(object? sender, EventArgs e)
    {
        IncarcaReteteInGrid(AplicaFiltru());
    }
}
