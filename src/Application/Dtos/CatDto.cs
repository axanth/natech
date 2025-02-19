using Domain;

namespace Application.Dtos
{
    public class CatDto
    {
        public int Id { get; set; }
        public string CatId { get; set; } = string.Empty;
        public int Width { get; set; }
        public int Height { get; set; }
        public string [] Tags { get; set; } = [];

        public static CatDto FromCat(Cat cat) =>
           new()
           {
               Id = cat.Id,
               CatId = cat.CatId,
               Height = cat.Height,
               Width = cat.Width,
               Tags = cat.Tags.Select(x => x.Name).ToArray()
           };
    }
}