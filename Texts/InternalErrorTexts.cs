namespace JobSearchManagerBackEnd.Texts;

/// <summary>
/// Contains the texts used into the app that NEVER WILL BE SENT TO THE FRONT CLIENT !!
/// </summary>
internal class InternalErrorTexts
{
    /// <summary>
    /// Used into StartUpDI for reporting the missing connection string
    /// </summary>
    public const string ERROR_MISSING_CONNEXION_STRING =
        "Les paramètres de connexion à la base de données {0} sont manquants";

    /// <summary>
    /// Used into StartUpOptions for reporting the missing front-end domains
    /// </summary>
    public const string ERROR_MISSING_CORS_POLICY_DOMAINS =
        "Some settings are missing for configuring CORS policy";
}
