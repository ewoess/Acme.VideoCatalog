namespace Acme.VideoCatalog.DataAccess.Dtos;
/// <summary>
/// Represents the data transfer object for a video. This class provides properties
/// to hold various details about a video.
/// </summary>
public class VideoDto
{
    /// <summary>
    /// Gets or sets the title of the video.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the bullet text associated with the video.
    /// </summary>
    public string? BulletText { get; set; }

    /// <summary>
    /// Gets or sets the description of the video.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the running time of the video in minutes.
    /// </summary>
    public int RunningTime { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the video.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the URL for the artwork associated with the video.
    /// </summary>
    public string? ArtUrl { get; set; }

    /// <summary>
    /// Gets or sets a list of related video IDs.
    /// </summary>
    public List<string> RelatedIds { get; set; }
}