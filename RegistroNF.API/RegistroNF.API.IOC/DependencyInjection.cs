using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegistroNF.API.Infrastructure;
using RegistroNF.API.Infrastructure.Repositories;
using RegistroNF.API.Core.Contracts.Repository;
using RegistroNF.API.Core.Contracts.Service;
using RegistroNF.API.Core.Entities;
using RegistroNF.API.Core.Validators;
using RegistroNF.API.Services;

namespace RegistroNF.API.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<INotaFiscalService, NotaFiscalService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            return services;
        }

        public static IServiceCollection InjectRepositories(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

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
