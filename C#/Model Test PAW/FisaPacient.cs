using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Test_PAW
{
    public class FisaPacient
    {
        private string nume;
        private string simptome;
        private int durata_tratament;
        private List<Medicament> medicamente;
        private List<int> cantitati;

        public FisaPacient(string nume, string simptome, int durata_tratament, List<Medicament> medicamente, List<int> cantitati)
        {
            this.nume = nume;
            this.simptome = simptome;
            this.durata_tratament = durata_tratament;
            this.medicamente = medicamente;
            this.cantitati = cantitati;
        }

        public string Nume
        {
            get { return nume; }
            set { nume = value; }
        }

        public string Simptome
        {
            get { return simptome; }
            set { simptome = value; }
        }

        public int Durata_tratament
        {
            get { return durata_tratament; }
            set { durata_tratament = value; }
        }

        public List<Medicament> Medicamente
        {
            get { return medicamente; }
            set { medicamente = value; }
        }

        public List<int> Cantitati
        {
            get { return cantitati; }
            set { cantitati = value; }
        }

        public static explicit operator double(FisaPacient fisa)
        {
            double total = 0;

            for (int i = 0; i < fisa.Medicamente.Count; i++)
            {
                total += fisa.Medicamente[i].Pret * fisa.Cantitati[i];
            }

            return total;
        }

        public static explicit operator int(FisaPacient fisa)
        {
            return fisa.Medicamente.Count;
        }
    }
}