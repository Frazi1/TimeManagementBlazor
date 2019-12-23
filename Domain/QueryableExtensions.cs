using System.Linq;

namespace Domain
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, Filter filter)
        {
            return query
                .Skip(filter.Skip)
                .Take(filter.Take);
        }
    }
}