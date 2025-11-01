namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.AtribuirProdutoFornecedor
{
    public class AtribuirProdutoFornecedorCommandResponse
    {
        public Guid FornecedorId { get; private set; }
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public List<AtribuirProdutoFornecedor_Produto> Produtos { get; private set; } = [];

        public AtribuirProdutoFornecedorCommandResponse(Guid fornecedorId, string nome, string documento, List<AtribuirProdutoFornecedor_Produto> produtos)
        {
            FornecedorId = fornecedorId;
            Nome = nome;
            Documento = documento;
            Produtos = produtos;    
        }
    }

    public class AtribuirProdutoFornecedor_Produto
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Valor {  get; private set; }

        public AtribuirProdutoFornecedor_Produto(Guid id, string nome, string descricao, string valor)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
