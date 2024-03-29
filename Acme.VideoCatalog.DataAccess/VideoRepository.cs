﻿using System.Text.Json;
using Acme.VideoCatalog.DataAccess.Dtos;
using Acme.VideoCatalog.Services.Exceptions;
using Acme.VideoCatalog.Services.Repositories;

namespace Acme.VideoCatalog.DataAccess;
/// <summary>
/// Represents a repository for managing video data.
/// </summary>
/// <typeparam name="T">The type of video data object. This type parameter must be a subclass of <see cref="VideoDto"/>.</typeparam>
/// <remarks>
/// This class is a concrete implementation of the <see cref="IRepository{T}"/> interface. It uses an instance of <see cref="HttpClient"/> to communicate with a remote service.
/// </remarks>
public class VideoRepository<T> : IRepository<T> where T : VideoDto
{
    private readonly HttpClient _httpClient;
    private readonly string _getAllUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="VideoRepository{T}"/> class.
    /// </summary>
    /// <param name="httpClient">The HttpClient used for making HTTP requests.</param>
    /// <param name="getAllUrl">The URL used for retrieving all video data.</param>
    public VideoRepository(HttpClient httpClient, string getAllUrl)
    {
        _httpClient = httpClient;
        _getAllUrl = getAllUrl;

    }
    /// <summary>
    /// Asynchronously retrieves all video entries from the remote service.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a read-only list of video entries.
    /// </returns>
    /// <exception cref="DataRetrievalException">Thrown when there is an error retrieving data from the remote service.</exception>
    /// <exception cref="DataParsingException">Thrown when there is an error parsing the data from the remote service.</exception>
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var response = await _httpClient.GetAsync(_getAllUrl);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            throw new DataRetrievalException("Error retrieving data from the remote service", e);
        }

        try
        {
            string content = await response.Content.ReadAsStringAsync();
            IReadOnlyList<T>? videos = JsonSerializer.Deserialize<IReadOnlyList<T>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return videos ?? new List<T>().AsReadOnly();
        }
        catch (JsonException e)
        {
            throw new DataParsingException("Error parsing the data from the remote service", e);
        }
    }
}