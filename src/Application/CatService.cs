using Domain;
using Application.Dtos;
using Application.Interfaces;
using Application.ExternalServices;

namespace Application
{
    /// <summary>
    ///  The Cats application service implementation
    /// </summary>
    /// <param name="catsRepository">An instance of the Cats Repository implementation. DI injected </param>
    /// <param name="externalCatApi">An instance of the Cats Api Client implementation. DI injected</param>
    public class CatService(ICatsRepository catsRepository, ICatsApiClient externalCatApi) : ICatServices
    {
        private readonly ICatsRepository _catsRepository = catsRepository;
        private readonly ICatsApiClient _catsApi = externalCatApi;
        private static readonly int BATCH_SIZE = 25;

        #region ICatServices
        public async Task<bool> FetchCatsAsync()
        {
            try
            {
                int dbCount = await _catsRepository.GetCatCountAsync();
                int START_FROM = dbCount > 0 ? dbCount / BATCH_SIZE : 0;

                var fetchedCats = await _catsApi.GetCats(START_FROM, BATCH_SIZE);
                if (fetchedCats != null)
                {
                    //Get the cats that do not exist in the database
                    var nonExisting = await _catsRepository.GetNonExistingCats(fetchedCats.Select(x => x.Id).ToList());

                    //filter out the existing cats from the fetched list
                    var apiCatsToProcess = fetchedCats
                        .Where(x => nonExisting.Contains(x.Id))
                        .Select(apiCat => new ApiCatModel(apiCat))
                        .ToList();

                    //if we do need to process some cats
                    if (apiCatsToProcess.Count != 0)
                    {
                        var newCats = await ProcessCats(apiCatsToProcess);
                        await _catsRepository.AddRangeAsync(newCats);
                        await _catsRepository.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<CatDto?> GetCatByIdAsync(int id)
        {
            var cat = await _catsRepository.GetCatByIdAsync(id);
            return cat == null ? default : CatDto.FromCat(cat);
        }
        public async Task<PagedResult<CatDto>> GetCatsByTagPagedAsync(int page, int pageSize, string? tagName)
        {
            int totalItems = await _catsRepository.GetCatCountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var filteredCats = await _catsRepository.GetPagedByTagAsync(page, pageSize, tagName);

            var mapped = filteredCats.Select(x => CatDto.FromCat(x)).ToList();  

            return new PagedResult<CatDto>(mapped, page, pageSize, totalItems, totalPages);
        }
        #endregion
        /// <summary>
        /// Private helper method for processing the api cats before persisting on the DB
        /// </summary>
        /// <param name="apiCats"></param>
        /// <returns></returns>
        private async Task<List<Cat>> ProcessCats(List<ApiCatModel> apiCats)
        {
            ArgumentNullException.ThrowIfNull(apiCats);

            List<Cat> newCats = [];
            foreach (var apiCat in apiCats)
            {
                var image = await ImageDownloader.GetImageAsByteArrayAsync(apiCat.ImageUrl);
                //Call helper to normalize the tag string.There where cases that the api returned the same tag like : playful and Playful
                var tags = await _catsRepository.HandleTagsAsync(apiCat.Tags.Select(t => CapitalizeEachWord(t)));  
                newCats.Add(new Cat(apiCat.CatId, apiCat.Width, apiCat.Height, image, tags));
            }
            return newCats;
        }
        /// <summary>
        ///  Helper method for string normalization
        /// </summary>
        /// <param name="input">the input string to capitalize</param>
        /// <returns>The string with each word cpitalized</returns>
        private static string CapitalizeEachWord(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;
            return string.Join(" ", input.Split(' ').Select(word => char.ToUpper(word[0]) + word[1..]));
        }
    }
}