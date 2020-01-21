using System;
using System.Linq;
using System.Linq.Expressions;

namespace Domain
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter.Skip.HasValue)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take.HasValue)
                query = query.Take(filter.Take.Value);

            return query;
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, Filter filter)
        {
            if (filter.Sortings.Any())
            {
                PropertySorting propertySorting = filter.Sortings[0];
                query = query.OrderBy(propertySorting.PropertyName, propertySorting.IsAscending);
            }

            return filter.Sortings
                .Skip(1)
                .Aggregate(query, (queryable, sorting) => queryable.ThenBy(sorting.PropertyName, sorting.IsAscending));
        }

        private static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string property, bool isAscending)
        {
            Type type = typeof(T);
            var p = Expression.Parameter(type, "p");
            var propertyExpr = Expression.Property(p, property);
            var lambdaPropertyExpr = Expression.Lambda(propertyExpr, p);
            string sortMethod = isAscending
                ? nameof(Queryable.OrderBy)
                : nameof(Queryable.OrderByDescending);

            var result = Expression.Call(
                typeof(Queryable),
                sortMethod,
                new[] {type, propertyExpr.Type},
                query.Expression,
                Expression.Quote(lambdaPropertyExpr)
            );

            return query.Provider.CreateQuery<T>(result);
        }

        private static IQueryable<T> ThenBy<T>(this IQueryable<T> query, string property, bool isAscending)
        {
            Type type = typeof(T);
            var p = Expression.Parameter(type, "p");
            var propertyExpr = Expression.Property(p, property);
            var lambdaPropertyExpr = Expression.Lambda(propertyExpr, p);
            string sortMethod = isAscending
                ? nameof(Queryable.ThenBy)
                : nameof(Queryable.ThenByDescending);

            var result = Expression.Call(
                typeof(Queryable),
                sortMethod,
                new[] {type, propertyExpr.Type},
                query.Expression,
                Expression.Quote(lambdaPropertyExpr));

            return query.Provider.CreateQuery<T>(result);
        }
    }
}