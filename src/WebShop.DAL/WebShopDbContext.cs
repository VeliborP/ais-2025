using Microsoft.EntityFrameworkCore;
using WebShop.DAL.Models;

namespace WebShop.DAL
{
    public partial class WebShopDbContext : DbContext
    {
        public WebShopDbContext()
        {
        }

        public WebShopDbContext(DbContextOptions<WebShopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //insert testnih podataka (seed)
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@test.com",
                    Password = "admin123",
                    RoleId = 1,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    Email = "user@test.com",
                    Password = "user123",
                    RoleId = 2,
                    IsActive = true
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Elektronika", Code = "CAT-001", Description = "Razne elektronske uređaje, uključujući računare, telefone i dodatke." },
                new Category { Id = 2, Name = "Knjige", Code = "CAT-002", Description = "Knjige svih žanrova: edukativne, beletristika i priručnici." },
                new Category { Id = 3, Name = "Odeća", Code = "CAT-003", Description = "Različita odeća za muškarce, žene i decu." }
            );


            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Code = "PROD-001", Description = "Laptop sa Intel i7 procesorom, 16GB RAM i SSD diskom.", CategoryId = 1, Price = 1200 },
                new Product { Id = 2, Name = "Pametni telefon", Code = "PROD-002", Description = "Smartphone sa 6.5\" ekranom, 128GB memorije i trostrukom kamerom.", CategoryId = 1, Price = 800 },
                new Product { Id = 3, Name = "ASP.NET Core knjiga", Code = "PROD-003", Description = "Detaljna knjiga o razvoju web aplikacija u ASP.NET Core.", CategoryId = 2, Price = 35 },
                new Product { Id = 4, Name = "Majica", Code = "PROD-004", Description = "Pamučna majica različitih veličina i boja.", CategoryId = 3, Price = 20 }
            );
        }
    }
}
