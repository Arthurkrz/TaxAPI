namespace RegistroNF.Core.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now.Date;
    }
}