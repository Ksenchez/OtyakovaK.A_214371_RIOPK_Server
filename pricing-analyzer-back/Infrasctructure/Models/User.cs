using System.ComponentModel.DataAnnotations;

namespace pricing_analyzer_back.Infrasctructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = "user"; // "admin" / "user"
    }

}
