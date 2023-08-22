using DotnetBoilerplate.Components.Domain.MongoDb.Entities.Base;
using System.Linq.Expressions;

namespace DotnetBoilerplate.Components.Domain.MongoDb.Repositories.Base
{
    public interface IBaseWriteRepository<TEntity> where TEntity : IBaseEntity
    {
        void InsertOne(TEntity entity);
        Task<TEntity> InsertOneAsync(TEntity entity);
        void InsertMany(ICollection<TEntity> entities);
        Task InsertManyAsync(ICollection<TEntity> entities);
        void ReplaceOne(TEntity document);

        Task ReplaceOneAsync(TEntity document);

        void DeleteOne(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression);

        void DeleteById(Guid id);

        Task DeleteByIdAsync(Guid id);

        void DeleteMany(Expression<Func<TEntity, bool>> filterExpression);

        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression);
    }
}
