namespace HomeForPets.Core.Models;

public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public long TotalCount { get; set; }
    public int PageSize { get; set; }
    public int Page { get; set; }
    public bool HasNextPage => Page * PageSize < TotalCount;
    public bool HasPreviousPage => Page > 1;
}