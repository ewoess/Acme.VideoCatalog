namespace Acme.VideoCatalog.Domain.DataModels;
/// <summary>
/// Represents the data model for a video in the Acme Video Catalog.
/// </summary>
/// <remarks>
/// This class is used to abstract away the VideoDto from the domain video model.
/// </remarks>

public class VideoData
{
    /// <summary>
    /// Gets or sets the title of the video.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the bullet text associated with the video, often used for brief highlights or key points.
    /// </summary>
    public string? BulletText { get; set; }

    /// <summary>
    /// Gets or sets a detailed description of the video.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the running time of the video in minutes.
    /// </summary>
    public int RunningTime { get; set; }

    /// <summary>
    /// Gets or sets a unique identifier for the video.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the URL of the artwork or thumbnail image associated with the video.
    /// </summary>
    public string? ArtUrl { get; set; }
    
    /// <summary>
    /// Gets or sets a list of identifiers for videos that are related to this video.
    /// </summary>
    public List<string> RelatedIds { get; set; }
}