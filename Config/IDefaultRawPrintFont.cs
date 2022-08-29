namespace Sonda.Core.RemotePrinting.Config
{
    /// <summary>
    /// Interfaz de Fuente de impresora matriz de punto por defecto
    /// </summary>
    public interface IDefaultRawPrintFont
    {
        /// <summary>
        /// Interfaz de nombre de la fuente.
        /// </summary>
        string FontName { get; set; }

        /// <summary>Interfaz de tamaño de la fuente.<summary>
        int FontSize { get; set; }
    }
}