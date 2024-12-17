using GusPizza.Application.Interfaces;
using GusPizza.Domain.Interfaces;
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
        services.AddScoped<ITransactionService, TransactionService>();

        // domain config
        services.AddScoped<IPizzaRepository, PizzaRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        // infrastructure config
        services.AddInfrastructure();

        return services;
    }
}