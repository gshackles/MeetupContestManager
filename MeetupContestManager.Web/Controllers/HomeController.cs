using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MeetupContestManager.Core.DataAccess;
using MeetupContestManager.Web.Models.Home;

namespace MeetupContestManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private IMeetingRepository _meetingRepository;

        public HomeController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public ViewResult Index()
        {
            var meetings = _meetingRepository.ListMeetings();
            var viewModel = Mapper.Map<IList<Meeting>>(meetings);

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Enter(string id)
        {
            var meeting = _meetingRepository.GetById(id);

            if (meeting == null)
                return new HttpNotFoundResult();

            var viewModel = new EnterViewModel
                                {
                                    MeetingId = meeting.Id,
                                    MeetingTitle = meeting.Name
                                };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Enter(EnterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_meetingRepository.AddEntry(model.MeetingId, model.Name, model.Email))
                {
                    ModelState.AddModelError("", "There was a problem adding you to the giveaway.");

                    return View(model);
                }

                return View("Thanks");
            }

            return View(model);
        }
    }
}
