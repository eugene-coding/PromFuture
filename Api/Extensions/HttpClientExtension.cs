using System.Text.Json;

namespace Api.Extensions;

/// <summary>
/// Расширяющий класс для <see cref="HttpClient"/>.
/// </summary>
internal static class HttpClientExtension
{
    /// <summary>
    /// Осуществляет запрос к <paramref name="url"/> и преобразовывает ответ в <see cref="JsonDocument"/>.
    /// </summary>
    /// <param name="client">HTTP клиент.</param>
    /// <param name="url"><see cref="Uri"/>, к которому нужно осуществить запрос.</param>
    /// <returns>Преобразованное в <see cref="JsonDocument"/> тело запроса.</returns>
    public static async Task<JsonDocument> GetJsonDocumentAsync(this HttpClient client, Uri url)
    {
        using var response = await client.GetStreamAsync(url);
        return await JsonDocument.ParseAsync(response);
    }
}
