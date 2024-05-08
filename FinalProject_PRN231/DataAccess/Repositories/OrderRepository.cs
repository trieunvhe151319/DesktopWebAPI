using AutoMapper;
using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel.Report;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly IMapper _mapper;
        public OrderRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<int> CreateOrder(OrderCreationModel model)
        {
            try
            {
                return await OrderDAO.Instance.CreateOrder(_mapper.Map<Order>(model));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> CreateOrderDetails(List<OrderDetailCreationModel> model)
        {
            try
            {
                return await OrderDAO.Instance.CreateOrderDetails(_mapper.Map<List<OrderDetail>>(model));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OrderDisplayModel> GetAllOrders(OrderQueryModel queryModel)
        {
            try
            {
                OrderDisplayModel model = new OrderDisplayModel();
                var data = await OrderDAO.Instance.GetOrdersWithFilter(queryModel);
                var orders = data.Item2;
                model.Orders = _mapper.Map<List<OrderModel>>(orders);
                model.TotalCount = data.Item1;
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OrderDetailDisplayModel> GetAllOrderDetails(OrderDetailQueryModel queryModel)
        {
            try
            {
                OrderDetailDisplayModel model = new OrderDetailDisplayModel();
                var data = await OrderDAO.Instance.GetOrderDetailsWithFilter(queryModel);
                var orders = data.Item2;
                model.OrderDetails = _mapper.Map<List<OrderDetailModel>>(orders);
                model.TotalCount = data.Item1;
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ReportModel> GetSummaryReport()
        {
            try
            {
                ReportModel model = new ReportModel();
                model = await ReportDAO.Instance.GetSummaryReport();
                return model;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
