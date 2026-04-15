using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Test_PAW
{
    public class Medicament
    {
        private readonly int cod;
        private string denumire;
        private float pret;
        public int Cod
        {
            get { return cod; }
        }
        public string Denumire
        {
            get { return denumire; }
            set { denumire = value; }
        }
        public float Pret
        {
            get { return pret; }
            set { pret = value; }
        }
        public Medicament(int cod, string denumire, float pret)
        {
            this.cod = cod;
            this.denumire = denumire;
            this.pret = pret;
        }
        public override string ToString()
        {
            return $"Cod: {cod}, Denumire: {denumire}, Pret: {pret}";
        }
    }
}
