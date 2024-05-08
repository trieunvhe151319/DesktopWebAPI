using AutoMapper;
using BusinessObject.Common;
using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using BusinessObject.ResponseModel.Account;
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
    public class AccountRepository : IAccountRepository
    {
        private readonly IMapper _mapper;
        public AccountRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<OperationResult> Register(RegisterCreationModel model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {           
                var customerId = await CustomerDAO.Instance.CreateCustomer(_mapper.Map<Customer>(model));
                if(!string.IsNullOrEmpty(customerId))
                {
                    model.CustomerId = customerId;
                    result = await AccountDAO.Instance.CreateAccount(_mapper.Map<Account>(model));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> ChangePassword(ChangePasswordQueryModel model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                result = await AccountDAO.Instance.UpdateAccount(model);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoginResponseModel> Login(LoginQueryModel model)
        {
            LoginResponseModel result = new LoginResponseModel();
            try
            {
                result = _mapper.Map<LoginResponseModel>(await AccountDAO.Instance.Login(model));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<AccountModel> GetAccountByEmail(string email)
        {
            var result = new AccountModel();
            try
            {
                result = _mapper.Map<AccountModel>(await AccountDAO.Instance.GetAccountByEmail(email));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
