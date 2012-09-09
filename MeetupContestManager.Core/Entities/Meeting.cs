using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetupContestManager.Core.Entities
{
    public class Meeting
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public IList<Entry> Entries { get; set; } 
    }
}
