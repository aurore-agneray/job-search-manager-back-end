using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.ApiMethods;

/// <summary>
/// Dedicated class to generate entities from the data sent by the user
/// </summary>
internal static class EntitiesGenerator
{
    /// <summary>
    /// Generates a new JobApplication entity from the data POSTED by the user.
    /// </summary>
    /// <param name="data">Data retrieved from the call of the POST Job Applicationg method</param>
    /// <param name="status">The status entity to bind to the job application</param>
    /// <returns>A new JobApplication object</returns>
    internal static JobApplication GeneratePostedJobApplication(
        JobApplicationPostDTO data,
        Status status
    )
    {
        // Only initializes the required properties at the beginning
        JobApplication newJobApplication = new()
        {
            Source = string.Empty,
            IsSpontaneous = false,
            IsFromMyInitiative = false,
            Position = string.Empty,
            Place = string.Empty,
            Status = status
        };

        // Updates all properties
        EntitiesUpdator.UpdateJobApplication(newJobApplication, data, status);
        return newJobApplication;
    }

    /// <summary>
    /// Generate a job application from the data IMPORTED from a file by the user
    /// </summary>
    /// <param name="data">Data retrieved from the file</param>
    /// <param name="status">The status entity to bind to the job application</param>
    /// <returns>A new JobApplication object</returns>
    internal static JobApplication GenerateImportedJobApplication(
        string[] data,
        Status status
    )
    {
        var jobAppDto = new JobApplicationPostDTO
        {
            Date = data[12],
            Source = data[0],
            IsSpontaneous = false,
            IsFromMyInitiative = true,
            // IsSpontaneous = data[4],
            // IsFromMyInitiative = data[5],
            OfferUrl = data[3],
            Position = data[4],
            Place = data[5],
            StatusId = status.Id.ToString(),
            Motivations = data[7],
            Notes = data[8],
            Contacts = data[9],
            // FeelingLevel = data[13]
        };

        return GeneratePostedJobApplication(jobAppDto, status);
    }
}
