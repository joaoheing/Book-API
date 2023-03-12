using Microsoft.AspNetCore.Identity;

namespace BookAPI.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
