using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Account;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepositories
{
    public interface IAccountRepository
    {
        public Task<OperationResult> Register(RegisterCreationModel model);
        public Task<LoginResponseModel> Login(LoginQueryModel model);
        public Task<AccountModel> GetAccountByEmail(string email);
        public Task<OperationResult> ChangePassword(ChangePasswordQueryModel model);
    }
}
