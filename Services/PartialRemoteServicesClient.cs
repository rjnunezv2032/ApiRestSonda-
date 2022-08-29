using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Sonda.Core.RemotePrinting.Configuration.Services.RemotePrintingAPI
{
    public class RemoteAPIEventArgs : System.EventArgs
    {
        public System.Net.Http.HttpClient Client { get; set; }
        public System.Net.Http.HttpResponseMessage Response { get; set; }

    }

    //partial class RemoteServicesAPI
    //{

    //    public delegate void OnResponseReceived(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);
    //    public event EventHandler<RemoteAPIEventArgs> ReceivedResponseEvent;
    //}
}
