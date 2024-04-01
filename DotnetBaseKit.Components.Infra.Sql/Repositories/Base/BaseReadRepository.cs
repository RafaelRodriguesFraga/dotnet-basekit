using DotnetBaseKit.Components.Domain.Sql.Entities.Base;
using DotnetBaseKit.Components.Domain.Sql.Repositories;
using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using Microsoft.EntityFrameworkCore;

namespace DotnetBaseKit.Components.Infra.Sql.Repositories.Base
{
    public class BaseReadRepository<TEntity> : IBaseReadRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly BaseContext _context;
        public DbSet<TEntity> Set { get; protected set; }

        public BaseReadRepository(BaseContext context)
        {
            _context = context;
            Set = _context.Set<TEntity>();
        }
        public TEntity GetById(Guid id)
        {
            return Set.Find(id);
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }            
    }
}
