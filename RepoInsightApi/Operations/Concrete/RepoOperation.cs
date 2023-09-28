using LibGit2Sharp;
using RepoInsightApi.Contract.Response;
using RepoInsightApi.Operations.Abstract;
using System.Data;

namespace RepoInsightApi.Operations.Concrete
{
    public class RepoOperation : IRepoOperation
    {
        Branch targetBranch;
        public RepoTablesResponse RepoToDataTable(string repoPath, string branchName)
        {


            DataTable commitsTable = new DataTable();
            commitsTable.Columns.Add("Commit Hash", typeof(string));
            commitsTable.Columns.Add("Author", typeof(string));
            commitsTable.Columns.Add("Date", typeof(DateTime));
            commitsTable.Columns.Add("Message", typeof(string));
            DataTable changesTable = new DataTable();
            changesTable.Columns.Add("Commit Hash", typeof(string));
            changesTable.Columns.Add("File Path", typeof(string));
            changesTable.Columns.Add("Change Type", typeof(string));
            int count = 0;
            using (var repo = new Repository(repoPath))
            {

                if (branchName != null)
                {
                    targetBranch = repo.Branches[branchName];
                    Commands.Checkout(repo, targetBranch);

                }
                foreach (Commit commit in targetBranch.Commits)
                {
                    DataRow commitRow = commitsTable.NewRow();
                    commitRow["Commit Hash"] = commit.Id.ToString();
                    commitRow["Author"] = commit.Author.Name;
                    commitRow["Date"] = commit.Author.When.DateTime.ToString("dd:MMMM,yyyyy");
                    commitRow["Message"] = commit.MessageShort;

                    commitsTable.Rows.Add(commitRow);

                    if (commit.Parents.Count() > 0)
                    {
                        var parent = commit.Parents.First();
                        foreach (TreeEntryChanges change in repo.Diff.Compare<TreeChanges>(parent.Tree, commit.Tree))
                        {
                            DataRow changeRow = changesTable.NewRow();
                            changeRow["Commit Hash"] = commit.Id.ToString();
                            changeRow["File Path"] = change.Path;
                            changeRow["Change Type"] = change.Status.ToString();

                            changesTable.Rows.Add(changeRow);
                        }
                        count++;
                    }
                    if (count >= 1)
                    {
                        break;
                    }

                }
            }
            var repoTable = new RepoTablesResponse();
            repoTable.ChangesTable = changesTable;
            repoTable.CommitsTable = commitsTable;
            return repoTable;
        }
    }
}
