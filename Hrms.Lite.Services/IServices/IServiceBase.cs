using System.Net.Http;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices
{
    public interface IServiceBase
    {
        Task<T> GetAsync<T>(string url);
        Task<T> GetReportAsync<T>(string url);
        Task<T> DeleteAsync<T>(string url);
        Task<TResponse> PostAsync<TResponse, TData>(string apiUrl, TData postObject);
        Task<HttpResponseMessage> PostAsync<TData>(string apiUrl, TData postObject);
        Task<TResponse> PostFormDataAsync<TResponse>(string url, MultipartFormDataContent data);

    }
}
