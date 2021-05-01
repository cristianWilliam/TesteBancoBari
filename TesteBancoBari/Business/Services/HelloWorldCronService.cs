using InfraLayer.CronHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TesteBancoBari.BusinessLayer.Models;
using TesteBancoBari.InfraLayer.Abstractions;

namespace BusinessLayer.Services
{
    public class HelloWorldCronService : CronJobService
    {
        private readonly ILogger<HelloWorldCronService> logger;
        private readonly IQueueService<HelloWorldModel> queueService;
        private readonly IAppIdentity appIdentity;

        public HelloWorldCronService(
            IScheduleConfig<HelloWorldCronService> config,
            ILogger<HelloWorldCronService> logger,
            IQueueService<HelloWorldModel> queueService,
            IAppIdentity appIdentity
        ) :base(config.CronExpression, config.TimeZoneInfo)
        {
            this.logger = logger;
            this.queueService = queueService;
            this.appIdentity = appIdentity;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            await queueService.SendItem(new HelloWorldModel("Hello World", appIdentity.GetAppInstanceId()));
            this.logger.LogInformation($"Hello World Has Sended! {DateTime.Now.ToString("HH:mm:ss")}");
        }
    }
}
