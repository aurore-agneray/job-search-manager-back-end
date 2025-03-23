using JobSearchManagerBack.Data;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchManagerBack.Configuration;

internal static class ApiMethods
{
    internal static void Configure(WebApplication app)
    {
        app.MapGet(
                "/jobapplications",
                ([FromServices] SqlServerDbContext database) =>
                {
                    var jobApps = database.JobApplications;
                    Console.WriteLine(jobApps.Count());
                    return jobApps;
                }
            )
            .WithName("GetJobApplication")
            .WithOpenApi();

        app.MapPost(
                "/jobapplication",
                ([FromServices] SqlServerDbContext database, JobApplicationDTO data) =>
                {
                    if (data is null)
                    {
                        return Results.Problem("The data are empty !");
                    }

                    Status? status = database
                        .Status.Where(s => s.Id == int.Parse(data.StatusId))
                        .SingleOrDefault();

                    if (status is null)
                    {
                        return Results.Problem("The status has not been identified !");
                    }

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
            )
            .WithName("PostJobApplication")
            .WithOpenApi();
    }
}
