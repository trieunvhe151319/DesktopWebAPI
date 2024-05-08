using APP.IServices;
using AutoMapper;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel.Product;
using BusinessObject.ResponseModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace APP.Pages.Admin.ManageProduct
{
    [Authorize(Roles = "1")]
    public class UpdateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        IProductServices _productServices;
        IMapper _mapper;
        public UpdateModel(IProductServices productServices, ILogger<IndexModel> logger, IMapper mapper)
        {
            _productServices = productServices;
            _logger = logger;
            _mapper = mapper;
        }
        public ProductDetailModel Product { get; set; }
        public OperationResult OperationResult { get; set; }
        public List<CategoryDisplayModel> Categories { get; set; } = new List<CategoryDisplayModel>();
        [BindProperty]
        public ProductCreationModel ProductUpdateModel { get; set; }
        public async Task OnGet(int id)
        {
            Categories = await _productServices.GetAllCategories();
            Product = await _productServices.GetProductDetailById(id);
        }
        public async Task OnPostUpdate()
        {
            OperationResult = await _productServices.UpdateProduct(ProductUpdateModel);
            Categories = await _productServices.GetAllCategories();
            Product = _mapper.Map<ProductDetailModel>(ProductUpdateModel);
        }
    }
}
