using EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar;
using EstartandoDevs.Application.Tests.Infra;
using EstartandoDevs.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using System.Net;

namespace EstartandoDevs.Application.Tests.Application.CasosDeUso.Forcenedores.Criar
{
    public class CriarFornecedorTests : BaseTesteBancoDeDados
    {
        private readonly AutoMocker _autoMocker;

        public CriarFornecedorTests()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.Use(AppDbContext);
        }

        [Fact]
        public async Task HandlerQuandoComandoInvalido_DeveRetornarErroDeValidacao()
        {
            // Arrange
            var command = new CriarFornecedorCommand("","123456789", 3); // Comando inválido (sem dados)

            //Act
            var handler = _autoMocker.CreateInstance<CriarFornecedorCommandHandler>();

            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Null(response.Dados);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("O nome do fornecedor deve ser informado.", response.Mensagem);
        }

        // Testes de criação de fornecedor serão implementados aqui
        [Fact]
        public async Task HandlerQuandoComandoCorreto_DeveRetornarIdFornecedor()
        {
           //Arrange
           var command = new CriarFornecedorCommand("Fornecedor Teste", "445.278.280-96", 1); // Comando válido

            //Act
            var handler = _autoMocker.CreateInstance<CriarFornecedorCommandHandler>();

            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Dados);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var fornecedor = await AppDbContext.Set<Fornecedor>()
                .FirstOrDefaultAsync(f => f.Id == (Guid)response.Dados);

            Assert.Equal(command.Nome, fornecedor.Nome);
            Assert.Equal(command.Documento, fornecedor.Documento);
            Assert.Equal(command.TipoFornecedor, (int)fornecedor.TipoFornecedor);
        }
    }
}
