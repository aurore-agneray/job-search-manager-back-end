using System.Diagnostics.CodeAnalysis;
using JobSearchManagerBackEnd.Data;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.Repositories;

/// <summary>
/// Repository class for the JobApplication entity
/// </summary>
internal class JobApplicationRepository
{
    /// <summary>
    /// The database context used to access the database
    /// </summary>
    [NotNull]
    internal SqlServerDbContext _database;

    /// <summary>
    /// Initializes a new instance of the JobApplicationRepository class with the specified database context.
    /// </summary>
    /// <param name="database">The database context</param>
    internal JobApplicationRepository(SqlServerDbContext database)
    {
        _database = database;
    }

    /// <summary>
    /// Retrieves a JobApplication entity from the database based on the provided id.
    /// </summary>
    /// <param name="id">The id of the job application to retrieve</param>
    /// <returns>The JobApplication entity with the specified id</returns>
    internal JobApplication? GetOneById(string id)
    {
        return _database.JobApplications.FirstOrDefault(job => job.Guid.ToString() == id);
    }

    /// <summary>
    /// Inserts a new JobApplication entity into the database.
    /// </summary>
    /// <param name="jobApplication">The job application to insert</param>
    internal void InsertOne(JobApplication jobApplication)
    {
        _database.JobApplications.Add(jobApplication);
        _database.SaveChanges();
    }

    /// <summary>
    /// Updates an existing JobApplication entity in the database.
    /// </summary>
    /// <param name="jobApplication">The job application to update</param>
    internal void UpdateOne(JobApplication jobApplication)
    {
        _database.JobApplications.Update(jobApplication);
        _database.SaveChanges();
    }
}