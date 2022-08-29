using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Core.RemotePrinting.Configuration.Model.Entities
{
    public class ConfigService
    {
        public ConfigService()
        {
            bRemotePrintingServiceBaseURL = true;
            bNotificationsURL = true;
        }


        public int RefreshRate { get; set; }
        public string RemotePrintingServiceBaseURL { get; set; }
        public bool bRemotePrintingServiceBaseURL { get; set; }

        public string NotificationsURL { get; set; }
        public bool bNotificationsURL { get; set; }

        public string codigoProceso { get; set; }
        public bool bcodigoProceso { get; set; }
        public string codigoEtapa { get; set; }
        public bool bcodigoEtapa { get; set; }

        public int ReceiveBuffersSize { get; set; }
        public bool bUrlSocketOK { get; set; }
        public string ServiceVersion { get; set; }
    }
}
