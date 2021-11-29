using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;
namespace ShishaProject.Common.ExceptionHandling
{
    public class ShishaLogger : IShishaLogger
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public ShishaLogger()
        {
        }

        public void Information(string message)
        {
            Logger.Info(message);
        }

        public void Warning(string message)
        {
            Logger.Warn(message);
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }
    }
}

public class ErrorDetails
{
    public int StatusCode { get; set; }

    public string Message { get; set; }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
