using DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    [Serializable]
    public class ArticleDTO
    {
        public int ArticleId { get; set; }
        public string Image { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public DateTime DateOfPublish { get; set; }
        public int UserId { get; set; }
        public User Publisher { get; set; }
        public List<News> ArticlesUsers { get; set; }
    }
}
