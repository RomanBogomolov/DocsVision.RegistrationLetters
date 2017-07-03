using System;

namespace DocsVision.RegistrationLetters.WinForm
{
    internal class ServiceException : Exception
    {
        public ServiceException(string message, params object[] args) : base(string.Format(message, args))
        {

        }
    }
}