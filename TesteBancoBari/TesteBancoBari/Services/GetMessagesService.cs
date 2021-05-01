using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBancoBari.Abstractions;
using TesteBancoBari.CrossCutting.Models;
using TesteBancoBari.InfraLayer.Abstractions;
using TesteBancoBari.Models;

namespace TesteBancoBari.Services
{
    public class GetMessagesService : IGetMessagesService
    {
        private readonly IQueueService<ReceiveMessageModel> queueService;
        public GetMessagesService(IQueueService<ReceiveMessageModel> queueService)
        {
            this.queueService = queueService;
        }

        public async Task<IEnumerable<GetMessageServiceViewModel>> ExecuteAsync()
        {
            var messages = await this.queueService.ReceiveItem();
            return messages.Select(x => new GetMessageServiceViewModel(x));
        }
    }
}
