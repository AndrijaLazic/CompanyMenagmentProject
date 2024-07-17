using Serilog.Events;

namespace BackendAPI.Serilog
{
    public class LogFileStruct
    {
        public string Time {  get; set; }
        public string Message { get; set; }
        public LogEventLevel LogLevel { get; set; }
    }
}
