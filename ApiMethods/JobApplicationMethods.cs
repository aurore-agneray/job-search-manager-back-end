using AutoMapper;
using JobSearchManagerBack.Data;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;
using JobSearchManagerBack.Texts;
using JobSearchManagerBack.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBack.ApiMethods;

/// <summary>
/// Defines all CRUD methods for the JobApplication entities
/// </summary>
internal static class JobApplicationMethods
{
    /// <summary>
    /// Get all job applications from the database
    /// </summary>
    /// <response code="200">The job applications has been found</response>
    internal static HashSet<JobApplicationGetDTO> GetAll(
        [FromServices] SqlServerDbContext database,
        [FromServices] IMapper mapper
    )
    {
        var jobApps = mapper.Map<HashSet<JobApplicationGetDTO>>(
            database.JobApplications.Include(applic => applic.Status)
        );
        return [.. jobApps];
    }

    /// <summary>
    /// Create a new job application in the database
    /// </summary>
    /// <response code="200">The job application has been created</response>
    /// <response code="400">The formats of some entries are invalid</response>
    /// <response code="500">An error occurred into the process, returns an explicit information message</response>
    internal static IResult PostOne(
        [FromServices] SqlServerDbContext database,
        [FromServices] IMapper mapper,
        [FromBody] JobApplicationPostDTO data
    )
    {
        if (data is null)
        {
            return Results.Problem(
                detail: RequestsErrorTexts.ERROR_EMPTY_DATA,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        DataValidator validator = new(database.Statuses, data);
        var potentialErrors = validator.ValidatePostedOneJobApplication();

        if (potentialErrors is not null)
        {
            return Results.ValidationProblem(potentialErrors);
        }

        // The status can be retrieved without any error because it was previously checked
        // TODO (PERHAPS) : ADD UPPERCASE CHECK FOR THE GUID
        Status status = database
            .Statuses.Where(s => data.StatusId.Equals(s.Guid.ToString()))
            .Single();

        JobApplication job = new()
        {
            Date = !string.IsNullOrEmpty(data.Date) ? DateTime.Parse(data.Date) : null,
            Source = data.Source,
            IsSpontaneous = data.IsSpontaneous,
            IsFromMyInitiative = data.IsFromMyInitiative,
            OfferUrl = data.OfferUrl,
            Position = data.Position,
            Place = data.Place,
            Status = status,
            Motivations = data.Motivations,
            Notes = data.Notes,
            Contacts = data.Contacts,
            FeelingLevel = data.FeelingLevel ?? 0,
        };

        database.JobApplications.Add(job);
        database.SaveChanges();

        return Results.Ok(mapper.Map<JobApplicationGetDTO>(job));
    }

    /// <summary>
    /// Delete a job application from the database
    /// </summary>
    /// <param name="database">Entity Framework Db Context</param>
    /// <param name="id">The encrypted id of the job application</param>
    /// <returns>A StatusCode response</returns>
    /// <response code="200">The job application has been deleted</response>
    /// <response code="404">The job application has not been found</response>
    internal static IResult DeleteOne(
        [FromServices] SqlServerDbContext database,
        [FromQuery] string id
    )
    {
        JobApplication? jobApp = null;

        if (
            (
                jobApp = database.JobApplications.FirstOrDefault(jobApp =>
                    jobApp.Guid.ToString() == id
                )
            ) != null
        )
        {
            database.Remove(jobApp);
            database.SaveChanges();

            return Results.Ok(RequestsErrorTexts.OK_JOB_APPLICATION_DELETED);
        }

        return Results.NotFound(RequestsErrorTexts.ERROR_JOB_APPLICATION_NOT_IDENTIFIED);
    }
}
