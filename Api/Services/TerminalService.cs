using Api.Extensions;

using Core.Interfaces;
using Core.Requests;
using Core.Responses;

using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Api.Services;

/// <inheritdoc cref="ITerminalService"/>
internal sealed class TerminalService : ITerminalService
{
    private readonly HttpClient _client;
    private readonly Endpoints _endpoints;
    private readonly ICommandService _commandService;

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="client">HTTP клиент.</param>
    /// <param name="endpoints">Конечные точки API.</param>
    /// <param name="commandService">Сервис для работы с командами.</param>
    public TerminalService(HttpClient client, Endpoints endpoints, ICommandService commandService)
    {
        _client = client;
        _endpoints = endpoints;
        _commandService = commandService;
    }

    /// <inheritdoc/>
    public async Task<HttpStatusCode> SendCommandAsync(TerminalCommandRequest terminalCommand)
    {
        var url = _endpoints.GetTerminalCommandsEndpoint(terminalCommand.TerminalId);

        using var content = CreateStringContent(terminalCommand);
        using var response = await _client.PostAsync(url, content);

        return response.StatusCode;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TerminalCommandResponse>> GetHistory(int id, int column, bool desc)
    {
        var url = _endpoints.GetTerminalCommandsEndpoint(id, column, desc);

        using var response = await _client.GetJsonDocumentAsync(url);

        var terminalCommands = ParseTerminalCommandResponse(response);
        var names = await _commandService.GetNamesAsync();

        var combinedTerminalCommands = CombineTerminalCommandsWithCommandNames(terminalCommands, names);

        return combinedTerminalCommands;
    }

    public async Task<bool> IsTerminalExist(int id)
    {
        var url = _endpoints.GetTerminalEndpoint(id);
        var response = await _client.GetAsync(url);

        return response.IsSuccessStatusCode;
    }

    private static StringContent CreateStringContent(TerminalCommandRequest terminalCommand)
    {
        var data = JsonSerializer.Serialize(terminalCommand);
        return new(data, Encoding.UTF8, MediaTypeNames.Application.Json);
    }

    private static IEnumerable<TerminalCommandResponse> ParseTerminalCommandResponse(JsonDocument json)
    {
        var items = json.RootElement.GetProperty("items");

        return items.Deserialize<IEnumerable<TerminalCommandResponse>>()
            ?? Enumerable.Empty<TerminalCommandResponse>();
    }

    private static IEnumerable<TerminalCommandResponse> CombineTerminalCommandsWithCommandNames(
        IEnumerable<TerminalCommandResponse> terminalCommands,
        IEnumerable<CommandName> names)
    {
        return terminalCommands.Join(names,
            terminalCommand => terminalCommand.CommandId,
            name => name.Id,
            (terminalCommand, name) =>
        {
            terminalCommand.CommandName = name.Name;
            return terminalCommand;
        });
    }
}
