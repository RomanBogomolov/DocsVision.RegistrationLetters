using NLog;

namespace DocsVision.RegistrationLetters.Log
{
    public static class Logger
    {
        public static NLog.Logger ServiceLog = LogManager.GetCurrentClassLogger();
    }
}
