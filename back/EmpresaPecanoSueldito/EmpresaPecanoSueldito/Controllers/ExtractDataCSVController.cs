using EmpresaPecanoSueldito.Application.DataCSV.Query;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresaPecanoSueldito.Controllers
{
    [ApiController]
    [Route("get")]
    public class ExtractDataCSVController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExtractDataCSVController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route(ApiRoutes.GetExtractedData)]
        public async Task<IActionResult> Get([FromQuery] int? tipoTrabajador, [FromQuery] string? dni)
        {
            var query = new ExtractDataCSVQuery { tipoTrabajador = tipoTrabajador, dni = dni };
            var result = await this._mediator.Send(query);
            return Ok(result);
        }
    }
}
