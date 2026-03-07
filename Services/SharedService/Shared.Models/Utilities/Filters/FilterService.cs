using System.Linq.Expressions;

namespace Shared.Models.Utilities.Filters;

public static class FilterService
{
    public static IQueryable<T> ApplyFilters<T>(IQueryable<T> query, List<Filter<object>> filters) where T : class
    {
        if (filters == null || !filters.Any())
            return query;

        foreach (var filter in filters)
        {
            if (string.IsNullOrWhiteSpace(filter.Value))
                continue;

            query = ApplyFilter(query, filter);
        }

        return query;
    }

    private static IQueryable<T> ApplyFilter<T>(IQueryable<T> query, Filter<object> filter) where T : class
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = GetPropertyExpression(parameter, filter.Id);

        if (property == null)
            return query; // Property not found, skip filter

        var filterExpression = BuildFilterExpression(property, filter, parameter);
        if (filterExpression != null)
        {
            var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

    private static Expression BuildFilterExpression(Expression property, Filter<object> filter, ParameterExpression parameter)
    {
        var propertyType = property.Type;
        
        // Handle nullable types
        var underlyingType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

        return filter.Operation switch
        {
            Operation.Eq => BuildEqualsExpression(property, filter.Value, underlyingType),
            Operation.Neq => BuildNotEqualsExpression(property, filter.Value, underlyingType),
            Operation.Gt => BuildGreaterThanExpression(property, filter.Value, underlyingType),
            Operation.Lt => BuildLessThanExpression(property, filter.Value, underlyingType),
            Operation.Gte => BuildGreaterThanOrEqualExpression(property, filter.Value, underlyingType),
            Operation.Lte => BuildLessThanOrEqualExpression(property, filter.Value, underlyingType),
            Operation.Contains => BuildContainsExpression(property, filter.Value),
            Operation.Between => BuildBetweenExpression(property, filter.Value, underlyingType),
            _ => null
        };
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

    private static Expression BuildEqualsExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.Equal(property, constant);
    }

    private static Expression BuildNotEqualsExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.NotEqual(property, constant);
    }

    private static Expression BuildGreaterThanExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.GreaterThan(property, constant);
    }

    private static Expression BuildLessThanExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.LessThan(property, constant);
    }

    private static Expression BuildGreaterThanOrEqualExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.GreaterThanOrEqual(property, constant);
    }

    private static Expression BuildLessThanOrEqualExpression(Expression property, string value, Type targetType)
    {
        if (!TryConvertValue(value, targetType, out var convertedValue))
            return null;

        var constant = Expression.Constant(convertedValue, property.Type);
        return Expression.LessThanOrEqual(property, constant);
    }

    private static Expression BuildContainsExpression(Expression property, string value)
    {
        if (property.Type != typeof(string))
            return null;

        var constant = Expression.Constant(value, typeof(string));
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        return Expression.Call(property, containsMethod, constant);
    }

    private static Expression BuildBetweenExpression(Expression property, string value, Type targetType)
    {
        var parts = value.Split('-');
        if (parts.Length != 2)
            return null;

        // Handle "500000+" case
        if (parts[1].EndsWith("+"))
        {
            if (TryConvertValue(parts[0], targetType, out var minValue))
            {
                var minConstant = Expression.Constant(minValue, property.Type);
                return Expression.GreaterThanOrEqual(property, minConstant);
            }
        }
        else if (TryConvertValue(parts[0], targetType, out var min) && TryConvertValue(parts[1], targetType, out var max))
        {
            var minConstant = Expression.Constant(min, property.Type);
            var maxConstant = Expression.Constant(max, property.Type);
            var greaterThan = Expression.GreaterThanOrEqual(property, minConstant);
            var lessThan = Expression.LessThanOrEqual(property, maxConstant);
            return Expression.AndAlso(greaterThan, lessThan);
        }

        return null;
    }

    private static bool TryConvertValue(string value, Type targetType, out object result)
    {
        result = null;
        try
        {
            if (targetType.IsEnum)
            {
                result = Enum.Parse(targetType, value, true);
                return true;
            }

            if (targetType == typeof(bool))
            {
                result = bool.Parse(value);
                return true;
            }

            result = Convert.ChangeType(value, targetType);
            return true;
        }
        catch
        {
            return false;
        }
    }
}