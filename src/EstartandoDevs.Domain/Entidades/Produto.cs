namespace EstartandoDevs.Domain.Entidades
{
    public class Produto : Entity
    {
        public Guid FornecedorId { get; private set; }
        public string? Nome { get; private set; }
        public string ? Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        // EF
        public Fornecedor? Fornecedor { get; private set; }

        public Produto() { }
        public Produto (string? nome, string? descricao, decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = valor;
            Ativo = true;
        }
    }
}
