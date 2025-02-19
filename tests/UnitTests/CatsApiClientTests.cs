namespace UnitTests
{
    using System.Threading.Tasks;
    using Application.ExternalServices;
    using Moq;
    using Xunit;

    public class CatsApiClientTests
    {
        private readonly Mock<ICatsApiClient> _mockApiClient;
        private readonly ApiCatsResult[] _apiCatsResults;
        private readonly string TEST_ID = "TGuAku7fM";
        private readonly string TEST_URL = "https://cdn2.thecatapi.com/images/aU69p2mTT.jpg";
        public CatsApiClientTests()
        {
            _mockApiClient = new Mock<ICatsApiClient>();
            _apiCatsResults = [new ApiCatsResult { Id = TEST_ID, Url = TEST_URL }];
        }

        [Fact]
        public async Task GetCats_ShouldReturnApiCatsResults_WhenCalled()
        {
            _mockApiClient.Setup(client => client.GetCats(1, 10))
                          .ReturnsAsync(_apiCatsResults);

            var result = await _mockApiClient.Object.GetCats(1, 10);

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(TEST_ID, result[0].Id);
            Assert.Equal(TEST_URL, result[0].Url);
        }
    }

}
