using System.Text.Json.Serialization;

namespace Core.Responses;

/// <summary>
/// Представляет ответ для запроса названий команд.
/// </summary>
public sealed class CommandName
{
    /// <summary>
    /// ID команды.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// Название команды.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
