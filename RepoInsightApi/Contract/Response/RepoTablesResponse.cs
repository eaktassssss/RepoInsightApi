using System.Data;

namespace RepoInsightApi.Contract.Response
{
    public class RepoTablesResponse
    {
        public DataTable CommitsTable { get; set; }
        public DataTable ChangesTable { get; set; }
    }
}
