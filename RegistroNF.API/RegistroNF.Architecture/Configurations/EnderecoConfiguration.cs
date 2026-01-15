using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Configurations
{
    internal class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(end => end.Id);

            builder.Property(end => end.Municipio)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(end => end.Logradouro)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(end => end.Numero)
                   .IsRequired();

            builder.Property(end => end.CEP)
                   .IsRequired();

            builder.HasOne(end => end.Empresa)
                   .WithOne(emp => emp.Endereco)
                   .HasForeignKey<Empresa>(emp => emp.EnderecoId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.Property(end => end.CreationDate)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.ToTable("Enderecos");
        }
    }
}
