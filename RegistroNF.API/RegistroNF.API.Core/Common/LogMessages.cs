namespace RegistroNF.API.Core.Common
{
    public static class LogMessages
    {
        public const string NFANTIGANUMEROMAIOR = "Já existe uma nota fiscal mais antiga cujo número é superior na série {serie} da empresa de CNPJ {CNPJ}";
        public const string NFRECENTENUMEROMENOR = "Já existe uma nota fiscal mais recente cujo número é inferior na série {serie} da empresa de CNPJ {CNPJ}";
        public const string NFNUMEROEXISTENTE = "Já existe uma nota fiscal com o número {numero} na série {serie} da empresa de CNPJ {CNPJ}";
    }
}
