using eCommerce_API.Models.AuthenticationModel;
using eCommerce_API.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eCommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SqlContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(SqlContext context, IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn(AuthModel auth)
        {
            if (string.IsNullOrEmpty(auth.Email) || string.IsNullOrEmpty(auth.Password))
                return BadRequest("Both email and password are needed.");

            var findUser = await _context.Users.Where(x => x.Email == auth.Email).FirstOrDefaultAsync();
            if (findUser == null || !findUser.CompareEncryptedPassword(auth.Password))
                return BadRequest("Incorrect input info.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("id", findUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, findUser.Email),
                    new Claim("usercode", _configuration.GetValue<string>("UserApiKey"))
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("security_secret"))),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
        }

        [HttpPost("SignInAdmin")]
        public async Task<ActionResult> SignInAdmin(AuthModel auth)
        {
            if (string.IsNullOrEmpty(auth.Email) || string.IsNullOrEmpty(auth.Password))
                return BadRequest("Both email and password are needed.");

            var findUser = await _context.Users.Where(x => x.Email == auth.Email).FirstOrDefaultAsync();
            if (findUser == null || !findUser.CompareEncryptedPassword(auth.Password))
                return BadRequest("Incorrect input info.");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim("id", findUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, findUser.Email),
                    new Claim("admincode", _configuration.GetValue<string>("AdminApiKey")),
                    new Claim("usercode", _configuration.GetValue<string>("UserApiKey"))
                }),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("security_secret"))),
                    SecurityAlgorithms.HmacSha512Signature)
            };

            return Ok(tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)));
        }
    }
}
