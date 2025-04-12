using Project1.Dtos.Stock;
using Project1.Helpers;
using Project1.Models;

namespace Project1.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject query);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockModel);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
        Task<Stock?> GetBySymbolAsync(string symbol);
    }
}
