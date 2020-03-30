using System;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace GiraffeTheLogger.DequeuerConsole {
    public static class ServiceCollectionExtensions {
        public static IServiceCollection ConfigureElastic (this IServiceCollection services,
            string uri, string index) {

            var node = new Uri (uri);

            ConnectionSettings connectionSettings = new ConnectionSettings (node)
                .DefaultIndex (index.ToLower ());

            services.AddSingleton<IElasticClient> (new ElasticClient (connectionSettings));

            return services;
        }
    }
}