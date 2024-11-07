using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Contollers
{
    [Route("api/portfolios")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolios()
        {
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);

            if (appUser == null) return NotFound("App User not found!"); // just to get rid of null warning

            var userPortfolio = await _portfolioRepository.GetUserPortfolioAsync(appUser);

            return Ok(userPortfolio);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            string userName = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(userName);
            StockModel? stock = await _stockRepository.FindStockBySymbolAsync(symbol);

            if (appUser == null)
            {
                return Unauthorized();
            }

            if (stock == null)
            {
                return NotFound("Stock Not Found");
            }

            List<StockModel> userStocks = await _portfolioRepository.GetUserPortfolioAsync(appUser);

            if (userStocks.Any(stock => string.Equals(stock.Symbol.ToLower(), symbol.ToLower())))
            {
                return BadRequest("Cannot Create Duplicate");
            }

            Portfolio newPortfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id,
            };

            await _portfolioRepository.CreatePortfolioAsync(newPortfolio);

            if (newPortfolio == null)
            {
                return BadRequest("Could not create");
            }

            return Created();
        }
    }
}