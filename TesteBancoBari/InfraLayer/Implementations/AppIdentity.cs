using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBancoBari.InfraLayer.Abstractions;

namespace TesteBancoBari.InfraLayer.Implementations
{
    public class AppIdentity : IAppIdentity
    {
        private readonly string randomAppId;

        public AppIdentity()
        {
            randomAppId = Guid.NewGuid().ToString();
        }

        public string GetAppInstanceId() => this.randomAppId;
    }
}
