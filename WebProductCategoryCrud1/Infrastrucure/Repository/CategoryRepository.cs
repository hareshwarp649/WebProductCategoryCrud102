using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Data;
using WebProductCategoryCrud1.Infrastrucure.IRepository;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            //_context.Categories.Update(category);
            var categoryDB= _context.Categories.FirstOrDefault(x=>x.CategoryId==category.CategoryId);
            if (categoryDB != null)
            {
                categoryDB.CategoryName = category.CategoryName;
                
            }
        }
    }
}
