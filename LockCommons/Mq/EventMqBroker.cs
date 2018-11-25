using LockCommons.Models;
using LockCommons.Models.Proto;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LockCommons.Mq
{
    public class EventMqBroker
    {
        public static string queueName = "event_que";
        public static void Queue(object obj)
        {
            string serailizedObject = JsonConvert.SerializeObject(obj);
            Queue(Encoding.UTF8.GetBytes(serailizedObject));
        }

        //@param src just for logging
        public static void Queue(string src, byte[] data)
        {
            try
            {

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: properties,
                                         body: data);
                    Console.WriteLine($"from:{src} {data.Length} bytes Sent to {queueName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error while Queue msg { BitConverter.ToString(data)} err {e}");
            }
        }

        public static void InitializeConsumer(DeliveryEventHandler lockEventDelegate, ushort nmrOfConcurrentWorkers)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //prefetchCount number of concurrent wokers
            channel.BasicQos(prefetchSize: 0, prefetchCount: nmrOfConcurrentWorkers, global: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async
                (model, ea) =>
            {
                try
                {
                    bool success = await lockEventDelegate.ProcessEvent(ea.Body);
                    if (success)
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);//if an error occurs queue item is not lost

                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error occured while processing msg {e}");
                }
            };

            channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);

        }
    }
}
