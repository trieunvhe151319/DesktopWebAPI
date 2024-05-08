using APP.Common;
using APP.IServices;
using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Account;
using Newtonsoft.Json;
using System;

namespace APP.Services
{
    public class AccountServices : IAccountServices
    {
        IConfiguration _configuration;
        string baseAddress;
        //HttpClient client;
        HttpClientBase clientBase;
        public AccountServices(IConfiguration configuration)
        {
            clientBase = new HttpClientBase();
            _configuration = configuration;
            baseAddress = _configuration.GetValue<string>("BassAddress");
            //client = new HttpClient();
            //client.BaseAddress = baseAddress;
        }
        public async Task<LoginResponseModel> Login(LoginQueryModel queryModel)
        {
            var rs = new LoginResponseModel();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Account/login", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<LoginResponseModel>(response);
                //ApiContext.Current.Email = rs.Member.Email;
                //ApiContext.Current.Role = rs.Role;
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> Register(RegisterCreationModel queryModel)
        {
            var rs = new OperationResult();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Account/register", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OperationResult>(response);
                //ApiContext.Current.Email = rs.Member.Email;
                //ApiContext.Current.Role = rs.Role;
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> ChangePassword(ChangePasswordQueryModel queryModel)
        {
            var rs = new OperationResult();
            try
            {
                var jsonContent = JsonConvert.SerializeObject(queryModel);
                var response = await clientBase.SendRequest(baseAddress + "/Account/changepassword", HttpMethod.Post, jsonContent);
                rs = JsonConvert.DeserializeObject<OperationResult>(response);
                //ApiContext.Current.Email = rs.Member.Email;
                //ApiContext.Current.Role = rs.Role;
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AccountModel> GetAccountByEmail(string email)
        {
            var rs = new AccountModel();
            try
            {
                //var jsonContent = JsonConvert.SerializeObject(email);
                var response = await clientBase.SendRequest(baseAddress + "/Account/getaccountbyemail/"+ email, HttpMethod.Get);
                rs = JsonConvert.DeserializeObject<AccountModel>(response);
                //ApiContext.Current.Email = rs.Member.Email;
                //ApiContext.Current.Role = rs.Role;
                return rs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
