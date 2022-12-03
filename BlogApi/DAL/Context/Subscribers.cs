using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Context
{
    public class Subscribers
    {
        public int SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public int PublisherId { get; set; }
        public User Publisher { get; set; }
    }
}
