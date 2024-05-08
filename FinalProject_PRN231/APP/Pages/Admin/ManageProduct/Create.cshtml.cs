using APP.IServices;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel.Product;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APP.Pages.Admin.ManageProduct
{
    [Authorize(Roles = "1")]
    public class CreateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IProductServices _productServices;
        public CreateModel(IProductServices productServices, ILogger<IndexModel> logger)
        {
            _productServices = productServices;
            _logger = logger;
        }
        public OperationResult OperationResult { get; set; }
        public List<CategoryDisplayModel> Categories { get; set; } = new List<CategoryDisplayModel>();
        [BindProperty]
        public ProductCreationModel ProductCreationModel { get; set; }
        public async Task OnGet()
        {
            Categories = await _productServices.GetAllCategories();
        }

        public async Task OnPostCreate()
        {
            OperationResult = await _productServices.CreateProduct(ProductCreationModel);
        }
    }
}
