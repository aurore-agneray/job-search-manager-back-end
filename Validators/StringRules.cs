using FluentValidation;

namespace JobSearchManagerBack.Validators;

internal static class StringRules
{
    const string _urlRegexExpression =
        @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";

    public static IRuleBuilderOptions<T, string> NeitherNullNorEmpty<T>(
        this IRuleBuilder<T, string> ruleBuilder
    )
    {
        return ruleBuilder.NotNull().NotEmpty();
    }

    public static IRuleBuilderOptions<T, string?> Url<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder.Matches(_urlRegexExpression);
    }
}
