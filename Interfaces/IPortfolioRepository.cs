using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockModel>> GetUserPortfolioAsync(AppUser user);
    }
}