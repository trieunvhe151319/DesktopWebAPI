using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Account;
using DataAccess.IRepositories;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountRepository accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }
        [HttpPost("login")]
        public async Task<LoginResponseModel> Login(LoginQueryModel queryModel)
        {
            LoginResponseModel model = new LoginResponseModel();
            try
            {
                model = await accountRepository.Login(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpPost("register")]
        public async Task<OperationResult> Register(RegisterCreationModel queryModel)
        {
            OperationResult model = new OperationResult();
            try
            {
                model = await accountRepository.Register(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpPost("changepassword")]
        public async Task<OperationResult> ChangePassword(ChangePasswordQueryModel queryModel)
        {
            OperationResult model = new OperationResult();
            try
            {
                model = await accountRepository.ChangePassword(queryModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
        [HttpGet("getaccountbyemail/{email}")]
        public async Task<AccountModel> GetAccountByEmail([FromRoute] string email)
        {
            AccountModel model = new AccountModel();
            try
            {
                model = await accountRepository.GetAccountByEmail(email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model;
        }
    }
}
