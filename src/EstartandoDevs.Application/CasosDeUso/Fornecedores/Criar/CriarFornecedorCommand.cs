using FluentValidation;
using FluentValidation.Results;
using MediatR;

using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar
{
    public class CriarFornecedorCommand : IRequest<CriarFornecedorCommandResponse>
    {
        public string Nome { get; private set; }
        public string Documento { get; private set; }
        public int TipoFornecedor { get; private set; }

        public ValidationResult  ResultadoDasValidacoes { get; private set; }

        public CriarFornecedorCommand(string nome, string documento, int tipoFornecedor)
        {
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;         
        }

        public bool Validar()
        {
            var validacoes = new InlineValidator<CriarFornecedorCommand>();

            validacoes.RuleFor(fornecedor => fornecedor.Nome)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString()) //"BadRequest" -> "400"
                .WithMessage("O nome do fornecedor deve ser informado.");

            validacoes.RuleFor(fornecedor => fornecedor.Documento)
               .NotEmpty()
               .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
               .WithMessage("O documento do fornecedor precisa ser informado");

            validacoes.RuleFor(fornecedor => fornecedor.TipoFornecedor)
                .NotEmpty()
                .InclusiveBetween(1, 2)
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O tipo do fornecedor precisa ser 1 (Pessoa Física) ou 2 (Pessoa Jurídica)");

            ResultadoDasValidacoes = validacoes.Validate(this);
            return ResultadoDasValidacoes.IsValid;
        }

    }
}
