using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IOrderRepository
    {
        public Task<int> CreateOrder(OrderCreationModel model);
        public Task<OperationResult> CreateOrderDetails(List<OrderDetailCreationModel> model);
        public Task<OrderDisplayModel> GetAllOrders(OrderQueryModel queryModel);
        public Task<ReportModel> GetSummaryReport();
        public Task<OrderDetailDisplayModel> GetAllOrderDetails(OrderDetailQueryModel queryModel);
    }
}
