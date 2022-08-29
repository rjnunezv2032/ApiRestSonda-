namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Informacion de Impresora de Agente
    /// </summary>
    public class PrinterInfo : IPrinterInfo
    {
        /// <summary>
        /// Identificador de agente
        /// </summary>
        public int IdAgent { get; set; }

        /// <summary>
        /// Identificador de impresora
        /// </summary>
        public int IdImpresora { get; set; }

        /// <summary>
        /// Nombre de la impresora
        /// </summary>
        public string NombreImpresora { get; set; }
        /// <summary>
        /// Descripción de la impresora
        /// </summary>
        public string DescrpcionImpresora { get; set; }

        /// <summary>
        /// Identificador de tipo de Impresora (de servicio remoto de impresion)
        /// </summary>
        public int IdTipoImpresora { get; set; }

        /// <summary>
        /// Identificador de Tipo de Documento (de servicio remoto de impresion)
        /// </summary>
        public int IdTipoDocumento { get; set; }

        /// <summary>
        /// Parametros por defecto de la impresora local
        /// </summary>
        public string ParametrosDefault { get; set; }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PrinterInfo()
        { }
    }
}