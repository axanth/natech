namespace Application
{
    /// <summary>
    /// Just a helper wrapper class for http image downlading
    /// </summary>
    public class ImageDownloader
    {
        private static readonly HttpClient _httpClient = new();

        public static async Task<byte[]> GetImageAsByteArrayAsync(string imageUrl)
        {
            try
            {
                return await _httpClient.GetByteArrayAsync(imageUrl);
            }
            catch (Exception ex)
            {
                //Better handling here
                Console.WriteLine($"Error downloading image: {ex.Message}");
                return []; // Return empty array on failure
            }
        }
    }
}
