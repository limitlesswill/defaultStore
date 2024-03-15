using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Product : BaseEntity
    {
        [MaxLength(50)]
        public string name { get; set; }
        [MaxLength(150)]
        public string? description { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal? price { get; set; }

        [ForeignKey("category")]
        public int? categoryID { get; set; }
        public Category? category { get; set; }
    }
}
