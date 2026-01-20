using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.ScheduledJobs.DTOs;
using CalculadoraImposto.API.ScheduledJobs.Mappers;
using System.Text.Json;

namespace CalculadoraImposto.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly HttpClient _httpClient;
        private readonly IImpostoService _impostoService;

        public APIJob(HttpClient httpClient, IImpostoService impostoService)
        {
            _httpClient = httpClient;
            _impostoService = impostoService;
        }

        public async Task ExecuteAsync()
        {
            var options = new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true };

            var month = DateTime.Now.AddMonths(-1);
            var response = await _httpClient.GetAsync($"https://localhost:7075/api/v1/NotaFiscal?mes={month.Month}&ano={month.Year}");

            var json = await response.Content.ReadAsStringAsync();
            var empresas = JsonSerializer.Deserialize<List<EmpresaDTO>>(json, options) ??
                throw new ArgumentNullException("Lista nula recebida.");

            var test = empresas.Select(e => e.ToEntity());

            await _impostoService.ProcessarImposto(
                empresas.Select(dto => dto.ToEntity()));
        }
    }
}
