using System;
using System.Configuration;
using System.IO;
using System.Threading;
using Microsoft.Azure.Jobs;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace ServiceBus
{
    class Program
    {
        private const string QueueNamePrefix = "queue_name";

        private static string _servicesBusConnectionString;

        private static NamespaceManager _namespaceManager;

        public static void SBQueueListener(
            [ServiceBusTrigger(QueueNamePrefix)] BrokeredMessage msg)
        {
            string fn = msg.Properties["first_name"].ToString();
            string ln = msg.Properties["last_name"].ToString();
            string fullName = fn + ln;
        }
        public static void Main()
        {
            _servicesBusConnectionString = ConfigurationManager.ConnectionStrings["ServiceBus"].ConnectionString;
            _namespaceManager = NamespaceManager.CreateFromConnectionString(_servicesBusConnectionString);
            JobHostConfiguration config = new JobHostConfiguration()
            {
                ServiceBusConnectionString = _servicesBusConnectionString,
            };
            JobHost host = new JobHost(config);
            host.RunAndBlock();
        }

    }
}
