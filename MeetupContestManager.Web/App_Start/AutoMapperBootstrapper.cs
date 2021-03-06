using AutoMapper;

namespace MeetupContestManager.Web
{
    public class AutoMapperBootstrapper
    {
        public static void CreateMaps()
        {
            Mapper.CreateMap<Core.Entities.Meeting, Models.Home.Meeting>();
            Mapper.CreateMap<Core.Entities.Entry, Models.Home.Entry>();

            Mapper.CreateMap<Core.Entities.Meeting, Models.Admin.Meeting>();
            Mapper.CreateMap<Core.Entities.Entry, Models.Admin.Winner>();
        }
    }
}