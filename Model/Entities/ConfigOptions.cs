using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Core.RemotePrinting.Configuration.Model.Entities
{
    public class ConfigOptions
    {
        public ConfigOptions()
        {
            bAgente = true;
            bServiceRemotingPrintingURL = true;
            bUserLogin = true;
            bPasswordLogin = true;
            bUrlRemotePrintingOK = true;
            bTemporaryFolder = true;
        }
        
        public int idAgente { set; get; }
        public string IP { get; set; }
        public bool bIP { get; set; }
        public string Agente { get; set; }
        public bool bAgente { get; set; }
        public string ServiceRemotingPrintingURL { get; set; }
        public bool bServiceRemotingPrintingURL { get; set; }
        public int PingTimeOut { get; set; }
        public string UserLogin { get; set; }
        public bool bUserLogin { get; set; }
        public string PasswordLogin { get; set; }
        public bool bPasswordLogin { get; set; }
        public bool bUrlRemotePrintingOK { get; set; }

        public string TemporaryFolder { get; set; }
        public bool bTemporaryFolder { get; set; }
        public bool bFolderMachineDefault { get; set; }

        public string DescripcionAgente { get; set; }
    }
}
