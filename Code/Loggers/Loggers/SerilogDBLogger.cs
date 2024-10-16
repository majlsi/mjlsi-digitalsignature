using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System;
using System.IO;

namespace Loggers
{
    public class SerilogDBLogger : ILogger
    {
        private readonly Logger _logger;
        private readonly string _connectString;
        private readonly string _tableName;
        public SerilogDBLogger(string connectString, string tableName)
        {
            _connectString = connectString;
            _tableName = tableName;
            _logger = CreateLogger();          
        }
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception exception)
        {
            _logger.Error(exception, string.Empty);
        }

        public void Error(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }


        private Logger CreateLogger()
        {
           
           
            Logger logger = new LoggerConfiguration().WriteTo.MSSqlServer(connectionString: _connectString, sinkOptions: new MSSqlServerSinkOptions { TableName = _tableName, AutoCreateSqlTable = true } )
                .CreateLogger();
            return logger;
        }
    }
}
