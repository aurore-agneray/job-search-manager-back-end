namespace JobSearchManagerBack.Entities;

/// <summary>
/// Represents the state of a job application (Sent, Processing, Rejected, etc)
/// </summary>
internal class Status
{
    public required int Id { get; set; }

    public required Guid Guid { get; set; }

    public required string Name { get; set; }
}
