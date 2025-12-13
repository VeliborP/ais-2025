using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShop.DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public required string Code { get; set; }
        [Required]
        [StringLength(150)]
        public required string Name { get; set; }
        [StringLength(1500)]
        public string? Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        [MaxLength(255)]
        public string? ImagePath { get; set; }
        public Category? Category { get; set; }
    }
}
