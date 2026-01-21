using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraImposto.API.Infrastructure.Configurations
{
    public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.HasKey(n => n.ID);

            builder.Property(e => e.ID)
                   .ValueGeneratedNever();

            builder.Property(n => n.Numero)
                   .IsRequired();

            builder.Property(n => n.Serie)
                   .IsRequired();

            builder.Property(n => n.DataEmissao)
                   .IsRequired();

            builder.Property(n => n.ValorTotal)
                   .IsRequired();

            builder.HasOne<Empresa>()
                   .WithMany()
                   .HasForeignKey(n => n.NotaFiscalEmpresaId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.CreationDate)
                   .IsRequired();
        }
    }
}
