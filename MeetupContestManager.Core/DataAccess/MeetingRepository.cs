using System;
using System.Collections.Generic;
using System.Linq;
using MeetupContestManager.Core.Entities;

namespace MeetupContestManager.Core.DataAccess
{
    public interface IMeetingRepository
    {
        void CreateMeeting(string name, DateTime date);
        Meeting GetById(string id);
        bool AddEntry(string meetingId, string name, string email);
        IList<Meeting> ListMeetings();
        Entry SelectNewWinner(string meetingId);
    }

    public class MeetingRepository : RepositoryBase, IMeetingRepository
    {
        public MeetingRepository(string connectionStringName)
            : base(connectionStringName)
        {
        }

        public void CreateMeeting(string name, DateTime date)
        {
            using (var session = GetSession())
            {
                session.Store(new Meeting
                                  {
                                      Name = name,
                                      Date = date,
                                      Entries = new List<Entry>()
                                  });
            }
        }

        public Meeting GetById(string id)
        {
            using (var session = GetSession())
            {
                return session
                        .Query<Meeting>()
                        .Where(meeting => meeting.Id == id)
                        .SingleOrDefault();
            }
        }

        public bool AddEntry(string meetingId, string name, string email)
        {
            using (var session = GetSession())
            {
                var meeting =
                    session
                        .Query<Meeting>()
                        .Where(m => m.Id == meetingId)
                        .SingleOrDefault();

                if (meeting == null || meeting.Entries.Count(entry => entry.EmailAddress.ToLowerInvariant() == email.ToLowerInvariant()) > 0)
                    return false;

                meeting.Entries.Add(new Entry
                                        {
                                            EmailAddress = email,
                                            Name = name,
                                            TimeEntered = DateTime.UtcNow
                                        });

                session.Store(meeting);
                session.SaveChanges();

                return true;
            }
        }

        public IList<Meeting> ListMeetings()
        {
            using (var session = GetSession())
            {
                return session
                    .Query<Meeting>()
                    .OrderByDescending(meeting => meeting.Date)
                    .ToList();
            }
        }

        public Entry SelectNewWinner(string meetingId)
        {
            var meeting = GetById(meetingId);
            var unselectedEntries = meeting.Entries.Where(entry => !entry.Selected);

            if (unselectedEntries.Count() == 0)
                return null;

            var winner = unselectedEntries.OrderBy(entry => Guid.NewGuid()).First();

            using (var session = GetSession())
            {
                winner.Selected = true;

                session.Store(meeting);
                session.SaveChanges();
            }

            return winner;
        }
    }
}
