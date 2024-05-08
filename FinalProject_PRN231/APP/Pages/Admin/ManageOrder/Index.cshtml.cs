using APP.IServices;
using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace APP.Pages.Admin.ManageOrder
{
    [Authorize(Roles = "1")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IOrderServices _orderServices;
        public IndexModel(ILogger<IndexModel> logger, IOrderServices orderServices)
        {
            _orderServices = orderServices;
            _logger = logger;
        }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public OperationResult OperationResult { get; set; }
        [BindProperty(SupportsGet = true)]
        public OrderQueryModel OrderQuery { get; set; } = new OrderQueryModel();
        public List<OrderModel> Orders { get; set; } = new List<OrderModel>();
        public async Task OnGet(int pageNumber = 1)
        {
            //OperationResult = JsonConvert.DeserializeObject<OperationResult>((string)TempData["OperationResult"] ?? "");
            //TempData["OperationResult"] = "";
            OrderQuery.Pagination.PageNumber = pageNumber;
            var responseModel = await _orderServices.GetAllOrders(OrderQuery);
            Orders = responseModel.Orders;
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

        //public async Task<IActionResult> OnGetDelete(string id)
        //{
        //    OperationResult = await _orderServices.DeleteOrder(id);
        //    TempData["OperationResult"] = JsonConvert.SerializeObject(OperationResult);
        //    return RedirectToPage("Index");
        //}
    }
}
