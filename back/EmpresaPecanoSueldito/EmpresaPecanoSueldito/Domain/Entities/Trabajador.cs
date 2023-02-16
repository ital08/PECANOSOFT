using EmpresaPecanoSueldito.Domain.Enums;
using System;

namespace EmpresaPecanoSueldito.Domain.Entities
{
    public class Trabajador
    {
        public string DNI { get; set; }
        public int HorasLaboradas { get; set; }
        public int DiasLaborados { get; set; }
        public int Faltas { get; set; }
        public int TipodeTrabajador { get; set; }
    }
}
