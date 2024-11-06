using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<StockModel>> GetUserPortfolioAsync(AppUser user)
        {
            return await _context.Portfolios
                .Where(port => port.AppUserId == user.Id)
                .Select(port => new StockModel
                {
                    Id = port.Stock.Id,
                    Symbol = port.Stock.Symbol,
                    Purchase = port.Stock.Purchase,
                    LastDiv = port.Stock.LastDiv,
                    Industry = port.Stock.Industry,
                    MarketCap = port.Stock.MarketCap,
                })
                .ToListAsync();
        }
    }
}