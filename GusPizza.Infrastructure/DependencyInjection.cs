using GusPizza.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace GusPizza.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return services;
    }
}
