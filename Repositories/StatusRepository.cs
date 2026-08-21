using System.Diagnostics.CodeAnalysis;
using JobSearchManagerBackEnd.Data;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.Repositories;

/// <summary>
/// Repository class for the Status entity
/// </summary>
internal class StatusRepository
{
    /// <summary>
    /// The database context used to access the database
    /// </summary>
    [NotNull]
    internal SqlServerDbContext _database;

    /// <summary>
    /// Initializes a new instance of the StatusRepository class with the specified database context.
    /// </summary>
    /// <param name="database">The database context</param>
    internal StatusRepository(SqlServerDbContext database)
    {
        _database = database;
    }

    /// <summary>
    /// Retrieves a Status entity from the database based on the provided statusId.
    /// </summary>
    /// <param name="statusId">The ID of the status to retrieve</param>
    /// <returns>The Status entity with the specified ID</returns>
    internal Status GetStatusById(string statusId)
    {
        return _database
            .Statuses.Where(s => statusId.Equals(s.Guid.ToString()))
            .Single();
    }

    /// <summary>
    /// Retrieves a Status entity from the database based on the provided Code Name.
    /// </summary>
    /// <param name="codeName">The code name of the status to retrieve</param>
    /// <returns>The Status entity with the specified code name, or null if not found</returns>
    internal Status? GetStatusByCodeName(string codeName)
    {
        return _database
            .Statuses.Where(s => codeName.Equals(s.CodeName))
            .FirstOrDefault();
    }
}