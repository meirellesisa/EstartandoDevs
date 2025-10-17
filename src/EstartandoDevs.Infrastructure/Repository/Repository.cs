using EstartandoDevs.Domain.Entidades;
using EstartandoDevs.Domain.Repository;
using EstartandoDevs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EstartandoDevs.Infrastructure.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext _contexto;
        protected readonly DbSet<TEntity> _dbSet; // atalho para acessar a tabela desejada sem precisar passar o nome da tabela 

        protected Repository(AppDbContext contexto)
        {
            _contexto = contexto;
            _dbSet = _contexto.Set<TEntity>();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            var dado = await _dbSet.FindAsync(id);
            return dado;
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            var dado = await _dbSet.ToListAsync();
            return dado;
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            _dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            var dado = await _contexto.SaveChangesAsync();
            return dado;
        }

        public void Dispose()
        {
            _contexto?.Dispose();
        }
    }
}
