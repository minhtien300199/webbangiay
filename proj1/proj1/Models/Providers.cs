using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace proj1.Models
{
    public class Providers
    {
        [Key]
        public int ProviderID { get; set; }
        public string ProviderName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
