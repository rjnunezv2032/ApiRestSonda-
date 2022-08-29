using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace Sonda.Core.RemotePrinting.Configuration.Model.Entities
{


    public class stPrinterProperties
    {
        [JsonPropertyName("Property")]
        public string Property { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }
    }

    public class stPrinterPreferences
    {
        [JsonPropertyName("Preference")]
        public string Preference { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Value")]
        public string Value { get; set; }

        [JsonPropertyName("Default")]
        public bool Default { get; set; }
    }

    #region -= Clases de Usuario =- 

    public class stPrintJob
    {
        public int RemoteJobId { get; set; }             // Identificador del trabajo de impresión en Servicio de Impresión Remoto
        public string PrintDate { get; set; }
        public string PrinterName { get; set; }
        public string DocumentName { get; set; }
        public string FullDocumentName { get; set; }    // path + DocumentName
        public string PrintStatus { get; set; }
        public string DocumentStatus { get; set; }
        public int LocalJobId { get; set; }             // Identificador del trabajo de impresión en Equipo Local
        public bool JobCollate { get; set; }
        public short JobCopies { get; set; }
        public string JobDuplex { get; set; }           // (Default,Simplex,Vertical or Horizontal)
        public string JobRange { get; set; }            // AllPages - CurrentPage - Selection - SomePages
        public string JobPages { get; set; }            // 1,3,5,8,9  etc 
        public int JobFromPage { get; set; }
        public int JobToPage { get; set; }
        public bool JobColor { get; set; }
        public bool JobPageOrientation { get; set; }    //  False: Vertical  ; True: Horizontal  
    }

 
    public class stPrinterSetting
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

    // duplicada
    //public class stPrinterPreferences
    //{
    //    public string Preference { get; set; }

    //    public string Name { get; set; }

    //    public string Value { get; set; }

    //    public bool Default { get; set; }
    //}

    //List<stPrintJob> lstPrintJobs = new List<stPrintJob>();

    //List<stPrinterSetting> lstDvcPrinterSetting = new List<stPrinterSetting>();

    //List<stPrinterPreferences> lstPrinterPreferences = new List<stPrinterPreferences>();

    #endregion


}
