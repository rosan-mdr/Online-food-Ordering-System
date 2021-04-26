using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrdering.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Product Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Price")]
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Display(Name = "Status")]
        public bool Status { get; set; }
        [Required]
        public string ProductCode { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AddedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? ModifiedDate { get; set; }
    }
    public class ProductViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [DisplayName("Food Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Price")]
        public decimal Price { get; set; }
        [DisplayName("Status")]
        public bool Status { get; set; }
    }
}