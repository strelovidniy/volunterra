using System.Linq.Expressions;
using System.Reflection;

namespace VolunteerManager.Domain.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<TResult> Select<TSource, TResult>(
        this IQueryable<TSource> query,
        string propertyName
    ) => InvokeQueryableMethod<TSource, TResult>(query, propertyName, nameof(Select));

    private static IQueryable<TResult> InvokeQueryableMethod<TSource, TResult>(
        IQueryable<TSource> query,
        string propertyName,
        string methodName
    )
    {
        var entityType = typeof(TSource);

        PropertyInfo? propertyInfo = null;

        //Create x=>x.PropName
        var arg = Expression.Parameter(entityType, "x");

        Expression property = arg;

        if (!propertyName.Contains("."))
        {
            propertyInfo = entityType.GetProperty(propertyName);

            property = Expression.Property(arg, propertyName);
        }
        else
        {
            var propertyNames = propertyName.Split(".");

            propertyInfo = propertyNames.Aggregate(propertyInfo,
                (current, name) =>
                    current is null ? entityType.GetProperty(name) : current.PropertyType.GetProperty(name));

            property = propertyName.Split('.').Aggregate(property, Expression.Property);
        }

        var selector = Expression.Lambda(property, arg);

        //Get System.Linq.Queryable.OrderBy() method.
        var enumerableType = typeof(Queryable);

        var method = enumerableType.GetMethods()
            .Where(m => m.Name == methodName && m.IsGenericMethodDefinition)
            .Where(m =>
            {
                var parameters = m.GetParameters().ToList();

                //Put more restriction here to ensure selecting the right overload                
                return parameters.Count == 2; //overload that has 2 parameters
            }).Single();

        //The linq OrderBy<TSource, TKey> has two generic types, which provided here
        var genericMethod = method
            .MakeGenericMethod(entityType, propertyInfo!.PropertyType);

        /*Call query.OrderBy(selector), with query and selector: x=> x.PropName
          Note that we pass the selector as Expression to the method and we don't compile it.
          By doing so EF can extract "order by" columns and generate SQL for it.*/
        var newQuery = (IQueryable<TResult>) genericMethod
            .Invoke(genericMethod, new object[] { query, selector })!;

        return newQuery;
    }
}