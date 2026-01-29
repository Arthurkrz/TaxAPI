namespace CalculadoraImposto.API.Core.Common
{
    public static class LogMessages
    {
        public const string EMPRESAINVALIDA = "Erros encontrados na Empresa de CNPJ {CNPJ}: {erros}";
        public const string NENHUMAEMPRESARECEBIDA = "Nenhuma empresa recebida pela API externa!";
        public const string REQUISICAOEMPRESAS = "Solicitando notas fiscais para {mes}/{ano}.";
        public const string EMPRESASRECEBIDAS = "{quantidadeEmpresas} empresas recebidas com {quantidadeNotasFiscais} notas fiscais.";
    }
}
