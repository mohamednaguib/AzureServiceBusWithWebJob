using Microsoft.Azure.Jobs;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusWebJob
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "queue_name";
            var _servicesBusConn = "service_bus_connection_string";
            QueueClient client = QueueClient.CreateFromConnectionString(_servicesBusConn, queueName);
            client.Receive();
            while (true)
            {
                var msg = client.Receive();
                if (msg != null)
                {
                        string fn = msg.Properties["first_name"].ToString();
                        string ln = msg.Properties["last_name"].ToString();
                        string name = fn + " " + ln;
                        Console.WriteLine("Name = " + name);
                        Trace.TraceInformation("Name = " + name);
                        msg.Complete();
                }
            }
        }
    }
}
