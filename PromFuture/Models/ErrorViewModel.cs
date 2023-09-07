namespace PromFuture.Models;

/// <summary>
/// Представление ошибки.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// ID запроса.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Определяет, показывать ли <see cref="RequestId"/>.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
