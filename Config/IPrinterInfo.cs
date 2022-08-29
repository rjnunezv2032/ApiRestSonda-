namespace Sonda.Core.RemotePrinting.Config
{
    public interface IPrinterInfo
    {
        int IdAgent { get; set; }
        int IdImpresora { get; set; }
        int IdTipoDocumento { get; set; }
        int IdTipoImpresora { get; set; }
        string NombreImpresora { get; set; }

        string ParametrosDefault { get; set; }
    }
}