using api.Dtos;
using api.Models;
using api.QueryObjects;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockModel>> GetAllAsync(StockQueries stockQueries);
        Task<StockModel?> FindStockByIdAsync(int id);
        Task<StockModel?> FindStockBySymbolAsync(string symbol);
        Task<StockModel> CreateStockAsync(StockModel model);
        Task<StockModel?> DeleteStockAsync(int id);
        Task<StockModel?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
    }
}