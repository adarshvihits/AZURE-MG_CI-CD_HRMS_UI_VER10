using Hrms.Lite.Services.IServices;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

public class ServiceBase : IServiceBase
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly string _baseUrl1;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private ISession _session => _httpContextAccessor.HttpContext.Session;

    public ServiceBase(IOptions<AppSettings> settings, IHttpContextAccessor httpContextAccessor)
    {
        _baseUrl = settings.Value.ApiBaseUrl;
        _baseUrl1 = settings.Value.ApiBaseUrl2;
        _httpClient = new HttpClient();
        _httpContextAccessor = httpContextAccessor;


    }
    public async Task<T> GetAsync<T>(string url)
    {
        url = $"{_baseUrl}{url}";
        T result = default(T);

        var token = _session.GetString("JwtToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = _httpClient.GetAsync(new Uri(url)).Result;
        if (response.ReasonPhrase == "Unauthorized")
        {
            throw new UnauthorizedAccessException();
        }
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            _session.SetString("LoginStatus", "Out");
            throw new UnauthorizedAccessException("403 Forbidden");
        }
        if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            _session.SetString("InvalidEntry", "1");
            throw new UnauthorizedAccessException("422 Unprocessable Entity");
        }
        response.EnsureSuccessStatusCode();
        await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
        {
            if (x.IsFaulted)
                throw x.Exception;

            result = JsonConvert.DeserializeObject<T>(x.Result);
        });

        return result;
    }







    public async Task<T> GetReportAsync<T>(string url)
    {
        url = $"{_baseUrl1}{url}";
        T result = default(T);

        //var token = _session.GetString("JwtToken");
        //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = _httpClient.GetAsync(new Uri(url)).Result;
        //if (response.ReasonPhrase == "Unauthorized")
        //{
        //    throw new UnauthorizedAccessException();
        //}
        response.EnsureSuccessStatusCode();
        await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
        {
            if (x.IsFaulted)
                throw x.Exception;

            result = JsonConvert.DeserializeObject<T>(x.Result);
        });

        return result;
    }

    public async Task<T> DeleteAsync<T>(string url)
    {
        url = $"{_baseUrl}{url}";
        T result = default(T);

        var token = _session.GetString("JwtToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        //var response = _httpClient.GetAsync(new Uri(url)).Result;
        var response = _httpClient.DeleteAsync(new Uri(url)).Result;
        if (response.ReasonPhrase == "Unauthorized")
        {
            throw new UnauthorizedAccessException();
        }
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            _session.SetString("LoginStatus", "Out");
            throw new UnauthorizedAccessException("403 Forbidden");
        }
        if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            _session.SetString("InvalidEntry", "1");
            throw new UnauthorizedAccessException("422 Unprocessable Entity");
        }
        response.EnsureSuccessStatusCode();
        await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
        {
            if (x.IsFaulted)
                throw x.Exception;

            result = JsonConvert.DeserializeObject<T>(x.Result);
        });

        return result;
    }

    public async Task<TResponse> PostAsync<TResponse, TData>(string apiUrl, TData postObject)
    {
        apiUrl = $"{_baseUrl}{apiUrl}";
        TResponse result = default(TResponse);
        var token = _session.GetString("JwtToken");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await _httpClient.PostAsync(apiUrl, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

        if (response.ReasonPhrase == "Unauthorized")
        {
            throw new UnauthorizedAccessException();
        }
        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            _session.SetString("LoginStatus", "Out");
            throw new UnauthorizedAccessException("403 Forbidden");
        }
        if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
        {
            _session.SetString("InvalidEntry", "1");
            throw new UnauthorizedAccessException("422 Unprocessable Entity");
        }
        response.EnsureSuccessStatusCode();

        await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
        {
            if (x.IsFaulted)
                throw x.Exception;

            result = JsonConvert.DeserializeObject<TResponse>(x.Result);

        });

        return result;
    }


    public async Task<HttpResponseMessage> PostAsync<TData>(string apiUrl, TData postObject)
    {
        apiUrl = $"{_baseUrl}{apiUrl}";

        var token = _session.GetString("JwtToken");
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        // Await the HTTP call properly
        HttpResponseMessage response = await _httpClient.PostAsync(
            apiUrl,
            postObject,
            new JsonMediaTypeFormatter()
        ).ConfigureAwait(false);

        return response;
    }

    public async Task<TResponse> PostFormDataAsync<TResponse>(string url,MultipartFormDataContent data)
    {
        url = $"{_baseUrl}{url}";

        var token = _session.GetString("JwtToken");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        using var response = await _httpClient.PostAsync(url, data);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            return Activator.CreateInstance<TResponse>();
        }

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<TResponse>(json);
    }


}