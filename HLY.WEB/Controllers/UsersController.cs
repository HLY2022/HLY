using HLY.WEB.Data;
using HLY.WEB.Data.IServices;
using HLY.WEB.Data.Module;
using HLY.WEB.Data.Module.Parameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HLY.WEB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IConfiguration _configuration;

        private readonly ILogger<WeatherForecastController> _logger;

        public UsersController(ILogger<WeatherForecastController> logger, IUsersService usersService,
          IConfiguration configuration)
        {
            _logger = logger;
            _usersService = usersService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UsersParam usersParam)
        {

            var users = _usersService.Authenticate(usersParam.Code, usersParam.Password);
            var tokenHandler = new JwtSecurityTokenHandler();
           // var TockenKey = _configuration["TockenKey"];
            var key = Encoding.ASCII.GetBytes(_configuration["TockenKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Code.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                OrgId = users.OrgId,
                Code = users.Code,
                Name = users.Name,
                Email = users.Email,
                ProfileImage = users.ProfileImage,
                PasswordHash = users.PasswordHash,
                Groups = users.Groups,
                ApiKey = users.ApiKey,
                Disabled = users.Disabled,
                Mobile = users.Mobile,
                OrgunitId = users.OrgunitId,
                Token = tokenString
            });
        }
    }
}
