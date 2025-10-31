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

        public ValidationResult ResultadoDasValidacoes { get; private set; }

        public EditarFornecedorCommand(Guid id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public bool Validar()
        {
            var validacoes = new InlineValidator<EditarFornecedorCommand>();

            validacoes.RuleFor(fornecedor => fornecedor.Nome)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString()) //"BadRequest" -> "400"
                .WithMessage("O nome do fornecedor deve ser informado.");

            ResultadoDasValidacoes = validacoes.Validate(this);
            return ResultadoDasValidacoes.IsValid;
        }

    }
}
