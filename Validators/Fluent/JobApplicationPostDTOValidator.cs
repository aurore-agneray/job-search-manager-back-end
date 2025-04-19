using FluentValidation;
using JobSearchManagerBackEnd.DTOs;
using JobSearchManagerBackEnd.Entities;
using JobSearchManagerBackEnd.Texts;

namespace JobSearchManagerBackEnd.Validators.Fluent;

/// <summary>
/// Validator dedicated to be called into the job application POST method
/// </summary>
internal class JobApplicationPostDTOValidator : AbstractValidator<JobApplicationPostDTO>
{
    /// <summary>
    /// Constructor of the validator
    /// </summary>
    /// <param name="allStatuses">All statuses available into the database</param>
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
