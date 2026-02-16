using Microsoft.AspNetCore.Mvc;
using RegistroNF.API.Core.Common;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Web.DTOs;
using RegistroNF.API.Web.Mappers;

namespace RegistroNF.API.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmpresaController : Controller
    {
        private readonly IEmpresaService _empresaService;
        private readonly ILogger<EmpresaController> _logger;

        public EmpresaController(IEmpresaService empresaService, ILogger<EmpresaController> logger)
        {
            _empresaService = empresaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaDTO>>> GetAsync([FromQuery] int mes, [FromQuery] int ano)
        {
            var empresa = await _empresaService.GetEmpresaByDateAsync(mes, ano);
            var dtos = empresa.Select(e => e.ToDTO()).ToList();
            var quantidadeNotas = dtos.Sum(e => e.NotasFiscais.Count);

            _logger.LogInformation(LogMessages.ENVIONF,
                quantidadeNotas, dtos.Count, mes, ano);

            return Ok(dtos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(EmpresaUpdateDTO empresaUpdateDTO)
        {
            var empresa = empresaUpdateDTO.ToEntity();
            await _empresaService.UpdateStatusCadastroAsync(empresa);

            return Ok();
        }
    }
}
