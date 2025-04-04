using Project1.Models;

namespace Project1.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
