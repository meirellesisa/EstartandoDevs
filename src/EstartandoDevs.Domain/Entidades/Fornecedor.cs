using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Domain.Entidades
{
    public class Fornecedor : Entity
    {
        public string? Nome { get; private set; }
        public string? Documento { get; private set; }
        public TipoFornecedorEnum TipoFornecedor { get; private set; }
        public bool Ativo { get; private set; }
        public Endereco? Endereco { get; private set; }

        // EF
        public List<Produto> Produtos { get; private set; } = [];

        public Fornecedor() { }
        public Fornecedor(string? nome, string? documento, int tipoFornecedor)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nome, nameof(nome));
            ArgumentException.ThrowIfNullOrEmpty(documento, nameof(documento));
            if (tipoFornecedor <= 0) throw new ArgumentOutOfRangeException(nameof(tipoFornecedor), "O TipoDoFornecedor deve ser maior que zero.");

            Nome = nome.Trim();
            Documento = documento.Trim();
            TipoFornecedor = Enum.Parse<TipoFornecedorEnum>(tipoFornecedor.ToString());
            Ativo = true;
        }

        public void AtribuirNome(string nome)
        {
            ArgumentException.ThrowIfNullOrEmpty(nome, nameof(nome));
            Nome = nome.Trim();
        }

        public void AtribuirDocumento(string docuemento)
        {
            ArgumentException.ThrowIfNullOrEmpty(docuemento, nameof(docuemento));
            Documento = docuemento.Trim();
        }

        public void AtribuirTipoFornecedor(int tipo)
        {
            if (tipo <= 0) throw new ArgumentOutOfRangeException(nameof(tipo), "O TipoDoFornecedor deve ser maior que zero.");

            TipoFornecedor = Enum.Parse<TipoFornecedorEnum>(tipo.ToString());
        }

        public void AtribuirProduto(Produto produto)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto), "O produto não pode ser nulo.");

            Produtos.Add(produto);
        }
    }
}
