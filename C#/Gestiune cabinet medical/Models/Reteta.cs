namespace CabinetMedical.Models;

public class Reteta
{
    public int Id { get; set; }
    public int MedicId { get; set; }
    public int PacientId { get; set; }
    public DateTime DataEmiterii { get; set; } = DateTime.Today;
    public string Diagnostic { get; set; } = string.Empty;
    public string Medicamente { get; set; } = string.Empty;
    public string Dozaj { get; set; } = string.Empty;
    public int DurataTratamentZile { get; set; }
    public string Recomandari { get; set; } = string.Empty;
    public bool Compensata { get; set; }

    public override string ToString()
    {
        return $"Reteta #{Id} - {Diagnostic} - {DataEmiterii:dd.MM.yyyy}";
    }

    public List<string> ValideazaDateReteta()
    {
        List<string> erori = new();

        if (MedicId <= 0)
        {
            erori.Add("Trebuie selectat un medic.");
        }

        if (PacientId <= 0)
        {
            erori.Add("Trebuie selectat un pacient.");
        }

        if (string.IsNullOrWhiteSpace(Diagnostic))
        {
            erori.Add("Diagnosticul este obligatoriu.");
        }

        if (string.IsNullOrWhiteSpace(Medicamente))
        {
            erori.Add("Campul medicamente este obligatoriu.");
        }

        if (DurataTratamentZile <= 0)
        {
            erori.Add("Durata tratamentului trebuie sa fie mai mare decat 0.");
        }

        return erori;
    }
}
