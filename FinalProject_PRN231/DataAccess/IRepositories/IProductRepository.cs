using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Product;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IProductRepository
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
    }
}
