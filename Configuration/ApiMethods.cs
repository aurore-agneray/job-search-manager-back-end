using JobSearchManagerBack.ApiMethods;

namespace JobSearchManagerBack.Configuration;

internal static class ApiMethods
{
    internal static void Configure(WebApplication app)
    {
        app.MapGet("/jobapplications", JobApplicationMethods.GetAll)
            .WithName("GetAllJobApplications")
            .WithOpenApi();

        app.MapPost("/jobapplication", JobApplicationMethods.PostOne)
            .WithName("PostJobApplication")
            .WithOpenApi();

        app.MapDelete("/jobapplication", JobApplicationMethods.DeleteOne)
            .WithName("DeleteJobApplication")
            .WithOpenApi();

        app.MapGet("/statuses", StatusMethods.GetAll).WithName("GetAllStatuses").WithOpenApi();
    }
}
