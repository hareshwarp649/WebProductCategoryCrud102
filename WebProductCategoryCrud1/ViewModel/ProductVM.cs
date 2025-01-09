using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<Product> Products { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
