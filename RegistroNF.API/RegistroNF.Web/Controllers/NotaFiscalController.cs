using Microsoft.AspNetCore.Mvc;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.DTOs;
using RegistroNF.Web.DTOs;
using RegistroNF.Web.Mappers;

namespace RegistroNF.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalService _notaFiscalService;
        private readonly IEmpresaService _empresaService;

        public NotaFiscalController(INotaFiscalService notaFiscalService, IEmpresaService empresaService)
        {
            _notaFiscalService = notaFiscalService;
            _empresaService = empresaService;
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

            return Ok(dtos);
        }
    }
}
