using CalculadoraImposto.API.Core.Common;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.ScheduledJobs.DTOs;
using CalculadoraImposto.API.ScheduledJobs.Mappers;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CalculadoraImposto.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly HttpClient _httpClient;
        private readonly IImpostoService _impostoService;
        private readonly IValidator<EmpresaDTO> _empresaDTOValidator;
        private readonly ILogger<APIJob> _logger;

        public APIJob(HttpClient httpClient, IImpostoService impostoService, IValidator<EmpresaDTO> empresaDTOValidator, ILogger<APIJob> logger)
        {
            _httpClient = httpClient;
            _impostoService = impostoService;
            _empresaDTOValidator = empresaDTOValidator;
            _logger = logger;
        }

        public async Task ExecuteAsync()
        {
            var options = new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true };

            var dataReferencia = DateTime.Now.AddMonths(-1);

            _logger.LogInformation(LogMessages.REQUISICAOEMPRESAS, 
                dataReferencia.Month, dataReferencia.Year);

            var response = await _httpClient.GetAsync($"https://localhost:7075/api/v1/NotaFiscal?mes={dataReferencia.Month}&ano={dataReferencia.Year}");
            var json = await response.Content.ReadAsStringAsync();

            var empresasDTO = JsonSerializer.Deserialize<List<EmpresaDTO>>(json, options);

            if (empresasDTO == null || empresasDTO.Count == 0)
            {
                _logger.LogWarning(LogMessages.NENHUMAEMPRESARECEBIDA);
                return;
            }

            _logger.LogInformation(LogMessages.EMPRESASRECEBIDAS, 
                empresasDTO!.Count, empresasDTO.Sum(e => e.NotasFiscais.Count));

            var empresas = new List<Empresa>();

            foreach (var empresa in empresasDTO)
            {
                var validationResult = await _empresaDTOValidator.ValidateAsync(empresa);

                if (!validationResult.IsValid)
                {
                    _logger.LogError(LogMessages.EMPRESAINVALIDA,
                        empresa.CNPJ,
                        string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    continue;
                }

                empresas.Add(empresa.ToEntity());
            }

            await _impostoService.ProcessarImpostoAsync(empresas);
        }
    }
}
