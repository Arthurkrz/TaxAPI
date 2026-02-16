using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroNF.API.Core.Entities;

namespace RegistroNF.API.Infrastructure.Configurations
{
    internal class EnderecoConfiguration : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(end => end.Id);

            builder.Property(end => end.Municipio)
                   .HasMaxLength(50);

            builder.Property(end => end.Logradouro)
                   .HasMaxLength(100);

            builder.Property(end => end.Numero);

            builder.Property(end => end.CEP);

            builder.Property(end => end.UF);

            builder.Property(end => end.CreationDate)
                   .HasColumnType("datetime2");

            builder.ToTable("Enderecos");
        }
    }
}
