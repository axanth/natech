using Application.Dtos;

namespace Application.Interfaces
{
    /// <summary>
    /// The contract definition of the Cats Application service
    /// </summary>
    public interface ICatServices
    {
        Task<bool> FetchCatsAsync();
        Task<CatDto?> GetCatByIdAsync(int id);
        Task<PagedResult<CatDto>> GetCatsByTagPagedAsync(int page, int pageSize, string? tagName);
    }
}
