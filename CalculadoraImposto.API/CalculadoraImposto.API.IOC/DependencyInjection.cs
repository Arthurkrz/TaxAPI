using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.Core.Validators;
using CalculadoraImposto.API.Infrastructure.Repositories;
using CalculadoraImposto.API.Service;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CalculadoraImposto.API.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IImpostoService, ImpostoService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            return services;
        }

        public static IServiceCollection InjectRepositories(this IServiceCollection services)
        {
            services.AddScoped<IImpostoRepository, ImpostoRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            return services;
        }

        public static IServiceCollection InjectValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<Empresa>, EmpresaValidator>();
            return services;
        }
    }
}
