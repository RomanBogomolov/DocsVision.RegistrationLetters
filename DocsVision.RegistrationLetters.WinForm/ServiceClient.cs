using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DocsVision.RegistrationLetters.Api.Models;
using DocsVision.RegistrationLetters.Model;

namespace DocsVision.RegistrationLetters.WinForm
{
    public class ServiceClient
    {
        private readonly Guid _currentUserId;
        private readonly HttpClient _client = new HttpClient();

        public ServiceClient(string connectionString, Guid currentUserId)
        {
            _currentUserId = currentUserId;
            _client.BaseAddress = new Uri(connectionString);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public ServiceClient(string connectionString)
        {
            _client.BaseAddress = new Uri(connectionString);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public User GetUserByEmail(string email)
        {
            var response = _client.GetAsync($"user?email={email}").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<User>().Result;
                return result;
            }

            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public MessagesReturnModel[] GetUserMessages()
        {
            var response = _client.GetAsync($"message/user/{_currentUserId}").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<MessagesReturnModel[]>().Result;
                return result;
            }

            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public MessagesInfoReturnModel GetMessageInfo(string url)
        {
            var response = _client.GetAsync($"{url}").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsAsync<MessagesInfoReturnModel>().Result;
                return result;
            }

            throw new ServiceException("Error: {0}", response.StatusCode);
        }

        public void SendMessageToUsers(CompositeMessageEmails obj)
        {
            var response = _client.PostAsJsonAsync("message/send", obj).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new ServiceException("Error: {0}", response.StatusCode);
            }
        }
    }
}