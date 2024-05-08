using APP.IServices;
using APP.Pages.TempModels;
using BusinessObject.Common;
using BusinessObject.RequestModel.Order;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace APP.Pages.Payment
{
    public class IndexModel : PageModel
    {
        IProductServices productServices;
        IOrderServices orderServices;
        IAccountServices accountServices;
        public IndexModel(IProductServices productServices, IOrderServices orderServices, IAccountServices accountServices)
        {
            this.productServices = productServices;
            this.orderServices = orderServices;
            this.accountServices = accountServices;
        }
        public Cart Cart { get; set; } = new Cart();
        [BindProperty]
        public OrderCreationModel Order { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type.Split("/").Last().Equals("name")).Value;

            var carts = HttpContext.Session.GetString("Carts");
            if (!string.IsNullOrEmpty(carts))
            {
                var cartObjs = JsonConvert.DeserializeObject<List<Cart>>(carts);
                Cart = cartObjs.FirstOrDefault(x => x.Email == email);
                Cart.TotalAmount = Cart.GetTotalAmount();
            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var email = User.Claims.FirstOrDefault(x => x.Type.Split("/").Last().Equals("name")).Value;
            var accountInfo = await accountServices.GetAccountByEmail(email);
            //var customer = await _productServices.GetCustomerById(accountInfo.CustomerId);
            var currentCart = new Cart();
            var carts = HttpContext.Session.GetString("Carts");
            if (!string.IsNullOrEmpty(carts))
            {
                var cartObjs = JsonConvert.DeserializeObject<List<Cart>>(carts);
                currentCart = cartObjs.FirstOrDefault(x => x.Email == email);
                currentCart.TotalAmount = currentCart.GetTotalAmount();
            }
            Order.CustomerId = accountInfo.CustomerId;
            var orderId = await orderServices.CreateOrder(Order);
            List<OrderDetailCreationModel> orderDetails = new List<OrderDetailCreationModel>();
            foreach (var item in currentCart.Items)
            {
                var orderDetail = new OrderDetailCreationModel()
                {
                    OrderId = orderId,
                    ProductId = item.Product.ProductId,
                    Quantity = (short)item.Quantity,
                    UnitPrice = (decimal)item.Product.UnitPrice,
                    Discount = 0,
                };
                orderDetails.Add(orderDetail);
            }
            
            var rs = await orderServices.CreateOrderDetails(orderDetails);
            if (rs.Status == CommonStatus.Success)
            {
                TempData["msg"] = "Successfull";
                HttpContext.Session.SetString("Carts", "");
            }
            else
            {
                TempData["msg"] = "Add Failed";
            }
            return Page();
        }

    }
}
