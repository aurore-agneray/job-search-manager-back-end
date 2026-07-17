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
}
