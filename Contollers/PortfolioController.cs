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
    }
}