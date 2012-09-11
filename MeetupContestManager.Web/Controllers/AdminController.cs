using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MeetupContestManager.Core.DataAccess;
using MeetupContestManager.Web.Models.Admin;

namespace MeetupContestManager.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IMeetingRepository _meetingRepository;

        public AdminController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        public ActionResult Index()
        {
            var meetings = _meetingRepository.ListMeetings();
            var viewModel = Mapper.Map<IList<Meeting>>(meetings);

            return View(viewModel);
        }

        public ActionResult SelectWinners(string id)
        {
            var meeting = _meetingRepository.GetById(id);

            if (meeting == null)
                return new HttpNotFoundResult();

            var viewModel = new SelectWinnersViewModel
            {
                MeetingId = meeting.Id,
                MeetingTitle = meeting.Name
            };

            return View(viewModel);
        }

        public JsonResult SelectNewWinner(string id)
        {
            var winner = _meetingRepository.SelectNewWinner(id);
            var winnerViewModel = Mapper.Map<Winner>(winner);

            return Json(winnerViewModel, JsonRequestBehavior.AllowGet);
        }

        public EmptyResult ResetContest(string id)
        {
            _meetingRepository.ResetContest(id);

            return new EmptyResult();
        }
    }
}
