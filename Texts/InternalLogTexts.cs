namespace JobSearchManagerBackEnd.Texts;

/// <summary>
/// Contains texts used into the app that NEVER WILL BE SENT TO THE FRONT CLIENT !!
/// </summary>
internal class InternalLogTexts
{
    /// <summary>
    /// Describes an imported row from a .xlsx file, used into ReadFromXlsx for reporting the imported data
    /// </summary>
    internal const string LOG_IMPORT_JOB_APPLICATIONS = "Row {0}: Source cell value = {1}";
}
