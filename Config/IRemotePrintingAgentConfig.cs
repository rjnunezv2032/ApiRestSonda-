using Sonda.Core.RemotePrinting.Configuration.Model.Entities;
using System.Collections.Generic;

namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Interfaz de Configuracion de Agente Remoto
    /// </summary>
    public interface IRemotePrintingAgentConfig
    {
        ///// <summary>Url del Servicio de Notificaciones de Sonda</summary>
        //string NotificationsURL { get; set; }

        /// <summary>
        /// Tamaño del buffer de recepcion de datos
        /// </summary>
        int ReceiveBuffersSize { get; set; }

        /// <summary>
        /// Tiempo en milisegundos
        /// </summary>
        int RefreshRate { get; set; }

        /// <summary>Url Base del socket de servicio de impresion remota.</summary>
        ///string RemotePrintingServiceBaseUrl { get; set; }

        ///// <summary>Version del servicio socket</summary>
        //string RemotePrintingServiceVersion { get; set; }
        ///// <summary>Controlador de peticiones del socket</summary>
        //string RemotePrintingServiceController { get; set; }

        /// <summary>
        /// URL completa del socket
        /// </summary>
        //string RemotePrintingServiceURL { get; }

        /// <summary>
        /// Directorio Temporal para archivos
        /// </summary>
        string TemporaryFolder { get; set; }

        /// <summary>
        /// Lista de Impresoras locales registradas en el servicio de impresion remota
        /// </summary>
        //List<LocalPrinter> LocalPrinter { get; set; }

        //List<PrinterInfo> RemotePrinting { get; set; }

        ///<summary>Fuente Default para imresion de texto plano</summary>
        DefaultRawPrintFont DefaultRawPrintFont { get; set; }

        /// <summary>
        /// Informacion del Agente
        /// </summary>
        AgentInfo AgentInfo { get; }
    }
}