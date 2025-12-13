using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            Categories = new List<SelectListItem>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public required string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        
        public required string Name { get; set; }
        
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be between 0.01 and 1,000,000")]
        public decimal Price { get; set; }

        [DisplayName("Category name")]
        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }

        //upload slike proizvoda
        [DisplayName("Product image")]
        public IFormFile? ImageFile { get; set; }
        [DisplayName("Product image")]
        public string? ImagePath { get; set; }
        public bool DeleteImage { get; set; }

        public List<SelectListItem> Categories { get; set; }

        internal static ProductViewModel FromEntity(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description == null ? "Empty description" : product.Description,
                Price = product.Price,
                CategoryName = product.Category == null ? "No category" : product.Category.Name,
                CategoryId = product.CategoryId,
                ImagePath = product.ImagePath
            };
        }

        internal static ProductViewModel FromEntity(Product product, List<Category> categories = null)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                CategoryName = product.Category == null ? "---" : product.Category.Name,
                CategoryId = product.CategoryId,
                ImagePath = product.ImagePath,
                Categories = categories.Select(model => new SelectListItem
                {
                    Value = model.Id.ToString(),
                    Text = model.Code + "-" + model.Name,
                    Selected = model.Id == product.CategoryId
                }).ToList()
            };
        }

        internal Product ToEntity(string? imagePath = null, bool deleteImage = false)
        {
            return new Product
            {
                Id = this.Id,
                Code = this.Code.Trim(),
                Name = this.Name.Trim(),
                Description = this.Description?.Trim(),
                Price = this.Price,
                CategoryId = this.CategoryId,
                ImagePath = deleteImage ? null : (imagePath ?? this.ImagePath)
            };
        }
    }
}
