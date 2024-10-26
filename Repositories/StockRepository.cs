using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;


namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<StockModel>> GetAllAsync()
        {
            return await _context.Stocks.Include(stock => stock.Comments).ToListAsync();
        }

        public async Task<StockModel?> FindStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(stock => stock.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
        }

        public async Task<StockModel> CreateStockAsync(StockModel model)
        {
            await _context.Stocks.AddAsync(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<StockModel?> DeleteStockAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return stock;

        }

        public async Task<StockModel?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);

            if (stock == null)
            {
                return null;
            }

            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();

            return stock;

        }

    }
}
