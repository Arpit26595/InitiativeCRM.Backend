using System.Linq.Expressions;

namespace Shared.Models.Utilities.Sorting;

public static class SortingService
{
    public static IQueryable<T> ApplySort<T>(IQueryable<T> query, string sortBy, SortDirection sortDirection) where T : class
    {
        if (string.IsNullOrWhiteSpace(sortBy))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression(parameter, sortBy);

        if (property == null)
            return query;

        var lambda = Expression.Lambda(property, parameter);
        var methodName = sortDirection == SortDirection.Asc ? "OrderBy" : "OrderByDescending";
        
        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)method.Invoke(null, new object[] { query, lambda });
    }

    private static Expression GetPropertyExpression(Expression parameter, string propertyPath)
    {
        try
        {
            var properties = propertyPath.Split('.');
            Expression property = parameter;

            foreach (var prop in properties)
            {
                var propertyInfo = property.Type.GetProperty(prop,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (propertyInfo == null)
                    return null;

                property = Expression.Property(property, propertyInfo);
            }

            return property;
        }
        catch
        {
            return null;
        }
    }
}