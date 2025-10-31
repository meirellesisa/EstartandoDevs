using EstartandoDevs.Application.CasosDeUso.Fornecedores.Criar;
using EstartandoDevs.Application.CasosDeUso.Fornecedores.Editar;
using EstartandoDevs.Application.Tests.Infra;
using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Repository;
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

            var fornecedorRepository = new FornecedorRepository(AppDbContext);
            _autoMocker.Use<IFornecedorRepository>(fornecedorRepository);
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

        // Testes de criação de fornecedorMock serão implementados aqui
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
                .FirstOrDefaultAsync(f => f.Id == response.Dados.Id);

            Assert.Equal(command.Nome, fornecedor.Nome);
            Assert.Equal(command.Documento, fornecedor.Documento);
            Assert.Equal(command.TipoFornecedor, (int)fornecedor.TipoFornecedor);
        }

        // Testes de criação de fornecedorMock serão implementados aqui
        [Fact]
        public async Task HandlerQuandoComandoCorreto_DeveRetornarIdFornecedorTeste()
        {
            var fornecedorMock = new Fornecedor("Fornecedor Existente", "123.456.789-00", 1);
            await AppDbContext.Set<Fornecedor>().AddAsync(fornecedorMock);
            await AppDbContext.SaveChangesAsync();
            //Arrange
            var command = new EditarFornecedorCommand(fornecedorMock.Id,"Fornecedor Teste", "445.278.280-96", 1); // Comando válido

            //Act
            var handler = _autoMocker.CreateInstance<EditarFornecedorCommandHandler>();

            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            
            var fornecedor = await AppDbContext.Set<Fornecedor>()
                .FirstOrDefaultAsync(f => f.Id == (Guid)fornecedorMock.Id);

            Assert.Equal(command.Nome, fornecedorMock.Nome);
            Assert.Equal(command.Documento, fornecedorMock.Documento);
            Assert.Equal(command.TipoFornecedor, (int)fornecedorMock.TipoFornecedor);
        }
    }
}
