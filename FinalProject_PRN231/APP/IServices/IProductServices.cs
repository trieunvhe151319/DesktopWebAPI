using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Product;

namespace APP.IServices
{
    public interface IProductServices
    {
        public Task<List<ProductModel>> GetTopProducts();
        public Task<ProductDisplayModel> GetAllProducts(ProductQueryModel queryModel);
        public Task<List<CategoryDisplayModel>> GetAllCategories();
        public Task<ProductModel> GetProductById(int id);
        public Task<OperationResult> CreateProduct(ProductCreationModel creationModel);
        public Task<OperationResult> UpdateProduct(ProductCreationModel updateModel);
        public Task<OperationResult> DeleteProduct(int id);
        public Task<ProductDetailDisplayModel> GetAllDetailProducts(ProductQueryModel queryModel);
        public Task<ProductDetailModel> GetProductDetailById(int id);
        //public Task<OperationResult> CreateOrder(OrderCreationModel creationModel);
    }
}
