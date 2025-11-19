using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EstartandoDevs.Infrastructure.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto) { }

        public async Task<Produto> ObterProdutoFornecedor(Guid produtoId)
        {
            var response = await _contexto.Produtos
                .AsNoTracking()
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(produto => produto.Id == produtoId);

            return response;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        {
            var response = await _contexto.Produtos
                .AsNoTracking()
                .Include(p => p.Fornecedor)
                .OrderBy(produto => produto.Nome)
                .ToListAsync();

            return response;
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        {
            var response = await _contexto.Produtos
                .AsNoTracking()
                .Where(produto => produto.FornecedorId == fornecedorId)
                .ToListAsync();

            return response;
        }

        public async Task<Produto> ProdutoExisteEPertenseAoFornecedor(Guid fornecedorId, Guid produtoId)
        {
            var response = await _contexto.Produtos
                .AsNoTracking()
                .FirstOrDefaultAsync(produto => produto.FornecedorId == fornecedorId && produto.Id == produtoId);

            return response;
        }
    }
}
