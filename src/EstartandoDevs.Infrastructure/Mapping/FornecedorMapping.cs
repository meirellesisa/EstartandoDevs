using EstartandoDevs.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstartandoDevs.Infrastructure.Mapping
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(f => f.Documento)
                   .IsRequired()
                   .HasColumnType("varchar(14)");

            // Relacionamentos 
            builder.HasOne(f => f.Endereco)
                   .WithOne(e => e.Fornecedor);

            builder.HasMany(p => p.Produtos)
                   .WithOne(p => p.Fornecedor)
                   .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Fornecedores");
        }
    }
}
