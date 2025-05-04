using System.Runtime.CompilerServices;

namespace _02;

public class QueryFilter
{
    public string Key { get; set; }
    public string Value { get; set; }

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

    public static ValueTask<QueryFilter?> BindAsync(HttpContext context)
    {
        var filter = new QueryFilter();

        var query = context.Request.Query.ToList();

        if (query.Count > 0)
        {
            return ValueTask.FromResult<QueryFilter?>(filter);
        }

        foreach (var item in query)
        {
            filter.AddFilter(new QueryFilter() { Key = item.Key, Value = item.Value.ToString() });
        }

        return ValueTask.FromResult<QueryFilter?>(filter);
    }
}