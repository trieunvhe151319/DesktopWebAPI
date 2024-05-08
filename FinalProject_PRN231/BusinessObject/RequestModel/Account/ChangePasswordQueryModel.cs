using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel.Account
{
    public class ChangePasswordQueryModel
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;      
        public string OldPassword { get; set; } = string.Empty;
    }
}
