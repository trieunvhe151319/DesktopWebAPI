using APP.IServices;
using BusinessObject.ResponseModel.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APP.Pages.Admin.Dashboard
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IReportServices reportServices;
        public IndexModel(ILogger<IndexModel> logger, IReportServices reportServices)
        {
            this.reportServices = reportServices;
            _logger = logger;
        }
        public ReportModel ReportModel { get; set; }

        public async Task OnGet()
        {
            ReportModel = await reportServices.GetSummaryReport();
        }
    }
}
