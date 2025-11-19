using EstartandoDevs.Domain.Entidades;

namespace EstartandoDevs.Domain.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> ObterProdutosFornecedores();
        Task<Produto> ObterProdutoFornecedor(Guid produtoId);
        Task<Produto> ProdutoExisteEPertenseAoFornecedor(Guid fornecedorId,Guid produtoId);

    }
}
