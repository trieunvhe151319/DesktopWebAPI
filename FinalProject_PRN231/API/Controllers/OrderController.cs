using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Order;
using DataAccess.IRepositories;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderRepository orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        [HttpPost("create")]
        public async Task<int> CreateOrder(OrderCreationModel model)
        {
            var rs = -1;
            try
            {
                rs = await orderRepository.CreateOrder(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        [HttpPost("createorderdetails")]
        public async Task<OperationResult> CreateOrderDetails(List<OrderDetailCreationModel> model)
        {
            var rs = new OperationResult();
            try
            {
                rs = await orderRepository.CreateOrderDetails(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        [HttpPost("orders")]
        public async Task<OrderDisplayModel> GetOrders(OrderQueryModel queryModel)
        {
            OrderDisplayModel model = new OrderDisplayModel();
            try
            {
                model = await orderRepository.GetAllOrders(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpPost("orderdetails")]
        public async Task<OrderDetailDisplayModel> GetOrderDetails(OrderDetailQueryModel queryModel)
        {
            OrderDetailDisplayModel model = new OrderDetailDisplayModel();
            try
            {
                model = await orderRepository.GetAllOrderDetails(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
    }
}
