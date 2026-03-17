using RegistroNF.API.Core.Common;

namespace RegistroNF.API.Core.Entities
{
    public class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; } = SystemTime.Now().Date;
    }
}