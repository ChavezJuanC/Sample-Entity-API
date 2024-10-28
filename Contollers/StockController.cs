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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var stock = await _stock_repo.FindStockByIdAsync(id);

            if (stock == null)
            {
                return NotFound("Sotck Not Found");
            }

            return Ok(stock.StockToDto());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStock([FromBody] CreateStockRequestDto stockDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.DtoToStock();

            await _stock_repo.CreateStockAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.StockToDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stock_repo.DeleteStockAsync(id);

            if (stock == null)
            {
                return NotFound("Stock Not Found");
            }

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromBody] UpdateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stock = await _stock_repo.UpdateStockAsync(id, stockDto);

            if (stock == null)
            {
                return NotFound("Stock Not Found");
            }

            return Ok(stock.StockToDto());
        }
    }
}
