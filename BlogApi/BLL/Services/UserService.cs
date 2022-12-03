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
    public class UserService
    {
        public static UserDTO LogedUser { get; set; }
        IMapper mapper;
        UserRepository repository;
        public UserService(BlogContext context)
        {
            repository = new UserRepository(context);
            MapperConfiguration config = new MapperConfiguration(x =>
            {
                x.CreateMap<User, UserDTO>();
                x.CreateMap<UserDTO, User>();
            });
            mapper = new Mapper(config);
        }

        public void Login(string name, string password)
        {
            UserDTO user = GetAll().Where(x => x.UserName == name && x.Password == password).FirstOrDefault();
            if (user != null)
                LogedUser = user;
        }
        public void Logout()
        {
            LogedUser = null;
        }
        public bool ValidateUserName(string name)
        {
            if (repository.GetAll().Any(x => x.UserName == name))
                return false;
            return true;
        }
        public async Task<UserDTO> GetAsync(int id)
        {
            User user = await repository.GetAsync(id);
            if (user is null)
                return null;
            return await Task.Factory.StartNew(() => mapper.Map<User, UserDTO>(user));
        }
        public IEnumerable<UserDTO> GetAll() => mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(repository.GetAll());
        public async Task AddOrUpdateAsync(UserDTO user)
        {
            if (await repository.GetAsync(user.UserId) != null)
                await repository.UpdateAsync(mapper.Map<UserDTO, User>(user));
            else
                await repository.AddAsync(mapper.Map<UserDTO, User>(user));
            await repository.SaveChangesAsync();
        }
        public async Task Remove(int id)
        {
            await repository.DeleteAsync(await repository.GetAsync(id));
            await repository.SaveChangesAsync();
        }
    }
}
