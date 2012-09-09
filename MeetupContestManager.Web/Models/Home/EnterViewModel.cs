using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MeetupContestManager.Web.Models.Home
{
    public class EnterViewModel
    {
        public string MeetingTitle { get; set; }
        public string MeetingId { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string Email { get; set; }
    }
}