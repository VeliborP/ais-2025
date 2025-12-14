using WebShop.DAL.Models;

namespace WebShop.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User?> ValidateUserAsync(string username, string password);
    }
}
