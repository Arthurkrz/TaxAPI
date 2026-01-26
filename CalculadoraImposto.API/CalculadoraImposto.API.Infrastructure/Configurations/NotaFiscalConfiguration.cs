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

            builder.Property(n => n.Numero)
                   .IsRequired();

            builder.Property(n => n.Serie)
                   .IsRequired();

            builder.Property(n => n.DataEmissao)
                   .IsRequired();

            builder.Property(n => n.ValorTotal)
                   .IsRequired();

            builder.HasOne(nf => nf.Empresa)
                   .WithMany(e => e.NotasFiscais)
                   .HasForeignKey(n => n.EmpresaId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(n => n.CreationDate)
                   .IsRequired();

            builder.ToTable("NotasFiscais");
        }
    }
}
