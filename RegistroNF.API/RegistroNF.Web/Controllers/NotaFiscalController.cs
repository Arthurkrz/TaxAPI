using Microsoft.AspNetCore.Mvc;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.DTOs;
using RegistroNF.Web.DTOs;
using RegistroNF.Web.Mapper;

namespace RegistroNF.Controllers
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
        public IActionResult Create(NotaFiscalDTO nfDTO)
        {
            _notaFiscalService.EmitirNota(nfDTO.ToEntity());
            return Created();
        }

        [HttpGet]
        public ActionResult<List<EmpresaDTO>> Get([FromQuery]int mes, [FromQuery]int ano) =>
            Ok(_empresaService.GetEmpresaByDateAsync(mes, ano).Select(e => e.ToDTO()).ToList());
    }
}
