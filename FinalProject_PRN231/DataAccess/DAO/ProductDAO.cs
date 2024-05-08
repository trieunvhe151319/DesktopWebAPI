using BusinessObject.Common;
using BusinessObject.RequestModel.Product;
using BusinessObject.ResponseModel;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        private static ProductDAO _instance = null;
        private static readonly object instanceLock = new object();
        PRN231DBContext db = new PRN231DBContext();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ProductDAO();
                    }
                    return _instance;
                }
            }
        }

        public async Task<List<Product>> GetTopProducts()
        {
            try
            {
                var listBestSaleProducts = (from product in db.Products
                                            from orderDetail in db.OrderDetails
                                            where orderDetail.ProductId == product.ProductId
                                            group orderDetail by product.ProductId into productGroups
                                            select new
                                            {
                                                productId = productGroups.Key,
                                                numberOfOrders = productGroups.Count()
                                            }
                           ).OrderByDescending(x => x.numberOfOrders).Distinct().Take(4);
                var listBestSaleProductsId = listBestSaleProducts.Select(x => x.productId).ToHashSet();

                var rs = db.Products.AsNoTracking().Where(x => listBestSaleProductsId.Contains(x.ProductId)).ToList();
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Tuple<List<Product>, int>> GetProductsWithFilter(ProductQueryModel queryModel)
        {
            int pageNumber = queryModel.Pagination.PageNumber;
            int pageSize = queryModel.Pagination.PageSize;

            IQueryable<Product> productsQuery = db.Products;
            if (!string.IsNullOrEmpty(queryModel.SearchText))
            {
                productsQuery = productsQuery.AsNoTracking().Where(x => x.ProductName.Contains(queryModel.SearchText));
                var test = productsQuery.ToList();
            }
            if (!string.IsNullOrEmpty(queryModel.PriceRange))
            {
                if (queryModel.PriceRange.IndexOf('+') >= 0)
                {
                    var maxPrice = queryModel.PriceRange.Split("+")[0];
                    productsQuery = productsQuery.AsNoTracking().Where(x => x.UnitPrice > decimal.Parse(maxPrice));
                }
                else
                {
                    var minPrice = queryModel.PriceRange.Split("-")[0];
                    var maxPrice = queryModel.PriceRange.Split("-")[1];
                    productsQuery = productsQuery.AsNoTracking().Where(x => x.UnitPrice > decimal.Parse(minPrice));
                    productsQuery = productsQuery.AsNoTracking().Where(x => x.UnitPrice < decimal.Parse(maxPrice));
                }
            }
            if (!string.IsNullOrEmpty(queryModel.CategoryId))
            {
                productsQuery = productsQuery.Where(x => x.CategoryId == int.Parse(queryModel.CategoryId));
                var test = productsQuery.ToList();
            }
            var totalCount = productsQuery.Count();
            productsQuery = productsQuery.AsNoTracking().OrderBy(x => x.UnitPrice).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var products = await productsQuery.AsNoTracking().Include(x => x.Category).ToListAsync();
            return new Tuple<List<Product>, int>(products, totalCount);
        }
        public async Task<OperationResult> DeleteProduct(int id)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var existProduct = await GetProdcutById(id);
                db.Products.Remove(existProduct);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
        public async Task<OperationResult> CreateProduct(Product model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                await db.Products.AddAsync(model);
                await db.SaveChangesAsync();
                db.Entry(model).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
        public async Task<OperationResult> UpdateProduct(Product model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var existProduct = db.Products.AsNoTracking().FirstOrDefault(x => x.ProductId == model.ProductId);
                if (existProduct == null)
                {
                    result.Status = CommonStatus.Failed;
                    result.ResultMessage = "Does not exist product";
                }
                else
                {
                    db.Products.Update(model);
                    await db.SaveChangesAsync();
                    db.Entry(model).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
        public async Task<Product> GetProdcutById(int id)
        {
            try
            {
                return await db.Products.AsNoTracking().Where(x => x.ProductId == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                return await db.Categories.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
