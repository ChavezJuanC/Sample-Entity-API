using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Contollers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stock_repo;
        public StockController(ApplicationDBContext context, IStockRepository repo)
        {
            _context = context;
            _stock_repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stock = await _stock_repo.GetAllSync();
            var stockDtos = stock.Select(s => s.StockToDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _context.Stocks.FindAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.StockToDto());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.DtoToStock();

            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.StockToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = stockDto.Symbol;
            stock.CompanyName = stockDto.CompanyName;
            stock.Purchase = stockDto.Purchase;
            stock.LastDiv = stockDto.LastDiv;
            stock.Industry = stockDto.Industry;
            stock.MarketCap = stockDto.MarketCap;

            return Ok(stock.StockToDto());
        }
    }
}
