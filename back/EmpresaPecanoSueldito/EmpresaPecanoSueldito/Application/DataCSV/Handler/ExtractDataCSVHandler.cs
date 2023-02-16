using CsvHelper;
using EmpresaPecanoSueldito.Application.DataCSV.Query;
using EmpresaPecanoSueldito.Domain.Entities;
using MediatR;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Text.RegularExpressions;

namespace EmpresaPecanoSueldito.Application.DataCSV.Handler
{
    public class ExtractDataCSVHandler : IRequestHandler<ExtractDataCSVQuery, IEnumerable<Trabajador>>
    {

        public async Task<IEnumerable<Trabajador>> Handle(ExtractDataCSVQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var lines = await System.IO.File.ReadAllLinesAsync("data-trabajadores.csv");
                var records = new List<Trabajador>();
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = "|",
                    HasHeaderRecord = true,
                    HeaderValidated = null,
                    MissingFieldFound = null,
                    PrepareHeaderForMatch = header => Regex.Replace(header.Header, @"\s", string.Empty)

                };
                using (var reader = new StreamReader("data-trabajadores.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    var data = csv.GetRecords<Trabajador>().ToList();
                    if (request.tipoTrabajador != null)
                    {
                        data = data.Where(p => p.TipodeTrabajador == request.tipoTrabajador).ToList();
                    }
                    if (request.dni != null)
                    {
                        data = data.Where(p => p.DNI == request.dni).ToList();
                    }
                    records = data.ToList();
                }
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
