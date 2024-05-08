using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel.Order
{
    public class OrderDetailDisplayModel
    {
        public List<OrderDetailModel> OrderDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
