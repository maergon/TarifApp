using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace TarifApp
{
    /**
     * public partial class App : Application
     * Служебные методы
     */

    public static class Utils
    {
        public static DataTable GetDataFromCSVFile(string csvFilePath)
        {
            DataTable csvData = new DataTable();

            try
            {
                using TextFieldParser csvReader = new TextFieldParser(csvFilePath);

                csvReader.SetDelimiters(new string[] { ";" });

                string[] columns = csvReader.ReadFields();

                foreach (string column in columns)
                {
                    DataColumn dataColumn = new DataColumn(column);

                    csvData.Columns.Add(dataColumn);
                }

                while (!csvReader.EndOfData)
                {
                    string[] fieldData = csvReader.ReadFields();

                    for (int i = 0; i < fieldData.Length; i++)
                    {
                        if (fieldData[i] == "")
                        {
                            fieldData[i] = null;
                        }
                    }

                    csvData.Rows.Add(fieldData);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return csvData;
        }
    }
}
