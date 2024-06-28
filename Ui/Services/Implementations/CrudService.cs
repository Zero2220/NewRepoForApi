using Microsoft.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Ui.Models;
using Ui.Services.Interfaces;
using Ui.Exceptions;
using Microsoft.AspNetCore.Mvc;

public class CrudService : ICrudService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _client;
    private const string baseUrl = "http://localhost:5017/api/";

    public CrudService(IHttpContextAccessor httpContextAccessor)
    {
        _client = new HttpClient();
        _httpContextAccessor = httpContextAccessor;
    }

    private void SetAuthorizationHeader()
    {
        _client.DefaultRequestHeaders.Remove(HeaderNames.Authorization);
        _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, _httpContextAccessor.HttpContext.Request.Cookies["token"]);
    }

    public async Task<PaginatedResponse<TResponse>> GetAllPaginated<TResponse>(int page)
    {
        SetAuthorizationHeader();

        using (var response = await _client.GetAsync($"{baseUrl}?page={page}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<PaginatedResponse<TResponse>>(await response.Content.ReadAsStringAsync(), options);
            }
            else
            {
                throw new HttpException(response.StatusCode);
            }
        }
    }

    public async Task<TResponse> Get<TResponse>(string id)
    {
        SetAuthorizationHeader();

        using (var response = await _client.GetAsync($"{baseUrl}{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync(), options);
            }
            else
            {
                throw new HttpException(response.StatusCode);
            }
        }
    }

    public async Task<CreateResponse> Create<TRequest>(TRequest request,string path)
    {

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");


        using (var response = await _client.PostAsync(baseUrl+path, content))
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CreateResponse>(await response.Content.ReadAsStringAsync(), options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new ModelException(System.Net.HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new HttpException(response.StatusCode, errorResponse.Message);
            }
        }
    }

    public async Task<CreateResponse> CreateFromForm<TRequest>(TRequest request, string path)
    {

        MultipartFormDataContent content = new MultipartFormDataContent();
        foreach (var prop in request.GetType().GetProperties())
        {
            var val = prop.GetValue(request);

            if (val is IFormFile file)
                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
            else if (val is not null)
                content.Add(new StringContent(val.ToString()), prop.Name);
        }

        using (HttpResponseMessage response = await _client.PostAsync(baseUrl + path, content))
        {
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CreateResponse>(await response.Content.ReadAsStringAsync(), options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new ModelException(System.Net.HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new HttpException(response.StatusCode, errorResponse.Message);
            }


        }
    }


    public async Task Update<TRequest>(TRequest request, string id)
    {
        SetAuthorizationHeader();

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        using (var response = await _client.PutAsync($"{baseUrl}{id}", content))
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new ModelException(System.Net.HttpStatusCode.BadRequest, errorResponse);
            }
            else if (!response.IsSuccessStatusCode)
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new HttpException(response.StatusCode, errorResponse.Message);
            }
        }
    }

    public async Task Delete(string id)
    {
        SetAuthorizationHeader();

        using (var response = await _client.DeleteAsync($"{baseUrl}{id}"))
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpException(response.StatusCode);
            }
        }
    }

    public async Task<CreateResponse> CreateFromForm<TRequest>(TRequest request)
    {
        SetAuthorizationHeader();

        MultipartFormDataContent content = new MultipartFormDataContent();
        foreach (var prop in request.GetType().GetProperties())
        {
            var val = prop.GetValue(request);

            if (val is IFormFile file)
                content.Add(new StreamContent(file.OpenReadStream()), prop.Name, file.FileName);
            else if (val is DateTime dateTime)
                content.Add(new StringContent(dateTime.ToLongDateString()), prop.Name);
            else if (val is not null)
                content.Add(new StringContent(val.ToString()), prop.Name);
        }

        using (var response = await _client.PostAsync(baseUrl, content))
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<CreateResponse>(await response.Content.ReadAsStringAsync(), options);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new ModelException(System.Net.HttpStatusCode.BadRequest, errorResponse);
            }
            else
            {
                var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), options);
                throw new HttpException(response.StatusCode, errorResponse.Message);
            }
        }
    }
}
