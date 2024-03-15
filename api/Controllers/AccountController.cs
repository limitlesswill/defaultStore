using Application.Context;
using DTOs.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Controllers
{
    [ApiController, Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(SignInManager<User> _signInManager, UserManager<User> _userManager)
        {
            this.signInManager = _signInManager;
            this.userManager = _userManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerdto)
        {
            var exists = await userManager.FindByEmailAsync(registerdto.email);
            if (exists is not null)
                return StatusCode(505, "Email already exists");
            User user = new User() { Email = registerdto.email, UserName = registerdto.email };
            var res = await userManager.CreateAsync(user, registerdto.password);
            if (res.Succeeded)
            {
                return Ok("account created successfully");
            }
            return StatusCode(200, "NOT SUCCEDED");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(RegisterDTO registerdto)
        {
            var error = Unauthorized("Email or password is wrong");
            var user = await userManager.FindByEmailAsync(registerdto.email);
            if (user == null) return error;
            var valid = await userManager.CheckPasswordAsync(user, registerdto.password);
            if (valid == false) return error;

            List<Claim> _claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Web API Day 2 this key should be hidden"));
            var signcred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var _issuer = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/").AbsoluteUri;
            //var _audience = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/").AbsoluteUri;
            var _issuer = "hi";
            var _audience = "bye";

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddDays(3),
                signingCredentials: signcred,
                claims: _claims,
                issuer: _issuer,
                audience: _audience
                );
            var str = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(str);
        }
    }
}
