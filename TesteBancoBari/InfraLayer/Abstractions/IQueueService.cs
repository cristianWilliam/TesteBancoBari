using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBancoBari.CrossCutting.Models;
using TesteBancoBari.InfraLayer.Models;

namespace TesteBancoBari.InfraLayer.Abstractions
{
    public interface IQueueService<T> where T : class
    {
        Task<AddItemResponseModel> SendItem(T bodyItem);
        Task<IEnumerable<ReceiveMessageModel>> ReceiveItem();
        Task DeleteItem(string itemId);
    }
}
