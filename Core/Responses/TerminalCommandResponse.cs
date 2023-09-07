using System.Text.Json.Serialization;

namespace Core.Responses;

/// <summary>
/// Представляет ответ для запроса отправленных к терминалу команд.
/// </summary>
public sealed class TerminalCommandResponse
{
    /// <summary>
    /// ID команды.
    /// </summary>
    [JsonPropertyName("command_id")]
    public int CommandId { get; set; }

    /// <summary>
    /// Название команды.
    /// </summary>
    [JsonIgnore]
    public string CommandName { get; set; } = string.Empty;
    
    /// <summary>
    /// Значение первого параметра.
    /// </summary>
    [JsonPropertyName("parameter1")]
    public int FirstParameter { get; set; }
    
    /// <summary>
    /// Значение второго параметра.
    /// </summary>
    [JsonPropertyName("parameter2")]
    public int SecondParameter { get; set; }
   
    /// <summary>
    /// Значение третьего параметра.
    /// </summary>
    [JsonPropertyName("parameter3")]
    public int ThirdParameter { get; set; }

    /// <summary>
    /// Дата и время отправки команды.
    /// </summary>
    [JsonPropertyName("time_created")]
    public required string DateTime { get; set; }
    
    /// <summary>
    /// Статус выполнения команды.
    /// </summary>
    [JsonPropertyName("state_name")]
    public required string Status { get; set; }
}
