using EmpresaPecanoSueldito.Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace EmpresaPecanoSueldito.Application.DataCSV.Query
{
    public class ExtractDataCSVQuery : IRequest<IEnumerable<Trabajador>>
    {
        public int? tipoTrabajador { get; set; }
        public string? dni { get; set; }
    }
}
