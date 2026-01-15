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
            var nf = NotaFiscalMapper.ToEntity(nfDTO);
            _notaFiscalService.EmitirNota(nf);
            return Created();
        }

        [HttpGet]
        public ActionResult<List<EmpresaDTO>> Get(int mes, int ano)
        {
            var empresas = _empresaService.GetEmpresaByDateAsync(mes, ano);

            return Ok(new List<EmpresaDTO>());
        }
    }
}
