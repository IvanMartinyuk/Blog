using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ArticleRepository : GenericRepository<Article>
    {
        public ArticleRepository(BlogContext context) : base(context)
        {

        }
        public override async Task<Article> GetAsync(int id)
        {
            BlogContext con = context as BlogContext;
            return await con.Articles.Where(x => x.ArticleId == id)
                        .Include(x => x.Publisher)
                        .Include(x => x.ArticlesUsers)
                        .FirstOrDefaultAsync();
        }
    }
}
