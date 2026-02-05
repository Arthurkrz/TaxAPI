namespace RegistroNF.API.Core.Common
{
    public static class LogMessages
    {
        public const string ENVIONF = "{0} notas enviadas de {2} empresas na data {3}/{4}";
        public const string EMPRESAINVALIDA = "Erros encontrados na empresa de CNPJ {0}: {1}";
        public const string NFINVALIDA = "Erros encontrados na NF de número {0} e série {1}: {2}";
        public const string NFANTIGANUMEROMAIOR = "Já existe uma nota fiscal mais antiga cujo número é superior na série {0} da empresa de CNPJ {1}";
        public const string NFRECENTENUMEROMENOR = "Já existe uma nota fiscal mais recente cujo número é inferior na série {0} da empresa de CNPJ {1}";
        public const string NFNUMEROEXISTENTE = "Já existe uma nota fiscal com o número {0} na série {1} da empresa de CNPJ {2}";
    }
}
