using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GiraffeTheLogger.DequeuerConsole {
    class Program {
        static void Main (string[] args) {

            string hostName = args[0];
            string queueName = args[1];

            var factory = new ConnectionFactory () { HostName = hostName };

            using (var connection = factory.CreateConnection ())
            using (var channel = connection.CreateModel ()) {

                channel.BasicQos (prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine ($" *** Listening to queue: \"{queueName}\" on host: \"{hostName}\"");

                var consumer = new EventingBasicConsumer (channel);

                consumer.Received += (model, ea) => {
                    
                    var body = ea.Body;
                    
                    var message = Encoding.UTF8.GetString (body);

                    Console.WriteLine ($" Received {message}");

                    channel.BasicAck (deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume (queue: queueName,
                    autoAck: false,
                    consumer: consumer);

                Console.ReadLine ();
            }
        }
    }
}