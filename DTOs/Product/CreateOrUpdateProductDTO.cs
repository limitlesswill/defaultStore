using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class CreateOrUpdateProductDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public decimal? price { get; set; }
        public int? categoryID { get; set; }
    }
}
