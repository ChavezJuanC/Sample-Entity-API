using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Contollers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ITokenService _tokenService;

        // Single constructor for dependency injection
        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (registerUserDto.Password == null)
                {
                    return BadRequest("Password field must be included");
                }

                var appUser = new AppUser
                {
                    UserName = registerUserDto.UserName,
                    Email = registerUserDto.Email
                };

                var createdUserResult = await _userManager.CreateAsync(appUser, registerUserDto.Password);
                if (createdUserResult.Succeeded)
                {

                    var applyRoleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (!applyRoleResult.Succeeded)
                    {
                        return BadRequest(applyRoleResult.Errors);
                    }

                    return Ok(new NewUserDto
                    {
                        Email = appUser.Email == null ? "" : appUser.Email,
                        UserName = appUser.UserName == null ? "" : appUser.UserName,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                else
                {

                    return BadRequest(createdUserResult.Errors);
                }

            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e.Message}");
            }
        }
    }
}
