using GusPizza.Application.Services;
using GusPizza.Domain.Repositories;
using GusPizza.Infrastructure;
using GusPizza.Infrastructure.Persistence.Repositories;
using GusPizza.Infrastructure.Services;

namespace GusPizza.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        // application config
        services.AddScoped<IPizzaService, PizzaService>();
        services.AddScoped<IUserService, UserService>();

        // domain config
        services.AddScoped<IPizzaRepository, PizzaRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        // infrastructure config
        services.AddInfrastructure();

        return services;
    }
}