using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Gtr.Controllers;
using Task_Gtr.Models.DTOs;
using Task_Gtr.Web.Configuration;

namespace Task_Gtr.Web.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly ILogger<AuthManagementController> _logger;
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(ILogger<AuthManagementController> logger, UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> _optionsMonitor)
        {
            _logger = logger;
            _usermanager = userManager;
            _jwtConfig = _optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                //check email exist or not
                var emailExist = await _usermanager.FindByEmailAsync(requestDto.Email);
                if (emailExist != null)
                {
                    return BadRequest("email already exist");
                }
                var newUser = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Email
                };
                var isCreated = await _usermanager.CreateAsync(newUser,requestDto.Password);
                if (isCreated.Succeeded) 
                {
                    //Generate Token
                    var token = GenerateJwtToken(newUser);
                    return Ok(new RegistrationRequestResponse()
                    {
                        Result = true,
                        Token = token
                    });
                }
                return BadRequest(isCreated.Errors.Select(x=>x.Description).ToList());
            }
            return BadRequest("Invalid Request Payload");
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto userDto)
        {
            if (ModelState.IsValid) 
            { 
                var existingUser = await _usermanager.FindByEmailAsync(userDto.Email);
                if(existingUser == null)
                {
                    return BadRequest("Invalid Username or Password");
                }
                var isPasswordValid = await _usermanager.CheckPasswordAsync(existingUser, userDto.Password);
                if (isPasswordValid) 
                {
                    var token = GenerateJwtToken(existingUser);
                    return Ok(new LoginRequestResponse()
                    {
                        Token = token,
                        Result = true                    
                    });
                }
            }
            return NotFound();
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512)

            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }
    }
}
