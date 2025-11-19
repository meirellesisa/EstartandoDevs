namespace EstartandoDevs.Domain.Entidades
{
    public class Produto : Entity
    {
        public Guid FornecedorId { get; private set; }
        public string? Nome { get; private set; }
        public string? NomeArquivo { get; private set;}
        public string ? Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        // EF
        public Fornecedor? Fornecedor { get; private set; }

        public Produto() { }
        public Produto (Guid fornecedorId, string? nome, string? descricao, decimal valor)
        {
            // estourar exceção se nulo ou vazio
            FornecedorId = fornecedorId;
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
        }

        public void AtribuirNomeArquivo(string nomeArquivo)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(nomeArquivo, nameof(nomeArquivo));
            NomeArquivo = nomeArquivo;
        }
    }
}
