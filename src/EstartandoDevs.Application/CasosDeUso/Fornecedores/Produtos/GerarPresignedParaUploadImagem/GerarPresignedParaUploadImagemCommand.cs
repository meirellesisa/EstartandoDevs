using EstartandoDevs.Application.Mediator;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Net;

namespace EstartandoDevs.Application.CasosDeUso.Fornecedores.Produtos.GerarPresignedParaUploadImagem
{
    public class GerarPresignedParaUploadImagemCommand : IRequest<CommandResponse<GerarPresignedParaUploadImagemResponse>>
    {
        public Guid FornecedorId { get; private set; }
        public Guid ProdutoId { get; private set; }
        public string NomeArquivo { get; private set; }
        public ValidationResult ResultadoDasValidacoes { get; private set; }

        public GerarPresignedParaUploadImagemCommand(Guid fornecedorId, Guid produtoId, string nomeArquivo)
        {
            FornecedorId = fornecedorId;
            ProdutoId = produtoId;
            NomeArquivo = nomeArquivo;
        }

        public bool Validar()
        {
            var validacoes = new InlineValidator<GerarPresignedParaUploadImagemCommand>();

            validacoes.RuleFor(fornecedor => fornecedor.FornecedorId)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString()) //"BadRequest" -> "400"
                .WithMessage("O id do fornecedor deve ser informado.");

            validacoes.RuleFor(fornecedor => fornecedor.ProdutoId)
               .NotEmpty()
               .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
               .WithMessage("O id do produto precisa ser informado.");

            validacoes.RuleFor(fornecedor => fornecedor.NomeArquivo)
                .NotEmpty()
                .WithErrorCode(((int)HttpStatusCode.BadRequest).ToString())
                .WithMessage("O nome do arquivo precisa ser informado.");

            ResultadoDasValidacoes = validacoes.Validate(this);
            return ResultadoDasValidacoes.IsValid;
        }
    }
}
