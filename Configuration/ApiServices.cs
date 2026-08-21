using JobSearchManagerBackEnd.Services;
using Microsoft.AspNetCore.Antiforgery;

namespace JobSearchManagerBackEnd.Configuration;

/// <summary>
/// Describes the CRUD methods of the application
/// </summary>
internal static class ApiServices
{
    /// <summary>
    /// Configure the API methods of the application, have to be called into the Program.cs file
    /// </summary>
    /// <param name="app">The object representation of the application</param>
    internal static void Configure(WebApplication app)
    {
        // Get token endpoint
        app.MapGet("antiforgery/token", (IAntiforgery forgeryService, HttpContext context) =>
        {
            var tokens = forgeryService.GetAndStoreTokens(context);
            var requestToken = tokens.RequestToken;
            return TypedResults.Content(requestToken ?? string.Empty, "text/plain");
        });
        //.RequireAuthorization(); // TODO : In a real world scenario, you'll only give this token to authorized users

        app.MapGet("/jobapplications", JobApplicationServices.GetAll)
            .WithName("GetAllJobApplications")
            .WithOpenApi();

        app.MapPost("/importjobapps", JobApplicationServices.ImportSeveralFromXlsx)
            .WithName("ImportJobApplications")
            // .DisableAntiforgery()
            .WithOpenApi();

        app.MapPost("/jobapplication", JobApplicationServices.PostOne)
            .WithName("PostJobApplication")
            .WithOpenApi();

        app.MapPut("/jobapplication", JobApplicationServices.UpdateOne)
            .WithName("UpdateJobApplication")
            .WithOpenApi();

        app.MapDelete("/jobapplication", JobApplicationServices.DeleteOne)
            .WithName("DeleteJobApplication")
            .WithOpenApi();

        app.MapGet("/statuses", StatusServices.GetAll).WithName("GetAllStatuses").WithOpenApi();
    }
}
