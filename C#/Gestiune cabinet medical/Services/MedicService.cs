using CabinetMedical.Models;

namespace CabinetMedical.Services;

public class MedicService
{
    public string AdaugaMedic(Medic medic)
    {
        List<string> erori = medic.ValideazaDateMedic();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        medic.Id = GenereazaIdNou();
        DataStorage.Medici.Add(medic);
        return string.Empty;
    }

    public string EditeazaMedic(Medic medicActualizat)
    {
        List<string> erori = medicActualizat.ValideazaDateMedic();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        Medic? medicExistent = DataStorage.Medici.FirstOrDefault(m => m.Id == medicActualizat.Id);

        if (medicExistent == null)
        {
            return "Medicul selectat nu a fost gasit.";
        }

        medicExistent.Nume = medicActualizat.Nume;
        medicExistent.Prenume = medicActualizat.Prenume;
        medicExistent.Specializare = medicActualizat.Specializare;
        medicExistent.Telefon = medicActualizat.Telefon;
        medicExistent.Email = medicActualizat.Email;
        medicExistent.AniExperienta = medicActualizat.AniExperienta;
        medicExistent.ProgramLucru = medicActualizat.ProgramLucru;

        return string.Empty;
    }

    public string StergeMedic(int id)
    {
        Medic? medic = DataStorage.Medici.FirstOrDefault(m => m.Id == id);

        if (medic == null)
        {
            return "Medicul selectat nu a fost gasit.";
        }

        bool areRetete = DataStorage.Retete.Any(r => r.MedicId == id);

        if (areRetete)
        {
            return "Medicul nu poate fi sters deoarece are retete asociate.";
        }

        DataStorage.Medici.Remove(medic);
        return string.Empty;
    }

    public List<Medic> CautaMedicDupaNume(string textCautare)
    {
        string text = textCautare.Trim().ToLower();

        return DataStorage.Medici
            .Where(m =>
                m.Nume.ToLower().Contains(text) ||
                m.Prenume.ToLower().Contains(text) ||
                m.Specializare.ToLower().Contains(text))
            .OrderBy(m => m.Nume)
            .ThenBy(m => m.Prenume)
            .ToList();
    }

    public List<Medic> GetAllMedici()
    {
        return DataStorage.Medici
            .OrderBy(m => m.Nume)
            .ThenBy(m => m.Prenume)
            .ToList();
    }

    public int GenereazaIdNou()
    {
        return DataStorage.Medici.Count == 0 ? 1 : DataStorage.Medici.Max(m => m.Id) + 1;
    }
}
