using CabinetMedical.Models;

namespace CabinetMedical.Services;

public class PacientService
{
    public string AdaugaPacient(Pacient pacient)
    {
        List<string> erori = pacient.ValideazaDatePacient();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        bool cnpExista = DataStorage.Pacienti.Any(p => p.CNP == pacient.CNP);

        if (cnpExista)
        {
            return "Exista deja un pacient cu acest CNP.";
        }

        pacient.Id = GenereazaIdNou();
        DataStorage.Pacienti.Add(pacient);
        return string.Empty;
    }

    public string EditeazaPacient(Pacient pacientActualizat)
    {
        List<string> erori = pacientActualizat.ValideazaDatePacient();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        bool cnpFolositDeAltPacient = DataStorage.Pacienti.Any(p => p.CNP == pacientActualizat.CNP && p.Id != pacientActualizat.Id);

        if (cnpFolositDeAltPacient)
        {
            return "CNP-ul este deja folosit de alt pacient.";
        }

        Pacient? pacientExistent = DataStorage.Pacienti.FirstOrDefault(p => p.Id == pacientActualizat.Id);

        if (pacientExistent == null)
        {
            return "Pacientul selectat nu a fost gasit.";
        }

        pacientExistent.Nume = pacientActualizat.Nume;
        pacientExistent.Prenume = pacientActualizat.Prenume;
        pacientExistent.CNP = pacientActualizat.CNP;
        pacientExistent.DataNasterii = pacientActualizat.DataNasterii;
        pacientExistent.Telefon = pacientActualizat.Telefon;
        pacientExistent.Adresa = pacientActualizat.Adresa;
        pacientExistent.GrupaSanguina = pacientActualizat.GrupaSanguina;
        pacientExistent.Asigurat = pacientActualizat.Asigurat;
        pacientExistent.Observatii = pacientActualizat.Observatii;

        return string.Empty;
    }

    public string StergePacient(int id)
    {
        Pacient? pacient = DataStorage.Pacienti.FirstOrDefault(p => p.Id == id);

        if (pacient == null)
        {
            return "Pacientul selectat nu a fost gasit.";
        }

        bool areRetete = DataStorage.Retete.Any(r => r.PacientId == id);

        if (areRetete)
        {
            return "Pacientul nu poate fi sters deoarece are retete asociate.";
        }

        DataStorage.Pacienti.Remove(pacient);
        return string.Empty;
    }

    public List<Pacient> CautaPacientDupaNumeSauCNP(string textCautare)
    {
        string text = textCautare.Trim().ToLower();

        return DataStorage.Pacienti
            .Where(p =>
                p.Nume.ToLower().Contains(text) ||
                p.Prenume.ToLower().Contains(text) ||
                p.CNP.ToLower().Contains(text))
            .OrderBy(p => p.Nume)
            .ThenBy(p => p.Prenume)
            .ToList();
    }

    public List<Pacient> GetAllPacienti()
    {
        return DataStorage.Pacienti
            .OrderBy(p => p.Nume)
            .ThenBy(p => p.Prenume)
            .ToList();
    }

    public int GenereazaIdNou()
    {
        return DataStorage.Pacienti.Count == 0 ? 1 : DataStorage.Pacienti.Max(p => p.Id) + 1;
    }
}
