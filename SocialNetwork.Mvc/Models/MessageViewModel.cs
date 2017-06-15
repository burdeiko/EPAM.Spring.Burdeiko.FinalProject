using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Mvc.Models
{
    public class MessageViewModel
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime Time { get; set; }
        public string SenderName { get; set; }
        public string Message { get; set; }
    }
}