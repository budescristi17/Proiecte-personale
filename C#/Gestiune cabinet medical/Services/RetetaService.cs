using CabinetMedical.Models;

namespace CabinetMedical.Services;

public class RetetaService
{
    public string AdaugaReteta(Reteta reteta)
    {
        List<string> erori = reteta.ValideazaDateReteta();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        reteta.Id = GenereazaIdNou();
        DataStorage.Retete.Add(reteta);
        return string.Empty;
    }

    public string EditeazaReteta(Reteta retetaActualizata)
    {
        List<string> erori = retetaActualizata.ValideazaDateReteta();

        if (erori.Count > 0)
        {
            return string.Join(Environment.NewLine, erori);
        }

        Reteta? retetaExistenta = DataStorage.Retete.FirstOrDefault(r => r.Id == retetaActualizata.Id);

        if (retetaExistenta == null)
        {
            return "Reteta selectata nu a fost gasita.";
        }

        retetaExistenta.MedicId = retetaActualizata.MedicId;
        retetaExistenta.PacientId = retetaActualizata.PacientId;
        retetaExistenta.DataEmiterii = retetaActualizata.DataEmiterii;
        retetaExistenta.Diagnostic = retetaActualizata.Diagnostic;
        retetaExistenta.Medicamente = retetaActualizata.Medicamente;
        retetaExistenta.Dozaj = retetaActualizata.Dozaj;
        retetaExistenta.DurataTratamentZile = retetaActualizata.DurataTratamentZile;
        retetaExistenta.Recomandari = retetaActualizata.Recomandari;
        retetaExistenta.Compensata = retetaActualizata.Compensata;

        return string.Empty;
    }

    public string StergeReteta(int id)
    {
        Reteta? reteta = DataStorage.Retete.FirstOrDefault(r => r.Id == id);

        if (reteta == null)
        {
            return "Reteta selectata nu a fost gasita.";
        }

        DataStorage.Retete.Remove(reteta);
        return string.Empty;
    }

    public List<Reteta> CautaReteteDupaPacient(int pacientId)
    {
        return DataStorage.Retete
            .Where(r => r.PacientId == pacientId)
            .OrderByDescending(r => r.DataEmiterii)
            .ToList();
    }

    public List<Reteta> CautaReteteDupaMedic(int medicId)
    {
        return DataStorage.Retete
            .Where(r => r.MedicId == medicId)
            .OrderByDescending(r => r.DataEmiterii)
            .ToList();
    }

    public List<Reteta> CautaRetete(string textCautare)
    {
        string text = textCautare.Trim().ToLower();

        return DataStorage.Retete
            .Where(r =>
            {
                Medic? medic = DataStorage.Medici.FirstOrDefault(m => m.Id == r.MedicId);
                Pacient? pacient = DataStorage.Pacienti.FirstOrDefault(p => p.Id == r.PacientId);

                string numeMedic = medic == null ? string.Empty : $"{medic.Nume} {medic.Prenume} {medic.Specializare}".ToLower();
                string numePacient = pacient == null ? string.Empty : $"{pacient.Nume} {pacient.Prenume} {pacient.CNP}".ToLower();

                return r.Diagnostic.ToLower().Contains(text) ||
                       numeMedic.Contains(text) ||
                       numePacient.Contains(text);
            })
            .OrderByDescending(r => r.DataEmiterii)
            .ToList();
    }

    public List<Reteta> GetAllRetete()
    {
        return DataStorage.Retete
            .OrderByDescending(r => r.DataEmiterii)
            .ToList();
    }

    public int GenereazaIdNou()
    {
        return DataStorage.Retete.Count == 0 ? 1 : DataStorage.Retete.Max(r => r.Id) + 1;
    }
}
