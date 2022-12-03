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
    public class SubscribersService
    {
        IMapper mapper;
        SubscribersRepository repository;
        public SubscribersService(BlogContext context)
        {
            repository = new SubscribersRepository(context);
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                x.CreateMap<Subscribers, SubscribersDTO>();
                x.CreateMap<SubscribersDTO, Subscribers>();
            });
            mapper = new Mapper(config);
        }
        public List<SubscribersDTO> Get(int id)
        {
            var list = mapper.Map<IEnumerable<Subscribers>, IEnumerable<SubscribersDTO>>(repository.GetAll()).ToList();
            return list.Where(x => x.PublisherId == id).ToList();
        }

        public async Task AddAsync(SubscribersDTO sub)
        {
            await repository.AddAsync(mapper.Map<SubscribersDTO, Subscribers>(sub));
            await repository.SaveChangesAsync();
        }
        public async Task RemoveAsync(SubscribersDTO sub)
        {
            await repository.DeleteAsync(mapper.Map<SubscribersDTO, Subscribers>(sub));
            await repository.SaveChangesAsync();
        }
    }
}
