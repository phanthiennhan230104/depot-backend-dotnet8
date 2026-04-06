using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Extensions;

public static class MvcExtension
{
    public static IServiceCollection SetupMvc(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}
