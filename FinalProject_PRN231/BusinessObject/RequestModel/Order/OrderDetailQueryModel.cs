using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel.Order
{
    public class OrderDetailQueryModel
    {
        public int OrderId { get; set; }
        public string? SearchText { get; set; } = string.Empty;       
        public string? PriceRange { get; set; } = string.Empty;
        public Pagination? Pagination { get; set; } = new Pagination();
    }
}
