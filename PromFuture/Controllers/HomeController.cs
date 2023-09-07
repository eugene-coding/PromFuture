using Core.Interfaces;
using Core.Requests;
using Core.Responses;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using PromFuture.Models;

using System.Diagnostics;
using System.Text.Json;

namespace PromFuture.Controllers;

/// <summary>
/// Контроллер для главной страницы.
/// </summary>
public class HomeController : Controller
{
    private readonly ICommandService _commandService;
    private readonly ITerminalService _terminalService;

    /// <summary>
    /// Создаёт экземпляр класса.
    /// </summary>
    /// <param name="commandService">Сервис для работы с командами.</param>
    /// <param name="terminalService">Сервис для работы с терминалами.</param>
    public HomeController(ICommandService commandService, ITerminalService terminalService)
    {
        _commandService = commandService;
        _terminalService = terminalService;
    }

    /// <summary>
    /// Возвращает представление страницы.
    /// </summary>
    /// <returns>Представление страницы.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Возвращает частичное представление с выпадающим списком из доступных команд.
    /// </summary>
    /// <returns>Частичное представление с выпадающим списком из доступных команд.</returns>
    public async Task<IActionResult> GetCommandNamesAsync()
    {
        var commands = await _commandService.GetNamesAsync();
        var selectList = new SelectList(commands, nameof(CommandName.Id), nameof(CommandName.Name));

        return PartialView("_Commands", selectList);
    }

    /// <summary>
    /// Возвращает частичное представление с полями для ввода параметров команды.
    /// </summary>
    /// <returns>Частичное представление с полями для ввода параметров команды.</returns>
    public async Task<IActionResult> GetParametersAsync(int id)
    {
        var parameters = await _commandService.GetParametersAsync(id);
        return PartialView("_Parameters", parameters);
    }

    /// <summary>
    /// Отправляет команду на терминал и возвращает код ответа.
    /// </summary>
    /// <returns>Код ответа на отправку команды.</returns>
    public async Task<IActionResult> SendCommandAsync()
    {
        var terminalCommand = await GetTerminalCommandFromRequestAsync();
        var statusCode = await _terminalService.SendCommandAsync(terminalCommand);

        return StatusCode((int) statusCode);
    }

    /// <summary>
    /// Возвращает частичное представление со списком отправленных на терминал команд.
    /// </summary>
    /// <param name="id">ID терминала.</param>
    /// <param name="column">Номер столбца сортировки.</param>
    /// <param name="desc">Сортировать ли по убыванию.</param>
    /// <returns>Частичное представление со списком отправленных на терминал команд.</returns>
    public async Task<IActionResult> GetHistoryAsync(int id, int column, bool desc)
    {
        var history = await _terminalService.GetHistory(id, column, desc);
        return PartialView("_History", history);
    }

    /// <summary>
    /// Возвращает представление ошибки.
    /// </summary>
    /// <returns>Представление ошибки.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task<TerminalCommandRequest> GetTerminalCommandFromRequestAsync()
    {
        return await JsonSerializer.DeserializeAsync<TerminalCommandRequest>(Request.Body) ?? new();
    }
}
