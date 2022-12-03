using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class News
    {
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
