using AutoMapper;
using JobSearchManagerBackEnd.Data;
using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;
using JobSearchManagerBackEnd.ImportTools;
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
    /// Import a list of job applications from a .xlsx file.
    /// COLUMNS OF THE COMPLETE FILE :
    /// id | date | DATE AU FORMAT TEXTE POUR SQL | source | isSpontaneous | fromMyInitiative | offerUrl | position	| place	| status | motivations | notes | contacts | feelingLevel | answerDelay (weeks) | dateForBackend
    /// </summary>
    /// <param name="database">Db Context</param>
    /// <exception cref="Exception">Appears if a cell is not properly read</exception>
    /// <response code="200">Returns an object with the number of inserted job applications and a list of JobApplicationGetDTOs</response>
    /// <response code="400">The formats of some entries are invalid</response>
    /// <response code="500">An error occurred into the process, returns an explicit information message</response>
    [ValidateAntiForgeryToken]
    internal static async Task<IResult> ImportSeveralFromXlsx(
        [FromServices] SqlServerDbContext database,
        [FromServices] IMapper mapper,
        [FromForm] IFormFile file
    )
    {
        if (file is null || file.Length == 0)
        {
            return Results.BadRequest("A .xlsx file must be provided.");
        }

        if (!file.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
        {
            return Results.BadRequest("Only .xlsx files are supported.");
        }

        int jobApplicationsCounter = 0;
        int firstRowIndex = 2;
        int firstColIndex = 4;
        int lastColIndex = 16;

        var jobAppRepository = new JobApplicationRepository(database);
        var statusRepository = new StatusRepository(database);

        JobApplication jobApp;
        List<JobApplicationGetDTO> insertedJobApps = new();
        JobApplicationPostDTO jobAppDto;
        Status status;

        await using var stream = file.OpenReadStream();

        // Process the retrieved data and return an error IMMEDIATELY when a validation error is detected
        foreach (var rowData in ReadFromXlsx.RetrieveData(stream, firstRowIndex, firstColIndex, lastColIndex))
        {
            status = statusRepository.GetStatusByCodeName(rowData[6]);

            // The returned array is relative to the selected Excel range [4..16], so indexes 0..12
            // correspond to Excel columns 4..16 respectively.
            jobAppDto = new JobApplicationPostDTO
            {
                Date = rowData[12],
                Source = rowData[0],
                IsSpontaneous = rowData[1] == "TRUE" ? true : false,
                IsFromMyInitiative = rowData[2] == "TRUE" ? true : false,
                OfferUrl = rowData[3],
                Position = rowData[4],
                Place = rowData[5],
                StatusId = status.Guid.ToString(),
                Motivations = rowData[7],
                Notes = rowData[8],
                Contacts = rowData[9],
                FeelingLevel = Int32.TryParse(rowData[10], out int result) ? Convert.ToInt32(rowData[10]) : 0
            };

            var validationResult = CheckGivenDataForPostingOrUpdating(database, jobAppDto);
        
            if (validationResult is not null)
            {
                return validationResult;
            }

            jobApp = EntitiesGenerator.GeneratePostedJobApplication(jobAppDto, status);
            jobApp = jobAppRepository.InsertOne(jobApp);
            insertedJobApps.Add(mapper.Map<JobApplicationGetDTO>(jobApp));

            jobApplicationsCounter += 1;
        }

        return Results.Ok(new {
            count = jobApplicationsCounter,
            insertedJobApps
        });
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
