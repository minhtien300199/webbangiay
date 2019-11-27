using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace proj1.Models.ViewModel
{
    public class ShopingCartModelView
    {
        public List<Bill> Bills { get; set; }
        public Customer Customer { get; set; }
        public List<Products> Products { get; set; }

    }
}
