namespace BestPracticeExceptionHandler.Extensions.DI;

public static class RegisterServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }
}