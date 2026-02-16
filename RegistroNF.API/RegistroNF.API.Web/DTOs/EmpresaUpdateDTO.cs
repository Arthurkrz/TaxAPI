namespace RegistroNF.API.Web.DTOs
{
    public class EmpresaUpdateDTO
    {
        public string CNPJ { get; set; } = string.Empty;

        public string NomeResponsavel { get; set; } = string.Empty;

        public string EmailResponsavel { get; set; } = string.Empty;

        public string RazaoSocial { get; set; } = string.Empty;

        public string NomeFantasia { get; set; } = string.Empty;

        public string Municipio { get; set; } = string.Empty;

        public string Logradouro { get; set; } = string.Empty;  

        public int Numero { get; set; }

        public int CEP { get; set; }

        public string UF { get; set; } = string.Empty;
    }
}
