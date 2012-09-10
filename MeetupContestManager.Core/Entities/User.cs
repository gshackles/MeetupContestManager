using System;
using System.Collections.Generic;
using System.Linq;

namespace MeetupContestManager.Core.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
