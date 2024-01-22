using System.Text.Json;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.Services.Repositories;

namespace Acme.VideoCatalog.DataAccess;

public class VideoRepository<T> : IRepository<T> where T : VideoDto
{
    private readonly HttpClient _httpClient;
    private readonly string _getAllUrl;

    public VideoRepository(HttpClient httpClient, string getAllUrl)
    {
        _httpClient = httpClient;
        _getAllUrl = getAllUrl;

    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(_getAllUrl);
        response.EnsureSuccessStatusCode();

        try
        {
            string content = await response.Content.ReadAsStringAsync();
            IReadOnlyList<T>? videos = JsonSerializer.Deserialize<IReadOnlyList<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return videos ?? throw new InvalidOperationException("Error deserializing the content");
        }
        catch (JsonException e)
        {
            throw new InvalidOperationException("Error deserializing the content", e);
        }
    }
}