using api.Data;
using api.Dtos;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
            var stock = await _stock_repo.GetAllAsync();
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
        public async Task<IActionResult> AddNewStock([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.DtoToStock();

            await _stock_repo.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.StockToDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stock_repo.DeleteStockAsync(id);

            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            var stock = await _stock_repo.UpdateStockAsync(id, stockDto);

            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.StockToDto());
        }
    }
}
