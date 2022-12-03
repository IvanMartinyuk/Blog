using DAL.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class SubscribersDTO
    {
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public int PublisherId { get; set; }
        public User Publisher { get; set; }
    }
}
