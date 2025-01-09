using System.Linq.Expressions;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.IRepository
{
    public interface ICategoryRepository:IRepository<Category>
    {
        
        void Update(Category category);
    }
}
