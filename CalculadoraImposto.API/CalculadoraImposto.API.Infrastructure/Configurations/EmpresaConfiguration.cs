using CalculadoraImposto.API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CalculadoraImposto.API.Infrastructure.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(e => e.ID)
                   .ValueGeneratedNever();

            builder.Property(e => e.CNPJ)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.Property(e => e.RazaoSocial)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(e => e.NomeResponsavel)
                   .IsRequired();

            builder.Property(e => e.EmailResponsavel)
                   .IsRequired();

            builder.Property(e => e.CreationDate)
                   .IsRequired();

            builder.HasIndex(e => e.CNPJ)
                   .IsUnique();

            builder.HasIndex(e => e.RazaoSocial)
                   .IsUnique();
        }
    }
}
