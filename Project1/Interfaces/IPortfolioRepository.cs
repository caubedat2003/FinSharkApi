using FinShark.Models;
using Project1.Models;

namespace FinShark.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser user);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task GetUserPortfolio(Task<AppUser?> appUser);
        Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
    }
}
