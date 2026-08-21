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
        "Les param�tres de connexion � la base de donn�es {0} sont manquants";

    /// <summary>
    /// Used into StartUpOptions for reporting the missing front-end domains
    /// </summary>
    public const string ERROR_MISSING_CORS_POLICY_DOMAINS =
        "Some settings are missing for configuring CORS policy";

    /// <summary>
    /// Used into ReadFromXlsx for reporting the no detected last row of the Excel file
    /// </summary>
    public const string ERROR_READ_XLSX_LAST_ROW = "The last row has not been detected !";

    /// <summary>
    /// Used into ReadFromXlsx for reporting a no detected cell of the Excel file
    /// </summary>
    public const string ERROR_READ_XLSX_CELL = "The cell at row {0}, column {1} has not been detected !";
}
