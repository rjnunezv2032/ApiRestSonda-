using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonda.Core.RemotePrinting.Configuration.Services.RemotePrintingAPI
{

    public static class ApiControllers
    {
        public static string AgenteSearch = "/api/v{version}/Agente/Search";
        public static string AgenteCreate = "/api/v{version}/Agente/Create";

        public static string PrinterSearch = "/api/v{version}/Printers/Search";
        public static string PrinterCreate = "/api/v{version}/Printers/Create";
        public static string PrinterUpdate = "/api/v{version}/Printers/Update";
        public static string PrinterDelete = "/api/v{version}/Printers/Delete";
    }




    #region Clases Propias
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Agente
    {
        [Newtonsoft.Json.JsonProperty("idAgente", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("codAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CodAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("urlAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string UrlAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("ipAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IpAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("frecActualizacion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int FrecActualizacion { get; set; }

        [Newtonsoft.Json.JsonProperty("maxDataTranfer", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int MaxDataTranfer { get; set; }


    }
    public partial class PrinterResult : baseResult
    {
        public Printer result { get; set; }
    }
    public partial class AgenteResult : baseResult
    {
        public Agente result { get; set; }        
    }
    public partial class AgenteSearchResult : baseResult
    {
        public System.Collections.Generic.List<Agente> result { get; set; }
    }

    public partial class PrinterSearchResult : baseResult
    {
        public System.Collections.Generic.List<Printer> result { get; set; }
    }
    public partial class baseResult
    {
        //[Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        //public object result { get; set; }

        [Newtonsoft.Json.JsonProperty("errorCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string errorCode { get; set; }
        [Newtonsoft.Json.JsonProperty("errorMessage", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string errorMessage { get; set; }
        [Newtonsoft.Json.JsonProperty("errorType", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string errorType { get; set; }
        [Newtonsoft.Json.JsonProperty("statusCode", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int statusCode { get; set; }
        [Newtonsoft.Json.JsonProperty("count", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int count { get; set; }
        [Newtonsoft.Json.JsonProperty("handledException", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string handledException { get; set; }
        [Newtonsoft.Json.JsonProperty("messages", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public dynamic messages { get; set; }
    }
    #endregion

    #region Clases Autogeneradas
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class QueryOptions
    {
        [Newtonsoft.Json.JsonProperty("skip", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Skip { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty("top", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Top { get; set; } = 0;

        [Newtonsoft.Json.JsonProperty("filter", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Filter { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("orderBy", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string OrderBy { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; } = "";

        [Newtonsoft.Json.JsonProperty("count", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Count { get; set; } = false;


    }
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdAgenteKey
    {
        [Newtonsoft.Json.JsonProperty("idAgente", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class ProblemDetails
    {
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Status { get; set; }

        [Newtonsoft.Json.JsonProperty("detail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [Newtonsoft.Json.JsonProperty("instance", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Instance { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdEstadoImpresionKey
    {
        [Newtonsoft.Json.JsonProperty("idEstadoImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class EstadosImpresion
    {
        [Newtonsoft.Json.JsonProperty("idEstadoImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstImp", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstImp { get; set; }

        [Newtonsoft.Json.JsonProperty("estadoReg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string EstadoReg { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdEstadoImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class EstadosImpresoras
    {
        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstado", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("estadoReg", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string EstadoReg { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("idPrinter", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdPrinter { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Printer
    {
        [Newtonsoft.Json.JsonProperty("idPrinter", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdPrinter { get; set; }

        [Newtonsoft.Json.JsonProperty("idAgent", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgent { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("idTipoDocumento", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoDocumento { get; set; }

        [Newtonsoft.Json.JsonProperty("idTipoImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idPurpose", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdPurpose { get; set; }

        [Newtonsoft.Json.JsonProperty("printerName", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PrinterName { get; set; }

        [Newtonsoft.Json.JsonProperty("description", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Description { get; set; }

        [Newtonsoft.Json.JsonProperty("ipLocation", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IpLocation { get; set; }

        [Newtonsoft.Json.JsonProperty("printerProperties", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string PrinterProperties { get; set; }

        [Newtonsoft.Json.JsonProperty("parameters", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Parameters { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class PrintersAgentInfo
    {
        [Newtonsoft.Json.JsonProperty("idAgente", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idProposito", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("idTpImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("idTipoDoc", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoDoc { get; set; }

        [Newtonsoft.Json.JsonProperty("idAdm", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAdm { get; set; }

        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("codAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CodAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("nombreImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NombreImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("impresoraDefaault", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ImpresoraDefaault { get; set; }

        [Newtonsoft.Json.JsonProperty("propiedades", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Propiedades { get; set; }

        [Newtonsoft.Json.JsonProperty("parametros", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Parametros { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionProposito", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionTpImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstadoImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstadoImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descTipoDoc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescTipoDoc { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdUsuarioKey
    {
        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("fechaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset FechaImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class PrintersUserInfo
    {
        [Newtonsoft.Json.JsonProperty("idAgente", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idProposito", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("idTpImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstado { get; set; }

        [Newtonsoft.Json.JsonProperty("idTipoDoc", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoDoc { get; set; }

        [Newtonsoft.Json.JsonProperty("idAdm", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAdm { get; set; }

        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("codAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CodAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("nombreImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NombreImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("impresoraDefault", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ImpresoraDefault { get; set; }

        [Newtonsoft.Json.JsonProperty("propiedades", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Propiedades { get; set; }

        [Newtonsoft.Json.JsonProperty("parametros", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Parametros { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionProposito", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionTpImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstadoImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstadoImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descTipoDoc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescTipoDoc { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class CodAgenteIpImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("codAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CodAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("ipAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IpAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class TrabajosImpresion
    {
        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idTrabajo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTrabajo { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstadoImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("nombreArchivo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NombreArchivo { get; set; }

        [Newtonsoft.Json.JsonProperty("documento", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public byte[] Documento { get; set; }

        [Newtonsoft.Json.JsonProperty("configImpresion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ConfigImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("posColaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PosColaImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("fechaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset FechaImpresion { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdTrabajoIdImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("idTrabajo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTrabajo { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstadoImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("posColaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PosColaImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("fechaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.DateTimeOffset FechaImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class RespuestaOutput
    {
        [Newtonsoft.Json.JsonProperty("resultado", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Resultado { get; set; }

        [Newtonsoft.Json.JsonProperty("mensaje", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Mensaje { get; set; }

        [Newtonsoft.Json.JsonProperty("elemento", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public object Elemento { get; set; }

        [Newtonsoft.Json.JsonProperty("esError", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool EsError { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class PrintJobsInfo
    {
        [Newtonsoft.Json.JsonProperty("idTrabajo", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTrabajo { get; set; }

        [Newtonsoft.Json.JsonProperty("nombreArchivo", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NombreArchivo { get; set; }

        [Newtonsoft.Json.JsonProperty("documento", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Documento { get; set; }

        [Newtonsoft.Json.JsonProperty("configImpresion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ConfigImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("posColaImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PosColaImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("idAgente", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("codAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string CodAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionAgente", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionAgente { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("nombreImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string NombreImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("propiedades", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Propiedades { get; set; }

        [Newtonsoft.Json.JsonProperty("parametros", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Parametros { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstadoImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstadoImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstadoImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idProposito", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionProposito", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("idTpImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionTpImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idEstadoImpresion", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdEstadoImpresion { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionEstImpresion", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionEstImpresion { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdPropositoKey
    {
        [Newtonsoft.Json.JsonProperty("idProposito", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Proposito
    {
        [Newtonsoft.Json.JsonProperty("idProposito", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdProposito { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionProposito", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionProposito { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class SearchTaskInput
    {
        [Newtonsoft.Json.JsonProperty("idProceso", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdProceso { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdTipoDocumentoKey
    {
        [Newtonsoft.Json.JsonProperty("idTipoDoc", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoDoc { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class TipoDocumento
    {
        [Newtonsoft.Json.JsonProperty("idTipoDoc", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTipoDoc { get; set; }

        [Newtonsoft.Json.JsonProperty("descTipoDoc", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescTipoDoc { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdTipoImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("idTpImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class TipoImpresora
    {
        [Newtonsoft.Json.JsonProperty("idTpImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdTpImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("descripcionTpImpresora", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string DescripcionTpImpresora { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdAdmIdUsuarioIdImpresoraKey
    {
        [Newtonsoft.Json.JsonProperty("idAmin", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAmin { get; set; }

        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("defaultPrinter", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? DefaultPrinter { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class IdKey
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("expand", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Expand { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.0.22.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class UsuarioImpresoras
    {
        [Newtonsoft.Json.JsonProperty("idAdmin", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdAdmin { get; set; }

        [Newtonsoft.Json.JsonProperty("idImpresora", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int IdImpresora { get; set; }

        [Newtonsoft.Json.JsonProperty("idUsuario", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string IdUsuario { get; set; }

        [Newtonsoft.Json.JsonProperty("impresoraDefault", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool ImpresoraDefault { get; set; }


    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.0.5.0 (NJsonSchema v10.0.22.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + response.Substring(0, response.Length >= 512 ? 512 : response.Length), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.0.5.0 (NJsonSchema v10.0.22.0 (Newtonsoft.Json v11.0.0.0))")]
    public partial class ApiException<TResult> : ApiException
    {
        public TResult Result { get; private set; }

        public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }


    #endregion Clases Autogeneradas
}
