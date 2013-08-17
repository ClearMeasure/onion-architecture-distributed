using NServiceBus;

namespace CreditEngineHost
{
    public class BusContext
    {
        private static IBus _bus;
        private static bool _startupComplete;

        private static readonly object _locker =
            new object();

        public static IBus GetBus()
        {
            EnsureStartup();
            return _bus;
        }

        public static void EnsureStartup()
        {
            if (!_startupComplete)
            {
                lock (_locker)
                {
                    if (!_startupComplete)
                    {
                        PerformStartup();
                        _startupComplete = true;
                    }
                }
            }
        }

        private static void PerformStartup()
        {
            IStartableBus startableBus = Configure.WithWeb()
                .Log4Net()
                .DefaultBuilder()
                .XmlSerializer()
                .MsmqTransport()
                .PurgeOnStartup(false)
                .UnicastBus()
                .ImpersonateSender(false)
                .CreateBus();
            IBus bus = startableBus.Start();
            _bus = bus;
        }
    }
}