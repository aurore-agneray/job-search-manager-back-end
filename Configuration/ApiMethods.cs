using JobSearchManagerBackEnd.ApiMethods;

namespace JobSearchManagerBackEnd.Configuration;

/// <summary>
/// Describes the CRUD methods of the application
/// </summary>
internal static class ApiMethods
{
    /// <summary>
    /// Configure the API methods of the application, have to be called into the Program.cs file
    /// </summary>
    /// <param name="app">The object representation of the application</param>
    internal static void Configure(WebApplication app)
    {
        app.MapGet("/jobapplications", JobApplicationMethods.GetAll)
            .WithName("GetAllJobApplications")
            .WithOpenApi();

        app.MapPost("/jobapplication", JobApplicationMethods.PostOne)
            .WithName("PostJobApplication")
            .WithOpenApi();

        app.MapPut("/jobapplication", JobApplicationMethods.UpdateOne)
            .WithName("UpdateJobApplication")
            .WithOpenApi();

        app.MapDelete("/jobapplication", JobApplicationMethods.DeleteOne)
            .WithName("DeleteJobApplication")
            .WithOpenApi();

        app.MapGet("/statuses", StatusMethods.GetAll).WithName("GetAllStatuses").WithOpenApi();
    }
}
