using System.Collections.Generic;

namespace Sonda.Core.RemotePrinting.Config
{
    public class NotificacionesConfig : INotificacionesConfig
    {
        public string NotificationsURL { get; set; }
        public string CodigoProceso { get; set; }
        public List<string> CodigoEtapa { get; set; }
        public bool ActivarNotificaciones { get; set; }

        public string AuthToken { get; set; }

 
    }
}