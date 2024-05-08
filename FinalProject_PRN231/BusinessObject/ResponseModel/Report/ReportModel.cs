using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel.Report
{
    public class ReportModel
    {
        public List<RevenueMonth> Revenue { get; set; } = new List<RevenueMonth>();
        public List<BestSeller> BestSellers { get; set; } = new List<BestSeller>();
        public List<TopUser> TopUsers { get; set; } = new List<TopUser>();

    }
    public class RevenueMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal revenueMonth { get; set; }
    }

    public class TopUser
    {
        public string Username { get; set; }
        public decimal TotalBill { get; set; }
    }

    public class BestSeller
    {
        public string ProductName { get; set; }
        public int Count { get; set; }
    }

}
