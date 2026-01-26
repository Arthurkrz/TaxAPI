using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.ScheduledJobs.DTOs;
using CalculadoraImposto.API.ScheduledJobs.Mappers;
using FluentValidation;
using System.Text.Json;

namespace CalculadoraImposto.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly HttpClient _httpClient;
        private readonly IImpostoService _impostoService;
        private readonly IValidator<EmpresaDTO> _empresaDTOValidator;

        public APIJob(HttpClient httpClient, IImpostoService impostoService, IValidator<EmpresaDTO> empresaDTOValidator)
        {
            _httpClient = httpClient;
            _impostoService = impostoService;
            _empresaDTOValidator = empresaDTOValidator;
        }

        public async Task ExecuteAsync()
        {
            var options = new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true };

            var month = DateTime.Now.AddMonths(-1);
            var response = await _httpClient.GetAsync($"https://localhost:7075/api/v1/NotaFiscal?mes={month.Month}&ano={month.Year}");

            var json = await response.Content.ReadAsStringAsync();

            var empresasDTO = JsonSerializer.Deserialize<List<EmpresaDTO>>(json, options) ??
                throw new ArgumentNullException("Lista nula recebida.");

            var empresas = new List<Empresa>();

            foreach (var empresa in empresasDTO)
            {
                var validationResult = await _empresaDTOValidator.ValidateAsync(empresa);

                if (!validationResult.IsValid)
                {
                    // log
                    continue;
                }

                empresas.Add(empresa.ToEntity());
            }

            await _impostoService.ProcessarImpostoAsync(empresas);
        }
    }
}
