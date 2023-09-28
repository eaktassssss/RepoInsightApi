using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using RepoInsightApi.Contract.Request;
using RepoInsightApi.Contract.Response;
using RepoInsightApi.Operations.Abstract;

namespace RepoInsightApi.Controllers
{
    [Route("api/analytics")]
    [ApiController]
    public class RepoAnalyticsController : ControllerBase
    {
        private readonly IExportOperation _exportOperation;
        private readonly IRepoOperation _repoOperation;
        public RepoAnalyticsController(IExportOperation exportOperation, IRepoOperation repoOperation)
        {
            _exportOperation = exportOperation;
            _repoOperation = repoOperation;
        }
        [HttpPost]
        [Route("export-excel")]
        public async Task<RepoAnalyticsResponse> RepoToExcel([FromBody]RepoAnalyticsRequest request)
        {
            try
            {
                var repoTable = _repoOperation.RepoToDataTable(request.Input,request.BranchName);
                _exportOperation.ExcelExport(request.Output, repoTable.CommitsTable,repoTable.ChangesTable);
                return new RepoAnalyticsResponse { IsExport=true,Message= "Export successful" };
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
