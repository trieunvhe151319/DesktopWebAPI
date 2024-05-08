using AutoMapper.Execution;
using BusinessObject.Common;
using BusinessObject.RequestModel.Account;
using BusinessObject.ResponseModel;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class AccountDAO
    {
        private static AccountDAO _instance = null;
        private static readonly object instanceLock = new object();
        PRN231DBContext db = new PRN231DBContext();
        private AccountDAO() { }
        public static AccountDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new AccountDAO();
                    }
                    return _instance;
                }
            }
        }
        public async Task<Account> Login(LoginQueryModel model)
        {
            try
            {
                return await db.Accounts.AsNoTracking().Where(m => m.Email == model.Email && m.Password == model.Password).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> CreateAccount(Account model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                await db.Accounts.AddAsync(model);
                await db.SaveChangesAsync();
                db.Entry(model).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
        public async Task<Account> GetAccountByEmail(string email)
        {
            try
            {
                return await db.Accounts.Where(x => x.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OperationResult> UpdateAccount(ChangePasswordQueryModel model)
        {
            OperationResult result = new OperationResult()
            {
                Status = CommonStatus.Success
            };
            try
            {
                var existAccount = await db.Accounts.AsNoTracking().Where(x => x.Email == model.Email && x.Password == model.OldPassword).FirstOrDefaultAsync();
                if (existAccount == null)
                {
                    result.Status = CommonStatus.Failed;
                    result.ResultMessage = "Does not exist account";
                }
                else
                {
                    existAccount.Password = model.Password;
                    db.Accounts.Update(existAccount);
                    await db.SaveChangesAsync();
                    db.Entry(existAccount).State = EntityState.Detached;
                }
            }
            catch (Exception ex)
            {
                result.Status = CommonStatus.Failed;
                result.ResultMessage = ex.Message;
            }
            return result;
        }
    }
}
