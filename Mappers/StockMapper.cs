using api.Dtos;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDto StockToDto(this StockModel Stock)
        {
            return new StockDto
            {
                Id = Stock.Id,
                Symbol = Stock.Symbol,
                CompanyName = Stock.CompanyName,
                Purchase = Stock.Purchase,
                LastDiv = Stock.LastDiv,
                Industry = Stock.Industry,
                MarketCap = Stock.MarketCap
            };
        }

        public static StockModel DtoToStock(this CreateStockRequestDto StockDto)
        {

            return new StockModel
            {
                Symbol = StockDto.Symbol,
                CompanyName = StockDto.CompanyName,
                Purchase = StockDto.Purchase,
                LastDiv = StockDto.LastDiv,
                Industry = StockDto.Industry,
                MarketCap = StockDto.MarketCap
            };
        }
    }
}