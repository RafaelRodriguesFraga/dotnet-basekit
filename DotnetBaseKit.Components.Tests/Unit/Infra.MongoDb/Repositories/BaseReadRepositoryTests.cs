using System.Linq.Expressions;
using DotnetBaseKit.Components.Infra.MongoDb.DbSettings;
using DotnetBaseKit.Components.Infra.MongoDb.Repositories.Base;
using DotnetBaseKit.Components.Tests.Mocks;
using MongoDB.Driver;
using Moq;

namespace DotnetBaseKit.Components.Tests.Unit.Infra.MongoDb.Repositories
{
    public class BaseReadRepositoryTests
    {
        private Mock<IMongoClient> _mockMongoClient;
        private Mock<IMongoDatabase> _mockDatabase;
        private Mock<IMongoCollection<FakeBaseEntityMongo>> _mockCollection;
        private Mock<IMongoSettings> _mockSettings;

        private static void SetupFind(
            Mock<IMongoCollection<FakeBaseEntityMongo>> collection,
            IEnumerable<FakeBaseEntityMongo> entities)
        {
            var cursor = new Mock<IAsyncCursor<FakeBaseEntityMongo>>();
            cursor.SetupGet(c => c.Current).Returns(entities);
            cursor.SetupSequence(c => c.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            cursor.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            collection.Setup(c => c.FindSync<FakeBaseEntityMongo>(
                    It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(),
                    It.IsAny<FindOptions<FakeBaseEntityMongo, FakeBaseEntityMongo>>(),
                    It.IsAny<CancellationToken>()))
                .Returns(cursor.Object);
            collection.Setup(c => c.FindAsync<FakeBaseEntityMongo>(
                    It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(),
                    It.IsAny<FindOptions<FakeBaseEntityMongo, FakeBaseEntityMongo>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(cursor.Object);
        }

        private BaseReadRepository<FakeBaseEntityMongo> CreateRepository()
        {
            _mockMongoClient = new Mock<IMongoClient>();
            _mockDatabase = new Mock<IMongoDatabase>();
            _mockCollection = new Mock<IMongoCollection<FakeBaseEntityMongo>>();
            _mockSettings = new Mock<IMongoSettings>();

            _mockSettings.Setup(s => s.DatabaseName).Returns("TestDb");
            _mockMongoClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null)).Returns(_mockDatabase.Object);
            _mockDatabase.Setup(db => db.GetCollection<FakeBaseEntityMongo>(It.IsAny<string>(), null))
                .Returns(_mockCollection.Object);

            return new BaseReadRepository<FakeBaseEntityMongo>(_mockMongoClient.Object, _mockSettings.Object);
        }

        [Fact(DisplayName = "Should return entity when FindById finds it")]
        public void Should_Return_Entity_When_FindById_finds_it()
        {
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedEntity = new FakeBaseEntityMongo(fakeId, fakeCreatedAt);

            var mockCollection = new Mock<IMongoCollection<FakeBaseEntityMongo>>();
            SetupFind(mockCollection, [expectedEntity]);

            var mockDatabase = new Mock<IMongoDatabase>();
            mockDatabase.Setup(db => db.GetCollection<FakeBaseEntityMongo>(It.IsAny<string>(), null))
                .Returns(mockCollection.Object);

            var mockClient = new Mock<IMongoClient>();
            mockClient.Setup(c => c.GetDatabase(It.IsAny<string>(), null))
                .Returns(mockDatabase.Object);

            var mockSettings = new Mock<IMongoSettings>();
            mockSettings.SetupGet(s => s.DatabaseName).Returns("FakeDatabase");

            var repository = new BaseReadRepository<FakeBaseEntityMongo>(mockClient.Object, mockSettings.Object);

            var entity = repository.FindById(fakeId);

            Assert.NotNull(entity);
            Assert.Equal(expectedEntity, entity);
        }

        [Fact(DisplayName = "Should return entity asynchronously when FindByIdAsync finds it")]
        public async Task FindByIdAsync_ShouldReturnEntity_WhenFound()
        {
            var repo = CreateRepository();
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedEntity = new FakeBaseEntityMongo(fakeId, fakeCreatedAt);

            SetupFind(_mockCollection, [expectedEntity]);

            var result = await repo.FindByIdAsync(fakeId);

            Assert.NotNull(result);
            Assert.Equal(fakeId, result.Id);
        }

        [Fact(DisplayName = "Should return entity when FindOne finds it")]
        public void FindOne_ShouldReturnEntity_WhenFound()
        {
            var repo = CreateRepository();
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedEntity = new FakeBaseEntityMongo(fakeId, fakeCreatedAt);
            Expression<Func<FakeBaseEntityMongo, bool>> filter = x => x.Id == expectedEntity.Id;

            SetupFind(_mockCollection, [expectedEntity]);

            var result = repo.FindOne(filter);

            Assert.NotNull(result);
            Assert.Equal(expectedEntity.Id, result.Id);
        }

        [Fact(DisplayName = "Should return entity asynchronously when FindOneAsync finds it")]
        public async Task FindOneAsync_ShouldReturnEntity_WhenFound()
        {
            var repo = CreateRepository();
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedEntity = new FakeBaseEntityMongo(fakeId, fakeCreatedAt);

            Expression<Func<FakeBaseEntityMongo, bool>> filter = x => x.Id == expectedEntity.Id;

            SetupFind(_mockCollection, [expectedEntity]);

            var result = await repo.FindOneAsync(filter);

            Assert.NotNull(result);
            Assert.Equal(expectedEntity.Id, result.Id);
        }

        [Fact(DisplayName = "Should return all entities asynchronously when FindAllAsync is called")]
        public async Task FindAllAsync_ShouldReturnAllEntities()
        {
            var repo = CreateRepository();
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedList = new List<FakeBaseEntityMongo>
            {
                new FakeBaseEntityMongo(fakeId, fakeCreatedAt),
                new FakeBaseEntityMongo(fakeId, fakeCreatedAt)
            };

            SetupFind(_mockCollection, expectedList);

            var result = await repo.FindAllAsync();

            Assert.NotNull(result);
            Assert.Equal(expectedList.Count, ((List<FakeBaseEntityMongo>)result).Count);
        }

        [Fact(DisplayName = "Should return all entities when FindAll is called")]
        public void FindAll_ShouldReturnAllEntities()
        {
            var repo = CreateRepository();
            var fakeId = Guid.NewGuid();
            var fakeCreatedAt = DateTime.Now;
            var expectedList = new List<FakeBaseEntityMongo>
            {
                new FakeBaseEntityMongo(fakeId, fakeCreatedAt),
                new FakeBaseEntityMongo(fakeId, fakeCreatedAt)
            };

            SetupFind(_mockCollection, expectedList);

            var result = repo.FindAll();

            Assert.NotNull(result);
            Assert.Equal(expectedList.Count, ((List<FakeBaseEntityMongo>)result).Count);
        }
    }
}
