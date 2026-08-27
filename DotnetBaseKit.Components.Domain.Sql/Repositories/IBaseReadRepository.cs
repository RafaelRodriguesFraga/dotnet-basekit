using DotnetBaseKit.Components.Domain.Sql.Entities.Base;

namespace DotnetBaseKit.Components.Domain.Sql.Repositories
{
    public interface IBaseReadRepository<TEntity> where TEntity : IBaseEntity
    {
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<(IEnumerable<TEntity> result, int totalRecords)> GetAllPaginatedAsync(int page, int quantityPerPage);
    }
}
