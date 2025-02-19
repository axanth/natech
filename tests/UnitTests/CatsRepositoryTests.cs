namespace UnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Interfaces;
    using Domain;
    using Moq;
    using Xunit;

    public class CatsRepositoryTests
    {
        private readonly Mock<ICatsRepository> _mockRepo;
        private readonly List<Cat> _cats;
        private readonly string TEST_CAT_ID_1 = "EHG3sOpAM";
        private readonly string TEST_CAT_ID_2 = "0XYvRd7oD";
        public CatsRepositoryTests()
        {
            _mockRepo = new Mock<ICatsRepository>();
            _cats =
            [
                new(TEST_CAT_ID_1,10,10,[],[]),
                new(TEST_CAT_ID_2,10,10,[],[]),
            ];
        }

        [Fact]
        public async Task GetCatByIdAsync_ShouldReturnCat_WhenCatExists()
        {
            _mockRepo.Setup(repo => repo.GetCatByIdAsync(1))
                     .ReturnsAsync(_cats.First(c => c.CatId == TEST_CAT_ID_1));

            var result = await _mockRepo.Object.GetCatByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(TEST_CAT_ID_1, result.CatId);
        }

        [Fact]
        public async Task GetPagedByTagAsync_ShouldReturnCats_WhenCatsExist()
        {
            _mockRepo.Setup(repo => repo.GetPagedByTagAsync(1, 10, "Fluffy"))
                     .ReturnsAsync(_cats);

            var result = await _mockRepo.Object.GetPagedByTagAsync(1, 10, "Fluffy");

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetNonExistingCats_ShouldReturnNonExistingCatIds()
        {
            var fetchedIds = new List<string> { "1", "3" };
            var nonExisting = new List<string> { "3" };

            _mockRepo.Setup(repo => repo.GetNonExistingCats(fetchedIds))
                     .ReturnsAsync(nonExisting);

            var result = await _mockRepo.Object.GetNonExistingCats(fetchedIds);

            Assert.Single(result);
            Assert.Equal("3", result[0]);
        }

        [Fact]
        public async Task GetCatCountAsync_ShouldReturnCorrectCount()
        {
            _mockRepo.Setup(repo => repo.GetCatCountAsync())
                     .ReturnsAsync(2);

            var result = await _mockRepo.Object.GetCatCountAsync();

            Assert.Equal(2, result);
        }

        [Fact]
        public async Task AddRangeAsync_ShouldInvokeOnce()
        {
            _mockRepo.Setup(repo => repo.AddRangeAsync(It.IsAny<IEnumerable<Cat>>()))
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.AddRangeAsync(_cats);

            _mockRepo.Verify(repo => repo.AddRangeAsync(It.IsAny<IEnumerable<Cat>>()), Times.Once);
        }

        [Fact]
        public async Task HandleTagsAsync_ShouldReturnTagList()
        {
            var tagNames = new List<string> { "Cute", "Fluffy" };
            var tags = new List<Tag> { new("Cute") , new("Fluffy") };

            _mockRepo.Setup(repo => repo.HandleTagsAsync(tagNames))
                     .ReturnsAsync(tags);

            var result = await _mockRepo.Object.HandleTagsAsync(tagNames);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, t => t.Name == "Cute");
            Assert.Contains(result, t => t.Name == "Fluffy");
        }

        [Fact]
        public async Task SaveChangesAsync_ShouldInvokeOnce()
        {
            _mockRepo.Setup(repo => repo.SaveChangesAsync())
                     .Returns(Task.CompletedTask)
                     .Verifiable();

            await _mockRepo.Object.SaveChangesAsync();

            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }

}
