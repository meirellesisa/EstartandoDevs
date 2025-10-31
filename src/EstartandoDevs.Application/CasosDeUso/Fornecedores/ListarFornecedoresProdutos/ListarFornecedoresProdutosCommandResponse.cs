using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.ListarFornecedoresProdutos
{
    public class ListarFornecedoresProdutosCommandResponse
    {
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public TipoFornecedorEnum TipoFornecedor { get; private set; }

        public ListarFornecedoresProdutosCommandResponse(string nome, string documento, int tipoFornecedor)
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = (TipoFornecedorEnum)tipoFornecedor;
        }
    }
}
