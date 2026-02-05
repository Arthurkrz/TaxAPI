namespace CalculadoraImposto.API.Core.Common
{
    public static class LogMessages
    {
        public const string EMPRESAINVALIDA = "Erros encontrados na Empresa de CNPJ {CNPJ}: {erros}";
        public const string NENHUMAEMPRESARECEBIDA = "Nenhuma empresa recebida pela API externa!";
        public const string REQUISICAOEMPRESAS = "Solicitando notas fiscais para {mes}/{ano}.";
        public const string EMPRESASRECEBIDAS = "{quantidadeEmpresas} empresas recebidas com {quantidadeNotasFiscais} notas fiscais.";
        public const string INICIOPROCESSAMENTO = "Iniciando processamento de impostos para {quantidadeEmpresas} empresas.";
        public const string EMPRESAEXISTENTE = "Empresa de CNPJ {CNPJ} já existe.";
        public const string EMPRESAINEXISTENTE = "Criando nova empresa de CNPJ {CNPJ}.";
        public const string NOTASFISCAISREGISTRADAS = "{quantidadeNotasFiscais} notas fiscais registradas para empresa de CNPJ {CNPJ}.";
        public const string IMPOSTOCRIADO = "Imposto criado para empresa de CNPJ {CNPJ}.";
    }
}
