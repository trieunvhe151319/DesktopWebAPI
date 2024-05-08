using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel.Account
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public int? Role { get; set; } = 2;
    }
}
