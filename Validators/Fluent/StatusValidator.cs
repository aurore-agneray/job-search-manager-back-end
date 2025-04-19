using FluentValidation;
using FluentValidation.Validators;
using JobSearchManagerBackEnd.Entities;
using JobSearchManagerBackEnd.Texts;

namespace JobSearchManagerBackEnd.Validators.Fluent;

/// <summary>
/// Validator dedicated to check the validity of the wanted status
/// </summary>
/// <param name="allStatuses">>List of all statuses</param>
internal class StatusValidator<T>(HashSet<Status> allStatuses) : PropertyValidator<T, string>
{
    /// <summary>
    /// List of all statuses available in the database
    /// </summary>
    private HashSet<Status> _allStatuses = allStatuses;

    /// <summary>
    /// Name of the validator, mandatory property
    /// </summary>
    public override string Name => "Status Validator";

    /// <summary>
    /// Is responsible for checking the validity of the status, mandatory method
    /// </summary>
    public override bool IsValid(ValidationContext<T> context, string statusId)
    {
        // TODO (PERHAPS) : ADD UPPERCASE CHECK FOR THE GUID
        return _allStatuses.Any(s => statusId.Equals(s.Guid.ToString()));
    }

    /// <summary>
    /// Returns the error message which describes an invalid status
    /// </summary>
    /// <param name="errorCode">HTTP Response code</param>
    protected override string GetDefaultMessageTemplate(string errorCode) =>
        RequestsErrorTexts.ERROR_STATUS_NOT_IDENTIFIED;
}
