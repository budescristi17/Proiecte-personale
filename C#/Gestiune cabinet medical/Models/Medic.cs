namespace CabinetMedical.Models;

public class Medic
{
    public int Id { get; set; }
    public string Nume { get; set; } = string.Empty;
    public string Prenume { get; set; } = string.Empty;
    public string Specializare { get; set; } = string.Empty;
    public string Telefon { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int AniExperienta { get; set; }
    public string ProgramLucru { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Nume} {Prenume} - {Specializare}";
    }

    public List<string> ValideazaDateMedic()
    {
        List<string> erori = new();

        if (string.IsNullOrWhiteSpace(Nume))
        {
            erori.Add("Numele medicului este obligatoriu.");
        }

        if (string.IsNullOrWhiteSpace(Prenume))
        {
            erori.Add("Prenumele medicului este obligatoriu.");
        }

        if (string.IsNullOrWhiteSpace(Telefon))
        {
            erori.Add("Telefonul medicului este obligatoriu.");
        }

        if (!string.IsNullOrWhiteSpace(Email) && !Email.Contains('@'))
        {
            erori.Add("Emailul medicului trebuie sa contina caracterul @.");
        }

        if (AniExperienta < 0)
        {
            erori.Add("Anii de experienta trebuie sa fie mai mari sau egali cu 0.");
        }

        return erori;
    }
}
