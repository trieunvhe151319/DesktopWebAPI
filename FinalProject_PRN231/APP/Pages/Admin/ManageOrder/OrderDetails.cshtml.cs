using APP.IServices;
using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace APP.Pages.Admin.ManageOrder
{
    [Authorize(Roles = "1")]
    public class OrderDetailsModel : PageModel
    {
        private readonly ILogger<OrderDetailsModel> _logger;
        IOrderServices _orderServices;
        public OrderDetailsModel(ILogger<OrderDetailsModel> logger, IOrderServices orderServices)
        {
            _orderServices = orderServices;
            _logger = logger;
        }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public OperationResult OperationResult { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderDetailQueryModel OrderDetailQuery { get; set; } = new OrderDetailQueryModel();
        public List<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();
        public async Task OnGet(int pageNumber = 1)
        {
            //OperationResult = JsonConvert.DeserializeObject<OperationResult>((string)TempData["OperationResult"] ?? "");
            //TempData["OperationResult"] = "";
            OrderDetailQuery.Pagination.PageNumber = pageNumber;
            var responseModel = await _orderServices.GetAllOrderDetails(OrderDetailQuery);
            OrderDetails = responseModel.OrderDetails;
            TotalPages = responseModel.TotalCount % 5 == 0 ? responseModel.TotalCount / 5 : responseModel.TotalCount / 5 + 1;
            if (TotalPages <= 5)
            {
                StartPage = 1;
                EndPage = TotalPages;
            }
            else
            {
                StartPage = pageNumber - 2;
                EndPage = pageNumber + 2;
                if (pageNumber <= 2)
                {
                    StartPage = 1;
                    EndPage = 5;
                }
                if (TotalPages - pageNumber <= 2)
                {
                    EndPage = TotalPages;
                    StartPage = TotalPages - 4;
                }
            }
        }
    }
}
