using Domain;

namespace Application.Interfaces
{
    /// <summary>
    /// The cats repository interface
    /// </summary>
    public interface ICatsRepository
    {
        Task<Cat?> GetCatByIdAsync(int id);
        Task<List<Cat>> GetPagedByTagAsync(int page, int pageSize, string? tagName);
        Task<List<string>> GetNonExistingCats(List<string> fetchedCatsIds);
        Task<int> GetCatCountAsync();
        Task AddRangeAsync(IEnumerable<Cat> cats);
        Task<List<Tag>> HandleTagsAsync(IEnumerable<string> tagNames);
        Task SaveChangesAsync();
    }
}
