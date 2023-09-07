using Core.Requests;
using Core.Responses;

using System.Net;

namespace Core.Interfaces;

/// <summary>
/// Сервис для работы с терминалами.
/// </summary>
public interface ITerminalService
{
    /// <summary>
    /// Отправляет команду в терминал.
    /// </summary>
    /// <remarks>
    /// ID терминала, в который нужно отправить команду, находится внутри <paramref name="command"/>.
    /// </remarks>
    /// <param name="command">Команда.</param>
    /// <returns><see cref="HttpStatusCode"/> ответа.</returns>
    Task<HttpStatusCode> SendCommandAsync(TerminalCommandRequest command);

    /// <summary>
    /// Возвращает историю отправленных к терминалу команд.
    /// </summary>
    /// <param name="id">ID терминала.</param>
    /// <param name="column">
    /// Номер столбца сортировки. По умолчанию: 15 - сортировка по дате и времени.
    /// </param>
    /// <param name="desc">Сортировать ли по убыванию.</param>
    /// <returns>Список отправленных к терминалу команд.</returns>
    Task<IEnumerable<TerminalCommandResponse>> GetHistory(int id, int column, bool desc);
}
