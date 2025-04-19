using FluentValidation.Results;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;
using JobSearchManagerBack.Validators.Fluent;
using Microsoft.EntityFrameworkCore;

namespace JobSearchManagerBack.Validators;

/// <summary>
/// Dedicated class to validate the data sent by the user
/// </summary>
/// <param name="statuses">List of all the statuses available into the database</param>
/// <param name="data">The data which will be validated, sent by the user</param>
internal class DataValidator(DbSet<Status> statuses, JobApplicationPostDTO data)
{
    /// <summary>
    /// List of all the statuses available into the database
    /// </summary>
    private readonly DbSet<Status> _statuses = statuses;

    /// <summary>
    /// The data which will be validated, sent by the user
    /// </summary>
    private readonly JobApplicationPostDTO _jobApplicationPostData = data;

    /// <summary>
    /// Validates the format of the given data using FluentValidation.
    /// Generates and returns error messages if relevant.
    /// </summary>
    /// <param name="data">The data that will be validated, sent by the user</param>
    /// <returns>A dictionary with the potential errors.
    /// The keys contain the concerned properties and the values the error messages.</returns>
    internal Dictionary<string, string[]>? ValidatePostedOneJobApplication()
    {
        Dictionary<string, string[]>? potentialErrors = null;
        JobApplicationPostDTOValidator validator = new([.. _statuses]);
        ValidationResult results = validator.Validate(_jobApplicationPostData);

        if (!results.IsValid)
        {
            // Using of a counter to avoid duplicate keys in the dictionary
            int errorCounter = 1;
            potentialErrors = results.Errors.ToDictionary(
                (error) => @$"{error.PropertyName}_{errorCounter++}",
                (error) => new string[] { error.ErrorMessage }
            );
        }

        return potentialErrors;
    }
}
