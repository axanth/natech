using Application.ExternalServices;

namespace Application.Dtos
{
    /// <summary>
    /// An internal class that holds only the data we need from the fetched data for processing
    /// </summary>
    internal class ApiCatModel(ApiCatsResult cats)
    {
        public string CatId { get; set; } = cats.Id;
        public int Width { get; set; } = cats.Width;
        public int Height { get; set; } = cats.Height;
        public string ImageUrl { get; set; } = cats.Url;
        public string[] Tags { get; set; } = cats.Breeds.FirstOrDefault()?.Temperament.Split(',').Select(x => x.Trim()).ToArray() ?? [];
    }
}
