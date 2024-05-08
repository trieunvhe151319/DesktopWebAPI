using BusinessObject.ResponseModel.Report;

namespace APP.IServices
{
    public interface IReportServices
    {
        public Task<ReportModel> GetSummaryReport();
    }
}
