using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Configurations
{
    internal class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.HasKey(emp => emp.Id);

            builder.Property(emp => emp.CNPJ)
                   .HasMaxLength(14)
                   .IsRequired();

            builder.Property(emp => emp.RazaoSocial)
                   .HasMaxLength(100);

            builder.Property(emp => emp.NomeFantasia)
                   .HasMaxLength(100);

            builder.Property(emp => emp.NomeResponsavel)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(emp => emp.EmailResponsavel)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.HasOne(emp => emp.Endereco)
                   .WithOne(end => end.Empresa)
                   .HasForeignKey<Endereco>(end => end.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(emp => emp.NotasFiscais)
                   .WithOne(nf => nf.Empresa)
                   .HasForeignKey(nf => nf.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(emp => emp.CreationDate)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.HasIndex(emp => emp.CNPJ)
                   .IsUnique();

            builder.ToTable("Empresas");
        }
    }
}
