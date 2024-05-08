using APP.Common;
using APP.IServices;
using BusinessObject.Common;
using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Order;
using Newtonsoft.Json;

namespace APP.Services
{
    public class OrderServices : IOrderServices
    {
        IConfiguration _configuration;
        string baseAddress;
        //HttpClient client;
        HttpClientBase clientBase;
        public OrderServices(IConfiguration configuration)
        {
            clientBase = new HttpClientBase();
            _configuration = configuration;
            baseAddress = _configuration.GetValue<string>("BassAddress");
            //client = new HttpClient();
            //client.BaseAddress = baseAddress;
        }
        public async Task<int> CreateOrder(OrderCreationModel creationModel)
        {
            //OperationResult rs = new OperationResult()
            //{
            //    Status = CommonStatus.Success
            //};
            int rs = -1;
            try
            {
                var jsonContent = JsonConvert.SerializeObject(creationModel);
                var response = await clientBase.SendRequest(baseAddress + "/Order/create", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<int>(response);
            }
            catch (Exception ex)
            {
                //rs.Status = CommonStatus.Failed;
                //rs.ResultMessage = ex.Message;
            }
            return rs;
        }
        public async Task<OrderDisplayModel> GetAllOrders(OrderQueryModel queryModel)
        {
            OrderDisplayModel rs = new OrderDisplayModel();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Order/orders", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OrderDisplayModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<OrderDetailDisplayModel> GetAllOrderDetails(OrderDetailQueryModel queryModel)
        {
            OrderDetailDisplayModel rs = new OrderDetailDisplayModel();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Order/orderdetails", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OrderDetailDisplayModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
        public async Task<OperationResult> CreateOrderDetails(List<OrderDetailCreationModel> creationModel)
        {
            OperationResult rs = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var jsonContent = JsonConvert.SerializeObject(creationModel);
                var response = await clientBase.SendRequest(baseAddress + "/Order/createorderdetails", HttpMethod.Post, jsonContent);
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
