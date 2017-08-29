using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.Api.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            _urlHelper = new UrlHelper(request);
        }

        public IEnumerable<MessagesReturnModel> Create(IEnumerable<Message> messages)
        {
            return messages.Select(mes => new MessagesReturnModel
            {
                    Url = _urlHelper.Link("GetMessageById", new {userId = mes.Sender.Id, messageId = mes.Id}),
                    Date = mes.Date,
                    Theme = mes.Theme,
                    Text = mes.Text
                })
                .ToList();
        }

        public MessagesInfoReturnModel Create(Message mes)
        {
            return new MessagesInfoReturnModel
            {
                MessageId = mes.Id,
                Theme = mes.Theme,
                Date = mes.Date,
                Text = mes.Text,
                SenderId = mes.Sender.Id,
                Fullname = $"{mes.Sender.Name} {mes.Sender.SecondName}",
                SenderEmail = mes.Sender.Email
            };
        }
    }

    public class MessagesReturnModel
    {
        public string Url { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }

    public class MessagesInfoReturnModel
    {
        public Guid MessageId { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid SenderId { get; set; }
        public string Fullname { get; set; }
        public string SenderEmail { get; set; }
        public string SenderDepartment { get; set; }
        public string SenderPosition { get; set; }
        
    }
}