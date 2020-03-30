using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GiraffeTheLogger.Service;
using GiraffeTheLogger.ServiceContract;
using GiraffeTheLogger.WebApi.Dtos.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace GiraffeTheLogger.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<QueueOptions>(options => Configuration.GetSection("QueueSettings").Bind(options));

            string hostName = Configuration.GetSection("QueueSettings")["HostName"];

            ConnectionFactory factory = new ConnectionFactory() { HostName = hostName };
            IConnection connection = factory.CreateConnection();
            services.AddSingleton(connection);

            services.AddScoped<IQueueService, QueueService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
