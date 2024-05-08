using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Account;

namespace APP.IServices
{
    public interface IAccountServices
    {
        public Task<LoginResponseModel> Login(LoginQueryModel queryModel);
        public Task<AccountModel> GetAccountByEmail(string email);
        public Task<OperationResult> Register(RegisterCreationModel queryModel);
        public Task<OperationResult> ChangePassword(ChangePasswordQueryModel queryModel);
    }
}
