using AutoMapper;
using BusinessObject.RequestModel.Order;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Product;
using DataAccess.DAO;
using DataAccess.IRepositories;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ProductModel>> GetTopProducts()
        {
            List<ProductModel> result = new List<ProductModel>();
            try
            {
                result = _mapper.Map<List<ProductModel>>(await ProductDAO.Instance.GetTopProducts());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductDisplayModel> GetAllProducts(ProductQueryModel queryModel)
        {
            try
            {
                ProductDisplayModel model = new ProductDisplayModel();
                var data = await ProductDAO.Instance.GetProductsWithFilter(queryModel);
                var products = data.Item1;
                model.Products = _mapper.Map<List<ProductModel>>(products);
                model.TotalCount = data.Item2;
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductDetailDisplayModel> GetAllDetailProducts(ProductQueryModel queryModel)
        {
            try
            {
                ProductDetailDisplayModel model = new ProductDetailDisplayModel();
                var data = await ProductDAO.Instance.GetProductsWithFilter(queryModel);
                var products = data.Item1;
                model.Products = _mapper.Map<List<ProductDetailModel>>(products);
                model.TotalCount = data.Item2;
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> DeleteProduct(int id)
        {
            try
            {
                return await ProductDAO.Instance.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> CreateProduct(ProductCreationModel model)
        {
            try
            {
                return await ProductDAO.Instance.CreateProduct(_mapper.Map<Product>(model));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> UpdateProduct(ProductCreationModel model)
        {
            try
            {
                return await ProductDAO.Instance.UpdateProduct(_mapper.Map<Product>(model));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductModel> GetProductById(int id)
        {
            try
            {
                var data = await ProductDAO.Instance.GetProdcutById(id);
                return _mapper.Map<ProductModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ProductDetailModel> GetProductDetailById(int id)
        {
            try
            {
                var data = await ProductDAO.Instance.GetProdcutById(id);
                return _mapper.Map<ProductDetailModel>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<CategoryDisplayModel>> GetAllCategories()
        {
            try
            {
                var data = await ProductDAO.Instance.GetAllCategories();
                return _mapper.Map<List<CategoryDisplayModel>>(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
