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
    public class NewsService
    {
        IMapper mapper;
        NewsRepository repository;
        public NewsService(BlogContext context)
        {
            repository = new NewsRepository(context);
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                x.CreateMap<News, NewsDTO>();
                x.CreateMap<NewsDTO, News>();
            });
            mapper = new Mapper(config);
        }
        public List<NewsDTO> GetNews(int userId)
        {
            var list = mapper.Map<IEnumerable<News>, IEnumerable<NewsDTO>>(repository.GetAll()).ToList();
            return list.Where(x => x.UserId == userId).ToList();
        }
        public async Task AddNewsAsync(NewsDTO news)
        {
            await repository.AddAsync(mapper.Map<NewsDTO, News>(news));
            await repository.SaveChangesAsync();
        }
        public async Task RemoveNewsAsync(NewsDTO news)
        {
            await repository.DeleteAsync(mapper.Map<NewsDTO, News>(news));
            await repository.SaveChangesAsync();
        }
    }
}
