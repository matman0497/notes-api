namespace _02;

public class QueryFilter
{

    public string Key { get; set; }
    public string Value { get; set; }

    public static List<QueryFilter> CreateFromString(string query)
    {

        return new List<QueryFilter>();
    }
}