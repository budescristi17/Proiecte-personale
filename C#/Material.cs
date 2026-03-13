using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar1 {
    class Material {
        int cod_material, cantitate;
        double pret_unitar;
        string denumire_material, unitate_masura;

        public int Cod_Material {
            get => cod_material;
            set => cod_material = value;
        }

        public int Cantitate {
            get => cantitate;
            set {
                if (value >= 0) cantitate = value; 
            }
        }

        public double Pret_Unitar {
            get => pret_unitar;
            set {
                if (value > 0.0) pret_unitar = value;
            }
        }

        public string Denumire_Material {
            get => denumire_material;
            set {
                if (value.Length > 0) denumire_material = value; 
            }
        }

        public string Unitate_Masura {
            get => unitate_masura;
            set {
                if (value.Length > 0) unitate_masura = value;
            }
        }

        public double Valoare => cantitate * pret_unitar;

        public Material() {
            cod_material = cantitate = 0;
            pret_unitar = 0F;
            denumire_material = unitate_masura = "N/A";
        }

        public override string ToString() => $"|{Cod_Material,5}|{Denumire_Material,-25}|{Cantitate,7}|{Pret_Unitar,7:F2}|{Unitate_Masura,5}|{Valoare,9:F2}|\n";

    
    
    }
}
