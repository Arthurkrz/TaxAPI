using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraImposto.API.Infrastructure.Configurations
{
    public class ImpostoConfiguration : IEntityTypeConfiguration<Imposto>
    {
        public void Configure(EntityTypeBuilder<Imposto> builder)
        {
            builder.HasKey(i => i.ID);

            builder.Property(e => e.ID)
                   .ValueGeneratedNever();

            builder.Property(i => i.ValorImposto)
                   .IsRequired();

            builder.Property(i => i.Aliquota)
                   .IsRequired();

            builder.Property(i => i.Vencimento)
                   .IsRequired();

            builder.Property(i => i.LucroPresumido)
                   .IsRequired();

            builder.Property(i => i.AnoReferencia)
                   .IsRequired();

            builder.Property(i => i.MesReferencia)
                   .IsRequired();

            builder.HasOne<Empresa>()
                   .WithMany()
                   .HasForeignKey(i => i.ImpostoEmpresaId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(i => i.CreationDate)
                   .IsRequired();
        }
    }
}
