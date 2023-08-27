using DotnetBoilerplate.Components.Domain.Sql.Entities.Base;
using DotnetBoilerplate.Components.Domain.Sql.Repositories;
using DotnetBoilerplate.Components.Infra.Sql.Context.Base;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Components.Infra.Sql.Repositories.Base
{
    public class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly BaseContext _context;
        public DbSet<TEntity> Set { get; protected set; }

        public BaseWriteRepository(BaseContext context)
        {
            _context = context;
            Set = _context.Set<TEntity>();
        }

        public void Insert(TEntity entity)
        {
            Set.Add(entity);
            _context.SaveChanges();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await Set.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            Set.Update(entity);
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            Set.Update(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            Set.Remove(entity);

        }

        public async Task DeleteAsync(TEntity entity)
        {
            Set.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
