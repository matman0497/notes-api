using System.Runtime.CompilerServices;

namespace _02;

public class HttpQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; } = 10;
    private List<QueryFilter> _filters = new List<QueryFilter>();

    private void AddFilter(QueryFilter filter)
    {
        _filters.Add(filter);
    }

    public bool HasFilters()
    {
        return _filters.Any();
    }

    public List<QueryFilter> GetFilters()
    {
        return _filters;
    }

    public static ValueTask<HttpQuery?> BindAsync(HttpContext context)
    {
        var filter = new HttpQuery();

        var query = context.Request.Query.ToList();

        foreach (var item in query)
        {
            if (item.Key == "page")
            {
                filter.Page = int.Parse(item.Value.ToString());
                continue;
            }

            if (item.Key == "pageSize")
            {
                filter.PageSize = int.Parse(item.Value.ToString());
                continue;
            }

            filter.AddFilter(new QueryFilter() { Key = item.Key, Value = item.Value.ToString() });
        }

        return ValueTask.FromResult<HttpQuery?>(filter);
    }
}