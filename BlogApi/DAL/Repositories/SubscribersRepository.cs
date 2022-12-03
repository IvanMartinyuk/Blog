using DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class SubscribersRepository : GenericRepository<Subscribers>
    {
        public SubscribersRepository(BlogContext context) : base(context)
        {
        }
    }
}
