using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj1.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public int ProvidersId { get; set; }
        [ForeignKey("ProvidersId")]
        public Providers Providers { get; set; }
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public ProductTypes ProductTypes { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public string Image { get; set; }
       
    }
}
