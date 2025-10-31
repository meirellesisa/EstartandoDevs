using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorEndereco
{
    public class ObterFornecedorEnderecoCommandResponse
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public TipoFornecedorEnum TipoFornecedor { get; private set; }
        public ObterFornecedorEndereco_Endereco? Endereco { get; private set; }

        public ObterFornecedorEnderecoCommandResponse(
            Guid id,
            string nome,
            string documento,
            TipoFornecedorEnum tipoFornecedor,
            ObterFornecedorEndereco_Endereco endereco)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            TipoFornecedor = (TipoFornecedorEnum)tipoFornecedor;
            Endereco = endereco;
        }
    }

    public class ObterFornecedorEndereco_Endereco
    {
        public Guid Id { get; private set; }
        public string Logradouro { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Complemento { get; private set; } = string.Empty;
        public string Bairro { get; private set; } = string.Empty;
        public string Cep { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;

        public ObterFornecedorEndereco_Endereco(
            Guid id,
            string logradouro,
            string numero,
            string complemento,
            string bairro,
            string cep,
            string cidade,
            string estado)
        {
            Id = id;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }
    }
}
