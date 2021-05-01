using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteBancoBari.BusinessLayer.Models
{
    public class HelloWorldModel
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public string Time { get; private set; }
        public string AppId { get; set; }

        public HelloWorldModel(string message, string AppId)
        {
            this.Id = Guid.NewGuid();
            this.Time = DateTime.Now.ToString("yyyy-mm-dd_HH:mm:ss");
            this.Message = message;
            this.AppId = AppId;
        }
    }
}
