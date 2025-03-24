using FluentValidation;
using FluentValidation.Validators;
using JobSearchManagerBack.Entities;

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
        if (int.TryParse(statusId, out int intStatusId))
        {
            return _allStatuses.Any(s => s.Id == int.Parse(statusId));
        }

        return false;
    }

    protected override string GetDefaultMessageTemplate(string errorCode) =>
        RequestsErrorTexts.ERROR_STATUS_NOT_IDENTIFIED;
}
