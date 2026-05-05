using System.Text.Json;
using CabinetMedical.Models;

namespace CabinetMedical.Services;

public static class DataStorage
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        PropertyNameCaseInsensitive = true
    };

    private static string MediciFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "medici.json");
    private static string PacientiFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pacienti.json");
    private static string ReteteFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "retete.json");

    public static List<Medic> Medici { get; private set; } = new();
    public static List<Pacient> Pacienti { get; private set; } = new();
    public static List<Reteta> Retete { get; private set; } = new();

    public static void Initialize()
    {
        bool existaFisierMedici = File.Exists(MediciFilePath);
        bool existaFisierPacienti = File.Exists(PacientiFilePath);
        bool existaFisierRetete = File.Exists(ReteteFilePath);

        Medici = existaFisierMedici ? LoadList<Medic>(MediciFilePath) : CreeazaMediciInitiali();
        Pacienti = existaFisierPacienti ? LoadList<Pacient>(PacientiFilePath) : CreeazaPacientiInitiali();
        Retete = existaFisierRetete ? LoadList<Reteta>(ReteteFilePath) : CreeazaReteteInitiale();

        // Daca fisierele nu existau, le cream imediat cu date de exemplu.
        if (!existaFisierMedici || !existaFisierPacienti || !existaFisierRetete)
        {
            SaveAll();
        }
    }

    public static void SaveAll()
    {
        SaveList(MediciFilePath, Medici);
        SaveList(PacientiFilePath, Pacienti);
        SaveList(ReteteFilePath, Retete);
    }

    private static List<T> LoadList<T>(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<T>();
            }

            return JsonSerializer.Deserialize<List<T>>(json, JsonOptions) ?? new List<T>();
        }
        catch
        {
            // Daca fisierul este corupt, pornim cu lista goala ca aplicatia sa nu crape.
            return new List<T>();
        }
    }

    private static void SaveList<T>(string filePath, List<T> lista)
    {
        string json = JsonSerializer.Serialize(lista, JsonOptions);
        File.WriteAllText(filePath, json);
    }

    private static List<Medic> CreeazaMediciInitiali()
    {
        return new List<Medic>
        {
            new()
            {
                Id = 1,
                Nume = "Popescu",
                Prenume = "Andrei",
                Specializare = "Medicina interna",
                Telefon = "0722000001",
                Email = "andrei.popescu@cabinet.ro",
                AniExperienta = 12,
                ProgramLucru = "Luni-Vineri 08:00-14:00"
            },
            new()
            {
                Id = 2,
                Nume = "Ionescu",
                Prenume = "Maria",
                Specializare = "Pediatrie",
                Telefon = "0722000002",
                Email = "maria.ionescu@cabinet.ro",
                AniExperienta = 8,
                ProgramLucru = "Luni-Joi 12:00-18:00"
            },
            new()
            {
                Id = 3,
                Nume = "Vasilescu",
                Prenume = "Ioana",
                Specializare = "Cardiologie",
                Telefon = "0722000003",
                Email = "ioana.vasilescu@cabinet.ro",
                AniExperienta = 15,
                ProgramLucru = "Luni-Miercuri 09:00-15:00"
            },
            new()
            {
                Id = 4,
                Nume = "Georgescu",
                Prenume = "Radu",
                Specializare = "Dermatologie",
                Telefon = "0722000004",
                Email = "radu.georgescu@cabinet.ro",
                AniExperienta = 10,
                ProgramLucru = "Marti-Vineri 10:00-16:00"
            },
            new()
            {
                Id = 5,
                Nume = "Marin",
                Prenume = "Claudia",
                Specializare = "Neurologie",
                Telefon = "0722000005",
                Email = "claudia.marin@cabinet.ro",
                AniExperienta = 14,
                ProgramLucru = "Luni-Vineri 13:00-19:00"
            },
            new()
            {
                Id = 6,
                Nume = "Dobre",
                Prenume = "Alexandru",
                Specializare = "Ortopedie",
                Telefon = "0722000006",
                Email = "alexandru.dobre@cabinet.ro",
                AniExperienta = 9,
                ProgramLucru = "Luni-Joi 08:30-14:30"
            },
            new()
            {
                Id = 7,
                Nume = "Enache",
                Prenume = "Sorina",
                Specializare = "Endocrinologie",
                Telefon = "0722000007",
                Email = "sorina.enache@cabinet.ro",
                AniExperienta = 11,
                ProgramLucru = "Miercuri-Vineri 11:00-17:00"
            },
            new()
            {
                Id = 8,
                Nume = "Dumitru",
                Prenume = "Victor",
                Specializare = "Oftalmologie",
                Telefon = "0722000008",
                Email = "victor.dumitru@cabinet.ro",
                AniExperienta = 7,
                ProgramLucru = "Luni-Vineri 09:30-15:30"
            }
        };
    }

    private static List<Pacient> CreeazaPacientiInitiali()
    {
        return new List<Pacient>
        {
            new()
            {
                Id = 1,
                Nume = "Dumitrescu",
                Prenume = "Elena",
                CNP = "2960101123456",
                DataNasterii = new DateTime(1996, 1, 1),
                Telefon = "0733000001",
                Adresa = "Str. Libertatii nr. 10",
                GrupaSanguina = "A+",
                Asigurat = true,
                Observatii = "Alergie la penicilina"
            },
            new()
            {
                Id = 2,
                Nume = "Stan",
                Prenume = "Mihai",
                CNP = "1890505123456",
                DataNasterii = new DateTime(1989, 5, 5),
                Telefon = "0733000002",
                Adresa = "Bd. Unirii nr. 25",
                GrupaSanguina = "0+",
                Asigurat = false,
                Observatii = "Hipertensiune"
            },
            new()
            {
                Id = 3,
                Nume = "Marin",
                Prenume = "Alex",
                CNP = "5010203123456",
                DataNasterii = new DateTime(2001, 2, 3),
                Telefon = "0733000003",
                Adresa = "Str. Principala nr. 7",
                GrupaSanguina = "B+",
                Asigurat = true,
                Observatii = "Fara alergii cunoscute"
            },
            new()
            {
                Id = 4,
                Nume = "Iordache",
                Prenume = "Ana",
                CNP = "6020714123456",
                DataNasterii = new DateTime(2002, 7, 14),
                Telefon = "0733000004",
                Adresa = "Str. Florilor nr. 18",
                GrupaSanguina = "AB-",
                Asigurat = true,
                Observatii = "Migrene ocazionale"
            },
            new()
            {
                Id = 5,
                Nume = "Neagu",
                Prenume = "Cristian",
                CNP = "1771122123456",
                DataNasterii = new DateTime(1977, 11, 22),
                Telefon = "0733000005",
                Adresa = "Aleea Parcului nr. 3",
                GrupaSanguina = "A-",
                Asigurat = false,
                Observatii = "Diabet tip 2"
            },
            new()
            {
                Id = 6,
                Nume = "Toma",
                Prenume = "Bianca",
                CNP = "2990308123456",
                DataNasterii = new DateTime(1999, 3, 8),
                Telefon = "0733000006",
                Adresa = "Bd. Republicii nr. 41",
                GrupaSanguina = "B-",
                Asigurat = true,
                Observatii = "Astigmatism"
            },
            new()
            {
                Id = 7,
                Nume = "Radu",
                Prenume = "Paul",
                CNP = "1850915123456",
                DataNasterii = new DateTime(1985, 9, 15),
                Telefon = "0733000007",
                Adresa = "Str. Teilor nr. 12",
                GrupaSanguina = "AB+",
                Asigurat = true,
                Observatii = "Durere lombara recurenta"
            },
            new()
            {
                Id = 8,
                Nume = "Munteanu",
                Prenume = "Laura",
                CNP = "2931210123456",
                DataNasterii = new DateTime(1993, 12, 10),
                Telefon = "0733000008",
                Adresa = "Str. Victoriei nr. 55",
                GrupaSanguina = "0-",
                Asigurat = false,
                Observatii = "Dermatita atopica"
            },
            new()
            {
                Id = 9,
                Nume = "Ilie",
                Prenume = "Stefan",
                CNP = "5040416123456",
                DataNasterii = new DateTime(2004, 4, 16),
                Telefon = "0733000009",
                Adresa = "Str. Independentei nr. 2",
                GrupaSanguina = "A+",
                Asigurat = true,
                Observatii = "Control periodic"
            },
            new()
            {
                Id = 10,
                Nume = "Barbu",
                Prenume = "Nicoleta",
                CNP = "2680625123456",
                DataNasterii = new DateTime(1968, 6, 25),
                Telefon = "0733000010",
                Adresa = "Bd. Dacia nr. 9",
                GrupaSanguina = "0+",
                Asigurat = true,
                Observatii = "Hipotiroidism"
            }
        };
    }

    private static List<Reteta> CreeazaReteteInitiale()
    {
        return new List<Reteta>
        {
            new()
            {
                Id = 1,
                MedicId = 1,
                PacientId = 1,
                DataEmiterii = new DateTime(2026, 5, 5),
                Diagnostic = "Raceala usoara",
                Medicamente = "Paracetamol, Vitamina C",
                Dozaj = "Paracetamol 500 mg la 8 ore",
                DurataTratamentZile = 5,
                Recomandari = "Odihna si hidratare",
                Compensata = true
            },
            new()
            {
                Id = 2,
                MedicId = 3,
                PacientId = 3,
                DataEmiterii = new DateTime(2026, 4, 18),
                Diagnostic = "Hipertensiune arteriala usoara",
                Medicamente = "Perindopril",
                Dozaj = "1 comprimat dimineata",
                DurataTratamentZile = 30,
                Recomandari = "Monitorizarea tensiunii de doua ori pe zi",
                Compensata = true
            },
            new()
            {
                Id = 3,
                MedicId = 2,
                PacientId = 9,
                DataEmiterii = new DateTime(2026, 4, 20),
                Diagnostic = "Faringita acuta",
                Medicamente = "Ibuprofen, spray antiseptic",
                Dozaj = "Ibuprofen 400 mg la nevoie",
                DurataTratamentZile = 4,
                Recomandari = "Evitarea bauturilor reci",
                Compensata = false
            },
            new()
            {
                Id = 4,
                MedicId = 4,
                PacientId = 8,
                DataEmiterii = new DateTime(2026, 4, 21),
                Diagnostic = "Dermatita atopica",
                Medicamente = "Crema emolienta, antihistaminic",
                Dozaj = "Crema de doua ori pe zi",
                DurataTratamentZile = 14,
                Recomandari = "Evitarea sapunurilor agresive",
                Compensata = false
            },
            new()
            {
                Id = 5,
                MedicId = 5,
                PacientId = 4,
                DataEmiterii = new DateTime(2026, 4, 22),
                Diagnostic = "Migrena",
                Medicamente = "Sumatriptan",
                Dozaj = "1 comprimat la debutul crizei",
                DurataTratamentZile = 10,
                Recomandari = "Jurnal al episoadelor de migrena",
                Compensata = true
            },
            new()
            {
                Id = 6,
                MedicId = 6,
                PacientId = 7,
                DataEmiterii = new DateTime(2026, 4, 23),
                Diagnostic = "Lombalgie mecanica",
                Medicamente = "Diclofenac gel, antiinflamator",
                Dozaj = "Aplicare locala de 3 ori pe zi",
                DurataTratamentZile = 7,
                Recomandari = "Repaus relativ si exercitii usoare",
                Compensata = false
            },
            new()
            {
                Id = 7,
                MedicId = 7,
                PacientId = 10,
                DataEmiterii = new DateTime(2026, 4, 24),
                Diagnostic = "Hipotiroidism",
                Medicamente = "Levotiroxina",
                Dozaj = "1 comprimat dimineata pe stomacul gol",
                DurataTratamentZile = 60,
                Recomandari = "Reevaluare TSH dupa 6 saptamani",
                Compensata = true
            },
            new()
            {
                Id = 8,
                MedicId = 8,
                PacientId = 6,
                DataEmiterii = new DateTime(2026, 4, 25),
                Diagnostic = "Astigmatism",
                Medicamente = "Lacrimi artificiale",
                Dozaj = "1 picatura in fiecare ochi de 3 ori pe zi",
                DurataTratamentZile = 21,
                Recomandari = "Consult optometric pentru corectie",
                Compensata = false
            },
            new()
            {
                Id = 9,
                MedicId = 1,
                PacientId = 5,
                DataEmiterii = new DateTime(2026, 4, 26),
                Diagnostic = "Diabet zaharat tip 2",
                Medicamente = "Metformin",
                Dozaj = "500 mg de doua ori pe zi",
                DurataTratamentZile = 30,
                Recomandari = "Regim alimentar si monitorizare glicemie",
                Compensata = true
            },
            new()
            {
                Id = 10,
                MedicId = 3,
                PacientId = 2,
                DataEmiterii = new DateTime(2026, 4, 27),
                Diagnostic = "Hipertensiune arteriala",
                Medicamente = "Amlodipina",
                Dozaj = "5 mg o data pe zi",
                DurataTratamentZile = 30,
                Recomandari = "Reducerea consumului de sare",
                Compensata = true
            },
            new()
            {
                Id = 11,
                MedicId = 2,
                PacientId = 1,
                DataEmiterii = new DateTime(2026, 4, 28),
                Diagnostic = "Alergie sezoniera",
                Medicamente = "Loratadina",
                Dozaj = "1 comprimat seara",
                DurataTratamentZile = 10,
                Recomandari = "Evitarea expunerii la polen",
                Compensata = false
            },
            new()
            {
                Id = 12,
                MedicId = 4,
                PacientId = 3,
                DataEmiterii = new DateTime(2026, 4, 29),
                Diagnostic = "Acnee usoara",
                Medicamente = "Gel dermatologic",
                Dozaj = "Aplicare seara pe zona afectata",
                DurataTratamentZile = 28,
                Recomandari = "Igiena locala si control dupa o luna",
                Compensata = false
            }
        };
    }
}
