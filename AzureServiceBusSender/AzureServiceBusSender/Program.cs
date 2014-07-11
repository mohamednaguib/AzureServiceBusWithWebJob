using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureServiceBusSender
{
    public class Program
    {
        public static string SericeBusConnectionString = "service_bus_connection_string";
        public static void Main(string[] args)
        {
            string queueName = "queue_name";
            var namespaceManager = NamespaceManager.CreateFromConnectionString(SericeBusConnectionString);
            if (!namespaceManager.QueueExists(queueName))
            {
                namespaceManager.CreateQueue(queueName);
            }
            QueueClient client = QueueClient.CreateFromConnectionString(SericeBusConnectionString, queueName);
            var message = new BrokeredMessage();
            message.Properties["first_name"] = "Mohamed";
            message.Properties["last_name"] = "Naguib";
            client.Send(message);
        }
    }
}
