using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EstartandoDevs.Infrastructure.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext contexto) : base(contexto) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid fornecedorId)
        {
            var response = await _contexto.Fornecedores
                .AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(fornecedor => fornecedor.Id == fornecedorId);

            return response;
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            var response = await _contexto.Enderecos
                .AsNoTracking()
                .FirstOrDefaultAsync(endereco => endereco.FornecedorId == fornecedorId);

            return response;
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid fornecedorId)
        {
            var response = await _contexto.Fornecedores
                 .AsNoTracking()
                 .Include(f => f.Endereco)
                 .Include(f => f.Produtos)
                 .FirstOrDefaultAsync(fornecedor => fornecedor.Id == fornecedorId);

            return response;
        }

        public Task<bool> NomeJaUtilizado(string nome)
        {
            var response = _contexto.Fornecedores
                .AsNoTracking()
                .AnyAsync(fornecedor => EF.Functions.Like(fornecedor.Nome, nome), CancellationToken.None);

            return response;
        }

        public async Task<Fornecedor> ObterFornecedorProdutos(Guid fornecedorId)
        {
            var response = await _contexto.Fornecedores
                 .AsNoTracking()
                 .Include(f => f.Produtos)
                 .FirstOrDefaultAsync(fornecedor => fornecedor.Id == fornecedorId);

            return response;
        }
    }
}
