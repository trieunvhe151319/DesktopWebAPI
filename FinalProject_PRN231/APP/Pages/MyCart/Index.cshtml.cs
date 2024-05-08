using APP.IServices;
using APP.Pages.TempModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace APP.Pages.MyCart
{
    [Authorize(Roles = "2")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IProductServices _productServices;
        public IndexModel(ILogger<IndexModel> logger, IProductServices productServices)
        {
            _productServices = productServices;
            _logger = logger;
        }
        public Cart Cart { get; set; } = new Cart();
        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

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
        public async Task OnPostAddToCart(int productId, int quantity = 1)
        {
            try
            {
                Cart cartUpdated = new Cart();
                var email = User.Claims.FirstOrDefault(x => x.Type.Split("/").Last().Equals("name")).Value;
                var currentCarts = HttpContext.Session.GetString("Carts");
                var isHasItems = false;
                if (!string.IsNullOrEmpty(currentCarts))
                {
                    var cartObjs = JsonConvert.DeserializeObject<List<Cart>>(currentCarts);
                    var currentCart = cartObjs.FirstOrDefault(x => x.Email == email);
                    if (currentCart != null)
                    {
                        foreach (var item in currentCart.Items)
                        {
                            if (item.Product.ProductId == productId)
                            {
                                isHasItems = true;
                                item.Quantity = quantity == 1 ? item.Quantity + 1 : quantity;
                                break;
                            }
                        }
                        if (!isHasItems)
                        {
                            var product = await _productServices.GetProductById(productId);
                            Cart newCart = new Cart();
                            newCart.Email = email;
                            newCart.Items.Add(new Item() { Product = product, Quantity = quantity });
                            //newCart.TotalAmount = newCart.GetTotalAmount();
                            foreach (var c in cartObjs)
                            {
                                if (c.Email == email)
                                {
                                    c.Items.AddRange(newCart.Items);
                                    c.TotalAmount = newCart.TotalAmount;
                                    break;
                                }
                            }
                        }
                        cartUpdated = currentCart;
                    }
                    else
                    {
                        var product = await _productServices.GetProductById(productId);
                        Cart newCart = new Cart();
                        newCart.Email = email;
                        newCart.Items.Add(new Item() { Product = product, Quantity = quantity });
                        //newCart.TotalAmount = newCart.GetTotalAmount();
                        cartUpdated = newCart;
                        cartObjs.Add(newCart);
                    }
                    var newCarts = JsonConvert.SerializeObject(cartObjs);
                    HttpContext.Session.SetString("Carts", newCarts);
                    //Cart.TotalAmount = Cart.GetTotalAmount();
                }
                else
                {
                    List<Cart> carts = new List<Cart>();
                    var product = await _productServices.GetProductById(productId);
                    Cart newCart = new Cart();
                    newCart.Email = email;
                    newCart.Items.Add(new Item() { Product = product, Quantity = quantity });
                    //newCart.TotalAmount = newCart.GetTotalAmount();
                    cartUpdated = newCart;
                    carts.Add(newCart);
                    var newCarts = JsonConvert.SerializeObject(carts);
                    HttpContext.Session.SetString("Carts", newCarts);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
