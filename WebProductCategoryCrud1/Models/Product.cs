using System.ComponentModel.DataAnnotations;

namespace WebProductCategoryCrud1.Models
{
    public class Product
    {
      

        public int ProductId { get; set; }
        [Required]
        public required string ProductName { get; set; }
        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
