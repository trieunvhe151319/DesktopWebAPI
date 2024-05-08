using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ResponseModel.Product
{
    public class ProductDisplayModel
    {
        public List<ProductModel> Products { get; set; }
        public int TotalCount { get; set; }
    }
    public class ProductDetailDisplayModel
    {
        public List<ProductDetailModel> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
