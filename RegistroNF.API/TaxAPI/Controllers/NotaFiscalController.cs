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

    }

    [HttpGet]
    public IEnumerable<NotaFiscal> GetAll()
    {

    }

    [HttpGet]
    public NotaFiscal GetById(int id)
    {

    }
}
