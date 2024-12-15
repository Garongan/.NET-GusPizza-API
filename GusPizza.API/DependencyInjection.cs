using GusPizza.Application.Services;
using GusPizza.Application.Services.Interfaces;
using GusPizza.Domain.Repositories;
using GusPizza.Infrastructure;
using GusPizza.Infrastructure.Repositories;

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