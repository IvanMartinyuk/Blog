using BLL.DTO;
using BLL.Services;
using DAL.Context;
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
    public class NewsController : Controller
    {
        UserService userService;
        NewsService service;
        ArticleService articleService;
        public NewsController(BlogContext context)
        {
            userService = new UserService(context);
            service = new NewsService(context);
            articleService = new ArticleService(context);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetId()
        {
            var userId = UserService.LogedUser.UserId;
            return Json(service.GetNews(userId));
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> GetNews()
        {
            var userId = UserService.LogedUser.UserId;
            List<ArticleDTO> articles = new List<ArticleDTO>();
            foreach (var news in service.GetNews(userId))
            {
                var art = await articleService.GetAsync(news.ArticleId);
                art.ArticlesUsers = null;
                art.Publisher = null;
                articles.Add(art);
            }
            return Json(articles);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddNews(int userId, int articleId)
        {
            await service.AddNewsAsync(new NewsDTO() { UserId = userId, ArticleId = articleId });
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteNews(int userId, int articleId)
        {
            await service.RemoveNewsAsync(new NewsDTO() { UserId = userId, ArticleId = articleId });
            return Ok();
        }
    }
}
