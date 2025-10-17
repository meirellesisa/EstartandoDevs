namespace EstartandoDevs.Domain.Entidades
{
    public class Endereco : Entity
    {
        public Guid FornecedorId { get; private set; }
        public string? Logradouro { get; private set; }
        public string? Numero { get; private set; }
        public string? Complemento { get; private set; }
        public string? Cep { get; private set; }
        public string? Bairro { get; private set; }
        public string? Cidade { get; private set; }
        public string? Estado { get; private set; }

        // EF
        public Fornecedor? Fornecedor { get; private set; } 

        public Endereco(
            string? logradouro, 
            string? numero, 
            string? complemento, 
            string? cep, 
            string? bairro, 
            string? cidade, 
            string? estado) 
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Cep = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}
