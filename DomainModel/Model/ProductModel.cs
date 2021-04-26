using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Model
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Display(Name="Product Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name ="Price")]
        [Required]
        public decimal Price { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
        [Required]
       
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
