using System;
using System.IO;
using System.Text;
using AutoMapper;
using GiraffeTheLogger.DbService;
using GiraffeTheLogger.DbServiceContract;
using GiraffeTheLogger.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace GiraffeTheLogger.DequeuerConsole {
    class Program {
        static void Main (string[] args) {

            if (args.Length != 2) {
                System.Console.WriteLine ("Add hostName and queueName arguments:\n dotnet run <hostName> <queueName>");

                Environment.Exit (-1);
            }

            var builder = new ConfigurationBuilder ()
                .SetBasePath (Directory
                    .GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json");

            IConfigurationRoot config = builder.Build ();

            ServiceProvider serviceProvider = GetServiceProvider (config);
            var logService = serviceProvider.GetService<ILogService> ();

            string hostName = args[0];
            string queueName = args[1];

            var factory = new ConnectionFactory () { HostName = hostName };

            using (var connection = factory.CreateConnection ())
            using (var channel = connection.CreateModel ()) {

                channel.BasicQos (prefetchSize: 0, prefetchCount: 1, global: false);

                Console.WriteLine ($" *** Listening to queue: \"{queueName}\" on host: \"{hostName}\"");

                var consumer = new EventingBasicConsumer (channel);

                consumer.Received += async (model, ea) => {

                    var body = ea.Body;

                    var message = Encoding.UTF8.GetString (body);

                    Console.WriteLine ($" Received {message}");

                    var messageDto = JsonConvert.DeserializeObject<MessageDto>(message);

                    await logService.CreateAsync(messageDto);

                    channel.BasicAck (deliveryTag: ea.DeliveryTag, multiple: false);
                };

                channel.BasicConsume (queue: queueName,
                    autoAck: false,
                    consumer: consumer);

                Console.ReadLine ();
            }
        }

        private static ServiceProvider GetServiceProvider (IConfiguration configuration) {
            var services = ConfigureServices (configuration);
            return services.BuildServiceProvider ();
        }
        private static IServiceCollection ConfigureServices (IConfiguration configuration) {
            
            var elasticUrl = configuration["ElasticUrl"];
            var elasticDefaultIndex = configuration["ElasticDefaultIndex"];

            IServiceCollection services = new ServiceCollection ();

            services.AddAutoMapper ();

            services.AddScoped<ILogService, LogService> ();

            services.ConfigureElastic(elasticUrl, elasticDefaultIndex);

            return services;

        }
    }
}