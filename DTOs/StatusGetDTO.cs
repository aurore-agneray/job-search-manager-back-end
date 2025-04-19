using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearchManagerBackEnd.DTOs;

internal class StatusGetDTO
{
    [Column("id")]
    public required string Id { get; set; }

    /// <summary>
    /// Name of the status (Sent, Processing, Rejected, etc)
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Color associated to the status for the front-end web app
    /// </summary>
    [Column("color")]
    public required string Color { get; set; }

    /// <summary>
    /// Name of the Material Design Icon used into the front-end web app
    /// https://pictogrammers.com/library/mdi/
    /// </summary>
    [Column("iconName")]
    public required string IconName { get; set; }
}
