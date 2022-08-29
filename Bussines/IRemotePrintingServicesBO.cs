using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using Sonda.Core.RemotePrintingAgent.Model.Output;
//using Sonda.Core.RemotePrintingAgent.Model.Input;
//using Sonda.Core.RemotePrintingAgent.Model.Entities;

namespace Sonda.Core.RemotePrinting.Configuration.Business
{ 
    public interface IRemotePrintingServicesBO
    {
   
            Task<bool> ConnectAsync();
            Task DisconnectAsync();
            //void Dispose();
            ////Task GetPrintingJobs(ConcurrentQueue<PrintJobsDTO> printJobs);
            //Task ReceiveDataServiceCommandAsync(Stream inputStream);
            //Task ReceiveLoop();
            //Task SendDataServiceCommandAsync(ServiceCommandDTO serviceCommand);
            //Task UpdatePrinterStatus(IdPrinterCodAgenteKeyDTO printerKey);
            //Task UpdatePrintJobs(ConcurrentQueue<PrintJobsDTO> printJobs);
       
    }

}
