using FluentValidation.Results;
using JobSearchManagerBack.Data;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;
using JobSearchManagerBack.Texts;
using JobSearchManagerBack.Validators;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchManagerBack.Configuration;

internal static class ApiMethods
{
    internal static void Configure(WebApplication app)
    {
        app.MapGet("/jobapplications", GetAllJobApplications)
            .WithName("GetAllJobApplications")
            .WithOpenApi();

        app.MapPost("/jobapplication", PostJobApplication)
            .WithName("PostJobApplication")
            .WithOpenApi();

        app.MapDelete("/jobapplication", DeleteJobApplication)
            .WithName("DeleteJobApplication")
            .WithOpenApi();
    }

    internal static HashSet<JobApplication> GetAllJobApplications(
        [FromServices] SqlServerDbContext database
    )
    {
        var jobApps = database.JobApplications;
        return [.. jobApps];
    }

    internal static IResult PostJobApplication(
        [FromServices] SqlServerDbContext database,
        [FromBody] JobApplicationPostDTO data
    )
    {
        if (data is null)
        {
            return Results.Problem(RequestsErrorTexts.ERROR_EMPTY_DATA);
        }

        Dictionary<string, string[]>? potentialErrors = null;

        // Dictionary<string, string[]>? potentialErrors = _GenericValidator<
        //     JobApplicationPostDTOValidator,
        //     JobApplicationPostDTO
        // >.TryValidation(data);

        JobApplicationPostDTOValidator validator = new(database.Status.ToHashSet());
        ValidationResult results = validator.Validate(data);

        if (!results.IsValid)
        {
            potentialErrors = results.Errors.ToDictionary(
                (value) => value.PropertyName,
                (value) => new string[] { value.ErrorMessage }
            );
        }

        if (potentialErrors is not null)
        {
            return Results.ValidationProblem(potentialErrors);
        }

        // Can be retrieved without any error because it was previously checked
        Status status = database.Status.Where(s => s.Id == int.Parse(data.StatusId)).Single();

        JobApplication job = new()
        {
            Source = data.Source,
            IsSpontaneous = data.IsSpontaneous,
            IsFromMyInitiative = data.IsFromMyInitiative,
            Position = data.Position,
            Status = status,
            Place = data.Place,
        };

        database.JobApplications.Add(job);
        database.SaveChanges();

        return Results.Ok(job);
    }

    internal static IResult DeleteJobApplication(
        [FromServices] SqlServerDbContext database,
        [FromQuery] string id
    )
    {
        JobApplication? jobApp = null;

        if (
            (jobApp = database.JobApplications.FirstOrDefault(jobApp => jobApp.Id.ToString() == id))
            != null
        )
        {
            database.Remove(jobApp);
            database.SaveChanges();

            return Results.Ok(RequestsErrorTexts.OK_JOB_APPLICATION_DELETED);
        }

        return Results.NotFound(RequestsErrorTexts.ERROR_JOB_APPLICATION_NOT_IDENTIFIED);
    }
}
