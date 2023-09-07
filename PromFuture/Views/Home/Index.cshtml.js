'use strict'

/** Поле ввода ID терминалов. */
const terminal = document.getElementById("terminal_id");

/** Блок для вставки динамически загружаемых параметров. */
const parameters = document.getElementById("parameters");

/** Кнопка отправки формы. */
const sendButton = document.getElementById("send-button");

/** Блок для вставки сообщения об ошибке отправки. */
const sendError = document.getElementById("send-error");

/** Блок для вставки списка отправленных на термианл команд. */
const history = document.getElementById("history")

/** Индекс активной кнопки сортировки по умолчанию. */
const defaultSortButtonIndex = 1;

/** Выпадающий список с доступными командами. */
let commandsSelect;

/** 
 * Индекс кнопки, по которой проводится сортировка 
 * отправленных на терминал команд.
*/
let activeSortButtonIndex = defaultSortButtonIndex;

document.addEventListener("DOMContentLoaded", () => {
    loadCommands();
    loadHistory();
});

document.addEventListener("submit", e => {
    e.preventDefault();
    sendCommand();
});

terminal.addEventListener("change", () => {
    sendError.innerHTML = "";

    history.innerHTML = "Загрузка...."
    loadHistory();
})

/**
 * Загружает список доступных команд и настраивает выпадающий список.
 */
function loadCommands() {
    const commands = document.getElementById("commands");

    fetch("/Home/GetCommandNames")
        .then(response => {
            if (response.ok) {
                response.text()
                    .then(data => {
                        commands.innerHTML = data;

                        commandsSelect = document.getElementById("command_id");
                        commandsSelect.options[0].setAttribute("disabled", true);

                        commandsSelect.addEventListener("change", () => {
                            sendError.innerHTML = "";

                            if (commandsSelect.value !== "") {
                                loadParameters(commandsSelect.value);
                                sendButton.removeAttribute("disabled");
                            }
                        });
                    });
            } else {
                commands.innerHTML = getLoadCommandParametersErrorMessage();;
            }
        })
        .catch(error => console.error(error));
}

/**
 * Загружает параметры для команды.
 * @param {Number} id ID команды.
 */
const loadParameters = (id) => {
    parameters.innerHTML = '<p>Загрузка...</p>';

    fetch(`/Home/GetParameters?id=${id}`)
        .then(response => {
            if (response.ok) {
                response.text()
                    .then(data => parameters.innerHTML = data);
            } else {
                parameters.innerHTML = getLoadCommandParametersErrorMessage();
            }
        })
        .catch(error => console.error(error));
}

/**
 * Отправляет команду на терминал.
 */
function sendCommand() {
    sendError.innerHTML = "";

    const sendButtonText = sendButton.textContent;
    sendButton.setAttribute("disabled", "");

    sendButton.innerText = "Отправляем...";

    fetch(`/Home/SendCommand`, {
        method: "POST",
        headers: {
            "content-type": "application/json"
        },
        body: convertFormDataToJson()
    })
        .then(response => {
            if (response.ok) {
                activeSortButtonIndex = defaultSortButtonIndex;
                loadHistory();
            } else {
                sendError.innerHTML = getSendCommandErrorMessage();
            }
        })
        .catch(error => console.error(error))
        .then(() => {
            // Возвращаем текст обратно, сбрасываем выпадающий список и очищаем параметры.
            sendButton.innerText = sendButtonText;
            commandsSelect.options[0].selected = true;
            parameters.innerHTML = "";
        });
}

/**
 * Загружает список отправленных на терминал комманд.
 * @param {Number} column Номер столбца, по которому отсортировать команды.
 * По умолчанию сортирует по дате и времени отправки команды.
 * @param {Boolean} desc Указывает, сортировать ли команды по убыванию.
 * По умолчанию сортирует по убыванию.
 */
const loadHistory = (column = 15, desc = true) => {
    fetch(`/Home/GetHistory?id=${terminal.value}&column=${column}&desc=${desc}`)
        .then(response => {
            if (response.ok) {
                response.text()
                    .then(data => {
                        history.innerHTML = data;
                        setActiveSortButton();
                    });
            } else {
                history.innerHTML = getLoadHistoryErrorMessage();
            }
        })
        .catch(error => console.error(error))
}

/**
 * Сортирует таблицу с отправленными на терминал командами.
 * @param {HTMLElement} clickedButton Нажатая кнопка сортировки.
 * @param {Number} column Номер столбца, по которому нужно произвести сортировку.
 * @param {Boolean} desc Определяет, сортировать ли таблицу по убыванию.
 */
function filterTable(clickedButton, column, desc) {
    activeSortButtonIndex = getClickedSortButtonIndex(clickedButton);
    loadHistory(column, desc);
}

/**
 * Конвертирует данные формы в JSON.
 * @returns Данные формы в JSON.
 */
function convertFormDataToJson() {
    const form = document.getElementById("form");
    const formData = new FormData(form);
    const entity = Object.fromEntries(formData);

    return JSON.stringify(entity);
}

/**
 * Возвращает индекс нажатой кнопки для сортировки в таблице.
 * @param {HTMLElement} clickedButton Нажатая кнопка.
 * @returns Индекс кнопки сортировки в таблице.
 */
function getClickedSortButtonIndex(clickedButton) {
    const sortButtons = document.querySelectorAll("#history-table .sort-button");
    return [...sortButtons].indexOf(clickedButton);
}

/**
 * Отмечает кнопку, по которой ведётся сортировка,
 * в качестве активной.
 */
function setActiveSortButton() {
    const sortButtons = document.querySelectorAll("#history-table .sort-button");

    if (sortButtons.length > 0) {
        sortButtons[activeSortButtonIndex].classList.add("text-warning");
    }
}

/**
 * Возвращает HTML блок с сообщением о неудачной загрузке списка доступных команд.
 * @returns HTML блок с сообщением о неудачной загрузке списка доступных команд.
 */
function getLoadCommandParametersErrorMessage() {
    return creatErrorMessage("Не удалось загрузить список доступных команд.");
}

/**
 * Возвращает HTML блок с сообщением о неудачной загрузке параметров команды.
 * @returns HTML блок с сообщением о неудачной загрузке параметров комананды.
 */
function getLoadCommandParametersErrorMessage() {
    return creatErrorMessage("Не удалось загрузить параметры команды.");
}

/**
 * Возвращает HTML блок с сообщением о неудачной загрузке списка отправленных команд.
 * @returns HTML блок с сообщением о неудачной загрузке списка отправленных команд.
 */
function getLoadHistoryErrorMessage() {
    return creatErrorMessage("Не удалось загрузить список отправленных команд.");
}

/**
 * Возвращает HTML блок с сообщением о неудачной отправке команды на терминал.
 * @returns HTML блок с сообщением о неудачной отправке команды на терминал.
 */
function getSendCommandErrorMessage() {
    return creatErrorMessage("Не удалось отправить команду на терминал(ы).");
}

/**
* Создаёт HTML блок с сообщением об ошибке.
* @param {String} message Сообщение об ошибке.
* @returns HTML блок с сообщением об ошибке.
*/
function creatErrorMessage(message) {
    return `<p class="alert alert-danger" role="alert">${message}</p>`;
}
