using Microsoft.EntityFrameworkCore;
using WebProductCategoryCrud1.Data;
using WebProductCategoryCrud1.Infrastrucure.IService;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.Service
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _context;

        public CategoryServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public async Task<bool> IsCategoryNameExistsAsync(string categoryName)
        {
            return await _context.Categories
                                 .AnyAsync(c => c.CategoryName.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
