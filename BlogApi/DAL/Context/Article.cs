using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Context
{
    public class Article
    {
        [Key]
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
