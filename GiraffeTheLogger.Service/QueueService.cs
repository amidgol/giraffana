using System;
using System.Text;
using GiraffeTheLogger.ServiceContract;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace GiraffeTheLogger.Service {
    public class QueueService : IQueueService {
        private readonly IConnection connection;

        public QueueService (IConnection connection) {
            this.connection = connection;
        }
        public void Enqueue (object messageObj, string queueName) {
            using (var channel = connection.CreateModel ()) {
                channel.QueueDeclare (queue: queueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var message = JsonConvert.SerializeObject(messageObj);

                var body = Encoding.UTF8.GetBytes (message);

                var properties = channel.CreateBasicProperties ();
                properties.Persistent = true;

                channel.BasicPublish (exchange: "",
                    routingKey : queueName,
                    basicProperties : properties,
                    body : body);
                
                Console.WriteLine ("Sent {0}", message);
            }
        }
    }
}