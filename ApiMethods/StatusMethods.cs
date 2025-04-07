using AutoMapper;
using JobSearchManagerBack.Data;
using JobSearchManagerBack.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchManagerBack.ApiMethods;

/// <summary>
/// Defines all CRUD methods for the Status entities
/// </summary>
internal static class StatusMethods
{
    /// <summary>
    /// Get all statuses from the database
    /// </summary>
    /// <response code="200">The statuses has been found</response>
    internal static HashSet<StatusGetDTO> GetAll(
        [FromServices] SqlServerDbContext database,
        [FromServices] IMapper mapper
    )
    {
        var statuses = mapper.Map<HashSet<StatusGetDTO>>(database.Statuses);
        return [.. statuses];
    }
}
