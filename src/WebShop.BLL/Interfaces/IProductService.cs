using WebShop.DAL.Models;

namespace WebShop.BLL.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int? id);
        Task<Product> CreateAsync(Product category);
        Task<Product> UpdateAsync(Product category);
        Task<bool> DeleteAsync(int id);
        Task<List<Category>> GetCategoriesWithProductsAsync();
    }
}
