namespace Catalog.API.Dtos;

public class PaginatedItems<TEntity> where TEntity : class
{
    public PaginatedItems(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
    {
        this.PageIndex = pageIndex;
        this.PageSize = pageSize;
        this.Count = count;
        this.Data = data;
    }
    public int PageIndex { get; }

    public int PageSize { get; }

    public long Count { get; }
    public long TotalPage => (long)Math.Ceiling(Count / (double)PageSize);

    public IEnumerable<TEntity> Data { get;}
}