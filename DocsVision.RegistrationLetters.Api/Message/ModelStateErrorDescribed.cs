using System.Web.Http.ModelBinding;

namespace DocsVision.RegistrationLetters.Api.Message
{
    public class ModelStateErrorDescribed
    {
        private static string InvEmails => "invalid_emails";
        private static string  InvMessage => "invalid_message";
        private static string InvMessageText => "Сообщение недоступно.";


        public static ModelStateDictionary InvalidMessage(ModelStateDictionary model)
        {
            model.AddModelError(InvMessage, InvMessageText);
            return model;
        }

        public static ModelStateDictionary InvalidEmails(ModelStateDictionary model, string[] invalidEmails)
        {
            model.AddModelError(InvEmails, string.Join(",", invalidEmails));
            return model;
        }
    }
}