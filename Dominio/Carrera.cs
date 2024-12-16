using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrera
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public Facultad Facultad { get; set; }
        public override string ToString()
        {
            return Nombre;
        }

    }
}
