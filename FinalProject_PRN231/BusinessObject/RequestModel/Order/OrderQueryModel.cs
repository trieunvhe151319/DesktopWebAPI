using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel.Order
{
    public class OrderQueryModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
