using FinShark.Models;
using Microsoft.AspNetCore.Identity;

namespace Project1.Models
{
    public class AppUser : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
