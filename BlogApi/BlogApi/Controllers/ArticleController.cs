using BLL.DTO;
using BLL.Services;
using DAL.Context;
using DAL.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllowOrigin")]
    public class ArticleController : Controller
    {
        ArticleService service;
        UserRepository repos;
        public ArticleController(BlogContext context)
        {
            service = new ArticleService(context);
            repos = new UserRepository(context);
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var article = await service.GetAsync(id);
            article.ArticlesUsers = null;
            article.Publisher = null;
            return Json(article);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(service.GetAll());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]ArticleDTO article)
        {
            article.UserId = UserService.LogedUser.UserId;
            article.Publisher = await repos.GetAsync(article.UserId);
            article.DateOfPublish = DateTime.Now;
            await service.AddOrUpdate(article);
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            await service.Remove(id);
            return Ok();
        }
    }
}
