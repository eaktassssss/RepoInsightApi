using RepoInsightApi.Contract.Response;
using System.Data;

namespace RepoInsightApi.Operations.Abstract
{
    public interface IRepoOperation
    {
        RepoTablesResponse RepoToDataTable(string repoPath,string branchName);
    }
}
