using FluentValidation;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;

internal class JobApplicationPostDTOValidator : AbstractValidator<JobApplicationPostDTO>
{
    public JobApplicationPostDTOValidator(HashSet<Status> allStatuses)
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
            .SetValidator(new StatusValidator<JobApplicationPostDTO>(allStatuses));
        RuleFor(jobApp => jobApp.FeelingLevel).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
    }

    private string GetRuleMessage(string fieldName)
    {
        return $"Le champ '{fieldName}' doit �tre saisi";
    }
}
