using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Services;

namespace CleanArchitecture.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services, AppSettings appsettings)
    {
        

        if (appsettings.FileStorageSettings.LocalStorage)
        {
            
        }
        else
        {
            
        }
        

        services.AddTransient<ICurrentTime, CurrentTime>();

        return services;
    }
}
