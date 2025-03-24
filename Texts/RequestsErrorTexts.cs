namespace JobSearchManagerBack.Texts;

internal class RequestsErrorTexts
{
    public const string SOURCE = "Source";
    public const string POSITION = "Poste";
    public const string PLACE = "Lieu";
    public const string ERROR_EMPTY_DATA = "Les données n'ont pas été fournies";
    public const string ERROR_STATUS_NOT_IDENTIFIED = "Le statut n'a pas été identifié";

    public static string GetRequiredMessage(string fieldName)
    {
        return $"Le champ '{fieldName}' doit être saisi";
    }
}
