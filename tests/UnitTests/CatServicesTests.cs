namespace UnitTests
{
    using System.Threading.Tasks;
    using Application.Dtos;
    using Application.Interfaces;
    using Moq;
    using Xunit;

    public class CatServicesTests
    {
        private readonly Mock<ICatServices> _mockService;
        private readonly CatDto _catDto;

        public CatServicesTests()
        {
            _mockService = new Mock<ICatServices>();
            _catDto = new CatDto { Id = 1, CatId = "itfFA4NWS" };
        }

        [Fact]
        public async Task FetchCatsAsync_ShouldReturnTrue()
        {
            _mockService.Setup(service => service.FetchCatsAsync())
                        .ReturnsAsync(true);

            var result = await _mockService.Object.FetchCatsAsync();

            Assert.True(result);
        }

        [Fact]
        public async Task GetCatByIdAsync_ShouldReturnCatDto_WhenCatExists()
        {
            _mockService.Setup(service => service.GetCatByIdAsync(1))
                        .ReturnsAsync(_catDto);

            var result = await _mockService.Object.GetCatByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("itfFA4NWS", result.CatId);
        }

        [Fact]
        public async Task GetCatsByTagPagedAsync_ShouldReturnPagedResult()
        {
            var pagedResult = new PagedResult<CatDto>([_catDto], 1, 10, 1, 1);
                
            
            _mockService.Setup(service => service.GetCatsByTagPagedAsync(1, 10, "Fluffy"))
                        .ReturnsAsync(pagedResult);

            var result = await _mockService.Object.GetCatsByTagPagedAsync(1, 10, "Fluffy");

            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal(1, result.TotalItems);
        }
    }
}
