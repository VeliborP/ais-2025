using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels.Home
{
    public class IndexViewModel
    {
        public List<CategoryProductsViewMOdel> Categories { get; set; } = [];

        public static IndexViewModel FromEntities(IEnumerable<Category> categories)
        {
            return new IndexViewModel
            {
                Categories = categories.Select(c => new CategoryProductsViewMOdel
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    Products = c.Products
                        .Select(p => new ProductViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            ImagePath = p.ImagePath,
                            Price = p.Price
                        })
                        .ToList()
                }).ToList()
            };
        }
    }

    public class CategoryProductsViewMOdel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public List<ProductViewModel> Products { get; set; } = [];
    }

    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImagePath { get; set; } = "";
        public decimal Price { get; set; }
    }
}
