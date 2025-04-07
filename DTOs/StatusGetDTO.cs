using System.ComponentModel.DataAnnotations.Schema;

namespace JobSearchManagerBack.DTOs;

internal class StatusGetDTO
{
    [Column("id")]
    public required string Id { get; set; }

    [Column("name")]
    public required string Name { get; set; }
}
