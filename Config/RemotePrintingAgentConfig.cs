using Sonda.Core.RemotePrinting.Configuration.Model.Entities;

using System.Collections.Generic;

namespace Sonda.Core.RemotePrinting.Config
{ 

    public class Root
    {
        [Newtonsoft.Json.JsonProperty("Loggin")]
        public string Loggin { get; set; }

        [Newtonsoft.Json.JsonProperty("NotificacionesConfig")]
        public NotificacionesConfig NotificacionesConfig { get; set; }

        [Newtonsoft.Json.JsonProperty("RemotePrintingAgentConfig")]
        public RemotePrintingAgentConfig RemotePrintingAgentConfig { get; set; }

        

    }

    //public class NotificacionesConfig : INotificacionesConfig
    //{
    //    public NotificacionesConfig NotificacionesConfigs { get; set; }
    //    public NotificacionesConfig()
    //    {
    //        NotificacionesConfigs = new NotificacionesConfig();
    //    }
      
    //}

    /// <summary>
    /// Clase de Configuracion de Agente de Impresion.
    /// </summary>
    public class RemotePrintingAgentConfig : IRemotePrintingAgentConfig
    {
        /// <summary>
        /// Url Base del socket de servicio de impresion remota.
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string RemotePrintingServiceBaseUrl { get; set; }

        /// <summary>
        /// Fuente por defecto para impresion de texto plano.
        /// </summary>
        //[Newtonsoft.Json.JsonProperty(Order =1)]
        public DefaultRawPrintFont DefaultRawPrintFont { get; set; }
        /// <summary>
        /// Tamaño del buffer de recepcion de datos
        /// </summary>
        public int ReceiveBuffersSize { get; set; }

        /// <summary>
        /// Tiempo en milisegundos
        /// </summary>
        public int RefreshRate { get; set; }

        /// <summary>
        /// Directorio temporal para archivos descargados.
        /// </summary>
        public string TemporaryFolder { get; set; }

        /// <summary>
        /// Informacion del Agente.
        /// </summary>
        public AgentInfo AgentInfo { get; set; }

        /// <summary>
        /// Url del servicio (completa)
        /// </summary>
        //[Newtonsoft.Json.JsonIgnore]
        //public string RemotePrintingServiceURL { get; set; }
        //{
        //    get
        //    {
        //        return RemotePrintingServiceBaseUrl;
        //    }
        //}

        /// <summary>Lista de impresoras registradas en el servicio de impresion remota<summary>
        //[Newtonsoft.Json.JsonIgnore]
        //public List<LocalPrinter> LocalPrinter { get; set; }
        //[Newtonsoft.Json.JsonIgnore]
        //public List<PrinterInfo> RemotePrinting { get; set; }
        



        /// <summary>
        /// Constructor
        /// </summary>
        public RemotePrintingAgentConfig()
        {
           
            DefaultRawPrintFont = new DefaultRawPrintFont();
            AgentInfo = new AgentInfo();
            //LocalPrinter = new List<LocalPrinter>();
            //RemotePrinting = new List<PrinterInfo>();
            
        }
    }
}