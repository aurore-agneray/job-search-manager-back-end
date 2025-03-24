using FluentValidation;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;

internal class JobApplicationDTOValidator : AbstractValidator<JobApplicationDTO>
{
    public JobApplicationDTOValidator(HashSet<Status> allStatuses)
    {
        RuleFor(jobApp => jobApp.Source)
            .NeitherNullNorEmpty()
            .WithMessage(GetRuleMessage("Source"));
        RuleFor(jobApp => jobApp.IsSpontaneous).NotNull();
        RuleFor(jobApp => jobApp.IsFromMyInitiative).NotNull();
        RuleFor(jobApp => jobApp.OfferUrl).Url();
        RuleFor(jobApp => jobApp.Position)
            .NeitherNullNorEmpty()
            .WithMessage(GetRuleMessage("Poste"));
        RuleFor(jobApp => jobApp.Place).NeitherNullNorEmpty().WithMessage(GetRuleMessage("Lieu"));
        RuleFor(jobApp => jobApp.StatusId)
            .NeitherNullNorEmpty()
            .SetValidator(new StatusValidator<JobApplicationDTO>(allStatuses));
        RuleFor(jobApp => jobApp.FeelingLevel).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
    }

    private string GetRuleMessage(string fieldName)
    {
        return $"Le champ '{fieldName}' doit être saisi";
    }
}
