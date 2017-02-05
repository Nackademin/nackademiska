using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Nackademiska.Services;
using Nackademiska.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;

namespace Nackademiska.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly ICustomerRepository _customers;
        private readonly IAdminRepository _admins;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly JwtOptions _jwtOptions;
        public AccountController (ICustomerRepository customers,
                                    IAdminRepository admins,
                                    UserManager<ApplicationUser> userManager, 
                                    RoleManager<IdentityRole> roleManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    IPasswordHasher<ApplicationUser> passwordHasher,
                                    IOptions<JwtOptions> jwtOptions)
        {
            _customers = customers;
            _admins = admins;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginInformation loginInformation)
        {
            try {
                var result = await _signInManager.PasswordSignInAsync(loginInformation.email, loginInformation.password, false, false);
                if (result.Succeeded) {
                    var user = await _userManager.FindByNameAsync(loginInformation.email);
                    return Ok(new {
                        id = user.CustomerId
                    });
                }

                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            try {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody]LoginInformation loginInformation)
        {
            try {
                var user = await _userManager.FindByNameAsync(loginInformation.email);
                if( user != null ) {
                    if( _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginInformation.password) == PasswordVerificationResult.Success ) 
                    {
                        var claims = new [] 
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: _jwtOptions.Issuer,
                            audience: _jwtOptions.Audiance,
                            claims: claims,
                            expires: DateTime.UtcNow.AddMinutes(15),
                            signingCredentials: creds
                        );
                        return Ok(new {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }

                }
                return Unauthorized();
            }
            catch (Exception)
            {
                return BadRequest();
            }   
        }
    }
}
