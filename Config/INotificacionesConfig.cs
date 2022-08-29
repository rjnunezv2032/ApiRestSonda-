using System.Collections.Generic;

namespace Sonda.Core.RemotePrinting.Config
{
    public interface INotificacionesConfig
    {
        bool ActivarNotificaciones { get; set; }
        List<string> CodigoEtapa { get; set; }
        string CodigoProceso { get; set; }
        string NotificationsURL { get; set; }
        string AuthToken { get; set; }
    }
}