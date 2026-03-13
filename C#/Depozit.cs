using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar1 {

    class Depozit {
        public event Action<Depozit, int> evModDep;
        string nume;
        List<Material> lista_materiale;
        public int NumarMateriale => lista_materiale.Count;

        public string nume_dep => nume;

        public Depozit(string fnume = "Depozit1") {
            nume = fnume;
            lista_materiale = new List<Material>();
        }

        public void adaugaMaterial(Material m) {
            lista_materiale.Add(m);
            evModDep?.Invoke(this, lista_materiale.Count-1);
        }

        public double Valoare => lista_materiale.Sum(x => x.Valoare);

        public List<Material> materiale => lista_materiale;

        public override string ToString() {
            string sir = $"Depozitul: {nume} are materialele: \n";
            foreach (var item in lista_materiale) sir += item.ToString();
            return sir;
        }
        public Material this[int i]  => lista_materiale.Find((x) => x.Cod_Material == i); 
       

        public Material this[string name] => lista_materiale.Find((x)=>x.Denumire_Material == name);
        
    }

    
}
