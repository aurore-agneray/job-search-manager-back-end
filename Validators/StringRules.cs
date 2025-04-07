using FluentValidation;
using JobSearchManagerBack.Texts;

namespace JobSearchManagerBack.Validators;

internal static class StringRules
{
    const string _urlRegexExpression =
        @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    /// <summary>
    /// Validates that the given string value is not null and not empty
    /// </summary>
    public static IRuleBuilderOptions<T, string> NeitherNullNorEmpty<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder.NotNull().NotEmpty();
    }

    /// <summary>
    /// Validates that the given string value has a correct URL format
    /// </summary>
    public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Matches(_urlRegexExpression);
    }

    /// <summary>
    /// Validates that the given string value is null or empty or a correct date format
    /// </summary>
    public static IRuleBuilderOptions<T, string?> DateFormat<T>(
        this IRuleBuilder<T, string?> ruleBuilder
    )
    {
        return (IRuleBuilderOptions<T, string?>)
            ruleBuilder.Custom(
                (date, context) =>
                {
                    if (!string.IsNullOrEmpty(date) && !DateTime.TryParse(date, out _))
                    {
                        context.AddFailure(
                            context.PropertyPath,
                            RequestsErrorTexts.ERROR_DATE_FORMAT
                        );
                    }
                }
            );
    }
}
