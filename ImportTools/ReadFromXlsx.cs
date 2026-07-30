using ClosedXML.Excel;

namespace JobSearchManagerBackEnd.ImportTools;

/// <summary>
/// Dedicated class to read data from .xlsx files
/// </summary>
internal static class ReadFromXlsx
{
    /// <summary>
    /// Retrieves data from a .xlsx file, formatted as strings
    /// </summary>
    /// <param name="fileName">File name</param>
    /// <param name="columnsQty">The total number of columns that will be read</param>
    /// <param name="firstRowIndex"></param>
    /// <param name="firstColIndex"></param>
    /// <returns>A list of string[]</returns>
    /// <exception cref="Exception">Appears if a row or a cell is not properly detected</exception>
    internal static IEnumerable<string[]> RetrieveData(string fileName, int columnsQty, int firstRowIndex, int firstColIndex)
    {
        List<string[]> dataList = new List<string[]>();

        using var workbook = new XLWorkbook(fileName);        
        var sheetWithJobApplications = workbook.Worksheet(1);
        var lastUsedRow = sheetWithJobApplications.LastRowUsed();

        if (lastUsedRow == null)
        {
            throw new Exception("The last row has not been detected !");
        }

        for (int rowIndex = firstRowIndex; rowIndex <= lastUsedRow.RowNumber(); rowIndex++)
        {
            var row = sheetWithJobApplications.Row(rowIndex);
            dataList.Add(new string[columnsQty]);

            for (int colIndex = firstColIndex; colIndex <= columnsQty; colIndex++)
            {
                var cell = row.Cell(colIndex);

                if (cell == null)
                {
                    throw new Exception($"The cell at row {rowIndex}, column {colIndex} has not been detected !");
                }

                dataList[rowIndex - firstRowIndex][colIndex - firstColIndex] = cell.GetString();

                //Console.WriteLine($"Row {rowIndex}, Column {colIndex}: Cell value = {cellValue}");
            }

            yield return dataList[rowIndex - firstRowIndex];

            Console.WriteLine($"Row {rowIndex}: Source cell value = {dataList[rowIndex - firstRowIndex][0]}");
        }
    }
}