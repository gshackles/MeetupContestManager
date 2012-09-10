using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetupContestManager.Web.Models.Admin
{
    public class Meeting
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}