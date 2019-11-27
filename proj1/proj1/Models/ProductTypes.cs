using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace proj1.Models
{
    public class ProductTypes
    {
        [Key]
        public int TypeId { get; set; }
        [Required]
        public string NameType { get; set; }

    }
}
