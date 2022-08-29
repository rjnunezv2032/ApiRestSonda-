using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Core.RemotePrinting.Configuration.Model.Entities
{


    public class PrinterProperties 
    {
        public string HostName { get; set; }
        public string HostAdress { get; set; }
        public string PrinterName { get; set; }
        public string PrinterPort { get; set; }
        public string PrinterStatus { get; set; }
        public bool PrinterIsLocal { get; set; }
        public bool PrinterIsNetwork { get; set; }
        public bool PrinterSupportColor { get; set; }
        public bool PrinterSupportDuplex { get; set; }
        public bool PrinterIsDefault { get; set; }
        public bool PrinterWorkOffLine { get; set; }
        public int PrinterVerticalResolution { get; set; }
        public int PrinterHorizontalResolution { get; set; }
        public string PrinterComment { get; set; }

    }
}
