using System.Data;
using IronXL;
using static TarifApp.Utils;

namespace TarifApp
{
    /**
     * Main class
     * Формирование Excel отчета с тарифами 
     */
    static class TarifApp
    {
        static void Main()
        {
            string tarifGroupPath = @"C:\GIT\TarifApp\TarifApp\TarifApp\tarif_group.csv";
            string tarifValuePath = @"C:\GIT\TarifApp\TarifApp\TarifApp\tarif_value.csv";

            DataTable tarifGroupData = GetDataFromCSVFile(tarifGroupPath);
            DataTable tarifValueData = GetDataFromCSVFile(tarifValuePath);

            List<TarifGroup> tarifGroups = new List<TarifGroup>();
            List<TarifValue> tarifValues = new List<TarifValue>();

            //настройка Excel файла
            var newXLFile = WorkBook.Create(ExcelFileFormat.XLSX);
            newXLFile.Metadata.Title = "Тарифы";
            var newWorkSheet = newXLFile.CreateWorkSheet("First page");

            //обработка данных из csv файлов
            for (int i = 0; i < tarifGroupData.Rows.Count; i++)
            {
                tarifGroups.Add(new TarifGroup
                {
                    ID = tarifGroupData.Rows[i][0].ToString(),
                    PNumber = tarifGroupData.Rows[i][1].ToString(),
                    TarifName = tarifGroupData.Rows[i][2].ToString()
                });
            }

            for (int i = 0; i < tarifValueData.Rows.Count; i++)
            {                
                tarifValues.Add(new TarifValue
                {
                    ID = tarifValueData.Rows[i][0].ToString(),
                    TarifID = tarifValueData.Rows[i][1].ToString(),
                    DateStart = tarifValueData.Rows[i][2].ToString(),
                    DateEnd = tarifValueData.Rows[i][3].ToString(),
                    Value = tarifValueData.Rows[i][4].ToString()
                });   
            }

            // наполнение excel файла
            int row = 1;
            tarifValues.Sort((x, y) => y.DateStart.CompareTo(x.DateStart)); //сортировка значений по дате

            //заполнение Excel отчета
            foreach (TarifGroup tarifGroup in tarifGroups)
            {
                newWorkSheet["A" + row].Value = "Группа " + tarifGroup.ID;

                row += 1; //вывод значений со 2 строки                
                foreach (TarifValue tarifValue in tarifValues)
                {
                    if (tarifValue.TarifID == tarifGroup.ID)
                    {
                        string _value = tarifValue.Value.ToString().Replace('.', ',');

                        newWorkSheet["B" + row].Value = _value;
                        newWorkSheet["C" + row].Value = tarifValue.DateStart;
                        newWorkSheet["D" + row].Value = tarifValue.DateEnd;
                        row++;
                    }
                }
            }

            newXLFile.SaveAs(@"C:\GIT\gitProjects\TarifApp\TarifApp\TarifApp\Report.xlsx");

            Console.ReadLine();
        }        
    }
}