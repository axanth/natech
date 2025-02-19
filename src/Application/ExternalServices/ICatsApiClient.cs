namespace Application.ExternalServices
{
    public interface ICatsApiClient
    {
        Task<ApiCatsResult[]?> GetCats(int fromPage, int howMany);
    }
}
