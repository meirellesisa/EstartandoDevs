using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos
{
    public class ListarFornecedoresProdutosCommandResponse
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public TipoFornecedorEnum TipoFornecedor { get; private set; }

        public ListarFornecedoresProdutosCommandResponse(Guid id, string nome, string documento, int tipoFornecedor)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            TipoFornecedor = (TipoFornecedorEnum)tipoFornecedor;
        }
    }
}
