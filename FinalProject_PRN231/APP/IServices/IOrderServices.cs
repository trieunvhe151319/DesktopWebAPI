using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Order;

namespace APP.IServices
{
    public interface IOrderServices
    {
        public Task<int> CreateOrder(OrderCreationModel creationModel);
        public Task<OperationResult> CreateOrderDetails(List<OrderDetailCreationModel> creationModel);
        public Task<OrderDisplayModel> GetAllOrders(OrderQueryModel queryModel);
        public Task<OrderDetailDisplayModel> GetAllOrderDetails(OrderDetailQueryModel queryModel);
    }
}
