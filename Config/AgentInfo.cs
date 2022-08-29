using System.Collections.Generic;

namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Informacion de configuracion interna del agente de impresion
    /// </summary>
    public class AgentInfo : IAgentInfo
    {
        /// <summary>
        /// Identificador de Agente (en servicio remoto)
        /// </summary>
        public int? IdAgent { get; set; }

        /// <summary>
        /// Codigo del Agente (en servicio remoto)
        /// </summary>
        public string CodAgent { get; set; }

        /// <summary>
        /// Direccion Ip del agente de configuracion
        /// </summary>
        public string IpAgent { get; set; }

        /// <summary>
        /// Lista de impresion
        /// </summary>
        public List<PrinterInfo> PrintersInfo { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public AgentInfo()
        {
            PrintersInfo = new List<PrinterInfo>();
        }
    }
}