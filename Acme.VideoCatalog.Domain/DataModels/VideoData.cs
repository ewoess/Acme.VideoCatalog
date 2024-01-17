namespace Acme.VideoCatalog.Domain.DataModels;

/// <summary>
/// Video Data to abstract away the VideoDto from the domain video model.
/// </summary>
public class VideoData
{
    public string? Title { get; set; }
    public string? BulletText { get; set; }
    public string? Description { get; set; }
    public int RunningTime { get; set; }
    public string? Id { get; set; }
    public string? ArtUrl { get; set; }
    public List<string> RelatedIds { get; set; }
}