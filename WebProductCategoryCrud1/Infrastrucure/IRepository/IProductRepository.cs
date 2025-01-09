using System.Linq.Expressions;
using WebProductCategoryCrud1.Models;

namespace WebProductCategoryCrud1.Infrastrucure.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        
        void Update(Product product);
    }
}
