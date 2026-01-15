namespace CalculadoraImposto.API.ScheduledJobs
{
    public class APIJob
    {
        private readonly HttpClient _httpClient;

        public APIJob(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task ExecuteAsync()
        {
            var month = DateTime.Now.AddMonths(-1);
            var response = await _httpClient.GetAsync($"https://localhost:7075/api/v1/NotaFiscal");
            var json = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }
    }
}
