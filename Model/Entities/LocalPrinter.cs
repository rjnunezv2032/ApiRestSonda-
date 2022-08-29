using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sonda.Core.RemotePrinting.Configuration.Model.Entities;

namespace Sonda.Core.RemotePrinting.Configuration.Model.Entities
{
    public class LocalPrinter
    {
        public virtual int IdPrinter { get; set; }
        public virtual int IdAgent { get; set; }
        public virtual int IdEstado { get; set; }
        public virtual int IdTipoDocumento { get; set; }
        public virtual int IdTipoImpresora { get; set; }
        public virtual int IdPurpose { get; set; }
        public virtual string PrinterName { get; set; }
        public virtual string Description { get; set; }        
        public virtual PrinterProperties PrinterProperties { get; set; }
        public virtual string Parameters { get; set; }
    }
    
}
