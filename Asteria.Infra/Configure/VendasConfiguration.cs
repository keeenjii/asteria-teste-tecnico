using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Asteria.Domain.Entities;

namespace Asteria.Infra.Configuration
{
    public class VendasConfiguration : IEntityTypeConfiguration<Vendas>
    {
        public void Configure(EntityTypeBuilder<Vendas> builder)
        {
            builder.ToTable("Vendas");

            builder.Property(v => v.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.HasKey(v => v.Id);

            builder.Property(v => v.CodigoCliente)
                .HasColumnName("CodigoCliente")
                .IsRequired();

            builder.Property(v => v.Categoria)
                .HasColumnName("Categoria")
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(v => v.sku)
                .HasColumnName("sku")
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(v => v.Data)
                .HasColumnName("Data")
                .IsRequired();

            builder.Property(v => v.Quantidade)
                .HasColumnName("Quantidade")
                .IsRequired();

            builder.Property(v => v.Faturamento)
                .HasColumnName("Faturamento")
                .IsRequired()
                .HasColumnType("double(10,2)");
        }
    }
}