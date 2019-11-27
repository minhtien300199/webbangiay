using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proj1.Models
{
    public class Customer
    {
        [Key]
        public int CusId { get; set; }
        public string CusName { get; set; }
        public string PhoneNum { get; set; }
        public string Address { get; set; }
        public ICollection<Bill> Bill { get; set; }
        public bool isConfirmed { get; set; }

    }
}
