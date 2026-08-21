using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.Managers;

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
    /// Generate a job application POST DTO from the data IMPORTED from a file by the user
    /// </summary>
    /// <param name="data">Data retrieved from the file</param>
    /// <param name="statusGuid">The GUID of the status to bind to the job application</param>
    /// <returns>A new JobApplicationPostDTO object</returns>
    internal static JobApplicationPostDTO GenerateImportedJobApplicationDTO(
        string[] data,
        string statusGuid
    )
    {
        // The returned array is relative to the selected Excel range [4..16], so indexes 0..12
        // correspond to Excel columns 4..16 respectively.
        return new JobApplicationPostDTO
        {
            Date = data[12],
            Source = data[0],
            IsSpontaneous = data[1] == "TRUE" ? true : false,
            IsFromMyInitiative = data[2] == "TRUE" ? true : false,
            OfferUrl = data[3],
            Position = data[4],
            Place = data[5],
            StatusId = statusGuid,
            Motivations = data[7],
            Notes = data[8],
            Contacts = data[9],
            FeelingLevel = Int32.TryParse(data[10], out int result) ? Convert.ToInt32(data[10]) : 0
        };
    }
}
