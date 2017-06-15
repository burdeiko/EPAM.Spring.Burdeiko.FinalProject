using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork.Mvc.Models
{
    public class DialogueViewModel
    {
        public IEnumerable<MessageViewModel> Messages { get; set; }
        public int WithPersonId { get; set; }
        public string WithPersonName { get; set; }
    }
}