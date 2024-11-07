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
        public async Task<List<StockModel>?> GetUserPortfolioAsync(AppUser user)
        {
            return await _context.Portfolios
                .Where(port => port.AppUserId == user.Id)
                .Select(port => new StockModel
                {
                    Id = port.Stock != null ? port.Stock.Id : 0,
                    Symbol = port.Stock != null ? port.Stock.Symbol : string.Empty,
                    Purchase = port.Stock != null ? port.Stock.Purchase : 0,
                    LastDiv = port.Stock != null ? port.Stock.LastDiv : 0,
                    Industry = port.Stock != null ? port.Stock.Industry : string.Empty,
                    MarketCap = port.Stock != null ? port.Stock.MarketCap : 0,
                })
                .ToListAsync();
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolioAsync(AppUser appUser, string Symbol)
        {
            Portfolio? portfolio = await _context.Portfolios.FirstOrDefaultAsync(port =>
            string.Equals(port.AppUserId, appUser.Id) && string.Equals(Symbol.ToLower(), port.Stock != null ?
            port.Stock.Symbol.ToLower() : string.Empty));

            if (portfolio == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }
    }
}