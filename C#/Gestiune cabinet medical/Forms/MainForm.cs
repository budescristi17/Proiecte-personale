using CabinetMedical.Services;

namespace CabinetMedical.Forms;

public class MainForm : Form
{
    private Label lblTitlu = null!;
    private Button btnMedici = null!;
    private Button btnPacienti = null!;
    private Button btnRetete = null!;
    private Button btnRaport = null!;
    private Button btnSalvare = null!;
    private Button btnIesire = null!;

    public MainForm()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        Text = "Cabinet Medical - Gestiune";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(620, 520);
        MinimumSize = new Size(620, 520);
        BackColor = Color.FromArgb(238, 246, 250);
        Font = new Font("Segoe UI", 10F);

        lblTitlu = new Label
        {
            Name = "lblTitlu",
            Text = "Cabinet Medical - Gestiune",
            Font = new Font("Segoe UI", 22F, FontStyle.Bold),
            ForeColor = Color.FromArgb(22, 79, 94),
            AutoSize = false,
            TextAlign = ContentAlignment.MiddleCenter,
            Location = new Point(20, 25),
            Size = new Size(560, 55)
        };

        btnMedici = CreeazaButon("btnMedici", "Gestionare medici", 110, 110, Color.FromArgb(0, 123, 167));
        btnPacienti = CreeazaButon("btnPacienti", "Gestionare pacienti", 110, 170, Color.FromArgb(46, 139, 87));
        btnRetete = CreeazaButon("btnRetete", "Gestionare retete", 110, 230, Color.FromArgb(145, 99, 182));
        btnRaport = CreeazaButon("btnRaport", "Rapoarte", 110, 290, Color.FromArgb(218, 132, 36));
        btnSalvare = CreeazaButon("btnSalvare", "Salvare date", 110, 350, Color.FromArgb(60, 100, 160));
        btnIesire = CreeazaButon("btnIesire", "Iesire", 110, 410, Color.FromArgb(190, 69, 69));

        // Legarea butoanelor de evenimentele Click.
        btnMedici.Click += btnMedici_Click;
        btnPacienti.Click += btnPacienti_Click;
        btnRetete.Click += btnRetete_Click;
        btnRaport.Click += btnRaport_Click;
        btnSalvare.Click += btnSalvare_Click;
        btnIesire.Click += btnIesire_Click;

        Controls.Add(lblTitlu);
        Controls.Add(btnMedici);
        Controls.Add(btnPacienti);
        Controls.Add(btnRetete);
        Controls.Add(btnRaport);
        Controls.Add(btnSalvare);
        Controls.Add(btnIesire);
    }

    private Button CreeazaButon(string name, string text, int x, int y, Color culoare)
    {
        return new Button
        {
            Name = name,
            Text = text,
            Location = new Point(x, y),
            Size = new Size(390, 45),
            BackColor = culoare,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold),
            Cursor = Cursors.Hand
        };
    }

    private void btnMedici_Click(object? sender, EventArgs e)
    {
        using FormMedic formMedic = new();
        formMedic.ShowDialog(this);
    }

    private void btnPacienti_Click(object? sender, EventArgs e)
    {
        using FormPacient formPacient = new();
        formPacient.ShowDialog(this);
    }

    private void btnRetete_Click(object? sender, EventArgs e)
    {
        using FormReteta formReteta = new();
        formReteta.ShowDialog(this);
    }

    private void btnRaport_Click(object? sender, EventArgs e)
    {
        using FormRaport formRaport = new();
        formRaport.ShowDialog(this);
    }

    private void btnSalvare_Click(object? sender, EventArgs e)
    {
        DataStorage.SaveAll();
        MessageBox.Show("Datele au fost salvate in fisierele JSON.", "Salvare reusita", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnIesire_Click(object? sender, EventArgs e)
    {
        Close();
    }
}
