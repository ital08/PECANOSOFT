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
using EmpresaPecanoSueldito.Domain.Enums;

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
                if (records.Count > 0)
                {
                    foreach(var item in records)
                    {
                        item.Sueldo = CalcularSalario(item.TipodeTrabajador, item.HorasLaboradas, item.Faltas);
                    }
                }
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static decimal CalcularSalario(int input, int horasTrabajadas, int faltas)
        {
            decimal salarioBase = 0m;
            decimal bonificacion = 0m;
            decimal impuesto = 0m;

            switch (input)
            {
                case TipoTrabajador.Obrero:
                    salarioBase = Math.Abs( horasTrabajadas) * 15.0m;
                    salarioBase -= Math.Abs(faltas) * 120;
                    bonificacion = 130m;
                    impuesto = 0.12m;
                    break;
                case TipoTrabajador.Supervisor:
                    salarioBase = Math.Abs(horasTrabajadas) * 35.0m;
                    salarioBase -= Math.Abs(faltas) * 280;
                    bonificacion = 200m;
                    impuesto = 0.16m;
                    break;
                case TipoTrabajador.Gerente:
                    salarioBase = Math.Abs(horasTrabajadas) * 85.0m;
                    salarioBase -= Math.Abs(faltas) * 680;
                    bonificacion = 350m;
                    impuesto = 0.18m;
                    break;
                default:
                    throw new ArgumentException("El valor de input debe ser 0, 1 o 2.");
            }

            decimal salarioFinal = salarioBase + bonificacion;
            decimal impuestoAplicado = salarioFinal * impuesto;
            salarioFinal -= impuestoAplicado;

            return salarioFinal<0?0: salarioFinal;
        }
    }

}
