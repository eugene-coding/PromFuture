using Api.Services;

using Core.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions;

/// <summary>
/// Расширяющий класс для <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExtension
{
    /// <summary>
    /// Настраивает сервисы для работы API.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/>.</param>
    /// <returns>
    /// Возвращает тот же самый <see cref="IServiceCollection"/> 
    /// для построения цепочек вызовов.
    /// </returns>
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddSingleton<Options>();
        services.AddSingleton<Endpoints>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ICommandService, CommandService>();
        services.AddScoped<ITerminalService, TerminalService>();

        return services;
    }
}
