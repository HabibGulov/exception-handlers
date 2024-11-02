namespace BestPracticeExceptionHandler.Extensions.EFCore;

public static class EfCoreExtensions
{
    public static void FilterSoftDeletedProperties(this ModelBuilder modelBuilder)
    {
        Expression<Func<BaseEntity, bool>> filterExpr = e => !e.IsDeleted;
        foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes()
                     .Where(m => m.ClrType.IsAssignableTo(typeof(BaseEntity))))
        {
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            var body = ReplacingExpressionVisitor
                .Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            var lambdaExpression = Expression.Lambda(body, parameter);

            mutableEntityType.SetQueryFilter(lambdaExpression);
        }
    }
}