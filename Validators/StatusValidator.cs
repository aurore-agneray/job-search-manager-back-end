using FluentValidation;
using FluentValidation.Validators;
using JobSearchManagerBack.Entities;
using JobSearchManagerBack.Texts;

namespace JobSearchManagerBack.Validators;

internal class StatusValidator<T> : PropertyValidator<T, string>
{
    private HashSet<Status> _allStatuses;

    public StatusValidator(HashSet<Status> allStatuses)
    {
        _allStatuses = allStatuses;
    }

    public override string Name => "Status Validator";

    public override bool IsValid(ValidationContext<T> context, string statusId)
    {
        // TODO (PERHAPS) : ADD UPPERCASE CHECK FOR THE GUID
        return _allStatuses.Any(s => statusId.Equals(s.Guid.ToString()));
    }

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        RequestsErrorTexts.ERROR_STATUS_NOT_IDENTIFIED;
}
