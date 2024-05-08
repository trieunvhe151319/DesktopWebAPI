using APP.IServices;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel.Product;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace APP.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<Pages.IndexModel> _logger;
        IProductServices _productServices;
        public IndexModel(ILogger<Pages.IndexModel> logger, IProductServices productServices)
        {
            _productServices = productServices;
            _logger = logger;
        }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public OperationResult OperationResult { get; set; }
        [BindProperty(SupportsGet = true)]
        public ProductQueryModel ProductQuery { get; set; } = new ProductQueryModel();
        public List<ProductModel> Products { get; set; } = new List<ProductModel>();
        public List<CategoryDisplayModel> Categories = new List<CategoryDisplayModel>();
        public async Task OnGet(int pageNumber = 1)
        {
            OperationResult = JsonConvert.DeserializeObject<OperationResult>((string)TempData["OperationResult"] ?? "");
            TempData["OperationResult"] = "";
            ProductQuery.Pagination.PageNumber = pageNumber;
            Categories = await _productServices.GetAllCategories();
            var responseModel = await _productServices.GetAllProducts(ProductQuery);
            Products = responseModel.Products;
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

        public async Task<IActionResult> OnGetDelete(int id)
        {
            OperationResult = await _productServices.DeleteProduct(id);
            TempData["OperationResult"] = JsonConvert.SerializeObject(OperationResult);
            return RedirectToPage("Index");
        }
    }
}
