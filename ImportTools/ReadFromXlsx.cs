using ClosedXML.Excel;
using JobSearchManagerBackEnd.Texts;

namespace JobSearchManagerBackEnd.ImportTools;

/// <summary>
/// Dedicated class to read data from .xlsx files
/// </summary>
internal static class ReadFromXlsx
{
    /// <summary>
    /// Retrieves data from a .xlsx file, formatted as strings
    /// </summary>
    /// <param name="fileStream">File stream</param>
    /// <param name="columnsQty">The total number of columns that will be read</param>
    /// <param name="firstRowIndex"></param>
    /// <param name="firstColIndex"></param>
    /// <returns>A list of string[]</returns>
    /// <exception cref="Exception">Appears if a row or a cell is not properly detected</exception>
    internal static IEnumerable<string[]> RetrieveData(Stream fileStream, int firstRowIndex, int firstColIndex, int lastColIndex)
    {
        List<string[]> dataList = [];

        using var workbook = new XLWorkbook(fileStream);
        var sheetWithJobApplications = workbook.Worksheet(1);
        var lastUsedRow = sheetWithJobApplications.LastRowUsed();

        if (lastUsedRow == null)
        {
            throw new Exception(InternalErrorTexts.ERROR_READ_XLSX_LAST_ROW);
        }

        for (int rowIndex = firstRowIndex; rowIndex <= lastUsedRow.RowNumber(); rowIndex++)
        {
            var row = sheetWithJobApplications.Row(rowIndex);
            dataList.Add(new string[lastColIndex - firstColIndex + 1]);

            // The Excel file columns are 1-based, but the returned array is 0-based and only contains
            // the columns requested by firstColIndex/lastColIndex.
            for (int colIndex = firstColIndex; colIndex <= lastColIndex; colIndex++)
            {
                var cell = row.Cell(colIndex);

                if (cell == null)
                {
                    throw new Exception(string.Format(InternalErrorTexts.ERROR_READ_XLSX_CELL, rowIndex, colIndex));
                }

                dataList[rowIndex - firstRowIndex][colIndex - firstColIndex] = cell.GetString();
            }

            yield return dataList[rowIndex - firstRowIndex];

            Console.WriteLine(string.Format(InternalLogTexts.LOG_IMPORT_JOB_APPLICATIONS, rowIndex, dataList[rowIndex - firstRowIndex][0]));
        }
    }
}