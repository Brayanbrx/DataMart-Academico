using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Periodo
    {
        public int Id { get; set; }
        public string PeriodoNombre {  get; set; }
        public override string ToString()
        {
            return PeriodoNombre;
        }

    }
}
