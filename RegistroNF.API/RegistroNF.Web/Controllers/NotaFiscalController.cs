using Microsoft.AspNetCore.Mvc;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.DTOs;
using RegistroNF.Web.Mapper;

namespace RegistroNF.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotaFiscalController : Controller
    {
        private readonly INotaFiscalService _notaFiscalService;

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        [HttpGet]
        public IActionResult Create(NotaFiscalDTO nfDTO)
        {
            var nf = NotaFiscalMapper.ToEntity(nfDTO);
            _notaFiscalService.EmitirNota(nf);
            return Created();
        }
    }
}
