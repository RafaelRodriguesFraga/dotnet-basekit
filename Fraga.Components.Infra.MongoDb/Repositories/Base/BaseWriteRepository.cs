using System.Linq.Expressions;
using Fraga.Components.Domain.MongoDb.Entities.Base;
using Fraga.Components.Domain.MongoDb.Repositories.Base;
using Fraga.Components.Infra.MongoDb.DbSettings;
using MongoDB.Driver;

namespace Fraga.Components.Infra.MongoDb.Repositories.Base
{
    public class BaseWriteRepository<TEntity> : IBaseWriteRepository<TEntity> where TEntity : IBaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public BaseWriteRepository(IMongoSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }
        public void InsertOne(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public async Task<TEntity> InsertOneAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);

            return entity;
        }

        public void InsertMany(ICollection<TEntity> entities)
        {
            _collection.InsertMany(entities);
        }

        public async Task InsertManyAsync(ICollection<TEntity> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public void ReplaceOne(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            _collection.FindOneAndReplace(filter, entity);
        }

        public async Task ReplaceOneAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);

            await _collection.FindOneAndReplaceAsync(filter, entity);
        }

        public void DeleteById(Guid id)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);

            _collection.FindOneAndDelete(filter);
        }

        public async Task DeleteByIdAsync(Guid id)
        {

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, id);

            await _collection.FindOneAndDeleteAsync(filter);
        }

        public void DeleteMany(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.DeleteMany(filterExpression);
        }

        public async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            await _collection.DeleteManyAsync(filterExpression);
        }

        public void DeleteOne(Expression<Func<TEntity, bool>> filterExpression)
        {
            _collection.FindOneAndDelete(filterExpression);
        }

        public async Task DeleteOneAsync(Expression<Func<TEntity, bool>> filterExpression)
        {
            await _collection.FindOneAndDeleteAsync(filterExpression);
        }
    }
}
