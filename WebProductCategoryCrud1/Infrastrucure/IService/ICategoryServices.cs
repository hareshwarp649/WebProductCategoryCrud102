using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.IService
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<bool> IsCategoryNameExistsAsync(string categoryName);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
