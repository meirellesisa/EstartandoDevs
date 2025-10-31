using EstartandoDevs.Domain.Entidades;

namespace EstartandoDevs.Domain.Repository
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid fornecedorId);
        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid fornecedorId);
        Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);

        Task<bool> NomeJaUtilizado(string nome);
    }
}
