using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Domain.Entidades
{
    public class Fornecedor : Entity
    {
        public string? Nome { get;private set ; }
        public string? Documento { get;private set ; }
        public TipoFornecedorEnum TipoFornecedor { get;private set ; }
        public bool Ativo { get;private set ; }
        public Endereco? Endereco { get;private set ; }

        // EF
        public IEnumerable<Produto> Produtos { get; private set; } = [];
       
        public Fornecedor() { }
        public Fornecedor(string? nome, string? documento, int tipoFornecedor)
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = Enum.Parse<TipoFornecedorEnum>(tipoFornecedor.ToString());
            Ativo = true;
        }
    }
}
