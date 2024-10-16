using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Services;

namespace WebAPI.Controllers
{
    [Route("api")]
    public class TokenController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SecurityHelper _securityHelper;
        private readonly IApplicationService _applicationService;
        public TokenController(IConfiguration configuration, IApplicationService applicationService, SecurityHelper securityHelper)
        {
            _securityHelper = securityHelper;
            _configuration = configuration;
            _applicationService = applicationService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public IActionResult Post([FromBody]ApplicationLoginDTO applicationLoginModel)
        {
            if (ModelState.IsValid)
            {
                Models.Application Application = _applicationService.ApplicationLogin(applicationLoginModel.ApplicationEmail, _securityHelper.Md5Encryption(applicationLoginModel.ApplicationPassword));
                if (Application != null)
                {
                    var claims = new[]
                    {
                        new Claim("AppId", Application.ApplicationID.ToString()),
                        new Claim("ReturnURL",Application.ReturnURL),
                        new Claim("CallbackURL", Application.CallbackURL)

                };
                    int tokenExpiration;
                    int.TryParse(_configuration["TokenExpiration"], out tokenExpiration);

                    JwtSecurityToken token = new JwtSecurityToken
                    (
                        issuer: _configuration["Issuer"],
                        audience: _configuration["Audience"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddMinutes(tokenExpiration),
                        notBefore: DateTime.UtcNow,
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SigningKey"])),
                             SecurityAlgorithms.HmacSha256)
                    );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }
                else
                {
                    return Unauthorized();
                }


            }

            return BadRequest();
        }

    }
}