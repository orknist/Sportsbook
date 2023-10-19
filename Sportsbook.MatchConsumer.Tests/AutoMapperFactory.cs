using AutoMapper;

namespace Sportsbook.TestBase
{
    public class AutoMapperFactory
    {
        private static IMapper? _instance = null;
        private static readonly object _padlock = new();

        protected AutoMapperFactory() { }

        public static IMapper Instance
        {
            get
            {
                lock (_padlock)
                {
                    _instance ??= new MapperConfiguration(cfg =>
                        cfg.AddMaps("Sportsbook.API.QueueService", "Sportsbook.MatchConsumer.Business")).CreateMapper();
                    return _instance;
                }
            }
        }
    }
}
