namespace JobSearchManagerBackEnd.DTOs;

/// <summary>
/// Description of a job offer displayed into the app
/// </summary>
internal class JobOfferDTO
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Location { get; set; }
}