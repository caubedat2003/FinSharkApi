using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Dtos.Stock;
using Project1.Helpers;
using Project1.Interfaces;
using Project1.Models;

namespace Project1.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            var stock = _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stock = stock.Where(s => s.CompanyName.Contains(query.CompanyName)); // Filter by company name
            }
            if(!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stock = stock.Where(s => s.Symbol.Contains(query.Symbol)); // Filter by symbol
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy.Equals("Symbol" , StringComparison.OrdinalIgnoreCase))
                {
                    //Sort by Symbol
                    stock = query.IsDecsending ? stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stock.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).ThenInclude(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var exsitingStock = await _context.Stocks.FirstOrDefaultAsync(x =>x.Id == id);
            if (exsitingStock == null)
            {
                return null;
            }
            exsitingStock.Symbol = stockDto.Symbol;
            exsitingStock.CompanyName = stockDto.CompanyName;
            exsitingStock.Price = stockDto.Price;
            exsitingStock.LastDiv = stockDto.LastDiv;
            exsitingStock.Industry = stockDto.Industry;
            exsitingStock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return exsitingStock;
        }
    }
}
