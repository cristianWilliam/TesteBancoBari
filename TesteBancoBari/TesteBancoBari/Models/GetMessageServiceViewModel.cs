using Newtonsoft.Json;
using TesteBancoBari.BusinessLayer.Models;
using TesteBancoBari.CrossCutting.Models;

namespace TesteBancoBari.Models
{
    public class GetMessageServiceViewModel
    {
        public string AmazonId { get; private set; }
        public HelloWorldModel Data { get; private set; }

        public GetMessageServiceViewModel(ReceiveMessageModel valueMessage)
        {
            this.AmazonId = valueMessage.Id;
            Data = this.GetMessageObject(valueMessage.Body);
        }

        private HelloWorldModel GetMessageObject(string obj)
        {
            return JsonConvert.DeserializeObject<HelloWorldModel>(obj);
        }
    }
}
