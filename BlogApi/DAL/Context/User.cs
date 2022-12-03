using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Text;

namespace DAL.Context
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        
        public List<Article> Articles { get; set; }
        public List<Subscribers> Subscribers { get; set; }
        public List<Subscribers> Publishers { get; set; }

        public List<News> ArticlesUsers { get; set; }
    }
}
