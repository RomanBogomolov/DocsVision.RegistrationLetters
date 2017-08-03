using System;
using System.Diagnostics;
using System.Reflection;
using NLog;

namespace DocsVision.RegistrationLetters.Log
{
    public class LogWrapper : IDisposable
    {
        private readonly NLog.Logger _logger;
        private readonly MethodBase _methodBase;

        public LogWrapper()
        {
            _logger = LogManager.GetCurrentClassLogger();
            _methodBase = new StackTrace().GetFrame(1).GetMethod();
            if (_methodBase.ReflectedType != null)
                _logger.Info($"Start of method {_methodBase.ReflectedType.Name}.{_methodBase.Name}");
        }

        public void Dispose()
        {
            if (_methodBase.ReflectedType != null)
                _logger.Info($"Finish of method {_methodBase.ReflectedType.Name}.{_methodBase.Name}");
        }

        public void Trace(string message)
        {
            _logger.Trace(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string exeptionMessage)
        {
            if (_methodBase.ReflectedType != null)
                _logger.Error(
                    $"Method {_methodBase.ReflectedType.Name}.{_methodBase.Name}|Error message: {exeptionMessage}");
        }

        public void Fatal(string exeptionMessage)
        {
            if (_methodBase.ReflectedType != null)
                _logger.Fatal(
                    $"Method {_methodBase.ReflectedType.Name}.{_methodBase.Name}|Error message: {exeptionMessage}");
        }
    }
}