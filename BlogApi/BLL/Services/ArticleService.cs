using AutoMapper;
using BLL.DTO;
using DAL.Context;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ArticleService
    {
        IMapper mapper;
        ArticleRepository repository;
        public ArticleService(BlogContext context)
        {
            repository = new ArticleRepository(context);
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                x.CreateMap<Article, ArticleDTO>();
                x.CreateMap<ArticleDTO, Article>();
            });
            mapper = new Mapper(config);
        }
        public async Task<ArticleDTO> GetAsync(int id) => mapper.Map<Article, ArticleDTO>(await repository.GetAsync(id));
        public List<ArticleDTO> GetByUserAsync(int userId)
        {
            var list = mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(repository.GetAll());
            return list.Where(x => x.UserId == userId).ToList();
        }
        public IEnumerable<ArticleDTO> GetAll() => mapper.Map<IEnumerable<Article>, IEnumerable<ArticleDTO>>(repository.GetAll());
        public async Task AddOrUpdate(ArticleDTO article)
        {
            if (await repository.GetAsync(article.ArticleId) != null)
                await repository.UpdateAsync(mapper.Map<ArticleDTO, Article>(article));
            else
                await repository.AddAsync(mapper.Map<ArticleDTO, Article>(article));
            await repository.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            await repository.DeleteAsync(await repository.GetAsync(id));
            await repository.SaveChangesAsync();
        }
    }
}
