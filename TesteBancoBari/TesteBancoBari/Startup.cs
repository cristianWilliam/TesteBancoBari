using BusinessLayer.Services;
using InfraLayer.CronHelpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TesteBancoBari.Abstractions;
using TesteBancoBari.InfraLayer.Abstractions;
using TesteBancoBari.InfraLayer.Implementations;
using TesteBancoBari.Services;

namespace TesteBancoBari
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
            services.AddScoped<IGetMessagesService, GetMessagesService>();
            services.AddSingleton<IAppIdentity, AppIdentity>();
            services.AddSingleton(typeof(IQueueService<>), typeof(SQSService<>));

            services.AddCronJob<HelloWorldCronService>(opt =>
            {
                opt.TimeZoneInfo = TimeZoneInfo.Local;
                opt.CronExpression = "*/5 * * * * *";
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteBancoBari", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteBancoBari v1"));
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
