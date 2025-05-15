using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models
{
    public class UserDetail
    {
        public int UserDetailId { get; set; }
        [EmailAddress]
        public string Email { get; set; }        
        public bool Gender { get; set; }
        public int Nationality { get; set; }
        public int UserType { get; set; }
        public DateTime DOB { get; set; }
        public IFormFile Photo { get; set; }
        
        public string? PhotoPath { get; set; }
    }
}
