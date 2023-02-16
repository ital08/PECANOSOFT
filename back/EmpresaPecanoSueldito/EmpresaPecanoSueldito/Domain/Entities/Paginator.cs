using System.Collections.Generic;

namespace EmpresaPecanoSueldito.Domain.Entities
{
    public class Paginator
    {
        public List<Trabajador> Trabajadores { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
