using Core.Responses;

namespace Core.Interfaces;

/// <summary>
/// Сервис для работы с командами.
/// </summary>
public interface ICommandService
{
    /// <summary>
    /// Возвращает список названий команд.
    /// </summary>
    /// <returns>Список названий команд.</returns>
    Task<IEnumerable<CommandName>> GetNamesAsync();

    /// <summary>
    /// Возвращает параметры команды.
    /// </summary>
    /// <param name="id">ID команды.</param>
    /// <returns>Параметры команды.</returns>
    Task<CommandParameter> GetParametersAsync(int id);
}
