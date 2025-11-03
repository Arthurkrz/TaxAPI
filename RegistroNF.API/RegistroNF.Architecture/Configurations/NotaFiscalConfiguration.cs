using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroNF.Core.Entities;

namespace RegistroNF.Architecture.Configurations
{
    internal class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.HasKey(nf => nf.Id);

            builder.Property(nf => nf.Id)
                   .HasColumnType("uniqueidentifier")
                   .IsRequired();

            builder.Property(nf => nf.Numero)
                   .HasColumnType("int")
                   .HasMaxLength(12)
                   .IsRequired();

            builder.Property(nf => nf.Serie)
                   .HasColumnType("int")
                   .HasMaxLength(8)
                   .IsRequired();

            builder.Property(nf => nf.DataEmissao)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(nf => nf.ValorBrutoProdutos)
                   .HasColumnType("float")
                   .IsRequired();

            builder.Property(nf => nf.ValorICMS)
                   .HasColumnType("float")
                   .IsRequired();

            builder.Property(nf => nf.ValorTotalNota)
                   .HasColumnType("float")
                   .IsRequired();

            builder.HasOne(nf => nf.Empresa)
                   .WithMany(nf => nf.NotasFiscais)
                   .HasForeignKey(nf => nf.EmpresaId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.Property(nf => nf.CreationDate)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.HasIndex(nf => new { nf.Serie, nf.Numero })
                   .IsUnique();

            builder.ToTable("NotasFiscais");
        }
    }
}
