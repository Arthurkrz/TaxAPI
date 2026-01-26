using CalculadoraImposto.API.Core.Contracts.Repository;
using CalculadoraImposto.API.Core.Contracts.Service;
using CalculadoraImposto.API.Core.Validators;
using CalculadoraImposto.API.Infrastructure;
using CalculadoraImposto.API.Infrastructure.Repositories;
using CalculadoraImposto.API.ScheduledJobs.DTOs;
using CalculadoraImposto.API.Service;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection InjectRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IImpostoRepository, ImpostoRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
            return services;
        }

        public static IServiceCollection InjectValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<EmpresaDTO>, EmpresaDTOValidator>();
            return services;
        }
    }
}
