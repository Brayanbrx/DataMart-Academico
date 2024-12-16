using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Estadisticas
    {
        public int Id { get; set; }
        public Carrera Carrera { get; set; }
        public Periodo Periodo { get; set; }
        public Localidad Localidad { get; set; }
        public Modalidad Modalidad { get; set; }
        public string AgrupadoPor { get; set; }
        public int Inscritos { get; set; }
        public int NuevosEstudiantes { get; set; }
        public int AntiguosEstudiantes { get; set; }
        public int Matriculados { get; set; }
        public int SinNota {  get; set; }
        public double SinNotaP {  get; set; }
        public int Aprobados {  get; set; }
        public double AprobadosP { get; set; }
        public int Reprobados { get; set; }
        public double ReprobadosP { get; set; }
        public int Reprobados0 { get; set; }
        public double ReprobadosOP { get; set; }
        public int Mora { get; set; }
        public double MoraP { get; set; }
        public int Retirados { get; set; }
        public int PPA { get; set; }
        public int PPS { get; set; }
        public int PPA1 { get; set; }
        public int PPAC { get; set; }
        public int Egresados { get; set; }
        public int Titulados { get; set; }

    }
}
