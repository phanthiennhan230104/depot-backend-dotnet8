using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Application.Services.Interfaces;

namespace CleanArchitecture.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, AppSettings appsettings)
    {
        services.AddScoped<IDepotService, DepotService>();

        services.AddTransient<ICurrentTime, CurrentTime>();

        return services;
    }
}
