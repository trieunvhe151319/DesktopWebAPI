using APP.Common;
using APP.IServices;
using BusinessObject.Common;
using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Product;
using Newtonsoft.Json;

namespace APP.Services
{
    public class ProductServices : IProductServices
    {
        IConfiguration _configuration;
        string baseAddress;
        //HttpClient client;
        HttpClientBase clientBase;
        public ProductServices(IConfiguration configuration)
        {
            clientBase = new HttpClientBase();
            _configuration = configuration;
            baseAddress = _configuration.GetValue<string>("BassAddress");
            //client = new HttpClient();
            //client.BaseAddress = baseAddress;
        }
        public async Task<List<ProductModel>> GetTopProducts()
        {
            List<ProductModel> rs = new List<ProductModel>();
            try
            {
                var response = await clientBase.SendRequest(baseAddress + $"/Product/top", HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<List<ProductModel>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<ProductDisplayModel> GetAllProducts(ProductQueryModel queryModel)
        {
            ProductDisplayModel rs = new ProductDisplayModel();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Product/products", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<ProductDisplayModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<ProductDetailDisplayModel> GetAllDetailProducts(ProductQueryModel queryModel)
        {
            ProductDetailDisplayModel rs = new ProductDetailDisplayModel();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Product/productdetails", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<ProductDetailDisplayModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<List<CategoryDisplayModel>> GetAllCategories()
        {
            List<CategoryDisplayModel> rs = new List<CategoryDisplayModel>();
            try
            {
                var response = await clientBase.SendRequest(baseAddress + "/Product/categories", HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<List<CategoryDisplayModel>>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<ProductModel> GetProductById(int id)
        {
            ProductModel rs = new ProductModel();
            try
            {
                var response = await clientBase.SendRequest(baseAddress + $"/Product/{id}", HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<ProductModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<ProductDetailModel> GetProductDetailById(int id)
        {
            ProductDetailModel rs = new ProductDetailModel();
            try
            {
                var response = await clientBase.SendRequest(baseAddress + $"/Product/detail/{id}", HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<ProductDetailModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<OperationResult> CreateProduct(ProductCreationModel creationModel)
        {
            OperationResult rs = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var jsonContent = JsonConvert.SerializeObject(creationModel);
                var response = await clientBase.SendRequest(baseAddress + "/Product/create", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OperationResult>(response);
            }
            catch (Exception ex)
            {
                rs.Status = CommonStatus.Failed;
                rs.ResultMessage = ex.Message;
            }
            return rs;
        }       
        public async Task<OperationResult> UpdateProduct(ProductCreationModel updateModel)
        {
            OperationResult rs = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var jsonContent = JsonConvert.SerializeObject(updateModel);
                var response = await clientBase.SendRequest(baseAddress + "/Product/update", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OperationResult>(response);
            }
            catch (Exception ex)
            {
                rs.Status = CommonStatus.Failed;
                rs.ResultMessage = ex.Message;
            }
            return rs;
        }
        public async Task<OperationResult> DeleteProduct(int id)
        {
            OperationResult rs = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var response = await clientBase.SendRequest(baseAddress + $"/Product/{id}", HttpMethod.Delete);
                rs = JsonConvert.DeserializeObject<OperationResult>(response);
            }
            catch (Exception ex)
            {
                rs.Status = CommonStatus.Failed;
                rs.ResultMessage = ex.Message;
            }
            return rs;
        }
    }
}
