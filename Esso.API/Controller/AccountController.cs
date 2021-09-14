using Esso.DataAccess.EfCore;
using Esso.Entity.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Esso.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly EssoDbContext _dbContext;

        public AccountController(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings, EssoDbContext dbContext)
        {
            _userManager = userManager;
            _appSettings = appSettings;
            _dbContext = dbContext;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(dto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                var userClaims = await _userManager.GetClaimsAsync(user);

                authClaims.AddRange(userClaims);

                var authSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(_appSettings.Value.JwtSecret));

                var token = new JwtSecurityToken(
                    issuer: _appSettings.Value.JwtIssuer,
                    audience: _appSettings.Value.JwtIssuer,
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new TokenResult
                {
                    access_token = stringToken,
                    expires_in = 1
                });
            }

            return Ok(new GeneralResponse<string>
            {
                Result = "Kullanıcı Adı veya Şifre Yanlış",
                IsError = true,
                Code = 1
            });
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Password) || string.IsNullOrEmpty(dto.ConfirmPassword))
            {
                return Ok(new GeneralResponse<string>
                {
                    Result = "Değerler Boş Geçilemez"
                });
            }
            else if (dto.Password != dto.ConfirmPassword)
            {
                return Ok(new GeneralResponse<string>
                {
                    Result = "Parolalar Eşit Değil"
                });
            }

            var user = new IdentityUser()
            {
                UserName = dto.Email,
                Email = dto.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                var identityUser = await _userManager.FindByNameAsync(dto.Email);
                var resultIdentity = await _userManager.AddClaimAsync(identityUser, new Claim(dto.ClaimName, dto.ClaimName.ToLower()));
                if (resultIdentity.Succeeded)
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Kullanıcı Başarıyla Oluşturuldu",
                        IsError= false
                    });
                }

            }


            return Ok(new GeneralResponse<string>
            {
                Result = "Kullanıcı Oluşturulamadı",
                IsError = true
            });
        }
    }
}
