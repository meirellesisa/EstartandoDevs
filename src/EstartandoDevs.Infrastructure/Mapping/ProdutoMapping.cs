using EstartandoDevs.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EstartandoDevs.Infrastructure.Mapping
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // Chave Primaria
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasColumnType("varchar(200)");

            builder.Property(p => p.NomeArquivo)
                   .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                   .IsRequired()
                   .HasColumnType("varchar(1000)");

            builder.ToTable("Produtos");
        }
    }
}
