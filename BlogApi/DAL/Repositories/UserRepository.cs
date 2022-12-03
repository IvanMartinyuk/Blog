using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(BlogContext context) : base(context)
        {

        }
        public override async Task<User> GetAsync(int id)
        {
            BlogContext con = context as BlogContext;
            return await con.Users.Where(x => x.UserId == id)
                        .Include(x => x.Articles)
                        .Include(x => x.ArticlesUsers)
                        .Include(x => x.Publishers)
                        .Include(x => x.Subscribers).FirstOrDefaultAsync();
        }
    }
}
