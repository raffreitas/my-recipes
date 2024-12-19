using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Tests;

public class MyRecipesClassFixture : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public MyRecipesClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

    protected async Task<HttpResponseMessage> DoPost<T>(string method, T request, string culture = "en")
    {
        ChangeRequestCulture(culture);

        return await _httpClient.PostAsJsonAsync(method, request);
    }

    protected async Task<HttpResponseMessage> DoPut<T>(string method, T request, string token = "", string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.PutAsJsonAsync(method, request);
    }

    protected async Task<HttpResponseMessage> DoGet(string method, string token = "", string culture = "en")
    {
        ChangeRequestCulture(culture);
        AuthorizeRequest(token);

        return await _httpClient.GetAsync(method);
    }

    private void ChangeRequestCulture(string culture)
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
    }

    private void AuthorizeRequest(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return;

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    }
}
