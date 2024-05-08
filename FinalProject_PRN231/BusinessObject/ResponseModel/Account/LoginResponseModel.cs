using BusinessObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel.Account
{
    public class LoginResponseModel
    {
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Customer;

    }
}
