using JobSearchManagerBackEnd.Texts;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace JobSearchManagerBackEnd.Configuration;

/// <summary>
/// Defines the options for the services declared into the Program.cs file
/// </summary>
public static class BuilderOptions
{
    /// <summary>
    /// Generates the general options for the Cors service
    /// (for instance the accepted front domains)
    /// </summary>
    /// <param name="frontEndDomains">Allowed front-end domains for calling the API,
    /// separated by ;</param>
    /// <returns>An Action object</returns>
    public static Action<CorsOptions> GetCorsOptions(string frontEndDomains)
    {
        if (string.IsNullOrEmpty(frontEndDomains))
        {
            throw new ArgumentException(InternalErrorTexts.ERROR_MISSING_CORS_POLICY_DOMAINS);
        }

        string[] frontDomains = frontEndDomains.Split(';');

        return options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                    .WithOrigins(frontDomains.Length > 0 ? frontDomains : [Constants.DEFAULT_FRONT_DOMAIN])
                    .WithHeaders("Content-type", Constants.ANTIFORGERY_HEADER_NAME)
                    .WithMethods("GET", "POST", "DELETE", "PUT")
                    .AllowCredentials();
            });
        };
    }
}
