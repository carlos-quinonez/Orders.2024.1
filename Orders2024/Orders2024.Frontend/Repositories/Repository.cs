using System.Text;
using System.Text.Json;

namespace Orders2024.Frontend.Repositories;

public class Repository : IRepository
{
    private readonly HttpClient _httpClient;
    public Repository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private JsonSerializerOptions _serializerDefaultOptions => new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    };
    public async Task<HttpResponseWrapper<T>> GetAsyn<T>(string url)
    {
        var responseHttp = await _httpClient.GetAsync(url);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await UnserializeResponse<T>(responseHttp);
            return new HttpResponseWrapper<T>(response,false, responseHttp);
        }
        return new HttpResponseWrapper<T>(default, true, responseHttp);
    }

    public async Task<HttpResponseWrapper<object>> PostAsyn<T>(string url, T model)
    {
        var modelJson = JsonSerializer.Serialize(model);
        var modelContent = new StringContent(modelJson, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, modelContent);        
        return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
    }

    public async Task<HttpResponseWrapper<TActionResponse>> PostAsyn<T, TActionResponse>(string url, T model)
    {
        var modelJson = JsonSerializer.Serialize(model);
        var modelContent = new StringContent(modelJson, Encoding.UTF8, "application/json");
        var responseHttp = await _httpClient.PostAsync(url, modelContent);
        if (responseHttp.IsSuccessStatusCode)
        {
            var response = await UnserializeResponse<TActionResponse>(responseHttp);
            return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp);
        }
        return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);        
    }

    private async Task<T> UnserializeResponse<T>(HttpResponseMessage responseHttp)
    {
        var response = await responseHttp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(response, _serializerDefaultOptions)!;
    }
}
