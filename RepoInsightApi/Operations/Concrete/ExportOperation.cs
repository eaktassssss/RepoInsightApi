using OfficeOpenXml;
using RepoInsightApi.Operations.Abstract;
using System.Data;

namespace RepoInsightApi.Operations.Concrete
{
    public class ExportOperation : IExportOperation
    {
    

        public void ExcelExport(string outputPath, DataTable commitsTable, DataTable changesTable)
        {
           
            using (ExcelPackage excel = new ExcelPackage())
            {
                var wsCommits = excel.Workbook.Worksheets.Add("Commits");
                wsCommits.Cells["A1"].LoadFromDataTable(commitsTable, true);

                var wsChanges = excel.Workbook.Worksheets.Add("Changes");
                wsChanges.Cells["A1"].LoadFromDataTable(changesTable, true);

                excel.SaveAs(new System.IO.FileInfo(outputPath));
            }

        }
    }
}
