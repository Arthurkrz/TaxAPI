using Microsoft.AspNetCore.Mvc;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Web.DTOs;
using RegistroNF.API.Web.Mappers;

namespace RegistroNF.API.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IEmpresaService _empresaService;
        private readonly ILogger<NotaFiscalController> _logger;

        public NotaFiscalController(INotaFiscalService notaFiscalService, IEmpresaService empresaService, ILogger<NotaFiscalController> logger)
        {
            _notaFiscalService = notaFiscalService;
            _empresaService = empresaService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(NotaFiscalDTO nfDTO)
        {
            await _notaFiscalService.EmitirNotaAsync(nfDTO.ToEntity());
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaDTO>>> GetAsync([FromQuery]int mes, [FromQuery]int ano)
        {
            var empresa = await _empresaService.GetEmpresaByDateAsync(mes, ano);
            var dtos = empresa.Select(e => e.ToDTO()).ToList();
            var quantidadeNotas = dtos.Sum(e => e.NotasFiscais.Count);

            _logger.LogInformation(LogMessages.ENVIONF, 
                quantidadeNotas, dtos.Count, mes, ano);

            return Ok(dtos);
        }
    }
}
