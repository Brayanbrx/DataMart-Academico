using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Modalidad
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public override string ToString()
        {
            return Tipo;
        }

    }
}
