using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Data;
using WebProductCategoryCrud1.Infrastrucure.IRepository;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Product product)
        {
            var productDB = _context.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
            if (productDB != null)
            {
                productDB.ProductName = product.ProductName;

            }
        }
    }
}
