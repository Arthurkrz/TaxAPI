using Microsoft.EntityFrameworkCore;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture
{
    public class Context : DbContext
    {
        public DbSet<NotaFiscal> NotasFiscais { get; set; } = default!;

        public DbSet<Empresa> Empresas { get; set; } = default!;

        public DbSet<Endereco> Enderecos { get; set; } = default!;

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }
}
