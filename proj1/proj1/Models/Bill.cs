using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj1.Models
{
    public class Bill
    {
        [Key]
        public int BillId { get; set; }
        public int CusId { get; set; }
        public int ProductId { get; set; }
        public int Number { get; set; }
        public DateTime DatePurchase { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        [ForeignKey("CusId")]
        public Customer Customer { get; set; }
    }
}
