using Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loggers
{
    public class LoggerFactory
    {

        /// <summary>
        /// Create a Search Criteria Logger
        /// </summary>
        /// <returns>A new Instance for the logger</returns>
        public static ILogger CreateLogger(string LogName ="")
        {

            ILogger logger;
            if (ConfigurationHelper.LoggingType.ToLower() == "database")
            {
                logger = new SerilogDBLogger(ConfigurationHelper.SerilogDBConnection, ConfigurationHelper.LogTableName);
            }
            else
            {
                string fileLoggerName = "Exceptions";
                if (LogName != string.Empty)
                {
                    fileLoggerName = LogName;
                }

                logger = new SerilogLogger(fileLoggerName);
            }
            return logger;

        }

    }
}
