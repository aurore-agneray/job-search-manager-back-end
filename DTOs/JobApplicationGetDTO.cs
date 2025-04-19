using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearchManagerBackEnd.DTOs;

internal class JobApplicationGetDTO
{
    [Column("id")]
    public required string Id { get; set; }

    [Column("Date")]
    public string Date { get; set; } = string.Empty;

    [Column("Source")]
    public required string Source { get; set; }

    [Column("IsSpontaneous")]
    public required bool IsSpontaneous { get; set; }

    [Column("IsFromMyInitiative")]
    public required bool IsFromMyInitiative { get; set; } = true;

    [Column("OfferUrl")]
    public string OfferUrl { get; set; } = string.Empty;

    [Column("Position")]
    public required string Position { get; set; }

    [Column("Place")]
    public required string Place { get; set; }

    [Column("Status")]
    public required string StatusId { get; set; }

    [Column("Motivations")]
    public string Motivations { get; set; } = string.Empty;

    [Column("Notes")]
    public string Notes { get; set; } = string.Empty;

    [Column("Contacts")]
    public string Contacts { get; set; } = string.Empty;

    [Column("FeelingLevel")]
    public int FeelingLevel { get; set; }
}
