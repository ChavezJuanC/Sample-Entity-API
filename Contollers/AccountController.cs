using api.Dtos.User;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Contollers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signingManager;

        private readonly ITokenService _tokenService;

        // Single constructor for dependency injection
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signingManager = signInManager;
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

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInDto logInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var foundUser = await _userManager.Users.FirstOrDefaultAsync(usr => usr.UserName == logInDto.UserName);

            if (foundUser == null)
            {
                return Unauthorized("UserName or Password Incorrect");
            }

            var passwordCheck = _signingManager.CheckPasswordSignInAsync(foundUser, logInDto.Password, false);

            if (!passwordCheck.IsCompletedSuccessfully)
            {
                return Unauthorized("UserName or Password Incorrect");
            }

            string userName = foundUser.UserName == null ? "" : foundUser.UserName;
            string email = foundUser.Email == null ? "" : foundUser.Email;

            return Ok(new LoggedInUserDto
            {
                UserName = userName,
                Email = email,
                Token = _tokenService.CreateToken(foundUser)
            });
        }
    }
}
