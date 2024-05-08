using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel.Report;
using DataAccess.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        IOrderRepository orderRepository;
        public ReportController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpGet("summaryreport")]
        public async Task<ReportModel> GetSummaryReport()
        {
            ReportModel model = new ReportModel();
            try
            {
                model = await orderRepository.GetSummaryReport();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
    }
}
