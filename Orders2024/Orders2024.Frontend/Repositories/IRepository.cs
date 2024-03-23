namespace Orders2024.Frontend.Repositories;

public interface IRepository
{
    Task<HttpResponseWrapper<T>> GetAsyn<T>(string url);

    Task<HttpResponseWrapper<object>> PostAsyn<T>(string url, T model);

    Task<HttpResponseWrapper<TActionResponse>> PostAsyn<T,TActionResponse>(string url, T model);
}
