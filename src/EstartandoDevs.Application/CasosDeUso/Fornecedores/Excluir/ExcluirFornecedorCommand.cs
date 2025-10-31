using EstartandoDevs.Application.CasosDeUso.Fornecedores.Editar;
using EstartandoDevs.Application.Mediator;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Excluir
{
    public class ExcluirFornecedorCommand : IRequest<CommandResponse<Unit>>
    {
        public Guid Id { get; private set; }
        public ValidationResult ResultadoDasValidacoes { get; private set; }


        public ExcluirFornecedorCommand(Guid id)
        {
            Id = id;
        }

        public bool Validar()
        {
            var validacoes = new InlineValidator<ExcluirFornecedorCommand>();

            validacoes.RuleFor(fornecedor => fornecedor.Id)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString()) 
                .WithMessage("O nome do fornecedor deve ser informado.");

            
            ResultadoDasValidacoes = validacoes.Validate(this);
            return ResultadoDasValidacoes.IsValid;
        }
    }
}
