namespace Core.Exceptions;

/// <summary>
/// Исключение, выбрасываемое, когда не удалось найти ключ в конфигурации.
/// </summary>
public class MissingConfigurationKeyException : Exception
{
    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    public MissingConfigurationKeyException()
    {
    }

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="key">Искомый ключ.</param>
    public MissingConfigurationKeyException(string key)
        : base(GetErrorMessage(key))
    {
    }

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="key">Искомый ключ.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public MissingConfigurationKeyException(string key, Exception innerException) 
        : base(GetErrorMessage(key), innerException)
    {
    }

    private static string GetErrorMessage(string key)
    {
        return $"Ключ {key} отсутствует в файле конфигурации.";
    }
}
