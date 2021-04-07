using ExcelLibrary.SpreadSheet;

namespace ReportingBot_VJaju
{
    public static class ExcelDBConnect
    {
        private static string filePath = @"C:\Users\vjaju\Desktop\Bot_PowerBI_Connect\CovidSurvey.xls";

        public static void AddNewRow(Covid19ExcelModel covid19ExcelModel)
        {

            //create new xls file
            //Workbook workbook = new Workbook();
            //Worksheet worksheet = new Worksheet("Covid Survey Details");
            //worksheet.Cells[0, 0] = new Cell("Id");
            //worksheet.Cells[0, 1] = new Cell("Age");
            //worksheet.Cells[0, 2] = new Cell("Gender");
            //worksheet.Cells[0, 3] = new Cell("HadPositive");
            //worksheet.Cells[0, 4] = new Cell("HadVaccine");
            //worksheet.Cells[0, 5] = new Cell("OneWordComment");
            ////worksheet.Cells[0, 1] = new Cell((short)1); 
            ////worksheet.Cells[2, 0] = new Cell(9999999);
            ////worksheet.Cells[3, 3] = new Cell((decimal)3.45); 
            ////worksheet.Cells[2, 2] = new Cell("Text string"); 
            ////worksheet.Cells[2, 4] = new Cell("Second string"); 
            ////worksheet.Cells[4, 0] = new Cell(32764.5, "#,##0.00");
            ////worksheet.Cells[5, 1] = new Cell(DateTime.Now, @"YYYY-MM-DD"); 
            ////worksheet.Cells.ColumnWidth[0, 1] = 3000; 
            //workbook.Worksheets.Add(worksheet);
            //workbook.Save(filePath);

            // open xls file 
            Workbook book = Workbook.Load(filePath); 
            Worksheet sheet = book.Worksheets[0];

            sheet.Cells[721, 0] = new Cell((short)721);
            sheet.Cells[721, 1] = new Cell((short)covid19ExcelModel.Age);
            sheet.Cells[721, 2] = new Cell(covid19ExcelModel.Gender);
            sheet.Cells[721, 3] = new Cell(covid19ExcelModel.HadPositive);
            sheet.Cells[721, 4] = new Cell(covid19ExcelModel.HadVaccine);
            sheet.Cells[721, 5] = new Cell(covid19ExcelModel.OneWordComment);
            book.Worksheets.Add(sheet);
            book.Save(filePath);
        }
    }
}