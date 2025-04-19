using FluentValidation;
using JobSearchManagerBack.DTOs;
using JobSearchManagerBack.Entities;
using JobSearchManagerBack.Texts;

namespace JobSearchManagerBack.Validators.Fluent;

internal class JobApplicationPostDTOValidator : AbstractValidator<JobApplicationPostDTO>
{
    public JobApplicationPostDTOValidator(HashSet<Status> allStatuses)
    {
        RuleFor(jobApp => jobApp.Date).DateFormat();
        RuleFor(jobApp => jobApp.Source)
            .NeitherNullNorEmpty()
            .WithMessage(RequestsErrorTexts.GetRequiredMessage(RequestsErrorTexts.SOURCE));
        RuleFor(jobApp => jobApp.IsSpontaneous).NotNull();
        RuleFor(jobApp => jobApp.IsFromMyInitiative).NotNull();
        RuleFor(jobApp => jobApp.OfferUrl).Url();
        RuleFor(jobApp => jobApp.Position)
            .NeitherNullNorEmpty()
            .WithMessage(RequestsErrorTexts.GetRequiredMessage(RequestsErrorTexts.POSITION));
        RuleFor(jobApp => jobApp.Place)
            .NeitherNullNorEmpty()
            .WithMessage(RequestsErrorTexts.GetRequiredMessage(RequestsErrorTexts.PLACE));
        RuleFor(jobApp => jobApp.StatusId)
            .NeitherNullNorEmpty()
            .SetValidator(new StatusValidator<JobApplicationPostDTO>(allStatuses));
        RuleFor(jobApp => jobApp.FeelingLevel).GreaterThanOrEqualTo(0).LessThanOrEqualTo(5);
    }
}
