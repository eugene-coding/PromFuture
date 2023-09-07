using Core.Exceptions;

using Microsoft.Extensions.Configuration;

namespace Api;

/// <summary>
/// Содержит основные параметры для работы с API.
/// </summary>
public sealed class Options
{
    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="configuration">Конфигурация.</param>
    /// <exception cref="MissingConfigurationSectionException">Выбрасывается, если не удаётся получить секцию API.</exception>
    /// <exception cref="MissingConfigurationKeyException">Выбрасывается, если не удаётся получить базовый адрес API.</exception>
    public Options(IConfiguration configuration)
    {
        var apiSection = configuration.GetRequiredSection("Api")
            ?? throw new MissingConfigurationSectionException("Api");

        BaseUrl = apiSection. GetValue<Uri>("BaseUrl")
            ?? throw new MissingConfigurationKeyException("BaseUrl");
    }

    /// <summary>
    /// Базовый адрес.
    /// </summary>
    public required Uri BaseUrl { get; init; }

    /// <summary>
    /// Токен авторизации.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
