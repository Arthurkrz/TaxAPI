using Microsoft.AspNetCore.Mvc;
using TaxAPI.Core.Entities;

namespace TaxAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class NotaFiscalController : ControllerBase
{
    private readonly ILogger<NotaFiscalController> _logger;

    public NotaFiscalController(ILogger<NotaFiscalController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public IActionResult CreateNF()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IEnumerable<NotaFiscal> GetAll()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public NotaFiscal GetById(int id)
    {
        throw new NotImplementedException();
    }
}
