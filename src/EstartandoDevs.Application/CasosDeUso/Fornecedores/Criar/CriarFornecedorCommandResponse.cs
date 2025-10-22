using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar
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

        public static CriarFornecedorCommandResponse ErroValidacao(string mensagem, string statusCode)
        {
            if(int.TryParse(statusCode, out var statusCodeInt) && 
                Enum.IsDefined(typeof(HttpStatusCode), statusCodeInt))
            {
                return new((HttpStatusCode)statusCodeInt, mensagem);
            }

            return new(statusCode: HttpStatusCode.InternalServerError, "Código de status inválido fornecido na resposta de erro de validação.");
        }
   

        public static CriarFornecedorCommandResponse ErroInterno(string mensagem)
            => new(statusCode: HttpStatusCode.InternalServerError, mensagem, null);
    }
}
