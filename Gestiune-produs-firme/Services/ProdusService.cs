using System.Collections.Generic;
using System.Linq;

namespace Gestiune_produs_firme
{
    public class ProdusService
    {
        public bool ExistaCod(List<Produs> produse, int cod, Produs produsIgnorat = null)
        {
            foreach (Produs p in produse)
            {
                if (p.Cod == cod && p != produsIgnorat)
                    return true;
            }

            return false;
        }

        public List<Produs> FiltreazaSiSorteaza(List<Produs> produse, string textCautare, string criteriuSortare)
        {
            IEnumerable<Produs> rezultat = produse;

            string text = textCautare.Trim().ToLower();

            if (text != "")
            {
                rezultat = rezultat.Where(p =>
                    p.Denumire.ToLower().Contains(text) ||
                    p.Categorie.ToLower().Contains(text) ||
                    p.Cod.ToString().Contains(text));
            }

            switch (criteriuSortare)
            {
                case "Pret crescator":
                    rezultat = rezultat.OrderBy(p => p.Pret);
                    break;

                case "Pret descrescator":
                    rezultat = rezultat.OrderByDescending(p => p.Pret);
                    break;

                case "Denumire A-Z":
                    rezultat = rezultat.OrderBy(p => p.Denumire);
                    break;

                case "Denumire Z-A":
                    rezultat = rezultat.OrderByDescending(p => p.Denumire);
                    break;

                case "Stoc crescator":
                    rezultat = rezultat.OrderBy(p => p.Stoc);
                    break;

                case "Stoc descrescator":
                    rezultat = rezultat.OrderByDescending(p => p.Stoc);
                    break;
            }

            return rezultat.ToList();
        }

        public Produs DeterminaProdusMinim(List<Produs> produse)
        {
            if (produse == null || produse.Count == 0)
                return null;

            Produs minim = produse[0];

            for (int i = 1; i < produse.Count; i++)
            {
                if (produse[i] < minim)
                    minim = produse[i];
            }

            return minim;
        }

        public double CalculeazaValoareTotalaStoc(List<Produs> produse)
        {
            double total = 0;

            foreach (Produs p in produse)
            {
                total += p.Pret * p.Stoc;
            }

            return total;
        }

        public string GenereazaRaport(List<Produs> produse)
        {
            if (produse == null || produse.Count == 0)
                return "Nu exista produse in gestiune.";

            Produs minim = DeterminaProdusMinim(produse);
            Produs maxim = produse[0];

            double sumaPreturi = 0;
            int totalStoc = 0;
            int produseStocMic = 0;
            int produseFaraStoc = 0;

            foreach (Produs p in produse)
            {
                sumaPreturi += p.Pret;
                totalStoc += p.Stoc;

                if (p.Pret > maxim.Pret)
                    maxim = p;

                if (p.Stoc < 5)
                    produseStocMic++;

                if (p.Stoc == 0)
                    produseFaraStoc++;
            }

            double pretMediu = sumaPreturi / produse.Count;
            double valoareTotalaStoc = CalculeazaValoareTotalaStoc(produse);

            string raport = "";
            raport += "================ RAPORT PRODUSE ================\n\n";
            raport += "Numar total produse: " + produse.Count + "\n";
            raport += "Cantitate totala in stoc: " + totalStoc + "\n";
            raport += "Valoare totala stoc: " + valoareTotalaStoc.ToString("0.00") + "\n";
            raport += "Pret mediu produse: " + pretMediu.ToString("0.00") + "\n";
            raport += "Produse cu stoc mic (<5): " + produseStocMic + "\n";
            raport += "Produse fara stoc: " + produseFaraStoc + "\n\n";

            raport += "Produs minim:\n";
            raport += " - " + minim.Denumire + " | Pret: " + minim.Pret.ToString("0.00") + " | Stoc: " + minim.Stoc + "\n\n";

            raport += "Produs maxim:\n";
            raport += " - " + maxim.Denumire + " | Pret: " + maxim.Pret.ToString("0.00") + " | Stoc: " + maxim.Stoc + "\n\n";

            raport += "=============== LISTA PRODUSE ===============\n";
            foreach (Produs p in produse)
            {
                raport += "Cod: " + p.Cod +
                          " | Denumire: " + p.Denumire +
                          " | Pret: " + p.Pret.ToString("0.00") +
                          " | Categorie: " + p.Categorie +
                          " | Stoc: " + p.Stoc +
                          " | Valoare stoc produs: " + (p.Pret * p.Stoc).ToString("0.00") +
                          "\n";
            }

            return raport;
        }
        public string VindeProdus(Produs produs, int cantitate)
        {
            if (produs == null)
                return "Produsul nu exista.";

            if (cantitate <= 0)
                return "Cantitatea trebuie sa fie mai mare decat 0.";

            if (produs.Stoc == 0)
                return "Produsul nu mai exista in stoc.";

            if (cantitate > produs.Stoc)
                return "Cantitatea ceruta depaseste stocul disponibil.";

            produs.Stoc -= cantitate;
            return "";
        }

    }
}
