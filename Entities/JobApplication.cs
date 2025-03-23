using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBack.Entities;

[Table("JobApplications")]
[PrimaryKey("Id")]
internal class JobApplication
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("Date")]
    public DateTime? Date { get; set; }

    [Column("Source")]
    public required string Source { get; set; }

    [Column("IsSpontaneous")]
    public required bool IsSpontaneous { get; set; }

    [Column("IsFromMyInitiative")]
    public required bool IsFromMyInitiative { get; set; } = true;

    [Column("OfferUrl")]
    public string? OfferUrl { get; set; }

    [Column("Position")]
    public required string Position { get; set; }

    [Column("Place")]
    public required string Place { get; set; }

    [Column("Status")]
    public required Status Status { get; set; }

    [Column("Motivations")]
    public string? Motivations { get; set; }

    [Column("Notes")]
    public string? Notes { get; set; }

    [Column("Contacts")]
    public string? Contacts { get; set; }

    [Column("FeelingLevel")]
    public int FeelingLevel { get; set; }
}
