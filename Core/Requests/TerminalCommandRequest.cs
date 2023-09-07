using System.Text.Json.Serialization;

namespace Core.Requests;

/// <summary>
/// Представляет тело запроса для отправки команды к терминалу.
/// </summary>
[Serializable]
public sealed class TerminalCommandRequest
{
    /// <summary>
    /// ID терминала.
    /// </summary>
    [JsonPropertyName("terminal_id")]
    public string TerminalId { get; set; } = string.Empty;

    /// <summary>
    /// ID команды.
    /// </summary>
    [JsonPropertyName("command_id")]
    public string CommandId { get; set; } = string.Empty;

    /// <summary>
    /// Первый параметр команды.
    /// </summary>
    [JsonPropertyName("parameter1")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FirstParameter { get; set; }

    /// <summary>
    /// Второй параметр команды.
    /// </summary>
    [JsonPropertyName("parameter2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SecondParameter { get; set; }

    /// <summary>
    /// Третий параметр команды.
    /// </summary>
    [JsonPropertyName("parameter3")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ThirdParameter { get; set; }
}
