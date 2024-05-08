using BusinessObject.Common;
using BusinessObject.ResponseModel;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO _instance = null;
        private static readonly object instanceLock = new object();
        PRN231DBContext db = new PRN231DBContext();
        private CustomerDAO() { }
        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new CustomerDAO();
                    }
                    return _instance;
                }
            }
        }

        public async Task<string> CreateCustomer(Customer model)
        {
            try
            {
                model.CustomerId = $"CUS{DateTime.Now.Ticks}";
                await db.Customers.AddAsync(model);
                await db.SaveChangesAsync();
                db.Entry(model).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return model.CustomerId;
        }
    }
}
