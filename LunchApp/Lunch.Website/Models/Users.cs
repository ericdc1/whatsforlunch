using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lunch.Website.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Guid { get; set; }
        public bool SendMorningEmailFlg { get; set; }
        public bool SendVotingIsOverEmailFlg { get; set; }
        public bool SendWhereWeAreGoingEmailFlg { get; set; }
    }
}