using System.ComponentModel.DataAnnotations;
using WebShop.DAL.Models;

namespace WebShop.MVC.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public required string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }

        public string? Description { get; set; }


        public static CategoryViewModel FromEntity(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Code = category.Code,
                Name = category.Name,
                Description = category.Description ?? "---"
            };
        }

        public Category ToEntity()
        {
            return new Category
            {
                Id = this.Id,
                Code = this.Code.Trim(),
                Name = this.Name.Trim(),
                Description = this.Description?.Trim()
            };
        }
    }
}
