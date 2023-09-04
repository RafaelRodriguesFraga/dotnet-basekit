using DotnetBoilerplate.Components.Domain.MongoDb.Entities.Base;
using System.Linq.Expressions;

namespace DotnetBoilerplate.Components.Domain.MongoDb.Repositories.Base
{
    public interface IBaseReadRepository<TEntity> where TEntity : class, IBaseEntity
    {
        IQueryable<TEntity> AsQueryable();    
        TEntity FindOne(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> filterExpression);
        TEntity FindById(Guid id);
        Task<TEntity> FindByIdAsync(Guid id);
        IEnumerable<TEntity> FindAll();
        Task<IEnumerable<TEntity>> FindAllAsync();
    }
}