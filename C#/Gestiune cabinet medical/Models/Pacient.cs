namespace CabinetMedical.Models;

public class Pacient
{
    public int Id { get; set; }
    public string Nume { get; set; } = string.Empty;
    public string Prenume { get; set; } = string.Empty;
    public string CNP { get; set; } = string.Empty;
    public DateTime DataNasterii { get; set; } = DateTime.Today;
    public string Telefon { get; set; } = string.Empty;
    public string Adresa { get; set; } = string.Empty;
    public string GrupaSanguina { get; set; } = string.Empty;
    public bool Asigurat { get; set; }
    public string Observatii { get; set; } = string.Empty;

    public int CalculVarsta()
    {
        int varsta = DateTime.Today.Year - DataNasterii.Year;

        if (DataNasterii.Date > DateTime.Today.AddYears(-varsta))
        {
            varsta--;
        }

        return varsta;
    }

    public override string ToString()
    {
        return $"{Nume} {Prenume} - CNP: {CNP}";
    }

    public List<string> ValideazaDatePacient()
    {
        List<string> erori = new();

        if (string.IsNullOrWhiteSpace(Nume))
        {
            erori.Add("Numele pacientului este obligatoriu.");
        }

        if (string.IsNullOrWhiteSpace(Prenume))
        {
            erori.Add("Prenumele pacientului este obligatoriu.");
        }

        if (string.IsNullOrWhiteSpace(CNP))
        {
            erori.Add("CNP-ul pacientului este obligatoriu.");
        }
        else if (CNP.Length != 13)
        {
            erori.Add("CNP-ul trebuie sa aiba exact 13 caractere.");
        }

        if (string.IsNullOrWhiteSpace(Telefon))
        {
            erori.Add("Telefonul pacientului este obligatoriu.");
        }

        if (DataNasterii.Date > DateTime.Today)
        {
            erori.Add("Data nasterii nu poate fi in viitor.");
        }

        return erori;
    }
}
