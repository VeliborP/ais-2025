using Microsoft.EntityFrameworkCore;
using WebShop.BLL.Interfaces;
using WebShop.DAL;
using WebShop.DAL.Models;

namespace WebShop.BLL.Services
{
    public class UserService : IUserService
    {
        private protected WebShopDbContext _context;
        public UserService(WebShopDbContext context)
        {
            _context = context;
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users
                                .Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Username == username && u.Password.Equals(password) && u.IsActive);

            if (user == null)
                return null;

            return user;
        }
    }
}
