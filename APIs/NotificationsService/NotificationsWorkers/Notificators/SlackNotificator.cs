using Microsoft.Extensions.Configuration;
using NotificationsService.NotificationsWorkers.Interfaces;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotificationsService.NotificationsWorkers.Notificators
{
    public class SlackNotificator : ISlackNotificator
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public SlackNotificator(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }

        public async Task<string> SendSlackMessageToChannelAsync(string text)
        {
            string slackWebhook = _configuration.GetSection("ApplicationSettings:SlackWebhookUrl").Get<string>();
            var contentObject = new { text = text };
            var contentObjectJson = JsonSerializer.Serialize(contentObject);
            var content = new StringContent(contentObjectJson, Encoding.UTF8, "application/json");

            var result = await _client.PostAsync(slackWebhook, content);
            var resultContent = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Slack webhook failed: " + resultContent);
            }
            return resultContent;
        }
    }
}
