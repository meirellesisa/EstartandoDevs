using FluentValidation.Results;
using System.Net;

namespace EstartandoDevs.Application.Mediator
{
    public class CommandResponse<T>
    {
        public HttpStatusCode StatusCode { get; private set; }
        public string Mensagem { get; private set; }
        public T Dados { get; private set; }

        public CommandResponse(HttpStatusCode statusCode, string mensagem, T dados)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
            Dados = dados;
        }
        public CommandResponse(HttpStatusCode statusCode, string mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }

        public static CommandResponse<T> Sucesso(T dados, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(statusCode, null, dados);

        public static CommandResponse<T> Sucesso(string mensagem, HttpStatusCode statusCode = HttpStatusCode.OK)
            => new(statusCode, mensagem);

        public static CommandResponse<T> AdicionarErro(string mensagem, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            => new(statusCode, mensagem);

        public static CommandResponse<T> ErroValidacao(string mensagem, string statusCode)
        {
            if (int.TryParse(statusCode, out var statusCodeInt) &&
                Enum.IsDefined(typeof(HttpStatusCode), statusCodeInt))
            {
                return new((HttpStatusCode)statusCodeInt, mensagem);
            }

            return new(HttpStatusCode.InternalServerError, "Código de status inválido fornecido na resposta de erro de validação.");
        }

        public static CommandResponse<T> ErroValidacao(ValidationResult resultadoDasValidacoes)
        {
            var erros = resultadoDasValidacoes.Errors
                .Select(f => new { mensagem = f.ErrorMessage, statusCode = f.ErrorCode })
                .ToList();

            if (int.TryParse(erros.First().statusCode, out var statusCodeInt) &&
                Enum.IsDefined(typeof(HttpStatusCode), statusCodeInt))
            {
                return new((HttpStatusCode)statusCodeInt, erros.First().mensagem);
            }

            return new(HttpStatusCode.InternalServerError, "Código de status inválido fornecido na resposta de erro de validação.");
        }

        public static CommandResponse<T> ErroCritico(string mensagem)
            => new(HttpStatusCode.InternalServerError, mensagem);

    }
}

