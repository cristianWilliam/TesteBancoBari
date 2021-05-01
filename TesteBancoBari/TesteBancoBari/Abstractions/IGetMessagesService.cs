using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBancoBari.CrossCutting.Models;
using TesteBancoBari.Models;

namespace TesteBancoBari.Abstractions
{
    public interface IGetMessagesService
    {
        Task<IEnumerable<GetMessageServiceViewModel>> ExecuteAsync();
    }
}
