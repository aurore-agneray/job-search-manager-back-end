using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBackEnd.Entities;

/// <summary>
/// Represents the state of a job application (Sent, Processing, Rejected, etc)
/// </summary>
[PrimaryKey(nameof(Id), nameof(Guid))]
internal class Status
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required Guid Guid { get; set; }

    /// <summary>
    /// Translated name of the status (Envoyée, Pas de réponse, Ghostée, etc)
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Code name of the status (Sent, Processing, Rejected, RefusedWithoutMeeting, etc)
    /// </summary>
    public required string CodeName { get; set; }

    /// <summary>
    /// Color associated to the status for the front-end web app
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Name of the Material Design Icon used into the front-end web app
    /// https://pictogrammers.com/library/mdi/
    /// </summary>
    public required string IconName { get; set; }
}
