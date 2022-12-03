using DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class UserDTO
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
