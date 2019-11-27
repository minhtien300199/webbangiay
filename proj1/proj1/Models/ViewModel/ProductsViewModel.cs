using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1.Models.ViewModel
{
    public class ProductsViewModel
    {
        public Products Products { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<Providers> Providers { get; set; }
        public IEnumerable<ProductTypes> ProductTypes { get; set; }
    }
}
