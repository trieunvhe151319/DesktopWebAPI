using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.RequestModel
{
    public class Pagination
    {
        public int PageSize { get; set; } = 8;
        public int PageNumber { get; set; } = 1;
    }
}
