namespace Core.Interfaces;

/// <summary>
/// Сервис для аутентификация пользователя.
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Аутентифицирует пользователя.
    /// </summary>
    /// <param name="login">Логин.</param>
    /// <param name="password">Пароль.</param>
    Task AuthenticateAsync(string login, string password);
}
