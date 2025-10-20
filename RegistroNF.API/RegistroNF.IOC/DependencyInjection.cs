using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RegistroNF.Core.Contracts.Service;
using RegistroNF.Core.Entities;
using RegistroNF.Core.Validators;
using RegistroNF.Services;
using TaxAPI.Core.Entities;
using TaxAPI.Services;

namespace RegistroNF.IOC
{
    public static class DependencyInjection
    {
        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
        }

        public static void InjectValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Empresa>, EmpresaValidator>();
            services.AddScoped<IValidator<NotaFiscal>, NotaFiscalValidator>();
        }
    }
}
