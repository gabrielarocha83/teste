using OfficeOpenXml;

namespace Yara.UnitTests
{
    public class ExportExcelTest
    {
        public void Excel()
        {
            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Accounts");
                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                pck.Save();
            }
        }


    }
}