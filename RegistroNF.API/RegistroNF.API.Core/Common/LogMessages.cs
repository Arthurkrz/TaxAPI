namespace RegistroNF.API.Core.Common
{
    public static class LogMessages
    {
        public const string ENVIONF = "{quantidadeNotas} notas enviadas de {quantidadeEmpresas} empresas na data {mes}/{ano}.";
        public const string NFANTIGANUMEROMAIOR = "Já existe uma nota fiscal mais antiga cujo número é superior na série {serie}";
        public const string NFRECENTENUMEROMENOR = "Já existe uma nota fiscal mais recente cujo número é inferior na série {serie}";
        public const string NFNUMEROEXISTENTE = "Já existe uma nota fiscal com o número {numero} nesta série";
        public const string EMPRESACRIADA = "Empresa de CNPJ {CNPJ} registrada no banco de dados.";
        public const string EMPRESAEXISTENTE = "Empresa de CNPJ {CNPJ} já existente no banco de dados.";
        public const string NFCRIADA = "Nota Fiscal da empresa de CNPJ {CNPJ} registrada no banco de dados.";
    }
}
