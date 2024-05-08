using BusinessObject.Common;
using BusinessObject.RequestModel.Order;
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
    public class OrderDAO
    {
        private static OrderDAO _instance = null;
        private static readonly object instanceLock = new object();
        PRN231DBContext db = new PRN231DBContext();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new OrderDAO();
                    }
                    return _instance;
                }
            }
        }
        public async Task<int> CreateOrder(Order model)
        {
            //OperationResult result = new OperationResult()
            //{
            //    Status = CommonStatus.Success
            //};
            try
            {
                model.CustomerId = model.CustomerId.Trim();
                await db.Orders.AddAsync(model);
                await db.SaveChangesAsync();
                db.Entry(model).State = EntityState.Detached;
                return db.Orders.OrderBy(x => x.OrderId).ToList().Last().OrderId;
            }
            catch (Exception ex)
            {
                //result.Status = CommonStatus.Failed;
                //result.ResultMessage = ex.Message;
            }
            return -1;
        }
        public async Task<OperationResult> CreateOrderDetails(List<OrderDetail>? model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                await db.OrderDetails.AddRangeAsync(model);
                await db.SaveChangesAsync();
                foreach (var item in model)
                {
                    db.Entry(item).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
        public async Task<Tuple<int, List<Order>>> GetOrdersWithFilter(OrderQueryModel queryModel)
        {
            List<Order> orders = new List<Order>();
            try
            {
                int pageNumber = queryModel.Pagination.PageNumber;
                int pageSize = queryModel.Pagination.PageSize;

                IQueryable<Order> ordersQuery = db.Orders;
                if (queryModel.StartDate.HasValue && queryModel.EndDate.HasValue)
                {
                    ordersQuery = ordersQuery.AsNoTracking().Where(x => x.OrderDate <= queryModel.EndDate && x.OrderDate > queryModel.StartDate);
                }

                //if (!string.IsNullOrEmpty(queryModel.MemberId))
                //{
                //    ordersQuery = ordersQuery.AsNoTracking().Where(x => x.MemberId == queryModel.MemberId);
                //}
                var totalCount = ordersQuery.AsNoTracking().Count();
                ordersQuery = ordersQuery.AsNoTracking().OrderByDescending(x => x.OrderDate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                orders = await ordersQuery.AsNoTracking().ToListAsync();
                return new Tuple<int, List<Order>>(totalCount, orders);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<Tuple<int, List<OrderDetail>>> GetOrderDetailsWithFilter(OrderDetailQueryModel queryModel)
        {
            List<OrderDetail> orderDetails = new List<OrderDetail>();
            try
            {
                int pageNumber = queryModel.Pagination.PageNumber;
                int pageSize = queryModel.Pagination.PageSize;

                IQueryable<OrderDetail> orderDetailsQuery = db.OrderDetails.Include(x=>x.Product).Where(x=>x.OrderId == queryModel.OrderId);
                if (!string.IsNullOrEmpty(queryModel.SearchText))
                {
                    orderDetailsQuery = orderDetailsQuery.AsNoTracking().Where(x => x.Product.ProductName.Contains(queryModel.SearchText));
                    var test = orderDetailsQuery.ToList();
                }
                if (!string.IsNullOrEmpty(queryModel.PriceRange))
                {
                    if (queryModel.PriceRange.IndexOf('+') >= 0)
                    {
                        var maxPrice = queryModel.PriceRange.Split("+")[0];
                        orderDetailsQuery = orderDetailsQuery.AsNoTracking().Where(x => x.UnitPrice > decimal.Parse(maxPrice));
                    }
                    else
                    {
                        var minPrice = queryModel.PriceRange.Split("-")[0];
                        var maxPrice = queryModel.PriceRange.Split("-")[1];
                        orderDetailsQuery = orderDetailsQuery.AsNoTracking().Where(x => x.UnitPrice > decimal.Parse(minPrice));
                        orderDetailsQuery = orderDetailsQuery.AsNoTracking().Where(x => x.UnitPrice < decimal.Parse(maxPrice));
                    }
                }
                var totalCount = orderDetailsQuery.AsNoTracking().Count();
                orderDetailsQuery = orderDetailsQuery.AsNoTracking().OrderByDescending(x => x.OrderId).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                orderDetails = await orderDetailsQuery.AsNoTracking().ToListAsync();
                return new Tuple<int, List<OrderDetail>>(totalCount, orderDetails);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
