using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace LogService
{
    public class LoggerService : ILoggerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void addLog(string message, LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.FATAL:
                    logger.Fatal(message);
                    break;
                case LogLevel.ERROR:
                    logger.Error(message);
                    break;
                case LogLevel.WARN:
                    logger.Warn(message);
                    break;
                case LogLevel.INFO:
                    logger.Info(message);
                    break;
                case LogLevel.DEBUG:
                    logger.Debug(message);
                    break;
            }
        }
    }
}
