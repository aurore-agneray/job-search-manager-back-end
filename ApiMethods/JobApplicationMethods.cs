using AutoMapper;
using JobSearchManagerBackEnd.Data;
using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;
using JobSearchManagerBackEnd.Repositories;
using JobSearchManagerBackEnd.Texts;
using JobSearchManagerBackEnd.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBackEnd.ApiMethods;

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
        var validationResult = CheckGivenDataForPostingOrUpdating(database, data);

        if (validationResult is not null)
        {
            return validationResult;
        }

        var jobAppRepository = new JobApplicationRepository(database);
        var statusRepository = new StatusRepository(database);

        // The status can be retrieved without any error because it was previously checked
        // TODO (PERHAPS) : ADD UPPERCASE CHECK FOR THE GUID
        Status status = statusRepository.GetStatusById(data.StatusId);

        JobApplication job = EntitiesGenerator.GeneratePostedJobApplication(data, status);

        jobAppRepository.InsertOne(job);

        return Results.Ok(mapper.Map<JobApplicationGetDTO>(job));
    }

    /// <summary>
    /// Update an existing job application in the database
    /// </summary>
    /// <response code="200">The job application has been updated</response>
    /// <response code="400">The formats of some entries are invalid</response>
    /// <response code="500">An error occurred into the process, returns an explicit information message</response>
    internal static IResult UpdateOne(
        [FromServices] SqlServerDbContext database,
        [FromServices] IMapper mapper,
        [FromBody] JobApplicationPostDTO data,
        [FromQuery] string id
    )
    {
        var validationResult = CheckGivenDataForPostingOrUpdating(database, data);
        
        if (validationResult is not null)
        {
            return validationResult;
        }

        var jobAppRepository = new JobApplicationRepository(database);
        var statusRepository = new StatusRepository(database);

        // The status can be retrieved without any error because it was previously checked
        // TODO (PERHAPS) : ADD UPPERCASE CHECK FOR THE GUID
        Status status = statusRepository.GetStatusById(data.StatusId);

        JobApplication? job = jobAppRepository.GetOneById(id);

        if (job is null)
        {
            return Results.NotFound(RequestsErrorTexts.ERROR_JOB_APPLICATION_NOT_IDENTIFIED);
        }

        EntitiesUpdator.UpdateJobApplication(job, data, status);

        jobAppRepository.UpdateOne(job);

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

        /* Try to retrieve the job application from the database
        identified by its GUID before deleting it */
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

    /// <summary>
    /// Checks the data sent by the user to create or update a job application and returns an error if any
    /// </summary>
    /// <param name="database">Entity Framework Db Context</param>
    /// <param name="data">The job application data to validate</param>
    /// <returns>A validation error if any, otherwise null</returns>
    private static IResult? CheckGivenDataForPostingOrUpdating(
        SqlServerDbContext database, JobApplicationPostDTO data
    ) {
        if (data is null)
        {
            return Results.Problem(
                detail: RequestsErrorTexts.ERROR_EMPTY_DATA,
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        // Validation of the formats of the sent data
        DataValidator validator = new(database.Statuses, data);
        var potentialErrors = validator.ValidatePostedOneJobApplication();

        if (potentialErrors is not null)
        {
            return Results.ValidationProblem(potentialErrors);
        }

        return null;
    }
}
