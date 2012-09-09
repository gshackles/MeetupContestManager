using System;

namespace MeetupContestManager.Core.Entities
{
    public class Entry
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public DateTime TimeEntered { get; set; }
    }
}