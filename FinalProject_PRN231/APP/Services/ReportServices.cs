using APP.Common;
using APP.IServices;
using BusinessObject.RequestModel.Order;
using BusinessObject.ResponseModel.Order;
using BusinessObject.ResponseModel.Report;
using Newtonsoft.Json;

namespace APP.Services
{
    public class ReportServices : IReportServices
    {
        IConfiguration _configuration;
        string baseAddress;
        //HttpClient client;
        HttpClientBase clientBase;
        public ReportServices(IConfiguration configuration)
        {
            clientBase = new HttpClientBase();
            _configuration = configuration;
            baseAddress = _configuration.GetValue<string>("BassAddress");
            //client = new HttpClient();
            //client.BaseAddress = baseAddress;
        }
        public async Task<ReportModel> GetSummaryReport()
        {
            ReportModel rs = new ReportModel();
            try
            {
                //var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Report/summaryreport", HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<ReportModel>(response);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return rs;
        }
    }
}
