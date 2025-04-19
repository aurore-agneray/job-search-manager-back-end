using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;

namespace JobSearchManagerBackEnd.ApiMethods;

/// <summary>
/// Dedicated class to generate entities from the data sent by the user
/// </summary>
internal static class EntitiesGenerator
{
    /// <summary>
    /// Generates a JobApplication entity from the data POSTED by the user.
    /// </summary>
    /// <param name="data">Data retrieved from the call of the POST Job Applicationg method</param>
    /// <param name="status">The status entity to bind to the job application</param>
    /// <returns>A new JobApplication object</returns>
    internal static JobApplication GeneratePostedJobApplication(
        JobApplicationPostDTO data,
        Status status
    )
    {
        return new()
        {
            Date = !string.IsNullOrEmpty(data.Date) ? DateTime.Parse(data.Date) : null,
            Source = data.Source,
            IsSpontaneous = data.IsSpontaneous,
            IsFromMyInitiative = data.IsFromMyInitiative,
            OfferUrl = data.OfferUrl,
            Position = data.Position,
            Place = data.Place,
            Status = status,
            Motivations = data.Motivations,
            Notes = data.Notes,
            Contacts = data.Contacts,
            FeelingLevel = data.FeelingLevel ?? 0,
        };
    }
}
