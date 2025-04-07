namespace JobSearchManagerBack.Texts;

internal class RequestsErrorTexts
{
    public const string SOURCE = "Source";
    public const string POSITION = "Poste";
    public const string PLACE = "Lieu";
    public const string OK_JOB_APPLICATION_DELETED = "La candidature a été supprimée";
    public const string ERROR_EMPTY_DATA = "Les données n'ont pas été fournies";
    public const string ERROR_STATUS_NOT_IDENTIFIED = "Le statut n'a pas été identifié";
    public const string ERROR_JOB_APPLICATION_NOT_IDENTIFIED = "La candidature n'a pas été trouvée";
    public const string ERROR_DATE_FORMAT = "Le format de la date fournie est incorrect";

    public static string GetRequiredMessage(string fieldName)
    {
        return $"Le champ '{fieldName}' doit être saisi";
    }
}
