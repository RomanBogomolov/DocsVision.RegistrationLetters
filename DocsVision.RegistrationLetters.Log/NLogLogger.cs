using System;
using System.IO;
using System.Runtime.CompilerServices;
using NLog;

namespace DocsVision.RegistrationLetters.Log
{
    public class NLogLogger : ILogger
    {
        private static NLog.Logger GetInnerLogger(string sourceFilePath)
        {
            var logger = sourceFilePath == null ? LogManager.GetCurrentClassLogger() : LogManager.GetLogger(Path.GetFileName(sourceFilePath));
            return logger;
        }

        public void Info(string message, [CallerFilePath] string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Info(message);
        }

        public void Info(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).InfoException(message, exc);
        }

        public void Debug(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Debug(message);
        }

        public void Debug(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).DebugException(message, exc);
        }

        public void Warn(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Warn(message);
        }

        public void Warn(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).WarnException(message, exc);
        }

        public void Error(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Error(message);
        }

        public void Error(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).ErrorException(message, exc);
        }

        public void Fatal(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Fatal(message);
        }

        public void Fatal(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).FatalException(message, exc);
        }

        public void Trace(string message, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).Trace(message);
        }

        public void Trace(string message, Exception exc, [CallerFilePath]string sourceFilePath = null)
        {
            GetInnerLogger(sourceFilePath).TraceException(message, exc);
        }
    }
}