using Microsoft.AspNetCore.Mvc;
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

        public NotaFiscalController(INotaFiscalService notaFiscalService)
        {
            _notaFiscalService = notaFiscalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(NotaFiscalDTO nfDTO)
        {
            await _notaFiscalService.EmitirNotaAsync(nfDTO.ToEntity());
            return Created();
        }
    }
}
