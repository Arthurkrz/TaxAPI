using CalculadoraImposto.API.Core.Entities;
using CalculadoraImposto.API.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CalculadoraImposto.API.Infrastructure
{
    public class Context : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; } = default!;

        public DbSet<NotaFiscal> NotasFiscais { get; set; } = default!;
        
        public DbSet<Imposto> Impostos { get; set; } = default!;

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmpresaConfiguration).Assembly);
        }
    }
}
