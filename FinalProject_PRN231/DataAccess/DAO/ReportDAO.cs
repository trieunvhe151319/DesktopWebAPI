using BusinessObject.ResponseModel.Report;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ReportDAO
    {
        private static ReportDAO _instance = null;
        private static readonly object instanceLock = new object();
        PRN231DBContext db = new PRN231DBContext();
        private ReportDAO() { }
        public static ReportDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ReportDAO();
                    }
                    return _instance;
                }
            }
        }
        public async Task<ReportModel> GetSummaryReport()
        {
            var result = new ReportModel();
            result.Revenue = await (from o in db.Orders
                       join od in db.OrderDetails on o.OrderId equals od.OrderId
                       group od by new { o.OrderId, o.Freight } into g
                       select new
                       {
                           OrderID = g.Key.OrderId,
                           NetRevenue = g.Sum(od => (decimal)od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount)),
                           Month = g.FirstOrDefault().Order.RequiredDate.Value.Month,
                           Year = g.FirstOrDefault().Order.RequiredDate.Value.Year
                       }).GroupBy(x => new
                       {
                           x.Year,
                           x.Month
                       }).Select(x => new RevenueMonth()
                       {
                           Year = x.Key.Year,
                           Month = x.Key.Month,
                           revenueMonth = x.Sum(x => x.NetRevenue)
                       }).OrderBy(x => x.Year).ThenBy(x => x.Month).ToListAsync();
            result.TopUsers = await (from o in db.Orders
                        join od in db.OrderDetails on o.OrderId equals od.OrderId
                        group od by o.CustomerId into g
                        select new
                        {
                            CustomerID = g.Key,
                            TotalPrice = g.Sum(od => (decimal)od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount))
                        }).Join(db.Customers, od => od.CustomerID, c => c.CustomerId, (od, c) => new TopUser
                        {
                            Username = c.ContactName,
                            TotalBill = od.TotalPrice,
                        }).Take(5).OrderByDescending(x => x.TotalBill).ToListAsync();

            result.BestSellers =await db.Products
                            .Join(db.OrderDetails,
                            p => p.ProductId,
                            od => od.ProductId,
                            (p, od) => new { p.ProductId, p.ProductName, od.Quantity })
                        .GroupBy(p => new { p.ProductId, p.ProductName })
                        .Select(g => new BestSeller
                        {
                            ProductName = g.Key.ProductName,
                            Count = g.Sum(x => x.Quantity)
                        })
                        .OrderByDescending(p => p.Count)
                        .Take(5)
                        .ToListAsync();
            return result;
        }
    }
}
