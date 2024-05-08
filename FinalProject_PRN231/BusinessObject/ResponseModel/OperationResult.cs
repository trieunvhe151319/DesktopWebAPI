using BusinessObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel
{
    public class OperationResult
    {
        public string ResultMessage { get; set; } = string.Empty;
        public CommonStatus Status { get; set; }
    }
}
