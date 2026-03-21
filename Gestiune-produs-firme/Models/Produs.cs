namespace Gestiune_produs_firme
{
    public class Produs
    {
        public int Cod { get; set; }
        public string Denumire { get; set; }
        public string Categorie { get; set; }
        public int Stoc { get; set; }

        private double pret;
        public double Pret
        {
            get { return pret; }
            set { pret = value; }
        }

        public Produs(int cod, string denumire, double pret)
        {
            Cod = cod;
            Denumire = denumire;
            Pret = pret;
            Categorie = "Diverse";
            Stoc = 0;
        }

        public Produs(int cod, string denumire, double pret, string categorie, int stoc)
        {
            Cod = cod;
            Denumire = denumire;
            Pret = pret;
            Categorie = categorie;
            Stoc = stoc;
        }

        public static bool operator <(Produs p1, Produs p2)
        {
            return p1.Pret < p2.Pret;
        }

        public static bool operator >(Produs p1, Produs p2)
        {
            return p1.Pret > p2.Pret;
        }
    }
}