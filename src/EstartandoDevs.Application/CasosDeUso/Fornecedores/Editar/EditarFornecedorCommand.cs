using EstartandoDevs.Application.Mediator;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Editar
{
    public class EditarFornecedorCommand : IRequest<CommandResponse<Unit>>
    {
        public Guid Id { get; set; }

        public string? Nome { get; set; }

        public string? Documento { get; set; }

        public int TipoFornecedor { get; set; }

        public ValidationResult ResultadoDasValidacoes { get; private set; }

        public EditarFornecedorCommand(Guid id, string nome, string documento, int tipoFornecedor)
        {
            Id = id;
            Nome = nome;
            Documento = documento;
            TipoFornecedor = tipoFornecedor;
        }
        public bool Validar()
        {
            var validacoes = new InlineValidator<EditarFornecedorCommand>();

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
