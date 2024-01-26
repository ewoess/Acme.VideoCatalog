namespace Acme.VideoCatalog.Domain.Models;

/// <summary>
/// Represents a video in the Acme Video Catalog.
/// </summary>
/// <remarks>
/// This class is used to model the properties of a video, including its title, description, running time, and related video IDs.
/// </remarks>
public class Video
{
    /// <summary>
    /// Gets or sets the title of the video.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// Gets or sets the bullet text of the video.
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
    /// Gets or sets the ID of the video.
    /// </summary>
    public string? Id { get; set; }
    /// <summary>
    /// Gets or sets the URL of the video's artwork.
    /// </summary>
    public string? ArtUrl { get; set; }
    /// <summary>
    /// Gets or sets the list of IDs related to the video.
    /// </summary>
    public List<string> RelatedIds { get; set; }
}