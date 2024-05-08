using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Product;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        [HttpGet("top")]
        public async Task<List<ProductModel>> GetTopProducts()
        {
            var result = new List<ProductModel>();
            result = await productRepository.GetTopProducts();
            return result;
        }
        [HttpPost("products")]
        public async Task<ProductDisplayModel> GetProducts(ProductQueryModel queryModel)
        {
            ProductDisplayModel model = new ProductDisplayModel();
            try
            {
                model = await productRepository.GetAllProducts(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpPost("productdetails")]
        public async Task<ProductDetailDisplayModel> GetProductDetails(ProductQueryModel queryModel)
        {
            ProductDetailDisplayModel model = new ProductDetailDisplayModel();
            try
            {
                model = await productRepository.GetAllDetailProducts(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpGet("categories")]
        public async Task<List<CategoryDisplayModel>> GetAllCategories()
        {
            List<CategoryDisplayModel> model = new List<CategoryDisplayModel>();
            try
            {
                return await productRepository.GetAllCategories();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ProductModel> GetProductById([FromRoute] int id)
        {
            ProductModel model = new ProductModel();
            try
            {
                model = await productRepository.GetProductById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpGet("detail/{id}")]
        public async Task<ProductDetailModel> GetProductDetailById([FromRoute] int id)
        {
            ProductDetailModel model = new ProductDetailModel();
            try
            {
                model = await productRepository.GetProductDetailById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpPost("create")]
        public async Task<OperationResult> CreateProduct(ProductCreationModel model)
        {
            var rs = new OperationResult();
            try
            {
                rs = await productRepository.CreateProduct(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        [HttpPost("update")]
        public async Task<OperationResult> UpdateProduct(ProductCreationModel model)
        {
            var rs = new OperationResult();
            try
            {
                rs = await productRepository.UpdateProduct(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }

        [HttpDelete("{id}")]
        public async Task<OperationResult> DeleteProduct([FromRoute] int id)
        {
            var rs = await productRepository.DeleteProduct(id);
            return rs;
        }
    }
}
