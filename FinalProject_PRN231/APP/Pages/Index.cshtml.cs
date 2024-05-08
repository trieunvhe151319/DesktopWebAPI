using APP.IServices;
using BusinessObject.Common;
using BusinessObject.ResponseModel.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IProductServices productServices;
        public IndexModel(IProductServices productServices, ILogger<IndexModel> logger)
        {
            this.productServices = productServices;
            _logger = logger;
        }
        public List<ProductModel> HottestProducts { get; set; } = new List<ProductModel>();
        public async Task<IActionResult> OnGet()
        {
            var role = User.Claims?.FirstOrDefault(x => x.Type.Split("/").Last().Equals("role"))?.Value;
            if(role != null && int.Parse(role) ==(int)Role.Admin)
            {
                return RedirectToPage("/Admin/Dashboard/Index");
            }
            HottestProducts = await productServices.GetTopProducts();
            return Page();
        }
    }
}