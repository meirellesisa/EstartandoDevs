using EstartandoDevs.Domain.Entidades.Enums;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Obter.ObterFornecedorProdutosEndereco
{
    public class ObterFornecedorProdutosEnderecoCommandResponse
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = string.Empty;
        public string Documento { get; private set; } = string.Empty;
        public TipoFornecedorEnum TipoFornecedor { get; private set; }
        public ObterFornecedorProdutosEndereco_Endereco? Endereco { get; private set; }
        public List<ObterFornecedorProdutosEndereco_Produtos> Produtos { get; private set; } = [];

        public ObterFornecedorProdutosEnderecoCommandResponse(
            Guid id,
            string nome,
            string documento,
            TipoFornecedorEnum tipoFornecedor,
            ObterFornecedorProdutosEndereco_Endereco? endereco,
            List<ObterFornecedorProdutosEndereco_Produtos>? produtos)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;
            Endereco = endereco;
            Produtos = produtos;
        }
    }

    public class ObterFornecedorProdutosEndereco_Endereco
    {
        public Guid Id { get; private set; }
        public string Logradouro { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Complemento { get; private set; } = string.Empty;
        public string Bairro { get; private set; } = string.Empty;
        public string Cep { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;

        public ObterFornecedorProdutosEndereco_Endereco(
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
    public class ObterFornecedorProdutosEndereco_Produtos
    {
        public string? Nome { get; private set; }
        public string? Descricao { get; private set; }
        public string? PreviewUrl { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ObterFornecedorProdutosEndereco_Produtos(
            string nome,
            string descricao,
            string? previewUrl,
            decimal valor)
        {
            Nome = nome;
            Descricao = descricao;
            PreviewUrl = previewUrl;
            Valor = valor;
            DataCadastro = DateTime.Now;

        }

        public void AdicionarPreviewUrl(string previewUrl)
        {
            PreviewUrl = previewUrl;
        }
    }
}
