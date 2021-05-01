using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBancoBari.Abstractions;
using TesteBancoBari.CrossCutting.Models;
using TesteBancoBari.Models;

namespace TesteBancoBari.Controllers
{
    public class HelloWorldController : BaseController
    {
        [HttpGet("get-messages")]
        public async Task<IEnumerable<GetMessageServiceViewModel>> GetMessages([FromServices] IGetMessagesService service)
            => await service.ExecuteAsync();
    }
}
