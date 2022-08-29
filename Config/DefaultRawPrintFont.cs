using System.Text.Json.Serialization;

namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Clase de parametria que representa el tipo de fuente por defecto para impresion de texto
    /// </summary>
    public class DefaultRawPrintFont : IDefaultRawPrintFont
    {
        /// <summary>
        /// Nombre del fuente
        /// </summary>
        [JsonPropertyName("FontName")]
        public string FontName { get; set; }

        /// <summary>
        /// Tamaño de la fuente
        /// </summary>
        [JsonPropertyName("FontSize")]
        public int FontSize { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public DefaultRawPrintFont()
        { }
    }
}