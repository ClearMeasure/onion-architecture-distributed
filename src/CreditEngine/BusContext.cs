using NServiceBus;

namespace CreditEngine
{
    public class BusContext
    {
        private static IBus _bus;
        private static bool _startupComplete;

        private static readonly object _locker =
            new object();

        public static IBus GetBus()
        {
            return _bus;
        }

        public static void EnsureWebStartup()
        {
            if (!_startupComplete)
            {
                lock (_locker)
                {
                    if (!_startupComplete)
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
                        _startupComplete = true;
                    }
                }
            }
        }

        public static void SetOutsideBus(IBus bus)
        {
            _bus = bus;
        }
    }
}