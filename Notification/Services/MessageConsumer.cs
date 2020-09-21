using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Notification.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notification.Services
{
    public class MessageConsumer : BackgroundService, IMessageConsumer
    {
        public static string FullAccessConnectionString { get; set; } = "Endpoint=sb://healthcare.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=Rv6QKxZY/OgLJQdBAwOeX3ZzhJasctahGvGVLWlSgPM=>";
        public static string NotificationHubName { get; set; } = "HealthCare/Healthcare-Hub";
        private const string FcmSampleNotificationContent = "{\"data\":{\"message\":\"Notification Hub test notification from SDK sample\"}}";
        private const string deviceKey = "AAAA46UfCnA:APA91bFnjKci00kEh07MlEzoWEqEtBvCen0bBS0RS75AulPr-vzLGWzS8S-wY--H7w_KldIO38494P6vPZXL2kCxAQqjF8rD6ni9ZxNsFWEubNd3Eoui7boUzFjD-4RODIjH4brFzU4r";
        private readonly IQueueClient _queueClient;
        List<Device> getDevice = new List<Device>();
        public MessageConsumer(IQueueClient queueClient){
            _queueClient = queueClient;    
        }

        
        public List<Device> Consume () 
        {
            
          

            _queueClient.RegisterMessageHandler((message, token)  =>
             {
                 getDevice.Add(
                   JsonConvert.DeserializeObject<Device>(Encoding.UTF8.GetString(message.Body)));
                 Console.WriteLine($"New device with id {Encoding.UTF8.GetString(message.Body)}");

                 return _queueClient.CompleteAsync(message.SystemProperties.LockToken);
             },
             new MessageHandlerOptions(args => Task.CompletedTask)
             { 
                        AutoComplete = false,
              MaxConcurrentCalls = 1
             });



            var nhClient = NotificationHubClient.CreateClientFromConnectionString(FullAccessConnectionString, NotificationHubName);
             var outcomeFcmByDeviceId =  nhClient.SendDirectNotificationAsync(CreateFcmNotification(), deviceKey);
            return getDevice;
            
        }
     
        private static FcmNotification CreateFcmNotification()
        {

            return new FcmNotification(FcmSampleNotificationContent);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {



            _queueClient.RegisterMessageHandler((message, token) =>
            {
                getDevice.Add(
                  JsonConvert.DeserializeObject<Device>(Encoding.UTF8.GetString(message.Body)));
                Console.WriteLine($"New device with id {Encoding.UTF8.GetString(message.Body)}");

                return _queueClient.CompleteAsync(message.SystemProperties.LockToken);
            },
             new MessageHandlerOptions(args => Task.CompletedTask)
             {
                 AutoComplete = false,
                 MaxConcurrentCalls = 1
             });
            return Task.CompletedTask;
        }
    }
}
