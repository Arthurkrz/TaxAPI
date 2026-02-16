using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Enum;

namespace RegistroNF.API.Services.Utilities
{
    public static class RegisterStatusValidator
    {
        public static Status ValidateRegisterStatus(Entity entity)
        {
            var type = entity.GetType();
            var properties = type.GetProperties().ToList();

            foreach (var prop in properties)
            {
                if (prop.PropertyType == typeof(Guid) ||
                    prop.PropertyType.IsAssignableTo(typeof(Entity)) ||
                    prop.PropertyType == typeof(Status))
                        continue;

                if (prop.GetValue(entity) == default) 
                    return Status.Parcial;
            }

            return Status.Completo;
        }
    }
}
