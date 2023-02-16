using EmpresaPecanoSueldito.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EmpresaPecanoSueldito.Application.DataCSV.Query
{
    public class ExtractDataCSVQuery : IRequest<Paginator>
    {
        public int? tipoTrabajador { get; set; }
        public string? dni { get; set; }
        public int? pageSize { get; set; }
        public int? pageIndex { get; set; }
    }
}
