using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<NotaFiscal> NotasFiscais { get; set; } = default!;

        public DbSet<Empresa> Empresas { get; set; } = default!;

        public DbSet<Endereco> Enderecos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}
