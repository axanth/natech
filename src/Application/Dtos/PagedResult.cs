namespace Application.Dtos
{
    public record PagedResult<T>(IEnumerable<T> Items, int Page, int PageSize, int TotalItems, int TotalPages);
}
