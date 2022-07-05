using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MDM01_VNVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StackExchange.Redis;

namespace MDM01_VNVC
{
    public class Startup
    {

        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();

            services.Configure<DocumentDatabaseSettings>(Configuration.GetSection("DocumentDatabase"));
            services.Configure<KeyValueDatabaseSettings>(Configuration.GetSection("KeyValueDatabase"));
            services.Configure<GraphDatabaseSettings>(Configuration.GetSection("GraphDatabase"));
            services.AddSingleton<IConfiguration>(Configuration);
            var redis = ConnectionMultiplexer.Connect("localhost");
            services.AddScoped(s => redis.GetDatabase());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
