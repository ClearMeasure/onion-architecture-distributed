using System;
using System.IO;
using log4net.Config;

namespace Infrastructure
{
    public class LogConfig
    {
        public static void InitializeLog4Net()
        {
            string configPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Log4Net.config");
            var fileInfo = new FileInfo(configPath);
            XmlConfigurator.ConfigureAndWatch(fileInfo);
        }
    }
}