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
    public class MqBroker
    {
        public string queueName = "";


        public MqBroker(string qeuName)
        {
            this.queueName = qeuName;
        }

        //@param src just for logging
        public void Queue(string src, byte[] msg)
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
                                         body: msg);
                    Console.WriteLine($"from:{src} {msg.Length} bytes Sent to {queueName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error while Queue msg { BitConverter.ToString(msg)} err {e}");
            }
        }

        public void DelayedQueu(string src, byte[] msg, int delayMs)
        {
            try
            {

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    IDictionary<string, object> args = new Dictionary<string, object>
                    {
                        {"x-delayed-type", "direct"}
                    };
                    channel.ExchangeDeclare($"{queueName}Exchange", "x-delayed-message", true, false, args);


                    var queue = channel.QueueDeclare(queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                    channel.QueueBind(queue, $"{queueName}Exchange", $"{queueName}RoutKey");


                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    properties.Headers = new Dictionary<string, object>
                    {
                        {"x-delay", delayMs}
                    };

                    channel.BasicPublish($"{queueName}Exchange", $"{queueName}RoutKey", properties, msg);

                    Console.WriteLine($"from:{src} {msg.Length} bytes Sent to {queueName}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error while Queue msg { BitConverter.ToString(msg)} err {e}");
            }



        }

        public void InitializeConsumer(IEventHandler lockEventDelegate, ushort nmrOfConcurrentWorkers)
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
