using System.Data;
using System.Xml.Serialization;

namespace RepoInsightApi.Operations.Abstract
{
    public interface IExportOperation
    {
        void ExcelExport(string outputPath, DataTable commitsTable, DataTable changesTable);
    }
}
