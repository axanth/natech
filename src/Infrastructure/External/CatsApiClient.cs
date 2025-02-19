using System.Net.Http.Headers;
using System.Net.Http.Json;

using Application.ExternalServices;

namespace Infrastructure.External
{
    public class CatsApiClient(HttpClient httpClient) : ICatsApiClient
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<ApiCatsResult[]?> GetCats(int fromPage, int howMany)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"/v1/images/search?size=med&mime_types=jpg&format=json&has_breeds=true&order=ASC&page={fromPage}&include_breeds=1&limit={howMany}");
            var content = new StringContent(string.Empty);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<ApiCatsResult[]>();    

        }
    }
}
