using System.ComponentModel.DataAnnotations;

namespace WebShop.DAL.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = "";

        [Required]
        [StringLength(255, MinimumLength = 6)]
        public string Password { get; set; } = "";

        [Required]
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
