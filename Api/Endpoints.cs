using System.Globalization;

namespace Api;

/// <summary>
/// Содержит методы для генерации ссылок на конечные точки API.
/// </summary>
public sealed class Endpoints
{
    private readonly Options _options;

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="options">Параметры API.</param>
    public Endpoints(Options options)
    {
        _options = options;
    }

    /// <summary>
    /// Формирует <see cref="Uri"/> для получения списка доступных типов команд.
    /// </summary>
    /// <returns><see cref="Uri"/> для получения списка доступных типов команд.</returns>
    public Uri CommandTypesEndpoint => new UriBuilder(_options.BaseUrl)
    {
        Path = "commands/types",
        Query = $"token={_options.Token}"
    }.Uri;

    /// <summary>
    /// Формирует <see cref="Uri"/> для запроса токена.
    /// </summary>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    /// <returns><see cref="Uri"/> для запроса токена.</returns>
    public Uri GetTokenEndpoint(string login, string password)
    {
        return new UriBuilder(_options.BaseUrl)
        {
            Path = "token",
            Query = $"login={login}&password={password}"
        }.Uri;
    }

    /// <summary>
    /// Формирует <see cref="Uri"/> для получения ранее отправленных на терминал команд.
    /// </summary>
    /// <param name="terminalId">ID терминала.</param>
    /// <param name="column">Номер столбца, по которому нужно провести сортировку.</param>
    /// <param name="desc">Определяет, сортировать ли <paramref name="column"/> по убыванию.</param>
    /// <returns>
    /// <see cref="Uri"/> для получения ранее отправленных на терминал команд.
    /// </returns>
    public Uri GetTerminalCommandsEndpoint(string? terminalId, int column, bool desc)
    {
        return new UriBuilder(_options.BaseUrl)
        {
            Path = $"terminals/{terminalId}/commands",
            Query = $"token={_options.Token}&OrderByColumn={column}&OrderDesc={desc}&ItemsOnPage=10"
        }.Uri;
    }

    /// <inheritdoc cref="GetTerminalCommandsEndpoint(string?, int, bool)"/>
    public Uri GetTerminalCommandsEndpoint(int terminalId, int column, bool desc)
    {
        return GetTerminalCommandsEndpoint(
            terminalId.ToString(CultureInfo.InvariantCulture), column, desc);
    }

    /// <summary>
    /// Формирует <see cref="Uri"/> для отправки команды на терминал.
    /// </summary>
    /// <param name="terminalId">ID терминала.</param>
    /// <returns>
    /// <see cref="Uri"/> для отправки команды на терминал.
    /// </returns>
    public Uri GetTerminalCommandsEndpoint(string? terminalId)
    {
        return new UriBuilder(_options.BaseUrl)
        {
            Path = $"terminals/{terminalId}/commands",
            Query = $"token={_options.Token}&ItemsOnPage=10"
        }.Uri;
    }

    /// <summary>
    /// Формирует <see cref="Uri"/> для получения или редактирования терминала.
    /// </summary>
    /// <param name="id">ID терминала.</param>
    /// <returns><see cref="Uri"/> для получения или редактирования терминала.</returns>
    public Uri GetTerminalEndpoint(int id)
    {
        return new UriBuilder(_options.BaseUrl)
        {
            Path = $"terminals/{id}",
            Query = $"token={_options.Token}"
        }.Uri;
    }
}
