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

        public const string NUMEROMAIORQUEZERO = "O número da nota fiscal deve ser maior que zero";
        public const string SERIEMAIORQUEZERO = "A série da nota fiscal deve ser maior que zero";
        public const string DATAEMISSAONAOFUTURA = "A data de emissão não pode ser futura";
        public const string VALORBRUTOMAIORQUEZERO = "O valor bruto dos produtos deve ser maior que zero";
        public const string VALORTOTALINVALIDO = "O valor total da nota fiscal deve ser igual ao valor bruto dos produtos mais o valor do ICMS";
        public const string EMPRESANAOINFORMADA = "A empresa emissora da nota fiscal deve ser informada";

        public const string CNPJINVALIDO = "O CNPJ deve conter 14 dígitos";
        public const string CNPJNAOINFORMADO = "O CNPJ da empresa deve ser informado";
        public const string NOMERESPONSAVELINVALIDO = "O nome do responsável deve ter entre 2 e 100 caracteres";
        public const string NOMERESPONSAVELNAOINFORMADO = "O nome do responsável deve ser informado";
        public const string EMAILRESPONSAVELINVALIDO = "O email do responsável deve ser um endereço de email válido";
        public const string EMAILRESPONSAVELNAOINFORMADO = "O email do responsável deve ser informado";
    }
}
