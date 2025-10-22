using EstartandoDevs.Infrastructure.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EstartandoDevs.Application.Tests.Infra
{
    public abstract class BaseTesteBancoDeDados : IDisposable
    {
        private AppDbContext _context;

        private SqliteConnection _sqliteConnection;

        public AppDbContext AppDbContext
        {
            get
            {
                _context ??= CriarConexao();

                return _context;
            }
        }

        // Método responsável por criar e configurar o banco de dados em memória
        private AppDbContext CriarConexao()
        {
            //1. Cria uma nova conexão com o SQLite setando o banco em memoria:
            _sqliteConnection = new SqliteConnection("DataSource=:memory:");

            // 2.Abre a conexão
            _sqliteConnection.Open();

            //3.Cria as opções de configuração para o AppDbContext.
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_sqliteConnection)
                .LogTo(mensagem => Debug.WriteLine(mensagem))
                .Options;

            //4.Cria uma nova instância do AppDbContext com as opções configuradas
            var context = new AppDbContext(options);

            //5.Desativa temporariamente as chaves estrangeiras
            context.Database.ExecuteSqlRaw("PRAGMA foreign_keys = OFF;");

            //6.Garante que o banco de dados e as tabelas sejam criadas
            context.Database.EnsureCreated();

            return context;
        }

        // Método responsável por liberar recursos
        public void Dispose()
        {
            // Fecha e descarta o contexto se ele existir
            _context?.Dispose();

            // Fecha a conexão SQLite
            _sqliteConnection?.Close();

            //descarta a conexão SQLite, destruindo o banco em memória
            _sqliteConnection?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
