using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestiune_produs_firme
{
    public class Container
    {
        public List<Produs> Produse { get; set; }

        public Container()
        {
            Produse = new List<Produs>();
        }
    }
}
