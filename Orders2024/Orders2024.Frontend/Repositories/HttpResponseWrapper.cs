using System.Globalization;

namespace Orders2024.Frontend.Repositories;

public class HttpResponseWrapper<T>
{
    public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage)
    {
        Response = response;
        Error = error;
        HttpResponseMessage = httpResponseMessage;
    }

    public bool Error { get; }
    public HttpResponseMessage HttpResponseMessage { get; }
    public T? Response { get; }

    public async Task<string> GetErrorMessageAsync()
    {
        if (!Error)
        {
            return string.Empty;
        }
        var statusCode = HttpResponseMessage.StatusCode;
        if (statusCode == System.Net.HttpStatusCode.NotFound)
        {
            return "Recurso no encontrado.";
        }
        if (statusCode == System.Net.HttpStatusCode.BadRequest)
        {
            return await HttpResponseMessage.Content.ReadAsStringAsync();
        }
        if (statusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return "Usuario no esta logueado.";
        }
        if (statusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return "No tiene permisos para realizar esta operacion.";
        }
        return "Error inesperado :'V";
    }
}