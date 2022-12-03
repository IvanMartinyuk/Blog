using BLL.DTO;
using BLL.Services;
using BlogApi.Config;
using DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllowOrigin")]
    public class UserController : Controller
    {
        UserService service;
        SubscribersService subService;
        public UserController(BlogContext context)
        {
            service = new UserService(context);
            subService = new SubscribersService(context);
        }
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Token([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest(new { error = "Invalid name or password" });
            service.Login(user.UserName, user.Password);
            if (UserService.LogedUser == null)
                return BadRequest(new { error = "Invalid name or password" });

            var claim = await GetClaimsIdentity(user.UserName, user.Password);

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: claim.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var response = new
            {
                access_token = accessToken,
                username = claim.Name
            };
            Console.WriteLine(response);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody] UserDTO user)
        {
            if (user == null)
                return BadRequest(new { error = "Invalid name or password" });
            if (!service.ValidateUserName(user.UserName))
                return BadRequest(new { arror = "Name is not valid" });
            await service.AddOrUpdateAsync(user);
            return Ok();
        }
        private async Task<ClaimsIdentity> GetClaimsIdentity(string login, string password)
        {
            var claims = new List<Claim>()
                {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
        [HttpGet]
        public async Task<IActionResult> Articles(int id)
        {
            var user = await service.GetAsync(id);
            user.Articles.ForEach(x =>
            {
                x.Publisher = null;
                x.ArticlesUsers = null;
            });
            return Json(user.Articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Subscribe(int blogerId)
        {
            SubscribersDTO subscribe = new SubscribersDTO()
            {
                SubscriberId = UserService.LogedUser.UserId,
                PublisherId = blogerId
            };
            await subService.AddAsync(subscribe);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unsubscribe(int blogerId)
        {
            SubscribersDTO subscribe = new SubscribersDTO()
            {
                SubscriberId = UserService.LogedUser.UserId,
                PublisherId = blogerId
            };
            await subService.RemoveAsync(subscribe);
            return Ok();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Subscribers()
        {
            var subs = subService.Get(UserService.LogedUser.UserId);
            return Json(subs);
        }
    }
}
