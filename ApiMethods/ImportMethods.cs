using JobSearchManagerBackEnd.Data;
using JobSearchManagerBackEnd.Entities;
using JobSearchManagerBackEnd.ImportTools;
using Microsoft.AspNetCore.Mvc;

namespace JobSearchManagerBackEnd.ApiMethods;

/// <summary>
/// Defines all CRUD methods used for importing data from files
/// </summary>
internal static class ImportMethods
{
    /// <summary>
    /// Launch import from a .xlsx file
    /// </summary>
    /// <param name="database">Db Context</param>
    /// <exception cref="Exception">Appears if a cell is not properly read</exception>
    internal static void LaunchFromXlsx(
        [FromServices] SqlServerDbContext database
    )
    {
        int columnsQuantity = 16;
        int firstRowIndex = 2;
        int firstColIndex = 4;
        string fileName = "Test-import.xlsx";

        JobApplication jobApp;
        Status defaultStatus = database.Statuses.First();

        List<string[]> dataList = ReadFromXlsx.RetrieveData(fileName, columnsQuantity, firstRowIndex, firstColIndex);

        // Process the extracted data as needed
        for (int i = 0; i < dataList.Count; i++)
        {
            jobApp = EntitiesGenerator.GenerateImportedJobApplication(dataList[i], defaultStatus);
        }
    }
}