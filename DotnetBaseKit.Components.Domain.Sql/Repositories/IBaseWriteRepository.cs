using DotnetBaseKit.Components.Domain.Sql.Entities.Base;

namespace DotnetBaseKit.Components.Domain.Sql.Repositories
{
    public interface IBaseWriteRepository<TEntity> where TEntity : IBaseEntity
    {
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        Task UpdateAsync(TEntity entity);
        void Delete(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
