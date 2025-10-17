using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedor.Criar
{
    public class CriarFornecedorCommandResponse
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Mensagem { get; private set; }
        public object Dados { get; private set; }

        private CriarFornecedorCommandResponse(HttpStatusCode statusCode, string mensagem = null, object dado = null )
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Dados = dado;       
        }

        public static CriarFornecedorCommandResponse Sucesso(object dados, HttpStatusCode statusCode)
           => new(statusCode, null, dados);

        public static CriarFornecedorCommandResponse ErroValidacao(List<string> mensagem, HttpStatusCode statusCode)
            => new(statusCode, mensagem.FirstOrDefault(), null);

        public static CriarFornecedorCommandResponse ErroInterno(string mensagem,HttpStatusCode statusCode)
            => new(statusCode, mensagem, null);
    }
}
