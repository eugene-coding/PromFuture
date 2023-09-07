using Api.Extensions;

using Core.Interfaces;
using Core.Responses;

using System.Text.Json;

namespace Api.Services;

/// <inheritdoc cref="ICommandService"/>
internal sealed class CommandService : ICommandService
{
    private readonly HttpClient _client;
    private readonly Uri _endpoint;

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="endpoints"></param>
    public CommandService(HttpClient client, Endpoints endpoints)
    {
        _client = client;
        _endpoint = endpoints.CommandTypesEndpoint;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CommandName>> GetNamesAsync()
    {
        using var response = await _client.GetJsonDocumentAsync(_endpoint);
        return ParseCommandNames(response);
    }

    /// <inheritdoc/>
    public async Task<CommandParameter> GetParametersAsync(int id)
    {
        using var response = await _client.GetJsonDocumentAsync(_endpoint);
        return ParseCommandParameter(response, id);
    }

    private static IEnumerable<CommandName> ParseCommandNames(JsonDocument json)
    {
        var items = json.RootElement.GetProperty("items");

        return items.Deserialize<IEnumerable<CommandName>>()
            ?? Enumerable.Empty<CommandName>();
    }

    private static CommandParameter ParseCommandParameter(JsonDocument json, int id)
    {
        var items = json.RootElement.GetProperty("items");

        var command = items.EnumerateArray()
            .FirstOrDefault(item => item.GetProperty("id").GetInt32() == id);

        return command.Deserialize<CommandParameter>() ?? new();
    }
}
