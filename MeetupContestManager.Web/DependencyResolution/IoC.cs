using MeetupContestManager.Core.DataAccess;
using StructureMap;
namespace MeetupContestManager.Web {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                    });

                            const string ravenConnectionStringName = "RavenDB";

                            x
                                .For<IMeetingRepository>()
                                .Use<MeetingRepository>()
                                .Ctor<string>("connectionStringName").Is(ravenConnectionStringName);
                        });
            return ObjectFactory.Container;
        }
    }
}