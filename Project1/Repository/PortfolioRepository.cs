using FinShark.Interfaces;
using FinShark.Models;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace FinShark.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortfolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolio = await _context.Portfolios.FirstOrDefaultAsync(x => x.AppUserId == appUser.Id
                && x.Stock.Symbol.ToLower() == symbol.ToLower());
            if (portfolio == null) return null;

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser appUser)
        {
            return await _context.Portfolios.Where(u => u.AppUserId == appUser.Id)
                .Select(stock => new Stock
                {
                    Id = stock.StockId,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Price = stock.Stock.Price,
                    LastDiv = stock.Stock.LastDiv,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,

                }).ToListAsync();
        }

        public Task GetUserPortfolio(Task<AppUser?> appUser)
        {
            throw new NotImplementedException();
        }
    }
}
