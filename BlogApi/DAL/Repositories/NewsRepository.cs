using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class NewsRepository : GenericRepository<News>
    {
        public NewsRepository(BlogContext context) : base(context)
        {
        }
    }
}
