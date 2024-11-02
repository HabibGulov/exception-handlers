namespace BestPracticeExceptionHandler.Extensions.DI;

public static class DbContextRegistration
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<DataContext.DataContext>(x =>
        {
            x.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
            x.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            x.LogTo(Console.WriteLine);
        });
        return builder;
    }
}