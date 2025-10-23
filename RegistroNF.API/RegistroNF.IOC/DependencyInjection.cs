using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RegistroNF.Architecture.Repositories;
using RegistroNF.Core.Contracts.Repository;
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
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            return services;
        }

        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            return services;
        }

        public static IServiceCollection InjectValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Empresa>, EmpresaValidator>();
            services.AddScoped<IValidator<NotaFiscal>, NotaFiscalValidator>();
            return services;
        }
    }
}
