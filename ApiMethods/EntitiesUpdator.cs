using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.ApiMethods;

/// <summary>
/// Dedicated class to update entities from the data sent by the user
/// </summary>
internal static class EntitiesUpdator
{
    /// <summary>
    /// Updates a JobApplication entity with the data sent by the user.
    /// </summary>
    /// <param name="jobAppToUpdate">The JobApplication entity to update</param>
    /// <param name="data">Data retrieved from the call of the PUT Job Applicationg method</param>
    /// <param name="status">The status entity to bind to the job application</param>
    /// <returns>A new JobApplication object</returns>
    internal static void UpdateJobApplication(
        JobApplication jobAppToUpdate,
        JobApplicationPostDTO data,
        Status status
    )
    {
        jobAppToUpdate.Date = !string.IsNullOrEmpty(data.Date) ? DateTime.Parse(data.Date) : null;
        jobAppToUpdate.Source = data.Source;
        jobAppToUpdate.IsSpontaneous = data.IsSpontaneous;
        jobAppToUpdate.IsFromMyInitiative = data.IsFromMyInitiative;
        jobAppToUpdate.OfferUrl = data.OfferUrl;
        jobAppToUpdate.Position = data.Position;
        jobAppToUpdate.Place = data.Place;
        jobAppToUpdate.Status = status;
        jobAppToUpdate.Motivations = data.Motivations;
        jobAppToUpdate.Notes = data.Notes;
        jobAppToUpdate.Contacts = data.Contacts;
        jobAppToUpdate.FeelingLevel = data.FeelingLevel ?? 0;
    }
}
