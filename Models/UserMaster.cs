using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class UserMaster
    {
        public int UserId { get; set; }
        public string? FullName { get; set; }
        
        public string? UserName { get; set; }

        [EmailAddress]
        required
        public string Email { get; set; }
        
        required
        public string Password { get; set; }
        public int UserGroup { get; set; }
        public bool IsActive { get; set; }

    }
}
