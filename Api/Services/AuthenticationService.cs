using Api.Extensions;

using Core.Interfaces;

namespace Api.Services;

/// <inheritdoc cref="IAuthenticationService"/>
internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _client;
    private readonly Endpoints _endpoints;
    private readonly Options _options;

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="client">HTTP клиент.</param>
    /// <param name="endpoints">Конечные точки API.</param>
    /// <param name="options">Параметры API.</param>
    public AuthenticationService(HttpClient client, Endpoints endpoints, Options options)
    {
        _client = client;
        _endpoints = endpoints;
        _options = options;
    }

    /// <summary>
    /// Аутентифицирует пользователя и сохраняет полученный в случае успеха токен
    /// в <see cref="Options.Token"/> для последующих запросов к API.
    /// </summary>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    public async Task AuthenticateAsync(string login, string password)
    {
        var token = await GetTokenAsync(login, password);
        ArgumentException.ThrowIfNullOrEmpty(token, nameof(token));

        _options.Token = token;
    }

    private async Task<string?> GetTokenAsync(string login, string password)
    {
        var url = _endpoints.GetTokenEndpoint(login, password);
        using var response = await _client.GetJsonDocumentAsync(url);
        
        return response.RootElement.GetProperty("token").GetString();
    }
}
