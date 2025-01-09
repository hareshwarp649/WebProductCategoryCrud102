using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebProductCategoryCrud1.Models
{
    public class Product
    {
      

        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]

        public Category Category { get; set; }
    }
}
