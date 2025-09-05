namespace RegistroNF.Core.Entities
{
    public class Contato : Entity
    {
        public int DDD { get; set; }

        public string Telefone { get; set; } = default!;
        
        public string Email { get; set; } = default!;
    }
}
