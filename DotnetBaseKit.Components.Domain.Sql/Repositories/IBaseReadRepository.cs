using DotnetBaseKit.Components.Domain.Sql.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetBaseKit.Components.Domain.Sql.Repositories
{
    public interface IBaseReadRepository<TEntity> where TEntity : IBaseEntity
    {
        TEntity GetById(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
