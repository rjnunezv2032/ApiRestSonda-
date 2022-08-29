namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Interfaz de Informacion de Agente
    /// </summary>
    public interface IAgentInfo
    {
        /// <summary>
        /// Codigo de Agente
        /// </summary>
        string CodAgent { get; set; }

        /// <summary>
        /// Identificador de Agente.
        /// </summary>
        int? IdAgent { get; set; }

        /// <summary>
        /// Direccion Ip del Agente.
        /// </summary>
        string IpAgent { get; set; }
    }
}