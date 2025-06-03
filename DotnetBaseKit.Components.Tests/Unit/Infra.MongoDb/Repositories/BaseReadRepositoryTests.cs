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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();

            mockFindFluent.Setup(f => f.FirstOrDefault(It.IsAny<CancellationToken>())).Returns(expectedEntity);

            var mockCollection = new Mock<IMongoCollection<FakeBaseEntityMongo>>();
            mockCollection
                .Setup(c => c.Find(
                    It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(), 
                    It.IsAny<FindOptions<FakeBaseEntityMongo, FakeBaseEntityMongo>>()
                ))
                .Returns(mockFindFluent.Object);

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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();
            mockFindFluent.Setup(f => f.SingleOrDefaultAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedEntity);
            _mockCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(), null))
                .Returns(mockFindFluent.Object);

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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();
            mockFindFluent.Setup(f => f.FirstOrDefault(It.IsAny<CancellationToken>())).Returns(expectedEntity);
            _mockCollection.Setup(c => c.Find(filter, null))
                .Returns(mockFindFluent.Object);

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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();
            mockFindFluent.Setup(f => f.FirstOrDefaultAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedEntity);
            _mockCollection.Setup(c => c.Find(filter, null))
                .Returns(mockFindFluent.Object);

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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();
            mockFindFluent.Setup(f => f.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(expectedList);
            _mockCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(), null))
                .Returns(mockFindFluent.Object);

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

            var mockFindFluent = new Mock<IFindFluent<FakeBaseEntityMongo, FakeBaseEntityMongo>>();
            mockFindFluent.Setup(f => f.ToList(It.IsAny<CancellationToken>())).Returns(expectedList);
            _mockCollection.Setup(c => c.Find(It.IsAny<FilterDefinition<FakeBaseEntityMongo>>(), null))
                .Returns(mockFindFluent.Object);

            var result = repo.FindAll();

            Assert.NotNull(result);
            Assert.Equal(expectedList.Count, ((List<FakeBaseEntityMongo>)result).Count);
        }
    }
}