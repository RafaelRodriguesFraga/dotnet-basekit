﻿using DotnetBoilerplate.Components.Domain.Sql.Entities.Base;
using DotnetBoilerplate.Components.Domain.Sql.Repositories;
using DotnetBoilerplate.Components.Infra.Sql.Context.Base;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Components.Infra.Sql.Repositories.Base
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
        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }
    }
}
