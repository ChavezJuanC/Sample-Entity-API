using api.Dtos;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockModel>> GetAllAsync();
        Task<StockModel?> FindStockByIdAsync(int id);
        Task<StockModel> CreateStockAsync(StockModel model);
        Task<StockModel?> DeleteStockAsync(int id);
        Task<StockModel?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto);
    }
}