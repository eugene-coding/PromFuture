namespace Core.Exceptions;

/// <summary>
/// Исключение, выбрасываемое, когда не удалось найти секцию в конфигурации.
/// </summary>
public class MissingConfigurationSectionException : Exception
{
    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    public MissingConfigurationSectionException()
    {
    }

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="section">Искомая секция.</param>
    public MissingConfigurationSectionException(string section)
        : base(GetErrorMessage(section))
    {
    }

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="section">Искомый секция.</param>
    /// <param name="innerException">Внутреннее исключение.</param>
    public MissingConfigurationSectionException(string section, Exception innerException)
        : base(GetErrorMessage(section), innerException)
    {
    }

    private static string GetErrorMessage(string section)
    {
        return $"Секция {section} отсутствует в файле конфигурации.";
    }
}
