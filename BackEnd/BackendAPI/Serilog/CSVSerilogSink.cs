using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Configuration;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

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
            Directory.CreateDirectory(path);
        }

        public void Emit(LogEvent logEvent)
        {
            if (LogEventLevel.Error < logEvent.Level)
                return;

            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            string filePath= Path.Combine(path, currentDate + ".csv");

            var dataToWrite = new List<LogFileStruct>
            {
                new LogFileStruct()
                {
                    Message = logEvent.RenderMessage(_formatProvider),
                    Time = DateTimeOffset.Now.ToString(),
                    LogLevel = logEvent.Level
                }
            };


            //Append to existing file
            if (File.Exists(filePath))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };

                using (var stream = File.Open(filePath, FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(
                        dataToWrite
                    );
                }
                return;
            }

            using (var stream = File.Open(filePath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(dataToWrite);
            }
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
