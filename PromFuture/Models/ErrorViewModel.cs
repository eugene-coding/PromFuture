namespace PromFuture.Models;

/// <summary>
/// ������������� ������.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// ID �������.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// ����������, ���������� �� <see cref="RequestId"/>.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
