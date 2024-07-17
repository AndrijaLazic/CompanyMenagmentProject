using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;

namespace BackendAPI.Serilog
{
    public class CSVSerilogSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly string path;
        private readonly string rollingInterval;
        private readonly LogLevel level;

        public CSVSerilogSink(IFormatProvider formatProvider, string path, string rollingInterval, LogLevel restrictedToMinimumLevel)
        {
            _formatProvider = formatProvider;
            this.path = path;
            this.rollingInterval = rollingInterval;
            this.level = restrictedToMinimumLevel;
        }

        public void Emit(LogEvent logEvent)
        {
            if (LogEventLevel.Error < logEvent.Level)
                return;
            var message = logEvent.RenderMessage(_formatProvider);
            Console.WriteLine(DateTimeOffset.Now.ToString() + path + message);
        }
    }

    public static class SCVSinkExstension
    {
        public static LoggerConfiguration CSVSerilogSink(
                  this LoggerSinkConfiguration loggerConfiguration,
                  string path,
                  string rollingInterval,
                  LogLevel restrictedToMinimumLevel,
                  IFormatProvider formatProvider = null
                  )
        {
            return loggerConfiguration.Sink(new CSVSerilogSink(formatProvider, path, rollingInterval, restrictedToMinimumLevel));
        }
    }
}
