using Az203kz.ServicebusPublish.ViewModels;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Az203kz.ServicebusPublish.Services
{
    public class PublisherService : IPublisherService
    {
        private const string ServiceBusPrimaryConnectionString = "Endpoint=sb://kz-servicebusns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=n9qNolgtOoiJMcObIrrUs8mcwVWHCheEAaHzVk78Ric=";
        private const string TopicName = "restaurant-orders";
        private static ITopicClient _topicClient;
        private readonly IConfiguration _configuration;

        protected ServiceBusConfiguration Config { get; }
        public PublisherService(IOptions<ServiceBusConfiguration> config)
        {
            this.Config = config.Value;
            _topicClient = new TopicClient(Config.ConnectionString,Config.TopicName);
        }

        public async Task Send(RestaurantOrderViewModel viewModel)
        {
            var message = ToMessage(viewModel);

            try
            {
                await _topicClient.SendAsync(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
            finally
            {
                await _topicClient.CloseAsync();
            }
        }

        private static Message ToMessage(RestaurantOrderViewModel model)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));

            var message = new Message
            {
                Body = body,
                ContentType = "text/plain",
            };

            return message;
        }
    }

}
